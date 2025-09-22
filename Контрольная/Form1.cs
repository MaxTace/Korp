using System;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Windows.Forms;

namespace Kr2
{
    public partial class Form1 : Form
    {
        private MonthCalendar monthCalendar;
        private Label selectedDateLabel;
        private Label dayTypeLabel;
        private Label infoLabel;
        private Button btnAddNote;
        private Button btnDelNote;
        private TextBox txtNote;

        private DateTime[] holidays = new DateTime[]
        {
            new DateTime(DateTime.Now.Year, 1, 1),   // Новый год
            new DateTime(DateTime.Now.Year, 1, 7),   // Рождество
            new DateTime(DateTime.Now.Year, 2, 23),  // День защитника отечества
            new DateTime(DateTime.Now.Year, 3, 8),   // Международный женский день
            new DateTime(DateTime.Now.Year, 5, 1),   // Праздник весны и труда
            new DateTime(DateTime.Now.Year, 5, 9),   // День Победы
            new DateTime(DateTime.Now.Year, 6, 12),  // День России
            new DateTime(DateTime.Now.Year, 11, 4)   // День народного единства
        };

        private Dictionary<DateTime, string> notes = new Dictionary<DateTime, string>();

        public Form1()
        {
            InitializeComponent();
            CreateCalendar();
        }

        private void CreateCalendar()
        {
            monthCalendar = new MonthCalendar();
            monthCalendar.Location = new System.Drawing.Point(10, 10);
            monthCalendar.DateSelected += MonthCalendar_DateSelected;

            selectedDateLabel = new Label();
            selectedDateLabel.Location = new System.Drawing.Point(10, 180);
            selectedDateLabel.Size = new System.Drawing.Size(200, 20);
            selectedDateLabel.Text = "Выберите дату";

            this.Controls.Add(monthCalendar);
            this.Controls.Add(selectedDateLabel);

            this.Text = "Календарь";
            this.Size = new System.Drawing.Size(250, 250);

            dayTypeLabel = new Label();
            dayTypeLabel.Location = new Point(10, 205);
            dayTypeLabel.Size = new Size(300, 25);
            dayTypeLabel.Font = new Font("Arial", 12, FontStyle.Bold);

            Label noteLabel = new Label();
            noteLabel.Text = "Заметка на выбранную дату:";
            noteLabel.Location = new Point(10, 230);
            noteLabel.Size = new Size(200, 20);

            infoLabel = new Label();
            infoLabel.Location = new Point(10, 255);
            infoLabel.Size = new Size(350, 60);
            infoLabel.Font = new Font("Arial", 9);

            txtNote = new TextBox();
            txtNote.Location = new Point(10, 390);
            txtNote.Size = new Size(350, 60);
            txtNote.Multiline = true;

            btnAddNote = new Button();
            btnAddNote.Location = new Point(10, 325);
            btnAddNote.Size = new Size(100, 30);
            btnAddNote.Text = "Сохранить";
            btnAddNote.Click += btnAddNote_Click;

            btnDelNote = new Button();
            btnDelNote.Location = new Point(120, 325);
            btnDelNote.Size = new Size(100, 30);
            btnDelNote.Text = "Очистить";
            btnDelNote.Click += btnDelNote_Click;


            this.Controls.AddRange(new Control[] {
                monthCalendar,
                selectedDateLabel,
                dayTypeLabel,
                btnAddNote,
                txtNote,
                noteLabel,
                btnAddNote,
                btnDelNote,
                infoLabel
            });

            UpdateDateInfo(DateTime.Today);
        }

        private void MonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            UpdateDateInfo(e.Start);
            selectedDateLabel.Text = $"Выбранная дата: {e.Start.ToShortDateString()}";
        }
        private void UpdateDateInfo(DateTime selectedDate)
        {
            bool isWeekend = IsWeekend(selectedDate);
            bool isHoliday = IsHoliday(selectedDate);
            bool isNotes = notes.ContainsKey(selectedDate);
            var culture = new CultureInfo("ru-RU");
            string dayName = culture.DateTimeFormat.GetDayName(selectedDate.DayOfWeek);

            if (isHoliday)
            {
                dayTypeLabel.Text = "ПРАЗДНИЧНЫЙ ДЕНЬ";
                dayTypeLabel.ForeColor = Color.Red;
            }
            else if (isWeekend)
            {
                dayTypeLabel.Text = "ВЫХОДНОЙ ДЕНЬ";
                dayTypeLabel.ForeColor = Color.Green;
            }
            else
            {
                dayTypeLabel.Text = "РАБОЧИЙ ДЕНЬ";
                dayTypeLabel.ForeColor = Color.Blue;
            }
            string noteInfo = isNotes ? "✓ Есть заметка" : "Нет заметки";
            infoLabel.Text = isNotes ? $"{selectedDate.ToLongDateString()}\n{char.ToUpper(dayName[0])}{dayName.Substring(1)} • {noteInfo}\n{notes[selectedDate]}": $"{selectedDate.ToLongDateString()}\n{char.ToUpper(dayName[0])}{dayName.Substring(1)} • {noteInfo}" ;
            infoLabel.ForeColor = isNotes ? Color.Green : Color.Black;
            
        }
        private void MonthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            UpdateDateInfo(e.Start);
            LoadNoteForDate(e.Start);
        }

        private bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        private bool IsHoliday(DateTime date)
        {
            foreach (var holiday in holidays)
            {
                if (date.Month == holiday.Month && date.Day == holiday.Day)
                    return true;
            }
            return false;
        }
        private void LoadNoteForDate(DateTime date)
        {
            if (notes.ContainsKey(date.Date))
            {
                txtNote.Text = notes[date.Date];
            }
            else
            {
                txtNote.Text = "";
            }
        }
        private void btnAddNote_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = monthCalendar.SelectionStart.Date;
            
            if (string.IsNullOrWhiteSpace(txtNote.Text))
            {
                if (notes.ContainsKey(selectedDate))
                {
                    notes.Remove(selectedDate);
                }
            }
            else
            {
                notes[selectedDate] = txtNote.Text.Trim();
            }
            
            UpdateDateInfo(selectedDate);
            MessageBox.Show("Заметка сохранена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelNote_Click(object sender, EventArgs e)
        {
            txtNote.Text = "";
        }

        
    }
}