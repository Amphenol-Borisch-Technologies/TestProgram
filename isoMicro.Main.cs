using System;
using System.Reflection;
using System.Windows.Forms;

namespace isoMicro {
    internal partial class isoMicro {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AssemblyName an = Assembly.GetExecutingAssembly().GetName();
            Application.Run(new isoMicroForm(an.Name, an.Version.ToString()));
        }
    }
}
