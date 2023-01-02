using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ABTTestLibrary;
using ABTTestLibrary.Config;
using ABTTestLibrary.Instruments;

//  Test Procedure requires a voltage ramp up between 1V/µSecond through 0.5V/mSecond
//   for its VIN, Primary & Secondary Bias power supplies, powered respectively to 12.5, 6.25 & 6.25 V.
// - The VIN power supply is a Keysight E36234A, which specifies 50 milliSecond up/down programming settling to
//   within 1% of total excursion.
// - The Primary & Secondary Bias power supplies are both Keysight E36103Bs, which specify 50 milliSecond
//   up/down programming settling to within 1% of total excursion.
// - Doing the arithmetic:
//   - 12.5V ÷ 0.050S = 0.252V/mS.
//   - 6.25V ÷ 0.050S = 0.125V/mS.
// - 0.252V/mS and 0.125V/mS both lie within required voltage ramp up rate between 1V/µSecond through 0.5V/mSecond.
namespace isoMicro {
    internal static class T50 {
        private static DialogResult _dialogResult;

        internal static String P10300(Test test, Dictionary<String, Instrument> instruments) {
            return "6.25";
        }

        internal static String P10301(Test test, Dictionary<String, Instrument> instruments) {
            return "0.5";
        }
    }
}
