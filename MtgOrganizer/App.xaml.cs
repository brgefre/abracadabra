using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;
using MtgOrganizer.Resources;

namespace MtgOrganizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        /// <summary>
        /// On application startup, check that certain application settings are set.
        /// If they aren't, set them, prompting the user as necessary.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void App_Startup(object sender, StartupEventArgs args)
        {
            string dataDir = MtgOrganizer.Properties.Settings.Default.DataDirectory;

            if (string.IsNullOrWhiteSpace(dataDir))
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                    {
                        MtgOrganizer.Properties.Settings.Default.DataDirectory = dialog.SelectedPath;
                        MtgOrganizer.Properties.Settings.Default.Save();
                    }
                }
            }
        }







    }
}