using System;
using System.Collections.Generic;
using System.Threading;
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

        internal static String T08(String testID, TestExecutor testExecutor) {
            return $"0x{0xABCD:X4}";
        }

        internal static String T09(String testID, TestExecutor testExecutor) {
            TestCustomizable testCustomizable = (TestCustomizable)testExecutor.ConfigTest.Tests[testID].ClassObject;
            if (String.Equals(testCustomizable.Arguments["CalibrationConstant"], "ϕ", StringComparison.Ordinal)) testExecutor.ConfigTest.Tests[testID].Result = EventCodes.PASS;
            else testExecutor.ConfigTest.Tests[testID].Result = EventCodes.FAIL;
            return "ϕ";
        }

        internal static String T11(String testID, TestExecutor testExecutor) {
            TestTextual testTextual = (TestTextual)testExecutor.ConfigTest.Tests[testID].ClassObject;
            return testTextual.Text;
        }
    }
}
