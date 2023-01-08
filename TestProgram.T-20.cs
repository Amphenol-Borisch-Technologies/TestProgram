using System;
using System.Collections.Generic;
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
namespace TestProgram {
    internal sealed partial class TestProgramTests {
        private static Int32 _U6_CRC_PostCalibration = 0xABCD;

        private static Int32 GetU6_CRC_PostCalibration() {
            return _U6_CRC_PostCalibration;
        }

        internal static String T08(Test test, Dictionary<String, Instrument> instruments) {
            if (GetU6_CRC_PreCalibration() != _U6_CRC_PreCalibration) return EventCodes.FAIL;
            // Program calibration info into Flash.  Implementation unspecified :-)
            if (GetU6_CRC_PostCalibration() == _U6_CRC_PostCalibration) return EventCodes.FAIL;
            // Deliberate FAIL, to demonstrate failing Test run.
            else return EventCodes.PASS;
        }
    }
}
