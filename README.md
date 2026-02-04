# ClientSchedule (WinForms + MySQL)

A Windows Forms scheduling application that supports user login, customer management, appointment management, simple reporting, and bilingual UI (English + Japanese). The app uses a MySQL database (`client_schedule`) and enforces scheduling rules in **US Eastern Time**.

---

## What this project does

This app is a small scheduling system:

- A user logs in with a username/password stored in MySQL.
- After login, the main screen lets the user:
  - Add / edit / delete customers
  - Add / edit / delete appointments
  - View appointments in a grid and via a calendar day view
  - Run a few basic reports
- The UI can be switched between **English** and **Japanese** using `.resx` resources.
- The app shows an alert if the logged-in user has an appointment starting within 15 minutes.
- Login activity is written to a `login_history.txt` file on the local machine.

---

## Tech stack

- **C# WinForms** (.NET 8)
- **MySQL** database (`client_schedule`)
- **MySql.Data** / MySQL Connector library
- **RESX** resource localization (English + Japanese)
- Time zone rules implemented using Windows time zone ID: `Eastern Standard Time`

---

## Localization (English + Japanese)

This project uses `Resources/Strings.resx` and `Resources/Strings.ja.resx`.

- All UI labels, button text, validation messages, and alert/report strings are loaded from resource keys.
- Switching language updates form text using the app culture state (for example via an `AppState` culture event + `ApplyStrings()` methods).

---

## Time handling (important)

Appointments are:
- **Displayed and validated in US Eastern Time**
- **Stored in the database as UTC** (recommended pattern)

Rules enforced:
- Monday–Friday only
- Between **9:00 AM and 5:00 PM Eastern**
- Start < End
- Same business day only
- No overlap for the same user

---

## Login history output

A login history file is written on the local machine:

- File: `login_history.txt`
- Location: `Documents/ClientSchedule`

Each login attempt is recorded (success/failure + timestamp + username).

---

# Project Requirements Checklist 

## A1) Login Form

### A1a) Determine a user’s location
- The login form determines the user’s location (based on system environment / time zone info).
- Location/time zone info is shown on screen.

### A1b) Translate login + error control messages into English + one additional language
- Implemented using `.resx` resources:
  - `Strings.resx` (English)
  - `Strings.ja.resx` (Japanese)
- Login UI strings and login error messages are pulled from resource keys.

Examples of translated messages:
- Empty username/password
- Invalid credentials
- Database error

### A1c) Verify correct username and password
- On login, credentials are validated against the MySQL `user` table.
- If valid → open main form
- If invalid → show localized error message

---

## A2) Customer Records (Add, Update, Delete)

### A2) Provide ability to add, update, delete customers
- Customer list grid on main form
- Customer add/edit dialog form (`CustomerEditForm`)
- Delete customer logic with database operation

### A2a) Validate customer record requirements
Validated in customer form:

- Customer record includes required fields:
  - Name
  - Address
  - Phone number
- Fields are trimmed and non-empty
- Phone number allows only digits and dashes (regex validation)

### A2b) Exception handling for add/update/delete operations
- Add customer: exception handling around insert transaction
- Update customer: exception handling around update transaction
- Delete customer: exception handling around delete logic
- Customer delete guards against FK/related-data constraints (ex: cannot delete customer with existing appointments)

---

## A3) Appointments (Add, Update, Delete)

### A3) Provide ability to add, update, delete appointments
- Appointment grid on main form
- Appointment add/edit dialog form (`AppointmentEditForm`)
- Delete appointment logic with database operation

Validation rules include:
- Required fields (customer, title, type, start, end)
- Business hours in Eastern Time (Mon–Fri 9–5)
- Start before end
- Same-day requirement
- Overlap prevention per user

---

## Additional features implemented

### Upcoming appointment alert (15-minute alert)
- On main form load, the app checks the next 15 minutes for the logged-in user.
- If found, shows a localized alert with customer, title, type, and start time.

### Calendar day view
- Calendar control allows selecting a date
- Appointments for that selected date display in a dedicated grid
- Date selection interpreted as an Eastern day for filtering

### Reports
A simple report tab supports:
- Appointment types by month
- Schedule per user
- Appointments per customer

---

## Notes / Assumptions

- The project uses WinForms on Windows.
- The time zone ID `Eastern Standard Time` is available on Windows.
- Appointments are stored as UTC and converted for display/validation.

---

## Screens / Forms

- **LoginForm**
  - Language selector
  - Username/password input
  - Localized error handling
    <img width="798" height="476" alt="image" src="https://github.com/user-attachments/assets/d5b6c8bd-25a5-4440-b1bc-04f8395aafdd" />

- **MainForm**
  - Customers tab (grid + add/edit/delete)
  - Appointments tab (grid + add/edit/delete)
  - Calendar day view
  - Reports tab
    
    <img width="793" height="475" alt="image" src="https://github.com/user-attachments/assets/339a4771-4de4-4c57-b459-0e8acbd23a7d" />
    <img width="795" height="476" alt="image" src="https://github.com/user-attachments/assets/2b839c8a-413a-4881-b118-c8b26fa02e03" />
    <img width="793" height="471" alt="image" src="https://github.com/user-attachments/assets/bd4bece4-0ec8-40e4-9326-bae6dd5b41a5" />



- **CustomerEditForm**
  - Name/address/phone/country/city
  - Validation + transactional save
    <img width="797" height="474" alt="image" src="https://github.com/user-attachments/assets/ef125c1f-eacf-45f4-b1c2-adca93859e33" />

- **AppointmentEditForm**
  - Customer/title/type/description/location
  - Start/end date + time selectors
  - Eastern time validation + overlap checks
    <img width="791" height="465" alt="image" src="https://github.com/user-attachments/assets/05f8714b-1c3e-44af-ba5f-90f53bdc5038" />


