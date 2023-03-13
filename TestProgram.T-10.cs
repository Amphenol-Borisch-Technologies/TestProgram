using System;
using TestLibrary;
using TestLibrary.Config;
using TestLibrary.Instruments;
using TestLibrary.Instruments.Keysight;
using TestLibrary.TestSupport;

// Place all Test methods, convenience methods, classes & comments exclusive to Group T-10 in this file.
namespace TestProgram {
    internal sealed partial class TestProgramTests {

        internal static String T07() {
            String ExitCode = TestTasks.ISP_ExitCode("MPLAB PICkit 4 In-Circuit Debugger", "J11", TestExecutor.Instance.ConfigTest.Tests[TestExecutor.TestID], TestExecutor.Instance.Instruments, PowerISPMethod);
            return $"0x{0x050C:X4}";
        }

        internal static String T10() {
            TestTextual testTextual = (TestTextual)TestExecutor.Instance.ConfigTest.Tests[TestExecutor.TestID].ClassObject;
            return testTextual.Text.ToLower();
        }
    }
}
