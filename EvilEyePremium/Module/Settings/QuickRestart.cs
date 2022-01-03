using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.Settings
{
    class QuickRestart : BaseModule
    {
        public QuickRestart() : base("Quick Restart", "Restart VRChat can also be triggerd by pressing \nctrl alt backspace", Main.Instance.settingsMisc, null, false)
        {
        }
        public override void OnEnable()
        {
            Process.Start("vrchat.exe", Environment.CommandLine.ToString());
            Main.OnApplicationQuit();
        }
    }
}
