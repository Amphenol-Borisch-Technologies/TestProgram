using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ABTTestLibrary;
using ABTTestLibrary.Config;
using ABTTestLibrary.Instruments;
using ABTTestLibrary.Instruments.Keysight;
using ABTTestLibrary.TestSupport;
using isoMicro;

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
    internal static class T30 {
        private static DialogResult _dialogResult;

        internal static String P00100(Test test, Dictionary<String, Instrument> instruments) {
            isoMicroForm.EnableNLowHigh();
            (Double _, Double A) = E3610xB.MeasureVA(instruments[Instrument.POWER_PRIMARY]);
            E3610xB.Off(instruments[Instrument.POWER_PRIMARY]);
            return A.ToString();
        }
        internal static String P00101(Test test, Dictionary<String, Instrument> instruments) {
            (Double V, Double _) = E3610xB.MeasureVA(instruments[Instrument.POWER_PRIMARY]);
            return V.ToString();
        }

        internal static String P00200(Test test, Dictionary<String, Instrument> instruments) {
            (Double _, Double A) = E3610xB.MeasureVA(instruments[Instrument.POWER_SECONDARY]);
            E3610xB.Off(instruments[Instrument.POWER_SECONDARY]);
            return A.ToString();
        }
        internal static String P00201(Test test, Dictionary<String, Instrument> instruments) {
            E3610xB.ON(instruments[Instrument.POWER_SECONDARY], Volts: 3.3, Amps: 0.210, SettlingDelayMS: 150);
            (Double V, Double _) = E3610xB.MeasureVA(instruments[Instrument.POWER_SECONDARY]);
            return V.ToString();
        }

        internal static String P00300(Test test, Dictionary<String, Instrument> instruments) {
            InstrumentTasks.Reset(instruments);
            _dialogResult = MessageBox.Show($"Connect PICkit 4 IC programmer to isoMicro UUT per Table 7.{Environment.NewLine}{Environment.NewLine}" +
                $"Press OK when completed, Cancel to cancel.", "Connect PICkit 4", MessageBoxButtons.OKCancel);
            // TODO: State which reference designator what Table 7's connector is.
            if (_dialogResult == DialogResult.Cancel) throw new ABTAbortException("Operator cancelled secondary side PICkit 4 pre-program, aborting.");
            P00200(test, instruments);
            // TODO: launch MPLAB IPE programmatically, with a script.

            // If IPE returns unsuccessfully, throw an ABTAbortException();
            // If not, close MPLAB IPE programmatically.
            E3610xB.Off(instruments[Instrument.POWER_SECONDARY]);
            _dialogResult = MessageBox.Show($"Disconnect PICkit 4 IC programmer from isoMicro UUT.{Environment.NewLine}{Environment.NewLine}" +
                $"Press OK when completed, Cancel to cancel.", "Disconnect PICkit 4", MessageBoxButtons.OKCancel);
            if (_dialogResult == DialogResult.Cancel) throw new ABTAbortException("Operator cancelled secondary side PICkit 4 post-program, aborting.");
            return P00200(test, instruments);
        }
        internal static String P00301(Test test, Dictionary<String, Instrument> instruments) {
            return P00201(test, instruments);
        }
        
        internal static String P00400(Test test, Dictionary<String, Instrument> instruments) {
            InstrumentTasks.Reset(instruments);
            _dialogResult = MessageBox.Show($"Connect PICkit 4 IC programmer to isoMicro UUT per Table 9.{Environment.NewLine}{Environment.NewLine}" +
                $"Press OK when completed, Cancel to cancel.", "Connect PICkit 4", MessageBoxButtons.OKCancel);
            // TODO: State which reference designator what Table 9's connector is.
            if (_dialogResult == DialogResult.Cancel) throw new ABTAbortException("Operator cancelled primary side PICkit 4 bootloading pre-program, aborting.");
            P00200(test, instruments);
            // TODO: launch MPLAB IPE programmatically, with a script.

            // If IPE returns unsuccessfully, throw an ABTAbortException();
            // If not, close MPLAB IPE programmatically.
            E3610xB.Off(instruments[Instrument.POWER_SECONDARY]);
            _dialogResult = MessageBox.Show($"Disconnect PICkit 4 IC programmer from isoMicro UUT.{Environment.NewLine}{Environment.NewLine}" +
                $"Press OK when completed, Cancel to cancel.", "Disconnect PICkit 4", MessageBoxButtons.OKCancel);
            if (_dialogResult == DialogResult.Cancel) throw new ABTAbortException("Operator cancelled primary side PICkit 4 bootloading post-program, aborting.");
            return P00200(test, instruments);
        }
        internal static String P00401(Test test, Dictionary<String, Instrument> instruments) {
            return P00201(test, instruments);
        }
    }
}
