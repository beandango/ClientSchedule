namespace ClientSchedule.Forms
{
    partial class AppointmentEditForm
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
            cmbCustomer = new ComboBox();
            lblCustomer = new Label();
            txtTitle = new TextBox();
            txtType = new TextBox();
            txtDescription = new TextBox();
            txtLocation = new TextBox();
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            btnSave = new Button();
            btnCancel = new Button();
            lblTitle = new Label();
            lblType = new Label();
            lblDescription = new Label();
            lblLocation = new Label();
            lblStart = new Label();
            lblEnd = new Label();
            dtpStartTime = new DateTimePicker();
            dtpEndTime = new DateTimePicker();
            SuspendLayout();
            // 
            // cmbCustomer
            // 
            cmbCustomer.FormattingEnabled = true;
            cmbCustomer.Location = new Point(457, 86);
            cmbCustomer.Name = "cmbCustomer";
            cmbCustomer.Size = new Size(231, 23);
            cmbCustomer.TabIndex = 0;
            // 
            // lblCustomer
            // 
            lblCustomer.AutoSize = true;
            lblCustomer.Location = new Point(694, 94);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(38, 15);
            lblCustomer.TabIndex = 1;
            lblCustomer.Text = "label1";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(35, 86);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(279, 23);
            txtTitle.TabIndex = 2;
            // 
            // txtType
            // 
            txtType.Location = new Point(35, 133);
            txtType.Name = "txtType";
            txtType.Size = new Size(279, 23);
            txtType.TabIndex = 3;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(35, 182);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(279, 158);
            txtDescription.TabIndex = 4;
            // 
            // txtLocation
            // 
            txtLocation.Location = new Point(35, 380);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(279, 23);
            txtLocation.TabIndex = 5;
            // 
            // dtpStart
            // 
            dtpStart.Format = DateTimePickerFormat.Short;
            dtpStart.Location = new Point(457, 158);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(231, 23);
            dtpStart.TabIndex = 6;
            // 
            // dtpEnd
            // 
            dtpEnd.Format = DateTimePickerFormat.Short;
            dtpEnd.Location = new Point(457, 251);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(231, 23);
            dtpEnd.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(457, 380);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 8;
            btnSave.Text = "button1";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(613, 379);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "button2";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(320, 89);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(38, 15);
            lblTitle.TabIndex = 10;
            lblTitle.Text = "label1";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(320, 136);
            lblType.Name = "lblType";
            lblType.Size = new Size(38, 15);
            lblType.TabIndex = 11;
            lblType.Text = "label1";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(320, 185);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(38, 15);
            lblDescription.TabIndex = 12;
            lblDescription.Text = "label1";
            // 
            // lblLocation
            // 
            lblLocation.AutoSize = true;
            lblLocation.Location = new Point(320, 384);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(38, 15);
            lblLocation.TabIndex = 13;
            lblLocation.Text = "label1";
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Location = new Point(694, 164);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(38, 15);
            lblStart.TabIndex = 14;
            lblStart.Text = "label1";
            // 
            // lblEnd
            // 
            lblEnd.AutoSize = true;
            lblEnd.Location = new Point(694, 259);
            lblEnd.Name = "lblEnd";
            lblEnd.Size = new Size(38, 15);
            lblEnd.TabIndex = 15;
            lblEnd.Text = "label1";
            // 
            // dtpStartTime
            // 
            dtpStartTime.CustomFormat = "HH:mm";
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.Location = new Point(457, 187);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.ShowUpDown = true;
            dtpStartTime.Size = new Size(231, 23);
            dtpStartTime.TabIndex = 16;
            // 
            // dtpEndTime
            // 
            dtpEndTime.CustomFormat = "HH:mm";
            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.Location = new Point(457, 280);
            dtpEndTime.Name = "dtpEndTime";
            dtpEndTime.ShowUpDown = true;
            dtpEndTime.Size = new Size(231, 23);
            dtpEndTime.TabIndex = 17;
            // 
            // AppointmentEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dtpEndTime);
            Controls.Add(dtpStartTime);
            Controls.Add(lblEnd);
            Controls.Add(lblStart);
            Controls.Add(lblLocation);
            Controls.Add(lblDescription);
            Controls.Add(lblType);
            Controls.Add(lblTitle);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(dtpEnd);
            Controls.Add(dtpStart);
            Controls.Add(txtLocation);
            Controls.Add(txtDescription);
            Controls.Add(txtType);
            Controls.Add(txtTitle);
            Controls.Add(lblCustomer);
            Controls.Add(cmbCustomer);
            Name = "AppointmentEditForm";
            Text = "AppointmentEditForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbCustomer;
        private Label lblCustomer;
        private TextBox txtTitle;
        private TextBox txtType;
        private TextBox txtDescription;
        private TextBox txtLocation;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private Button btnSave;
        private Button btnCancel;
        private Label lblTitle;
        private Label lblType;
        private Label lblDescription;
        private Label lblLocation;
        private Label lblStart;
        private Label lblEnd;
        private DateTimePicker dtpStartTime;
        private DateTimePicker dtpEndTime;
    }
}