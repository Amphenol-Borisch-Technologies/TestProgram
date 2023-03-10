using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using TestLibrary;
using TestLibrary.Config;
using TestLibrary.Instruments;
using TestLibrary.Instruments.Keysight;
using TestLibrary.TestSupport;

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
// NOTE: Two types of TestLibrary/Operator initiated cancellations possible, proactive & reactive:
//  1)  Proactive:
//      - Microsoft's recommended CancellationTokenSource technique, which can proactively
//        cancel currently executing Test, *if* implemented.
//      - Implementation is the Test Developer's responsibility.
//      - Implementation necessary if the *currently* executing Test must be cancellable during
//        execution.
//  2)  Reactive:
//      - TestLibrary's already implemented/always available & default reactive "Cancel before next Test" technique,
//        which simply sets this._cancelled Boolean to true, checked at the end of RunTest()'s foreach loop.
//      - If this._cancelled is true, RunTest()'s foreach loop is broken, causing reactive cancellation
//        prior to the next Test's execution.
//      - Note: This doesn't proactively cancel the *currently* executing Test, which runs to completion.
//  Summary:
//      - If it's necessary to deterministically cancel a specific Test's execution, Microsoft's
//        CancellationTokenSource technique *must* be implemented by the Test Developer.
//      - If it's only necessary to deterministically cancel overall Test Program execution,
//        TestLibrary's basic "Cancel before next Test" technique is already available without
//        any Test Developer implemenation needed.
//      - Note: Some Test's may not be safely cancellable mid-execution.
//          - For such, simply don't implement Microsoft's CancellationTokenSource technique.
//  https://learn.microsoft.com/en-us/dotnet/standard/threading/cancellation-in-managed-threads
//  https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/task-cancellation
//  https://learn.microsoft.com/en-us/dotnet/standard/threading/canceling-threads-cooperatively
//
//  NOTE: TestProgram/Test Developer initiated cancellations also possible:
//      - Any TestProgram's Test can initiate a cancellation programmatically by simply
//        throwing a TestCancellationException:
//        - Let's say we want to abort if specific conditions occur in a Test, for example if
//          power application fails.
//        - We don't want to continue testing if the UUT's applied power busses fail,
//          because any downstream failures are likely due to the UUT not being powered
//          correctly.
//        - So, simply throw a TestCancellationException if an applied power bus fails.
//        - This is simulated in T01 in https://github.com/Amphenol-Borisch-Technologies/TestProgram/blob/master/TestProgram.Shared.cs
//        - Test Developer must set TestCancellationException's message to Measeured
//          value for it to be Logged, else default String.Empty or Double.NaN values are Logged.
//
namespace TestProgram {
    internal sealed partial class TestProgramTests {
        static TestProgramTests() { }

        private static void PowerISPMethod() {
            // Simulates method to power UUT for ISP.  Returns true for success, false for failure.
        }

        internal static String T00() {
            TestNumerical testNumerical = (TestNumerical)TestExecutor.Instance.ConfigTest.Tests[TestExecutor.TestID].ClassObject;
            return (testNumerical.Low * double.PositiveInfinity).ToString();
        }

        internal static String T01() {
            Random r = new Random();
            Int32 i = r.Next(460, 540); // Random Int32 between 460 & 540.
            Double d = Convert.ToDouble(i) / 100.0; // Random Double between 4.6 & 5.4; 3 in 8 (37.5%) chance of failing.  Flaky 5VDC power supply!
            String s = d.ToString();
            TestNumerical testNumerical = (TestNumerical)TestExecutor.Instance.ConfigTest.Tests[TestExecutor.TestID].ClassObject;
            if ((testNumerical.Low <= d) && (d <= testNumerical.High)) return s;
            // Simulates 5VDC power bus passing.
            else throw new TestCancellationException(s);
            // Simulates 5VDC power bus failing, so cancel test execution, returning measured value for Logging.
        }

        internal static String T02() {
            return "3.29";
        }

        internal static String T03() {
            _ = MessageBox.Show($"The next Test, '{TestExecutor.TestID}', executes for 8 seconds, permitting Cancellation or Emergency Stopping if desired.{Environment.NewLine}{Environment.NewLine}"
                + $"It implements proactive Cancellation via Microsoft's CancellationToken.{Environment.NewLine}{Environment.NewLine}"
                + $"Note that Cancellation occurs immediately, interrupting Test '{TestExecutor.TestID}'.{Environment.NewLine}{Environment.NewLine}"
                + $"Note also that Measurement = 'NaN' because developer doesn't explicitly assign it a value.",
                "Cancel or Emergency Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            for (Int32 i = 0; i < 100; i++) {
                Thread.Sleep(50);
                if (TestExecutor.Instance.CancelTokenSource.IsCancellationRequested) throw new TestCancellationException();
                // Above implements Microsoft's proactive CancellationTokenSource technique, in one of multiple fashions,
                // which aborts the currently executing Test if Test Operator cancels.
                // Multiple Cancellation methods detailed at https://learn.microsoft.com/en-us/dotnet/standard/threading/cancellation-in-managed-threads.
            }
            return "0.9";
        }

        internal static String T04() {
            return "2.5";
        }

        internal static String T05() {
            _ = MessageBox.Show($"The next Test, '{TestExecutor.TestID}', also executes for 8 seconds, again permitting Cancellation or Emergency Stopping if desired.{Environment.NewLine}{Environment.NewLine}"
                + $"It *does not* implement proactive Cancellation via Microsoft's CancellationToken.{Environment.NewLine}{Environment.NewLine}"
                + $"Instead, it utilizes reactive Cancellation via TestLibrary's default 'Cancel before next Test' technique.{Environment.NewLine}{Environment.NewLine}"
                + $"Note that Cancellation *does not* occur immediately, and '{TestExecutor.TestID}' runs to completion.",
                "Cancel or Emergency Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            for (Int32 i = 0; i < 100; i++) {
                Thread.Sleep(50);
                //  Microsoft's proactive CancellationTokenSource technique not implemented, but TestLibrary's basic reactive
                //  "Cancel before next Test" is always operational.
            }
            return "1.75";
        }

        internal static String T06() {
            return "1.0001E7";
        }
    }
}
