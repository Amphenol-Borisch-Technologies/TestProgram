using System;
using System.Reflection;
using System.Threading.Tasks;
using TestLibrary;

namespace TestExecutor {
    internal sealed class TestExecutor : TestExecutive {
        internal TestExecutor() : base(Properties.Resources.Amphenol) {
            // NOTE: Change base constructor's Icon as applicable, depending on customer.
            // https://stackoverflow.com/questions/40933304/how-to-create-an-icon-for-visual-studio-with-just-mspaint-and-visual-studio
        }

        protected override async Task<String> RunTestAsync(String testID) {
            // NOTE: Override TestExecutive's abstract RunTestAsync() method.
            // Implementing RunTestAsync() in TestLibrary's RunTestAsync() method would necessiate
            // having a reference to this client Test project, and we don't want that.
            Type type = Type.GetType("TestProgram.TestProgramTests");
            MethodInfo methodInfo = type.GetMethod(testID, BindingFlags.Static | BindingFlags.NonPublic);
            Object o = await Task.Run(() => methodInfo.Invoke(null, new object[] { testID, this }));
            return (String)o;
        }
    }
}
