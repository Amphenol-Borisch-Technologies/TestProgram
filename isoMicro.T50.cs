using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ABTTestLibrary;
using ABTTestLibrary.Config;
using ABTTestLibrary.Instruments;
using ABTTestLibrary.Instruments.Keysight;
using ABTTestLibrary.TestSupport;

// Place all Test methods, convenience methods & classes solely exclusive to Group T50 in this file.
// Do not place them in any other file, as Tests should be unique to a Program the and methods & classes must be unique within a namespace.
// - Examples:
//      If Test01 belongs to both Groups T30 & T50, place it in IsoMicro.Shared.cs.
//      If Test02 belongs exclusively to Group T50, place it in this file.
//      If Test03 belongs to both Groups T50, GroupCustom1 & GroupCustom2, place it in IsoMicro.Shared.cs.
//      RunTestMethod is common to both Groups T30 & T50, so is placed in IsoMicro.Shared.cs.
namespace isoMicro {
    internal sealed partial class isoMicroTests {
        internal static String P10300(Test test, Dictionary<String, Instrument> instruments) {
            return "6.25";
        }

        internal static String P10301(Test test, Dictionary<String, Instrument> instruments) {
            return "0.5";
        }
    }
}
