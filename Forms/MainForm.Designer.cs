namespace ClientSchedule.Forms
{
    partial class MainForm
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
            tabMain = new TabControl();
            tabCustomers = new TabPage();
            btnDeleteCustomer = new Button();
            btnEditCustomer = new Button();
            btnAddCustomer = new Button();
            dgvCustomers = new DataGridView();
            tabAppointments = new TabPage();
            dgvDayAppointments = new DataGridView();
            lblSelectedDay = new Label();
            calAppt = new MonthCalendar();
            btnDeleteAppt = new Button();
            btnEditAppt = new Button();
            btnAddAppt = new Button();
            dgvAppointments = new DataGridView();
            tabReports = new TabPage();
            dgvReport = new DataGridView();
            btnRunReport = new Button();
            cmbReport = new ComboBox();
            tabMain.SuspendLayout();
            tabCustomers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            tabAppointments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDayAppointments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).BeginInit();
            tabReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReport).BeginInit();
            SuspendLayout();
            // 
            // tabMain
            // 
            tabMain.Controls.Add(tabCustomers);
            tabMain.Controls.Add(tabAppointments);
            tabMain.Controls.Add(tabReports);
            tabMain.Location = new Point(12, 12);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(756, 401);
            tabMain.TabIndex = 0;
            // 
            // tabCustomers
            // 
            tabCustomers.Controls.Add(btnDeleteCustomer);
            tabCustomers.Controls.Add(btnEditCustomer);
            tabCustomers.Controls.Add(btnAddCustomer);
            tabCustomers.Controls.Add(dgvCustomers);
            tabCustomers.Location = new Point(4, 24);
            tabCustomers.Name = "tabCustomers";
            tabCustomers.Padding = new Padding(3);
            tabCustomers.Size = new Size(748, 373);
            tabCustomers.TabIndex = 0;
            tabCustomers.Text = "Customers";
            tabCustomers.UseVisualStyleBackColor = true;
            // 
            // btnDeleteCustomer
            // 
            btnDeleteCustomer.Location = new Point(168, 289);
            btnDeleteCustomer.Name = "btnDeleteCustomer";
            btnDeleteCustomer.Size = new Size(75, 23);
            btnDeleteCustomer.TabIndex = 3;
            btnDeleteCustomer.Text = "Delete";
            btnDeleteCustomer.UseVisualStyleBackColor = true;
            // 
            // btnEditCustomer
            // 
            btnEditCustomer.Location = new Point(87, 289);
            btnEditCustomer.Name = "btnEditCustomer";
            btnEditCustomer.Size = new Size(75, 23);
            btnEditCustomer.TabIndex = 2;
            btnEditCustomer.Text = "Modify";
            btnEditCustomer.UseVisualStyleBackColor = true;
            // 
            // btnAddCustomer
            // 
            btnAddCustomer.Location = new Point(6, 289);
            btnAddCustomer.Name = "btnAddCustomer";
            btnAddCustomer.Size = new Size(75, 23);
            btnAddCustomer.TabIndex = 1;
            btnAddCustomer.Text = "Add";
            btnAddCustomer.UseVisualStyleBackColor = true;
            // 
            // dgvCustomers
            // 
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Location = new Point(6, 6);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.Size = new Size(736, 267);
            dgvCustomers.TabIndex = 0;
            // 
            // tabAppointments
            // 
            tabAppointments.Controls.Add(dgvDayAppointments);
            tabAppointments.Controls.Add(lblSelectedDay);
            tabAppointments.Controls.Add(calAppt);
            tabAppointments.Controls.Add(btnDeleteAppt);
            tabAppointments.Controls.Add(btnEditAppt);
            tabAppointments.Controls.Add(btnAddAppt);
            tabAppointments.Controls.Add(dgvAppointments);
            tabAppointments.Location = new Point(4, 24);
            tabAppointments.Name = "tabAppointments";
            tabAppointments.Padding = new Padding(3);
            tabAppointments.Size = new Size(748, 373);
            tabAppointments.TabIndex = 1;
            tabAppointments.Text = "Appointments";
            tabAppointments.UseVisualStyleBackColor = true;
            // 
            // dgvDayAppointments
            // 
            dgvDayAppointments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDayAppointments.Location = new Point(251, 214);
            dgvDayAppointments.MultiSelect = false;
            dgvDayAppointments.Name = "dgvDayAppointments";
            dgvDayAppointments.ReadOnly = true;
            dgvDayAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDayAppointments.Size = new Size(478, 124);
            dgvDayAppointments.TabIndex = 6;
            // 
            // lblSelectedDay
            // 
            lblSelectedDay.AutoSize = true;
            lblSelectedDay.Location = new Point(416, 196);
            lblSelectedDay.Name = "lblSelectedDay";
            lblSelectedDay.Size = new Size(74, 15);
            lblSelectedDay.TabIndex = 5;
            lblSelectedDay.Text = "Selected Day";
            // 
            // calAppt
            // 
            calAppt.Location = new Point(12, 196);
            calAppt.MaxSelectionCount = 1;
            calAppt.Name = "calAppt";
            calAppt.TabIndex = 4;
            // 
            // btnDeleteAppt
            // 
            btnDeleteAppt.Location = new Point(667, 344);
            btnDeleteAppt.Name = "btnDeleteAppt";
            btnDeleteAppt.Size = new Size(75, 23);
            btnDeleteAppt.TabIndex = 3;
            btnDeleteAppt.Text = "Delete";
            btnDeleteAppt.UseVisualStyleBackColor = true;
            // 
            // btnEditAppt
            // 
            btnEditAppt.Location = new Point(576, 344);
            btnEditAppt.Name = "btnEditAppt";
            btnEditAppt.Size = new Size(75, 23);
            btnEditAppt.TabIndex = 2;
            btnEditAppt.Text = "Modify";
            btnEditAppt.UseVisualStyleBackColor = true;
            // 
            // btnAddAppt
            // 
            btnAddAppt.Location = new Point(486, 344);
            btnAddAppt.Name = "btnAddAppt";
            btnAddAppt.Size = new Size(75, 23);
            btnAddAppt.TabIndex = 1;
            btnAddAppt.Text = "Add";
            btnAddAppt.UseVisualStyleBackColor = true;
            // 
            // dgvAppointments
            // 
            dgvAppointments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAppointments.Location = new Point(6, 6);
            dgvAppointments.Name = "dgvAppointments";
            dgvAppointments.Size = new Size(736, 178);
            dgvAppointments.TabIndex = 0;
            // 
            // tabReports
            // 
            tabReports.Controls.Add(dgvReport);
            tabReports.Controls.Add(btnRunReport);
            tabReports.Controls.Add(cmbReport);
            tabReports.Location = new Point(4, 24);
            tabReports.Name = "tabReports";
            tabReports.Size = new Size(748, 373);
            tabReports.TabIndex = 2;
            tabReports.Text = "Reports";
            tabReports.UseVisualStyleBackColor = true;
            // 
            // dgvReport
            // 
            dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReport.Location = new Point(14, 14);
            dgvReport.Name = "dgvReport";
            dgvReport.Size = new Size(707, 299);
            dgvReport.TabIndex = 3;
            // 
            // btnRunReport
            // 
            btnRunReport.Location = new Point(254, 331);
            btnRunReport.Name = "btnRunReport";
            btnRunReport.Size = new Size(75, 23);
            btnRunReport.TabIndex = 2;
            btnRunReport.Text = "button1";
            btnRunReport.UseVisualStyleBackColor = true;
            // 
            // cmbReport
            // 
            cmbReport.FormattingEnabled = true;
            cmbReport.Location = new Point(14, 331);
            cmbReport.Name = "cmbReport";
            cmbReport.Size = new Size(224, 23);
            cmbReport.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabMain);
            Name = "MainForm";
            Text = "MainForm";
            tabMain.ResumeLayout(false);
            tabCustomers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            tabAppointments.ResumeLayout(false);
            tabAppointments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDayAppointments).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).EndInit();
            tabReports.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReport).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabMain;
        private TabPage tabCustomers;
        private TabPage tabAppointments;
        private Button btnDeleteCustomer;
        private Button btnEditCustomer;
        private Button btnAddCustomer;
        private DataGridView dgvCustomers;
        private DataGridView dgvAppointments;
        private Button btnDeleteAppt;
        private Button btnEditAppt;
        private Button btnAddAppt;
        private MonthCalendar calAppt;
        private DataGridView dgvDayAppointments;
        private Label lblSelectedDay;
        private TabPage tabReports;
        private DataGridView dgvReport;
        private Button btnRunReport;
        private ComboBox cmbReport;
    }
}