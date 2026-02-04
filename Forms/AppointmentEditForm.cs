using ClientSchedule.Data;
using ClientSchedule.Models;
using ClientSchedule.Services;
using MySql.Data.MySqlClient;

namespace ClientSchedule.Forms;

public partial class AppointmentEditForm : Form
{
    private readonly int _userId;
    private readonly string _username;
    private readonly int? _appointmentId;

    public AppointmentEditForm(int userId, string username, int? appointmentId = null)
    {
        InitializeComponent();

        _userId = userId;
        _username = username;
        _appointmentId = appointmentId;

        cmbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;

        ConfigureTimePicker(dtpStartTime);
        ConfigureTimePicker(dtpEndTime);

        dtpStart.ValueChanged += (_, __) =>
        {
            if (dtpEnd.Value.Date != dtpStart.Value.Date)
                dtpEnd.Value = dtpStart.Value.Date;
        };

        dtpStartTime.ValueChanged += (_, __) =>
        {
            var s = GetStartEastern();
            var e = GetEndEastern();
            if (dtpEnd.Value.Date == dtpStart.Value.Date && e <= s)
                SetEndEastern(s.AddMinutes(30));
        };

        btnSave.Click += async (_, __) => await SaveAsync();
        btnCancel.Click += (_, __) => Close();

        Load += async (_, __) =>
        {
            ApplyStrings();
            await LoadCustomersAsync();

            if (_appointmentId is not null)
            {
                await LoadAppointmentForEditAsync(_appointmentId.Value);
            }
            else
            {
                var nowLocal = DateTime.Now;
                var nowEt = TimeRules.UtcToEastern(TimeZoneInfo.ConvertTimeToUtc(nowLocal, TimeZoneInfo.Local));
                var startEt = nowEt.AddMinutes(30);
                var endEt = startEt.AddMinutes(30);

                SetStartEastern(startEt);
                SetEndEastern(endEt);
            }
        };
    }

    private static void ConfigureTimePicker(DateTimePicker p)
    {
        p.Format = DateTimePickerFormat.Custom;
        p.CustomFormat = "HH:mm";
        p.ShowUpDown = true;

        p.Value = DateTime.Today.AddHours(9);
    }

    private void ApplyStrings()
    {
        Text = _appointmentId is null
            ? Resources.Strings.ApptEdit_Title_Add
            : Resources.Strings.ApptEdit_Title_Edit;

        btnSave.Text = Resources.Strings.Save;
        btnCancel.Text = Resources.Strings.Cancel;

        lblCustomer.Text = Resources.Strings.Appt_Customer;
        lblTitle.Text = Resources.Strings.Appt_Title;
        lblType.Text = Resources.Strings.Appt_Type;
        lblDescription.Text = Resources.Strings.Appt_Description;
        lblLocation.Text = Resources.Strings.Appt_Location;
        lblStart.Text = Resources.Strings.Appt_Start;
        lblEnd.Text = Resources.Strings.Appt_End;
    }

    private async Task LoadCustomersAsync()
    {
        var dt = await Db.QueryAsync(
            "SELECT customerId, customerName FROM customer WHERE active=1 ORDER BY customerName;");

        cmbCustomer.Items.Clear();
        foreach (System.Data.DataRow r in dt.Rows)
        {
            cmbCustomer.Items.Add(new CustomerItem(
                Convert.ToInt32(r["customerId"]),
                Convert.ToString(r["customerName"]) ?? ""
            ));
        }

        if (cmbCustomer.Items.Count > 0)
            cmbCustomer.SelectedIndex = 0;
    }

