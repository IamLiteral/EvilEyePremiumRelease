using EvilEye.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EvilEye.Module.PlayerModule
{
    class FPSUnlocker : BaseModule
    {
        public FPSUnlocker() : base("FPS Unlocker", "140 Fps", Main.Instance.settingsButtons, null, true) { }

        public override void OnEnable()
        {
            LoggerUtill.Log("Application Framerate Set To 140", ConsoleColor.Yellow);
            Application.targetFrameRate = 140;
        }

        public override void OnDisable()
        {
            LoggerUtill.Log("Application Framerate Set To 90", ConsoleColor.Yellow);
            Application.targetFrameRate = 90;
        }
    }
}
