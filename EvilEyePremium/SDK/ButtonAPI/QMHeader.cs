using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace EvilEye.SDK.ButtonAPI
{
    internal class QMHeader
    {
        public QMHeader(Transform menu, string contents)
        {
            GameObject header = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/QM_Foldout_UI_Elements").gameObject, menu);
            header.transform.parent = menu;
            header.name = "Header_" + menu.name + "_" + contents;
            header.SetActive(true);
            header.GetComponentInChildren<TextMeshProUGUI>().text = contents;
            header.transform.Find("Arrow").gameObject.active = false;
        }
    }
}
