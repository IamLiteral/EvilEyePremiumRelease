using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;

namespace EvilEye.SDK.ButtonAPI
{
    public class QMMenu
    {
		public UIPage page;
		public Transform menuContents;

		public QMMenu(string menuName, string pageTitle, bool root = true, bool backButton = true)
		{
			GameObject menu = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_DevTools").gameObject, Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent"));
			menu.name = "Menu_" + menuName;
			menu.transform.SetSiblingIndex(5);
			menu.SetActive(false);

			UnityEngine.Object.Destroy(menu.GetComponent<DevMenu>());

			page = menu.AddComponent<UIPage>();
			page.field_Public_String_0 = menuName;
			page.field_Private_Boolean_1 = true;
			page.field_Private_MenuStateController_0 = Main.Instance.quickMenuStuff.menuStateController;
			page.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
			page.field_Private_List_1_UIPage_0.Add(page);
            if (!root)
            {
				page.field_Public_Boolean_0 = true;
                try
                {
					menu.transform.Find("Scrollrect/Scrollbar").gameObject.SetActive(true);
					menu.transform.Find("Scrollrect").GetComponent<ScrollRect>().enabled = true;
					menu.transform.Find("Scrollrect").GetComponent<ScrollRect>().verticalScrollbar = menu.transform.Find("Scrollrect/Scrollbar").GetComponent<Scrollbar>();
					menu.transform.Find("Scrollrect").GetComponent<ScrollRect>().verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
				}
                catch { }
			}
			Main.Instance.quickMenuStuff.menuStateController.field_Private_Dictionary_2_String_UIPage_0.Add(menuName, page);
			if (root)
			{
				List<UIPage> list = Main.Instance.quickMenuStuff.menuStateController.field_Public_ArrayOf_UIPage_0.ToList<UIPage>();
				list.Add(page);
				Main.Instance.quickMenuStuff.menuStateController.field_Public_ArrayOf_UIPage_0 = list.ToArray();
			}
			TextMeshProUGUI pageTitleText = menu.GetComponentInChildren<TextMeshProUGUI>(true);
			pageTitleText.text = pageTitle;
			menuContents = menu.transform.Find("Scrollrect/Viewport/VerticalLayoutGroup/Buttons");
			for (int i = 0; i < menuContents.transform.childCount; i++)
				GameObject.Destroy(menuContents.transform.GetChild(i).gameObject);
			if (backButton)
            {
				GameObject backButtonGameObject = menu.transform.Find("Header_DevTools/LeftItemContainer/Button_Back").gameObject;
				backButtonGameObject.SetActive(true);
				backButtonGameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
				backButtonGameObject.GetComponent<Button>().onClick.AddListener(new Action(() =>
				{
					page.Method_Protected_Virtual_New_Void_0();
				}));
			}
		}

		public void OpenMenu()
		{
			Main.Instance.quickMenuStuff.menuStateController.Method_Public_Void_String_UIContext_Boolean_0(this.page.field_Public_String_0, null, false);
		}

		public void CloseMenu()
		{
			this.page.Method_Public_Virtual_New_Void_0();
		}
	}
}
