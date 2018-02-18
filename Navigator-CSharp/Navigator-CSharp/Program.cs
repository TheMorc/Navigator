using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Navigator_CSharp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            taskbar_brainz.kill();
            Application.Run(new taskbar());
            taskbar_brainz.create();

        }
    }
}
