using System;
using TestLibrary;
using TestLibrary.Config;
using TestLibrary.Instruments;
using TestLibrary.Instruments.Keysight;
using TestLibrary.TestSupport;

// Place all Test methods, convenience methods, classes & comments exclusive to Group T-20 in this file.
// Do not place them in any other file, as Tests should be unique to a Program the and methods & classes
// must be unique within a namespace.
// - Examples:
//      If T01 belongs to both Groups T-10 & T-20, place it in TestProgram.Shared.cs.
//      If T02 belongs exclusively to Group T-10, place it in TestProgram.T10.cs.
//      If T03 belongs exclusively to Group T-20, place it in this file.
//      RunTestMethod is common to both Groups T-10 & T-20, so is placed in TestProgram.Shared.cs.
//
namespace TestExecutor {
    internal sealed partial class TestProgramTests {

        internal static String T08() {
            return $"0x{0xABCD:X4}";
        }

        internal static String T09() {
            TestCustomizable testCustomizable = (TestCustomizable)TestExecutor.Instance.ConfigTest.Tests[TestExecutor.TestID].ClassObject;
            if (String.Equals(testCustomizable.Arguments["CalibrationConstant"], "ϕ", StringComparison.Ordinal)) TestExecutor.Instance.ConfigTest.Tests[TestExecutor.TestID].Result = EventCodes.PASS;
            else TestExecutor.Instance.ConfigTest.Tests[TestExecutor.TestID].Result = EventCodes.FAIL;
            return "ϕ";
        }

        internal static String T11() {
            TestTextual testTextual = (TestTextual)TestExecutor.Instance.ConfigTest.Tests[TestExecutor.TestID].ClassObject;
            return testTextual.Text;
        }
    }
}
