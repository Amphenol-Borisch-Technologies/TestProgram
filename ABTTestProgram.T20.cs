using System;
using System.Collections.Generic;
using ABTTestLibrary;
using ABTTestLibrary.Config;
using ABTTestLibrary.Instruments;
using ABTTestLibrary.Instruments.Keysight;
using ABTTestLibrary.TestSupport;

// Place all Test methods, convenience methods, classes & comments solely exclusive to Group T20 in this file.
// Do not place them in any other file, as Tests should be unique to a Program the and methods & classes
// must be unique within a namespace.
// - Examples:
//      If Test01 belongs to both Groups T10 & T20, place it in ABTTestProgram.Shared.cs.
//      If Test02 belongs exclusively to Group T20, place it in this file.
//      If Test03 belongs to both Groups T20, GroupCustom1 & GroupCustom2, place it in ABTTestProgram.Shared.cs.
//      RunTestMethod is common to both Groups T10 & T20, so is placed in ABTTestProgram.Shared.cs.
namespace ABTTestProgram {
    internal sealed partial class ABTTests {
        private static (Int32 U6, Int32 U7) _CRCsPostCalibration = (U6: 0xABCD, U7: 0xEF01);

        private static (Int32 U6, Int32 U7) GetCRCsPostCalibration() {
            return _CRCsPostCalibration;
        }

        internal static String T8(Test test, Dictionary<String, Instrument> instruments, ABTForm abtForm) {
            if (GetCRCsPreCalibration() != _CRCsPreCalibration) return EventCodes.FAIL;
            // Program calibration info into Flash.  Implementation unspecified :-)
            if (GetCRCsPostCalibration() == _CRCsPostCalibration) return EventCodes.FAIL;
            // Deliberate FAIL, to demonstrate failing Test run.
            else return EventCodes.PASS;
        }
    }
}
