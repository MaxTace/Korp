using System;
using System.Windows.Forms;

namespace Kr2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // или new CalendarForm()
        }
    }
}