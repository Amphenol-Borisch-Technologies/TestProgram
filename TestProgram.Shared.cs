using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using TestLibrary;
using TestLibrary.Config;
using TestLibrary.Instruments;
using TestLibrary.TestSupport;

// NOTE: Place all Test methods, convenience methods, classes & comments applicable to multiple Groups in this file.
//  - Do not place them in any other file, as methods & classes must be unique within a namespace.
// NOTE: All Test Programs include a TestProgram.Shared.cs file, if only to contain shared RunTestMethod() and these Notes.
// NOTE: Test Developer is responsible for ensuring Tests can be both safely & correctly called in sequence defined in App.config:
//  - That is, if Tests execute sequentially as (T01, T02, T03, T04, T05), Test Developer is responsible for ensuring all equipment is
//    configured safely & correctly between each Test step.
//    - If:
//      - T01 is unpowered Shorts & Opens testing.
//      - T02 is powered voltage measurements.
//      - T03 begins with unpowered operator cable connections/disconnections for In-System Programming.
//    - Then Test Developer must ensure necessary equipment state transitions are implemented so test operator isn't
//      plugging/unplugging a powered UUT in T03.
//
//  References:
//  - https://github.com/Amphenol-Borisch-Technologies/TestLibrary
//  - https://github.com/Amphenol-Borisch-Technologies/TestProgram
//  - https://github.com/Amphenol-Borisch-Technologies/TestLibraryTests
//
namespace TestProgram {
    internal sealed partial class TestProgramTests {
        private static DialogResult _dialogResult;
        private static Type _type;
        private static MethodInfo _methodInfo;
        private static Int32 _U6_CRC_PreCalibration = 0x050C;
        static TestProgramTests() { }

        public static String RunTestMethod(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            // https://stackoverflow.com/questions/540066/calling-a-function-from-a-string-in-c-sharp
            // https://www.codeproject.com/Articles/19911/Dynamically-Invoke-A-Method-Given-Strings-with-Met
            // Indirectly override TestForm's abstract RunTest() method.  Necessary because implementing RunTest()
            // in TestLibrary's RunTest() method would necessiate in having a reference to this
            // client Test project, and we don't want that.
            // We instead want this client Test project to reference the TestLibrary, and TestLibrary
            // to be blissfully ignorant of this client Test project.
            // TODO: Obsolete this method, by invoking Test methods via Reflection across classes,
            // originating from TestProgram.Form.cs method RunTest() instead of delegating to this
            // RunTestMethod().  Thoughts below.
            // https://stackoverflow.com/questions/34523717/how-to-get-namespace-class-methods-and-its-arguments-with-reflection
            // https://stackoverflow.com/questions/79693/getting-all-types-in-a-namespace-via-reflection
            _type = typeof(TestProgramTests);
            _methodInfo = _type.GetMethod(test.ID, BindingFlags.Static | BindingFlags.NonPublic);
            return (String)_methodInfo.Invoke(null, new object[] { test, instruments, cancellationToken });
        }

        private static Int32 GetU6_CRC_PreCalibration() {
            return _U6_CRC_PreCalibration;
        }

        internal static String T00(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            // Implementation unspecified :-)
            return "63";
        }

        internal static String T01(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            // Implementation unspecified :-)
            return "5.12";
        }

        internal static String T02(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            // Implementation unspecified :-)
            return "3.29";
        }

        internal static String T03(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            // Implementation unspecified :-)
            MessageBox.Show($"The next Test, '{test.ID}', executes for 8 seconds, permitting Cancellation or Emergency Stopping if desired.{Environment.NewLine}{Environment.NewLine}"
                + $"It implements proactive Cancellation via Microsoft's CancellationToken.{Environment.NewLine}{Environment.NewLine}"
                + $"Note that Cancellation occurs immediately, interrupting Test '{test.ID}'.",
                "Cancel or Emergency Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            for (Int32 i = 0; i < 100; i++) {
                Thread.Sleep(50); // Sleep so Cancel or Emergency Stop buttons can be tested.
                Application.DoEvents();
                if (cancellationToken.IsCancellationRequested) throw new TestCancellationException($"Test '{test.ID}' Cancelled by operator request.");
                // Above implements Microsoft's proactive CancellationTokenSource technique, in one of multiple fashions,
                // which aborts the currently executing Test if Test Operator cancels.
                // Multiple Cancellation methods detailed at https://learn.microsoft.com/en-us/dotnet/standard/threading/cancellation-in-managed-threads.
            }
            return "0.9";
        }

        internal static String T04(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            // Implementation unspecified :-)
            return "2.5";
        }

        internal static String T05(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            // Implementation unspecified :-)
            MessageBox.Show($"The next Test, '{test.ID}', also executes for 8 seconds, again permitting Cancellation or Emergency Stopping if desired.{Environment.NewLine}{Environment.NewLine}"
                + $"It *does not* implement proactive Cancellation via Microsoft's CancellationToken{Environment.NewLine}{Environment.NewLine}."
                + $"Instead, it utilizes reactive Cancellation via TestLibrary's default 'Cancel before next Test' technique.{Environment.NewLine}{Environment.NewLine}"
                + $"Note that Cancellation *does not* occur immediately, and '{test.ID}' runs to completion.",
                "Cancel or Emergency Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            for (Int32 i = 0; i < 100; i++) {
                Thread.Sleep(50); // Sleep so Cancel or Emergency Stop buttons can be tested.
                Application.DoEvents();
                //  Microsoft's proactive CancellationTokenSource technique not implemented, but TestLibrary's basic reactive
                //  "Cancel before next Test" is always operational.
            }
            return "1.75";
        }

        internal static String T06(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            // Implementation unspecified :-)
            return "1.0001E7";
        }

        internal static String T09(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            // Implementation unspecified :-)
            return EventCodes.PASS; // UUT is happy!
        }
    }
}
