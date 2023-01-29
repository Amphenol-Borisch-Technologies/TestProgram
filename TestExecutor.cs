using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TestLibrary;
using TestLibrary.Config;
using TestLibrary.Instruments;

namespace TestProgram {
    internal sealed class TestExecutor : TestExecutive {
        internal TestExecutor() : base(Properties.Resources.Amphenol) {
            // NOTE: Change base constructor's Icon as applicable, depending on customer.
            // https://stackoverflow.com/questions/40933304/how-to-create-an-icon-for-visual-studio-with-just-mspaint-and-visual-studio
        }

        protected override async Task<String> RunTestAsync(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            // NOTE: Override TestForm's abstract RunTestAsync() method.
            // Implementing RunTestAsync() in TestLibrary's RunTestAsync() method would necessiate
            // having a reference to this client Test project, and we don't want that.
            Type type = Type.GetType("TestProgram.TestProgramTests");
            MethodInfo methodInfo = type.GetMethod(test.ID, BindingFlags.Static | BindingFlags.NonPublic);
            Object o = await Task.Run(() => methodInfo.Invoke(null, new object[] { test, instruments, cancellationToken }));
            return (String)o;
            // return (String)methodInfo.Invoke(null, new object[] { test, instruments, cancellationToken });
        }
    }
}
