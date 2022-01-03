using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.SDK.ButtonAPI;
using UnityEngine;

namespace EvilEye.Module.Render
{
    internal class DebugLog : BaseModule
    {
        public static QMLable debugLog;
        public DebugLog() : base("Debug", "Debug on the side", Main.Instance.rendererUI, null, true, true)
        {
        }

        public override void OnEnable()
        {
            debugLog.lable.SetActive(true);

            debugLog.lable.transform.localPosition = new Vector3(609.902f, 457.9203f, 0);
            debugLog.text.enableWordWrapping = false;
            debugLog.text.fontSizeMin = 30;
            debugLog.text.fontSizeMax = 30;
            debugLog.text.alignment = TMPro.TextAlignmentOptions.Left;
            debugLog.text.verticalAlignment = TMPro.VerticalAlignmentOptions.Top;
            debugLog.text.color = Color.white;
        }

        public override void OnDisable()
        {
            debugLog.lable.SetActive(false);
        }

        public override void OnUIInit()
        {
            debugLog = new QMLable(GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Wing_Right/Button").transform, 609.902f, 457.9203f, "DebugLog");
            base.OnUIInit();
        }
    }
}
