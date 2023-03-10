using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestLibrary;

namespace TestExecutor {
    internal class TestExecutorMain {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(TestExecutor.Instance);
        }
    }

    internal sealed class TestExecutor : TestExecutive {
        internal static String TestID { get; private set; }
        private static readonly TestExecutor instance = new TestExecutor();
        static TestExecutor() { }
        // Singleton pattern requires explicit static constructor to tell C# compiler not to mark type as beforefieldinit.
        // https://csharpindepth.com/articles/singleton
        private TestExecutor() : base(Properties.Resources.Amphenol) { }
        // NOTE: Change base constructor's Icon as applicable, depending on customer.
        // https://stackoverflow.com/questions/40933304/how-to-create-an-icon-for-visual-studio-with-just-mspaint-and-visual-studio
        internal static TestExecutor Instance { get { return instance; } }

        protected override async Task<String> RunTestAsync(String testID) {
            // NOTE: Override abstract TestExecutive.RunTestAsync() method.
            // Implementing RunTestAsync() in TestExecutive.RunTestAsync() necessiates a reference to this client Test project in TestExecutive, and we don't want that.
            TestID = testID;
            Type type = Type.GetType("TestExecutor.IsoMicroTests");
            MethodInfo methodInfo = type.GetMethod(TestExecutor.TestID, BindingFlags.Static | BindingFlags.NonPublic);
            Object o = await Task.Run(() => methodInfo.Invoke(null, null));
            return (String)o;
        }
    }
}
