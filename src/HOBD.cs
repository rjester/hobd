﻿using System;
using System.Windows.Forms;
using Fleux.Core;

namespace hobd
{
    sealed class HOBD
    {
        public static Engine engine;
        public static SensorRegistry Registry;
        public static HOBDTheme theme = new HOBDTheme();
        
        [STAThread]
        private static void Main(string[] args)
        {


            HOBD.engine = new OBD2Engine();
            
            IStream stream = new SerialStream();
            engine.Init(stream, "COM7");
            
            Registry = new SensorRegistry();
            Registry.RegisterProvider(new OBD2Sensors());
            Registry.RegisterProvider(new ToyotaSensors());
            engine.Registry = Registry;
            engine.Activate();
            
//TODO: autoajust from screen size
#if WINCE
            FleuxApplication.TargetDesignDpi = 96;
#else
            // desktop - scale DPI
            FleuxApplication.TargetDesignDpi = 96 ;//* 480 / 800;
            
#endif
            FleuxApplication.Run(new HomePage());
            
            engine.Deactivate();

        }
        
    }
}