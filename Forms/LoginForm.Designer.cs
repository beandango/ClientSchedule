namespace ClientSchedule.Forms
{
    partial class LoginForm
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
            cmbLanguagec = new ComboBox();
            btnLogin = new Button();
            lblLocation = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            lblUsername = new Label();
            lblPassword = new Label();
            SuspendLayout();
            // 
            // cmbLanguagec
            // 
            cmbLanguagec.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguagec.FormattingEnabled = true;
            cmbLanguagec.Location = new Point(287, 55);
            cmbLanguagec.Name = "cmbLanguagec";
            cmbLanguagec.Size = new Size(223, 23);
            cmbLanguagec.TabIndex = 0;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(362, 287);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(75, 23);
            btnLogin.TabIndex = 1;
            btnLogin.UseVisualStyleBackColor = true;
            // 
            // lblLocation
            // 
            lblLocation.AutoSize = true;
            lblLocation.Location = new Point(12, 417);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(0, 15);
            lblLocation.TabIndex = 2;
            lblLocation.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(288, 171);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(222, 23);
            txtUsername.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(288, 222);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(223, 23);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(191, 179);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(0, 15);
            lblUsername.TabIndex = 5;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(191, 225);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(0, 15);
            lblPassword.TabIndex = 6;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblPassword);
            Controls.Add(lblUsername);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblLocation);
            Controls.Add(btnLogin);
            Controls.Add(cmbLanguagec);
            Name = "LoginForm";
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbLanguagec;
        private Button btnLogin;
        private Label lblLocation;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Label lblUsername;
        private Label lblPassword;
    }
}