using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Tooltips;

namespace EvilEye.SDK.ButtonAPI
{
    public class QMToggleButton
    {
        public Toggle toggleButton;

        public QMToggleButton(Transform parent, string text, string toolTip, Action<bool> action)
        {
            GameObject singleButton = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_1/Button_ToggleQMInfo").gameObject, parent);
            singleButton.transform.parent = parent;
            singleButton.name = text + "_EVILEYE_ToggleButton";

            singleButton.transform.Find("Text_H4").gameObject.GetComponent<TextMeshProUGUI>().text = text;
            singleButton.transform.Find("Icon_On").GetComponent<Image>().sprite = Main.Instance.quickMenuStuff.Panel_NoNotifications_MessageIcon.sprite;
            singleButton.GetComponent<UiToggleTooltip>().field_Public_String_0 = toolTip;
            toggleButton = singleButton.GetComponent<Toggle>();
            toggleButton.onValueChanged = new Toggle.ToggleEvent();
            toggleButton.onValueChanged.AddListener(action);
            singleButton.SetActive(true);
        }
        public void SetToggle(bool state)
        {
            toggleButton.isOn = state;
        }


    }
}
