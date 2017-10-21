using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace PowerPlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        static String scriptBase = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + System.IO.Path.DirectorySeparatorChar + "PowerPlay" + System.IO.Path.DirectorySeparatorChar;

        static void RunScripts( String dir )
        {
            List<String> files = Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories)
               .Where(s => (s.EndsWith(".exe") || s.EndsWith(".bat")))
               .ToList();

            foreach( String file in files )
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();

                // Don't want a shell window
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.StartInfo.FileName = file;

                process.Start();
            }
        }

        static void OnMains()
        {
            String folder = scriptBase + "OnMains";
            RunScripts(folder);
        }

        static void OnBattery()
        {
            String folder = scriptBase + "OnBattery";
            RunScripts(folder);
        }

        static System.Windows.Forms.PowerLineStatus previousPower = System.Windows.Forms.PowerLineStatus.Unknown;

        static void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if( e.Mode == PowerModes.StatusChange )
            {
                PowerStatus power = SystemInformation.PowerStatus;

                if (power.PowerLineStatus != previousPower)
                {

                    switch (power.PowerLineStatus)
                    {
                        case System.Windows.Forms.PowerLineStatus.Online:
                            OnMains();
                            break;

                        case System.Windows.Forms.PowerLineStatus.Offline:
                            OnBattery();
                            break;
                    }
                }

                previousPower = power.PowerLineStatus;
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
        }
    }
}
