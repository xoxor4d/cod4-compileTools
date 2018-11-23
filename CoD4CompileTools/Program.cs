using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Data;
using System.Drawing;
using System.Text;
using Microsoft.VisualBasic;
using System.Threading;

namespace CoD4CompileTools
{
    static class Program
    {
        public static dlg_bspTools MainForm_Inst;

        [STAThread]
        static void Main()
        {
            Application.ThreadException += ThreadException;
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run((MainForm_Inst = new dlg_bspTools()));
        }

        private static void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString());
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString());
        }

    }
}
