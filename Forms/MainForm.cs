using ClientSchedule.Data;
using ClientSchedule.Services;
using System.Data;

namespace ClientSchedule.Forms;

public partial class MainForm : Form
{
    private readonly int _userId;
    private readonly string _username;

    public MainForm(int userId, string username)
    {
        InitializeComponent();

        _userId = userId;
        _username = username;

        ConfigureControls();
        WireEvents();

        ApplyStrings();
        PopulateReports();

        AppState.CultureChanged += OnCultureChanged;
        FormClosed += (_, __) => AppState.CultureChanged -= OnCultureChanged;
    }

    // ----------------------------
    // Init / wiring
    // ----------------------------

    private void ConfigureControls()
    {
        calAppt.MaxSelectionCount = 1;
        cmbReport.DropDownStyle = ComboBoxStyle.DropDownList;
    }

    private void WireEvents()
    {
        Load += async (_, __) => await InitializeAsync();

        btnAddCustomer.Click += async (_, __) => await AddCustomerAsync();
        btnEditCustomer.Click += async (_, __) => await EditCustomerAsync();
        btnDeleteCustomer.Click += async (_, __) => await DeleteCustomerAsync();

        btnAddAppt.Click += async (_, __) => await AddApptAsync();
        btnEditAppt.Click += async (_, __) => await EditApptAsync();
        btnDeleteAppt.Click += async (_, __) => await DeleteApptAsync();

        dgvDayAppointments.CellDoubleClick += async (_, __) => await EditApptAsync();

        calAppt.DateSelected += async (_, e) => await LoadAppointmentsForSelectedDayAsync(e.Start);

        btnRunReport.Click += async (_, __) => await RunSelectedReportAsync();
    }

    private async Task InitializeAsync()
    {
        await LoadCustomersAsync();
        await LoadAppointmentsAsync();

        await UpcomingAlertService.ShowUpcomingAppointmentAlertAsync(_userId);

        await LoadAppointmentsForSelectedDayAsync(calAppt.SelectionStart);
    }

    // ----------------------------
    // Localization
    // ----------------------------

    private void OnCultureChanged()
    {
        ApplyStrings();
        PopulateReports();
    }

    private void ApplyStrings()
    {
        Text = Resources.Strings.Main_Title;

        tabCustomers.Text = Resources.Strings.Tab_Customers;
        tabAppointments.Text = Resources.Strings.Tab_Appointments;
        tabReports.Text = Resources.Strings.Tab_Reports;

        btnAddCustomer.Text = Resources.Strings.Add;
        btnEditCustomer.Text = Resources.Strings.Modify;
        btnDeleteCustomer.Text = Resources.Strings.Delete;

        btnAddAppt.Text = Resources.Strings.Add;
        btnEditAppt.Text = Resources.Strings.Appy_Modify;
        btnDeleteAppt.Text = Resources.Strings.Appt_Delete;

        btnRunReport.Text = Resources.Strings.Report_Run;
    }

    private void PopulateReports()
    {
        var current = cmbReport.SelectedIndex;

        cmbReport.Items.Clear();
        cmbReport.Items.Add(Resources.Strings.Report_TypesByMonth);
        cmbReport.Items.Add(Resources.Strings.Report_UserSchedules);
        cmbReport.Items.Add(Resources.Strings.Report_ByCustomer);

        cmbReport.SelectedIndex = (current >= 0 && current < cmbReport.Items.Count) ? current : 0;
    }

    // ----------------------------
    // Customers
    // ----------------------------

    private async Task LoadCustomersAsync()
    {
        var dt = await CustomerRepository.GetCustomersForGridAsync();

        dgvCustomers.DataSource = dt;

        dgvCustomers.Columns["customerId"].HeaderText = "ID";
        dgvCustomers.Columns["customerName"].HeaderText = "Name";
        dgvCustomers.Columns["address"].HeaderText = "Address";
        dgvCustomers.Columns["address2"].HeaderText = "Address 2";
        dgvCustomers.Columns["postalCode"].HeaderText = "Postal";
        dgvCustomers.Columns["phone"].HeaderText = "Phone";
        dgvCustomers.Columns["city"].HeaderText = "City";
        dgvCustomers.Columns["country"].HeaderText = "Country";
    }

    private int? GetSelectedCustomerId()
        => dgvCustomers.CurrentRow?.DataBoundItem is DataRowView drv
            ? Convert.ToInt32(drv.Row["customerId"])
            : null;

    private async Task AddCustomerAsync()
    {
        using var f = new CustomerEditForm(_username);
        if (f.ShowDialog() == DialogResult.OK)
            await LoadCustomersAsync();
    }

    private async Task EditCustomerAsync()
    {
        var id = GetSelectedCustomerId();
        if (id is null)
        {
            MessageBox.Show("Select a customer first.");
            return;
        }

        using var f = new CustomerEditForm(_username, id.Value);
        if (f.ShowDialog() == DialogResult.OK)
            await LoadCustomersAsync();
    }

