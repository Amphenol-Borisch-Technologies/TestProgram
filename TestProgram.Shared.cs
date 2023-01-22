using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        private static (String standardError, String standardOutput) 
            ISP(String ispProgrammer, String uutConnector, Test test, Dictionary<String, Instrument> instruments, Func<Boolean> PowerISPMethod) {
            InstrumentTasks.SCPI99Reset(instruments); // PowerOff Method.
            _ = MessageBox.Show($"UUT now unpowered.{Environment.NewLine}{Environment.NewLine}" +
                                $"Connect '{ispProgrammer}' to UUT '{uutConnector}'.{Environment.NewLine}{Environment.NewLine}" +
                                $"AFTER connecting, click OK to re-power.", $"Connect '{uutConnector}'", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (!PowerISPMethod()) throw new TestCancellationException();
            TestISP tisp = (TestISP)test.ClassObject;
            String standardError, standardOutput;
            using (Process process = new Process()) {
                ProcessStartInfo psi = new ProcessStartInfo {
                    Arguments = tisp.ISPExecutableArguments,
                    FileName = tisp.ISPExecutable,
                    WorkingDirectory = tisp.ISPExecutableFolder,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                };
                process.StartInfo = psi;
                process.Start();
                StreamReader se = process.StandardError;
                standardError = se.ReadToEnd().Trim();
                StreamReader so = process.StandardOutput;
                standardOutput = so.ReadToEnd().Trim();
            }
            InstrumentTasks.SCPI99Reset(instruments); // PowerOff Method.
            _ = MessageBox.Show($"UUT now unpowered.{Environment.NewLine}{Environment.NewLine}" +
                                $"Disconnect '{ispProgrammer}' from UUT '{uutConnector}'.{Environment.NewLine}{Environment.NewLine}" +
                                $"AFTER disconnecting, click OK to re-power.", $"Disconnect '{uutConnector}'", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (!PowerISPMethod()) throw new TestCancellationException();
            return (standardError, standardOutput);
        }

        private static Boolean PowerISPMethod() {
            // Simulates method to power UUT for ISP.  Returns true for success, false for failure.
            return true;
        }

        internal static String T00(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            TestNumerical tn = (TestNumerical)test.ClassObject;
            return (tn.Low * double.PositiveInfinity).ToString();
        }

        internal static String T01(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            Random r = new Random();
            Int32 i = r.Next(460, 540); // Random Int32 between 460 & 540.
            Double d = Convert.ToDouble(i) / 100.0; // Random Double between 4.6 & 5.4; 3 in 8 (37.5%) chance of failing.  Flaky 5VDC power supply!
            String s = d.ToString();
            TestNumerical tn = (TestNumerical)test.ClassObject;
            if ((tn.Low <= d) && (d <= tn.High)) return s;
            // Simulates 5VDC power bus passing.
            else throw new TestCancellationException(s);
            // Simulates 5VDC power bus failing, so cancel test execution, returning measured value for Logging.
        }

        internal static String T02(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            return "3.29";
        }

        internal static String T03(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            MessageBox.Show($"The next Test, '{test.ID}', executes for 8 seconds, permitting Cancellation or Emergency Stopping if desired.{Environment.NewLine}{Environment.NewLine}"
                + $"It implements proactive Cancellation via Microsoft's CancellationToken.{Environment.NewLine}{Environment.NewLine}"
                + $"Note that Cancellation occurs immediately, interrupting Test '{test.ID}'.{Environment.NewLine}{Environment.NewLine}"
                + $"Note also that Measurement = 'NaN' because developer doesn't explicitly assign it a value.",
                "Cancel or Emergency Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            for (Int32 i = 0; i < 100; i++) {
                Thread.Sleep(50); // Sleep so Cancel or Emergency Stop buttons can be tested.
                Application.DoEvents();
                if (cancellationToken.IsCancellationRequested) throw new TestCancellationException();
                // Above implements Microsoft's proactive CancellationTokenSource technique, in one of multiple fashions,
                // which aborts the currently executing Test if Test Operator cancels.
                // Multiple Cancellation methods detailed at https://learn.microsoft.com/en-us/dotnet/standard/threading/cancellation-in-managed-threads.
            }
            return "0.9";
        }

        internal static String T04(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            return "2.5";
        }

        internal static String T05(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            MessageBox.Show($"The next Test, '{test.ID}', also executes for 8 seconds, again permitting Cancellation or Emergency Stopping if desired.{Environment.NewLine}{Environment.NewLine}"
                + $"It *does not* implement proactive Cancellation via Microsoft's CancellationToken.{Environment.NewLine}{Environment.NewLine}"
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
            return "1.0001E7";
        }
    }
}
