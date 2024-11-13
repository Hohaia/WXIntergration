using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WXIntergration.UI
{
    public partial class UserLogin : Form
    {
        //private readonly IIngredientsRepository _ingredientsRepository;

        private bool _errorOccured = false;
        public UserLogin()
        {
            InitializeComponent();
            //InitializeComponent(IIngredientsRepository ingredientsRepository);
            //_ingredientsRepository = ingredientsRepository;

            //_ingredientsRepository.OnError += OnErrorOccured;
        }

        // BACKGROUND METHODS //
        private void Login()
        {

        }

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

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            Login();
        }
    }
}
