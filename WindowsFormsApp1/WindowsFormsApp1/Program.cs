using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += OnExit;
            Application.Run(new FormStart());
            
        }

        static void OnExit(object sender, EventArgs args)
        {
            CardService.Instance.SaveCards(AppSettings.jsonDirPath + AppSettings.jsonFileName);
        }
    }
}
