namespace ClientSchedule.Forms
{
    partial class CustomerEditForm
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
            txtCustomerName = new TextBox();
            txtAddress1 = new TextBox();
            txtAddress2 = new TextBox();
            cmbCountry = new ComboBox();
            cmbCity = new ComboBox();
            txtPostalCode = new TextBox();
            txtPhone = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            lblName = new Label();
            lblAddress = new Label();
            lblAddress2 = new Label();
            lblCountry = new Label();
            lblCity = new Label();
            lblPhone = new Label();
            lblPostal = new Label();
            SuspendLayout();
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(131, 62);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(286, 23);
            txtCustomerName.TabIndex = 0;
            // 
            // txtAddress1
            // 
            txtAddress1.Location = new Point(131, 108);
            txtAddress1.Name = "txtAddress1";
            txtAddress1.Size = new Size(286, 23);
            txtAddress1.TabIndex = 1;
            // 
            // txtAddress2
            // 
            txtAddress2.Location = new Point(131, 156);
            txtAddress2.Name = "txtAddress2";
            txtAddress2.Size = new Size(286, 23);
            txtAddress2.TabIndex = 2;
            // 
            // cmbCountry
            // 
            cmbCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCountry.FormattingEnabled = true;
            cmbCountry.Location = new Point(551, 65);
            cmbCountry.Name = "cmbCountry";
            cmbCountry.Size = new Size(109, 23);
            cmbCountry.TabIndex = 4;
            // 
            // cmbCity
            // 
            cmbCity.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCity.FormattingEnabled = true;
            cmbCity.Location = new Point(551, 108);
            cmbCity.Name = "cmbCity";
            cmbCity.Size = new Size(109, 23);
            cmbCity.TabIndex = 5;
            // 
            // txtPostalCode
            // 
            txtPostalCode.Location = new Point(133, 231);
            txtPostalCode.Name = "txtPostalCode";
            txtPostalCode.Size = new Size(109, 23);
            txtPostalCode.TabIndex = 6;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(551, 231);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(168, 23);
            txtPhone.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(172, 353);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(109, 23);
            btnSave.TabIndex = 8;
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(520, 353);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(104, 23);
            btnCancel.TabIndex = 9;
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(28, 65);
            lblName.Name = "lblName";
            lblName.Size = new Size(38, 15);
            lblName.TabIndex = 10;
            lblName.Text = "label1";
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(28, 108);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(38, 15);
            lblAddress.TabIndex = 11;
            lblAddress.Text = "label2";
            // 
            // lblAddress2
            // 
            lblAddress2.AutoSize = true;
            lblAddress2.Location = new Point(28, 156);
            lblAddress2.Name = "lblAddress2";
            lblAddress2.Size = new Size(38, 15);
            lblAddress2.TabIndex = 12;
            lblAddress2.Text = "label3";
            // 
            // lblCountry
            // 
            lblCountry.AutoSize = true;
            lblCountry.Location = new Point(440, 65);
            lblCountry.Name = "lblCountry";
            lblCountry.Size = new Size(38, 15);
            lblCountry.TabIndex = 13;
            lblCountry.Text = "label4";
            // 
            // lblCity
            // 
            lblCity.AutoSize = true;
            lblCity.Location = new Point(440, 111);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(38, 15);
            lblCity.TabIndex = 14;
            lblCity.Text = "label5";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(440, 234);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(38, 15);
            lblPhone.TabIndex = 15;
            lblPhone.Text = "label6";
            // 
            // lblPostal
            // 
            lblPostal.AutoSize = true;
            lblPostal.Location = new Point(58, 231);
            lblPostal.Name = "lblPostal";
            lblPostal.Size = new Size(38, 15);
            lblPostal.TabIndex = 16;
            lblPostal.Text = "label7";
            // 
            // CustomerEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblPostal);
            Controls.Add(lblPhone);
            Controls.Add(lblCity);
            Controls.Add(lblCountry);
            Controls.Add(lblAddress2);
            Controls.Add(lblAddress);
            Controls.Add(lblName);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtPhone);
            Controls.Add(txtPostalCode);
            Controls.Add(cmbCity);
            Controls.Add(cmbCountry);
            Controls.Add(txtAddress2);
            Controls.Add(txtAddress1);
            Controls.Add(txtCustomerName);
            Name = "CustomerEditForm";
            Text = "Edit Customer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCustomerName;
        private TextBox txtAddress1;
        private TextBox txtAddress2;
        private ComboBox cmbCountry;
        private ComboBox cmbCity;
        private TextBox txtPostalCode;
        private TextBox txtPhone;
        private Button btnSave;
        private Button btnCancel;
        private Label lblName;
        private Label lblAddress;
        private Label lblAddress2;
        private Label lblCountry;
        private Label lblCity;
        private Label lblPhone;
        private Label lblPostal;
    }
}