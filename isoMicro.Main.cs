using System;
using System.Windows.Forms;

namespace isoMicro {
    internal class isoMicroMain {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new isoMicroForm());
        }
    }
}
