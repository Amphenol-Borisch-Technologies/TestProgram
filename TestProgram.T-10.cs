using System;
using TestLibrary;
using TestLibrary.Config;
using TestLibrary.Instruments;
using TestLibrary.Instruments.Keysight;
using TestLibrary.TestSupport;

// Place all Test methods, convenience methods, classes & comments exclusive to Group T-10 in this file.
// Do not place them in any other file, as Tests should be unique to a Program the and methods & classes
// must be unique within a namespace.
// - Examples:
//      If T01 belongs to both Groups T-10 & T-20, place it in TestProgram.Shared.cs.
//      If T02 belongs exclusively to Group T-10, place it in this file.
//      If T03 belongs exclusively to Group T-20, place it in TestProgram.T20.cs
//      RunTestMethod is common to both Groups T-10 & T-20, so is placed in TestProgram.Shared.cs.
//
namespace TestExecutor {
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
