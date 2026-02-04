using ClientSchedule.Data;
using ClientSchedule.Models;
using ClientSchedule.Services;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace ClientSchedule.Forms;

public partial class CustomerEditForm : Form
{
    private readonly string _username;
    private readonly int? _customerId;

    private bool _wiredCountryHandler;

    public CustomerEditForm(string username, int? customerId = null)
    {
        InitializeComponent();

        _username = username;
        _customerId = customerId;

        cmbCountry.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbCity.DropDownStyle = ComboBoxStyle.DropDownList;

        AppState.CultureChanged += ApplyStrings;
        FormClosed += (_, __) => AppState.CultureChanged -= ApplyStrings;

        btnSave.Click += async (_, __) => await SaveAsync();
        btnCancel.Click += (_, __) => Close();

        Load += async (_, __) => await InitializeAsync();

        ApplyStrings();
    }

    // ----------------------------
    // Init
    // ----------------------------

    private async Task InitializeAsync()
    {
        await LoadCountriesAsync();

        if (_customerId is not null)
            await LoadCustomerForEditAsync(_customerId.Value);
    }

    // ----------------------------
    // Localization
    // ----------------------------

    private void ApplyStrings()
    {
        Text = _customerId is null
            ? Resources.Strings.CustomerEdit_Title_Add
            : Resources.Strings.CustomerEdit_Title_Edit;

        btnSave.Text = Resources.Strings.Save;
        btnCancel.Text = Resources.Strings.Cancel;

        lblName.Text = Resources.Strings.Customer_Name;
        lblAddress.Text = Resources.Strings.Customer_Address;
        lblAddress2.Text = Resources.Strings.Customer_Address2;
        lblCity.Text = Resources.Strings.Customer_City;
        lblCountry.Text = Resources.Strings.Customer_Country;
        lblPostal.Text = Resources.Strings.Customer_Postal;
        lblPhone.Text = Resources.Strings.Customer_Phone;
    }

    // ----------------------------
    // Lookups
    // ----------------------------

    private async Task LoadCountriesAsync()
    {
        var dt = await CustomerRepository.GetCountriesAsync();

        cmbCountry.Items.Clear();
        foreach (System.Data.DataRow r in dt.Rows)
        {
            cmbCountry.Items.Add(new CountryItem(
                Convert.ToInt32(r["countryId"]),
                Convert.ToString(r["country"]) ?? ""
            ));
        }

        if (cmbCountry.Items.Count > 0)
            cmbCountry.SelectedIndex = 0;

        if (!_wiredCountryHandler)
        {
            cmbCountry.SelectedIndexChanged += async (_, __) =>
            {
                if (cmbCountry.SelectedItem is CountryItem ci)
                    await LoadCitiesAsync(ci.CountryId);
            };

            _wiredCountryHandler = true;
        }

        if (cmbCountry.SelectedItem is CountryItem first)
            await LoadCitiesAsync(first.CountryId);
    }

    private async Task LoadCitiesAsync(int countryId)
    {
        var dt = await CustomerRepository.GetCitiesByCountryAsync(countryId);

        cmbCity.Items.Clear();
        foreach (System.Data.DataRow r in dt.Rows)
        {
            cmbCity.Items.Add(new CityItem(
                Convert.ToInt32(r["cityId"]),
                Convert.ToString(r["city"]) ?? ""
            ));
        }

        if (cmbCity.Items.Count > 0)
            cmbCity.SelectedIndex = 0;
    }

    // ----------------------------
    // Edit mode load
    // ----------------------------

    private async Task LoadCustomerForEditAsync(int customerId)
    {
        var dt = await CustomerRepository.GetCustomerForEditAsync(customerId);
        if (dt.Rows.Count == 0) return;

        var row = dt.Rows[0];

        txtCustomerName.Text = Convert.ToString(row["customerName"]) ?? "";
        txtAddress1.Text = Convert.ToString(row["address"]) ?? "";
        txtAddress2.Text = Convert.ToString(row["address2"]) ?? "";
        txtPostalCode.Text = Convert.ToString(row["postalCode"]) ?? "";
        txtPhone.Text = Convert.ToString(row["phone"]) ?? "";

        var countryId = Convert.ToInt32(row["countryId"]);
        var cityId = Convert.ToInt32(row["cityId"]);

        SelectCountry(countryId);
        await LoadCitiesAsync(countryId);
        SelectCity(cityId);
    }

    private void SelectCountry(int countryId)
    {
        for (int i = 0; i < cmbCountry.Items.Count; i++)
        {
            if (cmbCountry.Items[i] is CountryItem co && co.CountryId == countryId)
            {
                cmbCountry.SelectedIndex = i;
                return;
            }
        }
    }

    private void SelectCity(int cityId)
    {
        for (int i = 0; i < cmbCity.Items.Count; i++)
        {
            if (cmbCity.Items[i] is CityItem ci && ci.CityId == cityId)
            {
                cmbCity.SelectedIndex = i;
                return;
            }
        }
    }

    // ----------------------------
    // Validation
    // ----------------------------

    private bool ValidateInputs(out string error)
    {
        var name = txtCustomerName.Text.Trim();
        var addr1 = txtAddress1.Text.Trim();
        var phone = txtPhone.Text.Trim();

        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(addr1) ||
            string.IsNullOrWhiteSpace(phone))
        {
            error = Resources.Strings.Customer_Validation_Required;
            return false;
        }

        if (!Regex.IsMatch(phone, @"^[0-9-]+$"))
        {
            error = Resources.Strings.Customer_Validation_Phone;
            return false;
        }

        if (cmbCountry.SelectedItem is not CountryItem)
        {
            error = Resources.Strings.Customer_Validation_Country;
            return false;
        }

        if (cmbCity.SelectedItem is not CityItem)
        {
            error = Resources.Strings.Customer_Validation_City;
            return false;
        }

        error = "";
        return true;
    }

    // ----------------------------
    // Save
    // ----------------------------

    private async Task SaveAsync()
    {
        if (!ValidateInputs(out var err))
        {
            MessageBox.Show(err);
            return;
        }

        var customerName = txtCustomerName.Text.Trim();
        var address1 = txtAddress1.Text.Trim();
        var address2 = txtAddress2.Text.Trim();
        var postal = txtPostalCode.Text.Trim();
        var phone = txtPhone.Text.Trim();
        var cityId = ((CityItem)cmbCity.SelectedItem!).CityId;

        try
        {
            if (_customerId is null)
            {
                await CustomerRepository.AddCustomerAsync(
                    _username,
                    customerName,
                    address1,
                    string.IsNullOrWhiteSpace(address2) ? null : address2,
                    cityId,
                    string.IsNullOrWhiteSpace(postal) ? null : postal,
                    phone);
            }
            else
            {
                await CustomerRepository.UpdateCustomerAsync(
                    _username,
                    _customerId.Value,
                    customerName,
                    address1,
                    string.IsNullOrWhiteSpace(address2) ? null : address2,
                    cityId,
                    string.IsNullOrWhiteSpace(postal) ? null : postal,
                    phone);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Save failed.\n" + ex.Message);
        }
    }
}
