namespace WXIntergration.UI
{
    partial class UserLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            LoginBtn = new Button();
            ResetBtn = new Button();
            ExitBtn = new Button();
            HttpsChck = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            DomainTxt = new TextBox();
            UsernameTxt = new TextBox();
            PasswordTxt = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(410, 224);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // LoginBtn
            // 
            LoginBtn.Location = new Point(12, 359);
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Size = new Size(410, 33);
            LoginBtn.TabIndex = 1;
            LoginBtn.Text = "Login";
            LoginBtn.UseVisualStyleBackColor = true;
            LoginBtn.Click += LoginBtn_Click;
            // 
            // ResetBtn
            // 
            ResetBtn.Location = new Point(12, 398);
            ResetBtn.Name = "ResetBtn";
            ResetBtn.Size = new Size(202, 33);
            ResetBtn.TabIndex = 2;
            ResetBtn.Text = "Reset";
            ResetBtn.UseVisualStyleBackColor = true;
            ResetBtn.Click += ResetBtn_Click;
            // 
            // ExitBtn
            // 
            ExitBtn.Location = new Point(220, 398);
            ExitBtn.Name = "ExitBtn";
            ExitBtn.Size = new Size(202, 33);
            ExitBtn.TabIndex = 3;
            ExitBtn.Text = "Exit";
            ExitBtn.UseVisualStyleBackColor = true;
            ExitBtn.Click += ExitBtn_Click;
            // 
            // HttpsChck
            // 
            HttpsChck.AutoSize = true;
            HttpsChck.CheckAlign = ContentAlignment.MiddleRight;
            HttpsChck.Location = new Point(12, 244);
            HttpsChck.Name = "HttpsChck";
            HttpsChck.Size = new Size(79, 29);
            HttpsChck.TabIndex = 4;
            HttpsChck.Text = "Https:";
            HttpsChck.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(97, 245);
            label1.Name = "label1";
            label1.Size = new Size(105, 25);
            label1.TabIndex = 5;
            label1.Text = "Domain/IP:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 284);
            label2.Name = "label2";
            label2.Size = new Size(101, 25);
            label2.TabIndex = 6;
            label2.Text = "Username:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 323);
            label3.Name = "label3";
            label3.Size = new Size(95, 25);
            label3.TabIndex = 7;
            label3.Text = "Password:";
            // 
            // DomainTxt
            // 
            DomainTxt.Location = new Point(208, 242);
            DomainTxt.Name = "DomainTxt";
            DomainTxt.PlaceholderText = "192.168.1.2";
            DomainTxt.Size = new Size(214, 33);
            DomainTxt.TabIndex = 8;
            // 
            // UsernameTxt
            // 
            UsernameTxt.Location = new Point(119, 281);
            UsernameTxt.Name = "UsernameTxt";
            UsernameTxt.PlaceholderText = "Enter Username...";
            UsernameTxt.Size = new Size(303, 33);
            UsernameTxt.TabIndex = 9;
            // 
            // PasswordTxt
            // 
            PasswordTxt.Location = new Point(119, 320);
            PasswordTxt.Name = "PasswordTxt";
            PasswordTxt.PasswordChar = '*';
            PasswordTxt.PlaceholderText = "Enter Password...";
            PasswordTxt.Size = new Size(303, 33);
            PasswordTxt.TabIndex = 10;
            // 
            // UserLogin
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 443);
            Controls.Add(PasswordTxt);
            Controls.Add(UsernameTxt);
            Controls.Add(DomainTxt);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(HttpsChck);
            Controls.Add(ExitBtn);
            Controls.Add(ResetBtn);
            Controls.Add(LoginBtn);
            Controls.Add(pictureBox1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(5);
            Name = "UserLogin";
            Text = "User Login";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button LoginBtn;
        private Button ResetBtn;
        private Button ExitBtn;
        private CheckBox HttpsChck;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox DomainTxt;
        private TextBox UsernameTxt;
        private TextBox PasswordTxt;
    }
}