    private async Task DeleteCustomerAsync()
    {
        var id = GetSelectedCustomerId();
        if (id is null)
        {
            MessageBox.Show("Select a customer first.");
            return;
        }

        if (MessageBox.Show("Delete this customer?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
            return;

        try
        {
            await CustomerRepository.DeleteCustomerAsync(id.Value);
            await LoadCustomersAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Delete failed.\n" + ex.Message);
        }
    }

    // ----------------------------
    // Appointments
    // ----------------------------

    private async Task RefreshAppointmentsViewsAsync()
    {
        await LoadAppointmentsAsync();
        await LoadAppointmentsForSelectedDayAsync(calAppt.SelectionStart);
    }

    private async Task LoadAppointmentsAsync()
    {
        var dt = await AppointmentRepository.GetAppointmentsForUserAsync(_userId);
        BindAppointmentsGrid(dgvAppointments, dt, useEasternForDisplay: true);
    }

    private int? GetSelectedAppointmentId()
        => dgvAppointments.CurrentRow?.DataBoundItem is DataRowView drv
            ? Convert.ToInt32(drv.Row["appointmentId"])
            : null;

    private async Task AddApptAsync()
    {
        using var f = new AppointmentEditForm(_userId, _username);
        if (f.ShowDialog() == DialogResult.OK)
            await RefreshAppointmentsViewsAsync();
    }

    private async Task EditApptAsync()
    {
        var id = GetSelectedAppointmentId();
        if (id is null)
        {
            MessageBox.Show(Resources.Strings.SelectApptFirst);
            return;
        }

        using var f = new AppointmentEditForm(_userId, _username, id.Value);
        if (f.ShowDialog() == DialogResult.OK)
            await RefreshAppointmentsViewsAsync();
    }

    private async Task DeleteApptAsync()
    {
        var id = GetSelectedAppointmentId();
        if (id is null)
        {
            MessageBox.Show(Resources.Strings.SelectApptFirst);
            return;
        }

        if (MessageBox.Show(Resources.Strings.ConfirmDeleteAppt, "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
            return;

        try
        {
            await AppointmentRepository.DeleteAsync(id.Value);
            await RefreshAppointmentsViewsAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(Resources.Strings.Appt_DeleteFailed + "\n" + ex.Message);
        }
    }

    // ----------------------------
    // Calendar day view (Eastern)
    // ----------------------------

    private async Task LoadAppointmentsForSelectedDayAsync(DateTime selectedLocalDate)
    {
        var selectedDateEt = selectedLocalDate.Date;

        var startOfDayEt = new DateTime(
            selectedDateEt.Year, selectedDateEt.Month, selectedDateEt.Day,
            0, 0, 0, DateTimeKind.Unspecified);

        var endOfDayEt = startOfDayEt.AddDays(1);

        var startUtc = TimeRules.EasternToUtc(startOfDayEt);
        var endUtc = TimeRules.EasternToUtc(endOfDayEt);

        var dt = await AppointmentRepository.GetAppointmentsForUserBetweenUtcAsync(_userId, startUtc, endUtc);

        // For the day grid, show LOCAL time (requirement A5)
        BindAppointmentsGrid(dgvDayAppointments, dt, useEasternForDisplay: false);

        if (lblSelectedDay is not null)
            lblSelectedDay.Text = $"Selected day (Eastern): {selectedDateEt:yyyy-MM-dd}";
    }

    private static void BindAppointmentsGrid(DataGridView grid, DataTable dt, bool useEasternForDisplay)
    {
        if (!dt.Columns.Contains("startLocal")) dt.Columns.Add("startLocal", typeof(string));
        if (!dt.Columns.Contains("endLocal")) dt.Columns.Add("endLocal", typeof(string));

        foreach (DataRow r in dt.Rows)
        {
            var sUtc = TimeRules.ReadDbUtc(r["start"]);
            var eUtc = TimeRules.ReadDbUtc(r["end"]);

            r["startLocal"] = useEasternForDisplay
                ? TimeRules.FormatEasternForGrid(sUtc)
                : sUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm");

            r["endLocal"] = useEasternForDisplay
                ? TimeRules.FormatEasternForGrid(eUtc)
                : eUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
        }

        grid.DataSource = dt;

        if (grid.Columns.Contains("start")) grid.Columns["start"].Visible = false;
        if (grid.Columns.Contains("end")) grid.Columns["end"].Visible = false;

        grid.Columns["appointmentId"].HeaderText = Resources.Strings.Col_ApptId;
        grid.Columns["customerName"].HeaderText = Resources.Strings.Col_ApptCustomer;
        grid.Columns["title"].HeaderText = Resources.Strings.Col_ApptTitle;
        grid.Columns["type"].HeaderText = Resources.Strings.Col_ApptType;
        grid.Columns["startLocal"].HeaderText = Resources.Strings.Col_ApptStart;
        grid.Columns["endLocal"].HeaderText = Resources.Strings.Col_ApptEnd;
    }

    // ----------------------------
    // Reports
    // ----------------------------

    private async Task RunSelectedReportAsync()
    {
        try
        {
            var sel = cmbReport.SelectedItem?.ToString() ?? "";

            if (sel == Resources.Strings.Report_TypesByMonth)
            {
                dgvReport.DataSource = await ReportService.GetTypesByMonthAsync();
            }
            else if (sel == Resources.Strings.Report_UserSchedules)
            {
                dgvReport.DataSource = await ReportService.GetScheduleForEachUserAsync();
            }
            else if (sel == Resources.Strings.Report_ByCustomer)
            {
                dgvReport.DataSource = await ReportService.GetAppointmentsPerCustomerAsync();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(Resources.Strings.Report_Error + "\n" + ex.Message);
        }
    }
}
