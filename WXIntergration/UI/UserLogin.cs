using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DataAccessLayer.Repositories;

namespace WXIntergration.UI
{
    public partial class UserLogin : Form
    {
        private bool _errorOccured = false;
        public UserLogin()
        {
            InitializeComponent();
            //InitializeComponent(IControllerAPI controllerAPI);
            //_controllerAPI = controllerAPI;

            //_controllerAPI.OnError += OnErrorOccured;
        }

        // BACKGROUND METHODS //

        // UI METHODS //
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            HttpsChck.Checked = false;
            DomainTxt.Clear();
            UsernameTxt.Clear();
            PasswordTxt.Clear();
        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            bool https = false;
            string isHttps = "";
            string domain = DomainTxt.Text;
            string userName = UsernameTxt.Text;
            string password = PasswordTxt.Text;
            string passwordHash = await ControllerAPI.Sha1FromString(password);

            if (HttpsChck.Checked)
            {
                https = true;
                isHttps = "(HTTPS)";
            }

            ControllerAPI controllerAPI = new ControllerAPI(domain, https);

            bool loggedIn = await controllerAPI.LogIn(userName, passwordHash.ToLower());
            if (!loggedIn)
            {
                errorMessage += $"\n Failed to log in. {isHttps}";
                MessageBox.Show(errorMessage);
                return;
            }

            NameValueCollection details = await controllerAPI.GetControllerSettings();
            string? serialNum = details["SERIALNUMBER"];
            MessageBox.Show($"Controllers serial number is: [{serialNum}] {isHttps}");

            await controllerAPI.LogOut();
        }
    }
}
