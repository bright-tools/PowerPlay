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

        static String scriptBase = Environment.SpecialFolder.MyDocuments + System.IO.Path.PathSeparator + "PowerPlay" + System.IO.Path.PathSeparator;

        static void RunScripts( String dir )
        {
            List<String> files = Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories)
               .Where(s => (s.EndsWith(".exe") || s.EndsWith(".bat")) && (s.Count(c => c == '.') == 2))
               .ToList();

            foreach( String file in files )
            {
                System.Diagnostics.Process.Start(file);
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

        static void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if( e.Mode == PowerModes.StatusChange )
            {
                PowerStatus power = SystemInformation.PowerStatus;

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
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
        }
    }
}
