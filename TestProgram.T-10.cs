﻿using System;
using System.Collections.Generic;
using System.Threading;
using TestLibrary;
using TestLibrary.Config;
using TestLibrary.Instruments;
using TestLibrary.Instruments.Keysight;
using TestLibrary.TestSupport;

// Place all Test methods, convenience methods, classes & comments exclusive to Group T-10 in this file.
// Do not place them in any other file, as Tests should be unique to a Program the and methods & classes
// must be unique within a namespace.
// - Examples:
//      If T01 belongs to both Groups T-10 & T-20, place it in TestProgram.Shared.cs.
//      If T02 belongs exclusively to Group T-10, place it in this file.
//      If T03 belongs exclusively to Group T-20, place it in TestProgram.T20.cs
//      RunTestMethod is common to both Groups T-10 & T-20, so is placed in TestProgram.Shared.cs.
//
namespace TestProgram {
    internal sealed partial class TestProgramTests {

        internal static String T07(Test test, Dictionary<INSTRUMENTS, Instrument> instruments, CancellationToken CT) {
            (String standardError, String standardOutput) = ISP("MPLAB PICkit 4 In-Circuit Debugger", "J11", test, instruments, PowerISPMethod);
            // NOTE: Parse & return portion of ISP's standardError/standardOutput corresponding to App.config's ISPResult.
            return $"0x{0x050C:X4}";
        }

        internal static String T10(Test test, Dictionary<INSTRUMENTS, Instrument> instruments, CancellationToken CT) {
            TestTextual tt = (TestTextual)test.ClassObject;
            return tt.Text.ToLower();
        }
    }
}
