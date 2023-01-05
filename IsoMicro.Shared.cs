using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using ABTTestLibrary;
using ABTTestLibrary.Config;
using ABTTestLibrary.Instruments;
using ABTTestLibrary.TestSupport;
using TIDP.SAA; // https://www.ti.com/tool/FUSION_USB_ADAPTER_API/

// Place all Test methods, convenience methods & classes, comments applicable to multiple Groups in this file.
// Do not place them in any other file, as methods & classes must be unique within a namespace.
//
//  isoMicro's 0001624557, Draft A Test Specification requires a voltage ramp up between 1V/µSecond through 0.5V/mSecond
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
    internal sealed partial class isoMicroTests {
        private static DialogResult _dialogResult;
        private static Type _type;
        private static MethodInfo _methodInfo;

        static isoMicroTests() { }

        public static String RunTestMethod(Test test, Dictionary<String, Instrument> instruments) {
            // https://stackoverflow.com/questions/540066/calling-a-function-from-a-string-in-c-sharp
            // https://www.codeproject.com/Articles/19911/Dynamically-Invoke-A-Method-Given-Strings-with-Met
            // Indirectly override TestForm's abstract RunTest() method.  Necessary because implementing RunTest()
            // in ABTTestLibrary's RunTest() method would necessiate in having a reference to this
            // client Test project, and we don't want that.
            // We instead want this client Test project to reference the ABTTEstLibray, and ABTTestLibary
            // to be blissfully ignorant of this client Test project.
            // TODO: Obsolete this method, by invoking Test methods via Reflection across classes,
            // originating from isoMicro.Form.cs method RunTest() instead of delegating to this
            // RunTestMethod().  Thoughts below.
            // https://stackoverflow.com/questions/34523717/how-to-get-namespace-class-methods-and-its-arguments-with-reflection
            // https://stackoverflow.com/questions/79693/getting-all-types-in-a-namespace-via-reflection
            _type = typeof(isoMicroTests);
            _methodInfo = _type.GetMethod(test.ID, BindingFlags.Static | BindingFlags.NonPublic);
            return (String)_methodInfo.Invoke(null, new object[] { test, instruments });
        }

        internal static void EnableNLow() {
            // TODO: Assume EnableN signal driven by the Wave Generator connected through a USB-ERB24 relay.
            // KS33509B.ApplyDC(base.Instruments[Instrument.WAVE_GENERATOR], 3.3);
        }

        internal static void EnableNHigh() {
            // TODO: Assume EnableN signal driven by the Wave Generator connected through a USB-ERB24 relay.
            // KS33509B.SetOutputOff(base.Instruments[Instrument.WAVE_GENERATOR]);
        }

        internal static void EnableNLowHigh() {
            EnableNLow();
            EnableNHigh();
        }
    }

    internal static class SMBA {
        // NOTE: Update to .Net 7.0 & C# 11.0 if possible.  See 2nd Note below.
        // - Used .Net FrameWork 4.8 instead of .Net 7.0 because required Texas Instruments' TIDP.SAA Fusion Library targets
        //   .Net FrameWork 2.0, incompatible with .Net 7.0, C# 11.0 & UWP.
        // https://www.ti.com/tool/FUSION_USB_ADAPTER_API
        // NOTE: Microsoft supports I2C with their Windows.Devices.I2c & System.Device.I2c Namespaces:
        // - Conceivable above TI's TIDP.SAA Fusion Library could be replaced by these Namespaces, though they'd need
        //   to communicate with a GPIO adapter capable of interfacing to the customer's UUT.
        // - https://learn.microsoft.com/en-us/uwp/api/windows.devices.i2c?view=winrt-22621
        // - https://learn.microsoft.com/en-us/dotnet/api/system.device.i2c?view=iot-dotnet-latest
        // - https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/IoT-I2C
        // - Texas Instruments has 2 USB-TO-GPIO adapters, and there are others, so potentially doable.
        // - Migrating from TI's TIDP.SAA to Microsoft's Windows.Devices.I2c & System.Device.I2c would also
        //   allow migration from .Net FrameWork 4.8, C# 7.3 & WinForms to .Net 7.0, C# 11.0 & UWP.
        // - Would require OS to be Windows Enterprise IoT 10 or 11, but that's eminently doable.
        // - https://www.ti.com/tool/USB-TO-GPIO, © 2006
        // - https://www.ti.com/tool/USB-TO-GPIO2, © 2022
        internal static String SMBusAddress = System.Configuration.ConfigurationManager.AppSettings["CUSTOM_SMBusAddress"];
        internal static SMBusAdapter Get() {
            if (SMBusAdapter.Discover() == 0) throw new Exception("No 'USB Interface Adapter, Texas Instruments © 2006' found!");
            SMBusAdapter smba = SMBusAdapter.Adapter;
            smba.Set_Bus_Speed(SMBusAdapter.BusSpeed.Speed400KHz);
            smba.Set_PEC_Enabled(true);
            smba.Set_Pull_Ups(SMBusAdapter.ResistorValue.Ohm22k, SMBusAdapter.ResistorValue.Ohm22k, SMBusAdapter.ResistorValue.Ohm22k);
            smba.Set_Parallel_Mode(false);
            return smba;
        }
    }
}
