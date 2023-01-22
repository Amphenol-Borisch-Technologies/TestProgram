using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using TestLibrary;
using TestLibrary.Config;
using TestLibrary.Instruments;

namespace TestProgram {
    internal sealed class TestProgramForm : TestLibraryForm {
        internal TestProgramForm() : base(Properties.Resources.Amphenol) {
            // NOTE: Change base constructor's Icon as applicable, depending on customer.
            // https://stackoverflow.com/questions/40933304/how-to-create-an-icon-for-visual-studio-with-just-mspaint-and-visual-studio
        }

        protected override String RunTest(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            // NOTE: Override TestForm's abstract RunTest() method.  Necessary because implementing RunTest()
            // in TestLibrary's RunTest() method would necessiate in having a reference to this
            // client Test project, and we don't want that.
            Type type = Type.GetType("TestProgram.TestProgramTests");
            MethodInfo methodInfo = type.GetMethod(test.ID, BindingFlags.Static | BindingFlags.NonPublic);
            return (String)methodInfo.Invoke(null, new object[] { test, instruments, cancellationToken });
        }
    }
}
