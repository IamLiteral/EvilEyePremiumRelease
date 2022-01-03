using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;

namespace EvilEye.SDK.ButtonAPI
{
    public class QMNestedButton
    {
        public QMMenu menu;
        public Transform menuTransform;

        public QMNestedButton(Transform perant, string name, Sprite icon = null)
        {
            menu = new QMMenu(name, name, false, true);
            menuTransform = menu.menuContents.parent;
            
            for (int i = 0; i < menuTransform.transform.childCount; i++)
                GameObject.Destroy(menuTransform.transform.GetChild(i).gameObject);
            menuTransform.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
            new QMSingleButton(perant, name, name, icon, delegate
            {
                menu.OpenMenu();
            });
        }

    }
}
