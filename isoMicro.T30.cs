using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ABTTestLibrary;
using ABTTestLibrary.Config;
using ABTTestLibrary.Instruments;
using ABTTestLibrary.Instruments.Keysight;
using ABTTestLibrary.TestSupport;

// Place all Test methods, convenience methods, classes & comments solely exclusive to Group T30 in this file.
// Do not place them in any other file, as Tests should be unique to a Program the and methods & classes
// must be unique within a namespace.
// - Examples:
//      If Test01 belongs to both Groups T30 & T50, place it in isoMicro.Shared.cs.
//      If Test02 belongs exclusively to Group T30, place it in this file.
//      If Test03 belongs to both Groups T50, GroupCustom1 & GroupCustom2, place it in isoMicro.Shared.cs.
//      RunTestMethod is common to both Groups T30 & T50, so is placed in isoMicro.Shared.cs.
namespace isoMicro {
    internal sealed partial class isoMicroTests {
        internal static String P00100(Test test, Dictionary<String, Instrument> instruments) {
            EnableNLowHigh();
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
            if (_dialogResult == DialogResult.Cancel) throw new TestAbortException("Operator cancelled secondary side PICkit 4 pre-program, aborting.");
            P00200(test, instruments);
            // TODO: launch MPLAB IPE programmatically, with a script.

            // If IPE returns unsuccessfully, throw an TestAbortException();
            // If not, close MPLAB IPE programmatically.
            E3610xB.Off(instruments[Instrument.POWER_SECONDARY]);
            _dialogResult = MessageBox.Show($"Disconnect PICkit 4 IC programmer from isoMicro UUT.{Environment.NewLine}{Environment.NewLine}" +
                $"Press OK when completed, Cancel to cancel.", "Disconnect PICkit 4", MessageBoxButtons.OKCancel);
            if (_dialogResult == DialogResult.Cancel) throw new TestAbortException("Operator cancelled secondary side PICkit 4 post-program, aborting.");
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
            if (_dialogResult == DialogResult.Cancel) throw new TestAbortException("Operator cancelled primary side PICkit 4 bootloading pre-program, aborting.");
            P00200(test, instruments);
            // TODO: launch MPLAB IPE programmatically, with a script.

            // If IPE returns unsuccessfully, throw an TestAbortException();
            // If not, close MPLAB IPE programmatically.
            E3610xB.Off(instruments[Instrument.POWER_SECONDARY]);
            _dialogResult = MessageBox.Show($"Disconnect PICkit 4 IC programmer from isoMicro UUT.{Environment.NewLine}{Environment.NewLine}" +
                $"Press OK when completed, Cancel to cancel.", "Disconnect PICkit 4", MessageBoxButtons.OKCancel);
            if (_dialogResult == DialogResult.Cancel) throw new TestAbortException("Operator cancelled primary side PICkit 4 bootloading post-program, aborting.");
            return P00200(test, instruments);
        }
        internal static String P00401(Test test, Dictionary<String, Instrument> instruments) {
            return P00201(test, instruments);
        }
    }
}
