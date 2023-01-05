using System;
using System.Collections.Generic;
using ABTTestLibrary;
using ABTTestLibrary.Config;
using ABTTestLibrary.Instruments;
using ABTTestLibrary.Instruments.Keysight;
using ABTTestLibrary.TestSupport;

// Place all Test methods, convenience methods, classes & comments solely exclusive to Group T10 in this file.
// Do not place them in any other file, as Tests should be unique to a Program the and methods & classes
// must be unique within a namespace.
// - Examples:
//      If Test01 belongs to both Groups T10 & T20, place it in ABTTestProgram.Shared.cs.
//      If Test02 belongs exclusively to Group T10, place it in this file.
//      If Test03 belongs to both Groups T20, GroupCustom1 & GroupCustom2, place it in ABTTestProgram.Shared.cs.
//      RunTestMethod is common to both Groups T10 & T20, so is placed in ABTTestProgram.Shared.cs.
namespace ABTTestProgram {
    internal sealed partial class ABTTests {
        internal static String T7(Test test, Dictionary<String, Instrument> instruments) {
            // Implementation unspecified :-)
            if (GetCRCsPreCalibration() == _CRCsPreCalibration) return EventCodes.PASS;
            else return EventCodes.FAIL;
        }
    }
}
