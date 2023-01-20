using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        internal static String T08(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            Int32 _U6_CRC_PostCalibration = 0xABCD;
            return $"0x{_U6_CRC_PostCalibration:X4}";
        }

        internal static String T09(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            TestCustomizable tc = (TestCustomizable)test.ClassObject;
            if (String.Equals(tc.Arguments["CalibrationConstant"], "ϕ", StringComparison.Ordinal)) return EventCodes.PASS;
            else return EventCodes.FAIL;
        }

        internal static String T10(Test test, Dictionary<String, Instrument> instruments, CancellationToken cancellationToken) {
            TestTextual tt = (TestTextual)test.ClassObject;
            return tt.Text.ToUpper();
        }
    }
}
