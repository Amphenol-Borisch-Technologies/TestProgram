using System;
using System.Collections.Generic;
using ABTTestLibrary;
using ABTTestLibrary.Config;
using ABTTestLibrary.Instruments;
using ABTTestLibrary.Instruments.Keysight;
using ABTTestLibrary.TestSupport;

// Place all Test methods, convenience methods, classes & comments exclusive to Group T-20 in this file.
// Do not place them in any other file, as Tests should be unique to a Program the and methods & classes
// must be unique within a namespace.
// - Examples:
//      If T01 belongs to both Groups T-10 & T-20, place it in ABTTestProgram.Shared.cs.
//      If T02 belongs exclusively to Group T-10, place it in ABTTestProgram.T10.cs.
//      If T03 belongs exclusively to Group T-20, place it in this file.
//      RunTestMethod is common to both Groups T-10 & T-20, so is placed in ABTTestProgram.Shared.cs.
namespace ABTTestProgram {
    internal sealed partial class ABTTests {
        private static (Int32 U6, Int32 U7) _CRCsPostCalibration = (U6: 0xABCD, U7: 0xEF01);

        private static (Int32 U6, Int32 U7) GetCRCsPostCalibration() {
            return _CRCsPostCalibration;
        }

        internal static String T08(Test test, Dictionary<String, Instrument> instruments, ABTForm abtForm) {
            if (GetCRCsPreCalibration() != _CRCsPreCalibration) return EventCodes.FAIL;
            // Program calibration info into Flash.  Implementation unspecified :-)
            if (GetCRCsPostCalibration() == _CRCsPostCalibration) return EventCodes.FAIL;
            // Deliberate FAIL, to demonstrate failing Test run.
            else return EventCodes.PASS;
        }
    }
}
