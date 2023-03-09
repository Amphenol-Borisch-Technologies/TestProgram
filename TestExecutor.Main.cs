using System;
using System.Windows.Forms;

namespace TestExecutor {
    internal class TestExecutorMain {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestExecutor());
        }
    }
}
