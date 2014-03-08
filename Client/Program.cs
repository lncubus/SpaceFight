using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ControlMode = SF.Space.ControlMode;

namespace Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
                foreach (var arg in args)
                {
                    int id;
                    ControlMode mode;
                    if (int.TryParse(arg, out id))
                        ClientForm.PredefinedIdShip = id;
                    else if (Enum.TryParse(arg, true, out mode))
                        ClientForm.PredefinedControlMode = mode;
                }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ClientForm());
        }
    }
}
