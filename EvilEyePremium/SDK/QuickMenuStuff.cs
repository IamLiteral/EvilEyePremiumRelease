using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;

namespace EvilEye.SDK
{
    public class QuickMenuStuff
    {
        public Image Button_WorldsIcon;
        public Image Button_AvatarsIcon;
        public Image Button_SocialIcon;
        public Image Button_SafetyIcon;
        public Image Panel_NoNotifications_MessageIcon;

        public Image Button_NameplateVisibleIcon;

        public Image Button_GoHomeIcon;
        public Image Button_RespawnIcon;
        public Image StandIcon;

        public VRC.UI.Elements.QuickMenu quickMenu;
        public SelectedUserMenuQM selectedUserMenuQM;
        public MenuStateController menuStateController;

        public Transform tabMenuTemplat;
        public Transform Menu_DevTools;
        public Transform Menu_Dashboard;
        public Transform QMParent;
        public Transform page_Buttons_QM;

        public QuickMenuStuff()
        {
            quickMenu = Resources.FindObjectsOfTypeAll<VRC.UI.Elements.QuickMenu>().First();
            menuStateController = quickMenu.gameObject.GetComponent<MenuStateController>();

            Button_WorldsIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Worlds/Icon").GetComponent<Image>();
            Button_AvatarsIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars/Icon").GetComponent<Image>();
            Button_SocialIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Social/Icon").GetComponent<Image>();
            Button_SafetyIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Safety/Icon").GetComponent<Image>();

            Button_GoHomeIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_GoHome/Icon").GetComponent<Image>();
            Button_RespawnIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn/Icon").GetComponent<Image>();
            StandIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/SitStandCalibrateButton/Button_SitStand/Icon_Off").GetComponent<Image>();
            Panel_NoNotifications_MessageIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Notifications/Panel_NoNotifications_Message/Icon").gameObject.GetComponent<Image>();

            Button_NameplateVisibleIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_1/Button_NameplateControls/Buttons/Button A/Icon").GetComponent<Image>();

            selectedUserMenuQM = quickMenu.transform.Find("Container/Window/QMParent/Menu_SelectedUser_Local").GetComponent<SelectedUserMenuQM>();
            tabMenuTemplat = quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools");
            Menu_DevTools = quickMenu.transform.Find("Container/Window/QMParent/Menu_DevTools");
            QMParent = quickMenu.transform.Find("Container/Window/QMParent");
            page_Buttons_QM = quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup");
            Menu_Dashboard = quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings");
        }

    }
}