    private async Task LoadAppointmentForEditAsync(int appointmentId)
    {
        var row = await AppointmentRepository.GetAppointmentByIdAsync(appointmentId);
        if (row is null) return;

        var customerId = Convert.ToInt32(row["customerId"]);

        txtTitle.Text = Convert.ToString(row["title"]) ?? "";
        txtType.Text = Convert.ToString(row["type"]) ?? "";
        txtDescription.Text = Convert.ToString(row["description"]) ?? "";
        txtLocation.Text = Convert.ToString(row["location"]) ?? "";

        var startUtc = TimeRules.ReadDbUtc(row["start"]);
        var endUtc = TimeRules.ReadDbUtc(row["end"]);

        SetStartEastern(TimeRules.UtcToEastern(startUtc));
        SetEndEastern(TimeRules.UtcToEastern(endUtc));

        for (int i = 0; i < cmbCustomer.Items.Count; i++)
        {
            if (cmbCustomer.Items[i] is CustomerItem ci && ci.CustomerId == customerId)
            {
                cmbCustomer.SelectedIndex = i;
                break;
            }
        }
    }


    private bool ValidateInputs(out string message)
    {
        if (cmbCustomer.SelectedItem is not CustomerItem)
        {
            message = Resources.Strings.Appt_Validation_Required;
            return false;
        }

        var title = txtTitle.Text.Trim();
        var type = txtType.Text.Trim();

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(type))
        {
            message = Resources.Strings.Appt_Validation_Required;
            return false;
        }

        var startEt = GetStartEastern();
        var endEt = GetEndEastern();

        if (!TimeRules.ValidateEasternBusinessHours(startEt, endEt, out message))
            return false;

        message = "";
        return true;
    }

    private Task<bool> HasOverlapAsync(DateTime startUtc, DateTime endUtc)
    => AppointmentRepository.HasOverlapAsync(_userId, startUtc, endUtc, _appointmentId);


    private async Task SaveAsync()
    {
        if (!ValidateInputs(out var msg))
        {
            MessageBox.Show(msg);
            return;
        }

        var customerId = ((CustomerItem)cmbCustomer.SelectedItem!).CustomerId;
        var title = txtTitle.Text.Trim();
        var type = txtType.Text.Trim();
        var description = txtDescription.Text.Trim();
        var location = txtLocation.Text.Trim();

        var startEt = GetStartEastern();
        var endEt = GetEndEastern();

        var startUtc = TimeRules.EasternToUtc(startEt);
        var endUtc = TimeRules.EasternToUtc(endEt);

        if (await HasOverlapAsync(startUtc, endUtc))
        {
            MessageBox.Show(Resources.Strings.Appt_Validation_Overlap);
            return;
        }


        var pStart = new MySqlParameter("@s", MySqlDbType.DateTime) { Value = startUtc };
        var pEnd = new MySqlParameter("@e", MySqlDbType.DateTime) { Value = endUtc };

        try
        {
            if (_appointmentId is null)
            {
                await AppointmentRepository.InsertAsync(
                    customerId: customerId,
                    userId: _userId,
                    title: title,
                    type: type,
                    description: description,
                    location: location,
                    startUtc: startUtc,
                    endUtc: endUtc,
                    username: _username
                );
            }
            else
            {
                await AppointmentRepository.UpdateAsync(
                    appointmentId: _appointmentId.Value,
                    customerId: customerId,
                    title: title,
                    type: type,
                    description: description,
                    location: location,
                    startUtc: startUtc,
                    endUtc: endUtc,
                    username: _username
                );
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(Resources.Strings.Appt_SaveFailed + "\n" + ex.Message);
        }

    }

    private DateTime GetStartEastern()
    {
        var d = dtpStart.Value.Date;
        var t = dtpStartTime.Value;
        return new DateTime(d.Year, d.Month, d.Day, t.Hour, t.Minute, 0, DateTimeKind.Unspecified);
    }

    private DateTime GetEndEastern()
    {
        var d = dtpEnd.Value.Date;
        var t = dtpEndTime.Value;
        return new DateTime(d.Year, d.Month, d.Day, t.Hour, t.Minute, 0, DateTimeKind.Unspecified);
    }

    private void SetStartEastern(DateTime eastern)
    {
        dtpStart.Value = eastern.Date;
        dtpStartTime.Value = DateTime.Today.AddHours(eastern.Hour).AddMinutes(eastern.Minute);
    }

    private void SetEndEastern(DateTime eastern)
    {
        dtpEnd.Value = eastern.Date;
        dtpEndTime.Value = DateTime.Today.AddHours(eastern.Hour).AddMinutes(eastern.Minute);
    }
}
