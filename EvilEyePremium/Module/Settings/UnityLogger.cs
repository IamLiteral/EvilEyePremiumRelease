using EvilEye.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.Module.Settings
{
    class UnityLogger : BaseModule
    {
        public static UnityLogger Instance;

        public UnityLogger() : base("UnityLogger", "Logs Unity Debug", Main.Instance.settingsLogger, null, true, true)
        {
            Instance = this;
        }
    }
}
