using System;
using System.Windows.Forms;

namespace isoMicro {
    internal partial class isoMicro {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new isoMicroForm());
        }
    }
}
