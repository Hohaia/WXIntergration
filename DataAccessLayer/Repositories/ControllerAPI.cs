using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Specialized;

using DataAccessLayer.Contracts;
using Utility.Logging;

namespace DataAccessLayer.Repositories
{
    public class ControllerAPI : IControllerAPI
    {
        public event Action<string> OnError;

        private byte[] _sessionAesKey;
        private readonly string _hostAddress;
        private readonly bool _hostHttps;
        private readonly HttpClient _client;

        public ControllerAPI(string Host, bool Https)
        {
            _hostAddress = Host;
            _hostHttps = Https;
            _sessionAesKey = new byte[16];
            HttpClientHandler httpClientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                {
                    return true;
                }
            };
            _client = new HttpClient(httpClientHandler);
        }

        // PRIVATE METHODS //
        private void ErrorOccured(string errorMessage, Exception ex)
        {
            if (OnError != null)
                OnError.Invoke(errorMessage);

            Logger.Log(ex.Message, LogType.ERROR);
        }

        private bool ShouldEncrypt(string parameters)
        {
            if (_hostHttps)
            {
                return false;
            }

            if (parameters.StartsWith("Command&Type=Session&SubType=InitSession") ||
                parameters.StartsWith("Command&Type=Session&SubType=CheckPassword"))
            {
                return false;
            }
            return true;
        }

        private string Encrypt(string parameters)
        {
            using Aes aes128 = Aes.Create();
            aes128.Key = _sessionAesKey;
            aes128.GenerateIV();
            byte[] parameterBytes = Encoding.UTF8.GetBytes(parameters);
            byte[] encryptedBytes = aes128.EncryptCbc(parameterBytes, aes128.IV, PaddingMode.PKCS7);
            string ivString = BitConverter.ToString(aes128.IV).Replace("-", "");
            string dataString = BitConverter.ToString(encryptedBytes).Replace("-", "");

            return $"{ivString}{dataString}";
        }

        private string Decrypt(string data)
        {
            using Aes aes128 = Aes.Create();
            aes128.Key = _sessionAesKey;
            byte[] dataBytes = Convert.FromHexString(data);
            aes128.IV = dataBytes[..16];
            byte[] decryptedBytes = aes128.DecryptCbc(dataBytes[16..], aes128.IV, PaddingMode.PKCS7);

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        private async Task<string> GetResponseString(string parameters)
        {
            bool shouldEncrypt = ShouldEncrypt(parameters);
            if (shouldEncrypt)
            {
                parameters = Encrypt(parameters);
            }

            string protocol = _hostHttps ? "https" : "http";

            string url = $"{protocol}://{_hostAddress}/PRT_CTRL_DIN_ISAPI.dll?{parameters}";

            HttpResponseMessage response = await _client.PostAsync($"{protocol}://{_hostAddress}/PRT_CTRL_DIN_ISAPI.dll?", new StringContent(parameters));
            if (response.IsSuccessStatusCode == false)
            {
                return string.Empty;
            }

            string responseString = await response.Content.ReadAsStringAsync();
            if (shouldEncrypt)
            {
                responseString = Decrypt(responseString);
            }

            return responseString;
        }

        private static string XorFn(string inputString, UInt32 number)
        {
            string numBinary = Convert.ToString(number, 2).PadLeft(sizeof(UInt32) * 8, '0');
            char[] charArray = numBinary.ToCharArray();
            int startPosition = numBinary.Length;
            StringBuilder stringBuilder = new();

            for (int i = 0; i < charArray.Length; i++)
            {
                int charCode = charArray[i] & 0xff;
                startPosition = startPosition == 0 ? numBinary.Length - 8 : startPosition - 8;
                string byteString = numBinary.Substring(startPosition, 8);
                int byteNum = Convert.ToInt32(byteString, 2);
                stringBuilder.Append((charCode ^ byteNum).ToString("X2"));
            }

            return stringBuilder.ToString();
        }

        // PUBLIC METHODS //
        public static async Task<string> Sha1FromString(string inputString)
        {
            using SHA1 sha1 = SHA1.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            using MemoryStream stream = new(bytes);
            byte[] inputStringHash = await sha1.ComputeHashAsync(stream);

            return BitConverter.ToString(inputStringHash).Replace("-", "").ToUpper();
        }

        public async Task<bool> LogIn(string userName, string passwordHash)
        {
            string sessionRandIdString = await GetResponseString("Command&Type=Session&SubType=InitSession");
            uint sessionRandIdVal = UInt32.Parse(sessionRandIdString);
            string xorUserName = XorFn(userName, sessionRandIdVal + 1);
            string hashXorUserName = await Sha1FromString(xorUserName);
            string xorPasswordHash = XorFn(passwordHash, sessionRandIdVal);
            string hashXorPasswordHash = await Sha1FromString(xorPasswordHash);
            string checkPasswordFunction = _hostHttps ? "CheckPasswordServer" : "CheckPassword";
            string sessionRandIdString2 = await GetResponseString($"Command&Type=Session&SubType={checkPasswordFunction}&Name={hashXorUserName}&Password={hashXorPasswordHash}");

            if (sessionRandIdString2.Trim().StartsWith("FAIL"))
            {
                string failMessage = $"\n Error in authentication: {sessionRandIdString2}";
                Logger.Log(failMessage, LogType.FAIL);

                return false;
            }

            if (_hostHttps == false)
            {
                uint sessionRandIdValue2 = UInt32.Parse(sessionRandIdString2);
                string xorPasswordHash2 = XorFn(passwordHash, sessionRandIdValue2);
                string hashXorPasswordHash2 = await Sha1FromString(xorPasswordHash2);
                _sessionAesKey = Encoding.UTF8.GetBytes(xorPasswordHash2[..16]);
            }

            return true;
        }

        public async Task LogOut()
        {
            await GetResponseString("Command&Type=Session&SubType=CloseSession");
        }

        public async Task<NameValueCollection> GetControllerSettings()
        {
            string list = await GetResponseString("Request&Type=Detail&SubType=GXT_CONTROLLERSETTINGS_TBL");

            return HttpUtility.ParseQueryString(list);
        }
    }
}