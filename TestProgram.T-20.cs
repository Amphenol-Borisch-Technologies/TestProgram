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
namespace TestProgram {
    internal sealed partial class TestProgramTests {

        internal static String T08(Test test, TestExecutor testExecutor) {
            return $"0x{0xABCD:X4}";
        }

        internal static String T09(Test test, TestExecutor testExecutor) {
            TestCustomizable testCustomizable = (TestCustomizable)test.ClassObject;
            if (String.Equals(testCustomizable.Arguments["CalibrationConstant"], "ϕ", StringComparison.Ordinal)) test.Result = EventCodes.PASS;
            else test.Result = EventCodes.FAIL;
            return "ϕ";
        }

        internal static String T11(Test test, TestExecutor testExecutor) {
            TestTextual testTextual = (TestTextual)test.ClassObject;
            return testTextual.Text;
        }
    }
}
