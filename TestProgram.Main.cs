using System;
using System.Windows.Forms;

namespace TestProgram {
    internal class TestProgramMain {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestProgramForm());
        }
    }
}
