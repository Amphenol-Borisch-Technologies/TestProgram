using System;
using System.Collections.Generic;
using ABTTestLibrary;
using ABTTestLibrary.Config;
using ABTTestLibrary.Instruments;

namespace isoMicro {
    internal class isoMicroForm : TestForm {
        internal isoMicroForm() : base() { }

        protected override String RunTest(Test test, Dictionary<String, Instrument> instruments) {
            // https://stackoverflow.com/questions/540066/calling-a-function-from-a-string-in-c-sharp
            // https://www.codeproject.com/Articles/19911/Dynamically-Invoke-A-Method-Given-Strings-with-Met
            // Override TestForm's abstract RunTest() method.  Necessary because implementing RunTest()
            // in ABTTestLibrary's RunTest() method would necessiate in having a reference to this
            // client Test project, and we don't want that.
            //  - We instead want this client Test project to reference the ABTTEstLibray, and ABTTestLibary
            //    to be blissfully ignorant of this client Test project.
            //  - Use Reflection's MethodInfo.Invoke to invoke the correct test method.
            //  - This is performed in the isoMicro.Shared.cs file, in partial class IsoMicroTests,
            //    method RunTestMethod.
            //  - It's not performed here because this TestForm class would then have to be extended
            //    to the multiple Tests files comprising partial class isoMicroTests, which would
            //    not be pretty.
            return isoMicroTests.RunTestMethod(test, instruments);
        }
    }
}
