using System;
using System.Windows.Forms;

namespace ABTTestProgram {
    internal class ABTMain {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ProgramForm());
        }
    }
}
