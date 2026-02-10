using ClientSchedule.Data;
using ClientSchedule.Resources;
using ClientSchedule.Services;
using MySql.Data.MySqlClient;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ClientSchedule.Forms;

public partial class LoginForm : Form
{
    public int LoggedInUserId { get; private set; }
    public string LoggedInUsername { get; private set; } = "";

    public LoginForm()
    {
        InitializeComponent();

        txtPassword.UseSystemPasswordChar = true;
        cmbLanguagec.DropDownStyle = ComboBoxStyle.DropDownList;

        SetupLanguageDropdown();
        ApplyStrings();
        ShowLocationInfo();

        btnLogin.Click += btnLogin_Click;

        txtUsername.Text = "test";
        txtPassword.Text = "test";
    }

    private void SetupLanguageDropdown()
    {
        cmbLanguagec.Items.Clear();
        cmbLanguagec.Items.Add(new LangItem("English", "en-US"));
        cmbLanguagec.Items.Add(new LangItem("日本語", "ja-JP"));

        var ui = AppState.CurrentCulture.Name;
        var defaultCulture = ui.StartsWith("ja", StringComparison.OrdinalIgnoreCase) ? "ja-JP" : "en-US";


        cmbLanguagec.SelectedItem = cmbLanguagec.Items
            .Cast<LangItem>()
            .First(x => x.Culture == defaultCulture);

        cmbLanguagec.SelectedIndexChanged += (_, __) =>
        {
            var selected = (LangItem)cmbLanguagec.SelectedItem!;
            AppState.SetCulture(selected.Culture);
            ApplyStrings();
            ShowLocationInfo();
        };
    }

    private void ApplyStrings()
    {
        Text = Resources.Strings.Login_Title;
        lblUsername.Text = Resources.Strings.Username;
        lblPassword.Text = Resources.Strings.Password;
        btnLogin.Text = Resources.Strings.Login_Button;
        lblLanguage.Text = Resources.Strings.Language;
    }

    private void ShowLocationInfo()
    {
        var culture = CultureInfo.CurrentCulture.Name;
        var uiCulture = CultureInfo.CurrentUICulture.Name;
        var tz = TimeZoneInfo.Local.DisplayName;

        lblLocation.Text = $"{culture} | {uiCulture} | {tz}";
    }

    private async void btnLogin_Click(object? sender, EventArgs e)
    {
        var user = txtUsername.Text.Trim();
        var pass = txtPassword.Text.Trim();

        if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
        {
            MessageBox.Show(Resources.Strings.Error_EmptyFields);
            return;
        }

        try
        {
            var dt = await Db.QueryAsync(
                "SELECT userId, userName FROM `user` WHERE userName = @u AND password = @p AND active = 1;",
                new MySqlParameter("@u", user),
                new MySqlParameter("@p", pass)
            );

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(Resources.Strings.Error_InvalidCredentials);
                return;
            }

            LoggedInUserId = Convert.ToInt32(dt.Rows[0]["userId"]);
            LoggedInUsername = Convert.ToString(dt.Rows[0]["userName"]) ?? user;

            LoginHistoryService.AppendLogin(LoggedInUsername);
            var path = LoginHistoryService.GetLogPath();

            MessageBox.Show(string.Format(Resources.Strings.Login_History, path));





            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(Resources.Strings.Error_db + Environment.NewLine + ex.Message);
        }
    }


    private sealed record LangItem(string Label, string Culture)
    {
        public override string ToString() => Label;
    }
}
