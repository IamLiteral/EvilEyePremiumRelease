using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace EvilEye.SDK.ButtonAPI
{
    public class QMButtonGroup
    {
        public GameObject buttonGroup;
        public int buttonamount = 0;
        public QMButtonGroup(Transform parent, string name)
        {
            buttonGroup = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_2").gameObject, parent);
            buttonGroup.name = "Buttons_" + name;
            for (int i = 0; i < buttonGroup.transform.childCount; i++)
                GameObject.Destroy(buttonGroup.transform.GetChild(i).gameObject);
        }
    }
}
