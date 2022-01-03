using System;
using System.Linq;
using System.Reflection;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using UnityEngine.UI;

namespace EvilEye.SDK.ButtonAPI
{
    public static class QMPopup
    {
		public static void HideCurrentPopup(this VRCUiPopupManager vrcUiPopupManager)
		{
			//VRCUiManager.prop_VRCUiManager_0.HideScreen("POPUP");
		}
		public static void ShowStandardPopup(this VRCUiPopupManager vrcUiPopupManager, string title, string content, System.Action<VRCUiPopup> onCreated = null)
		{
			QMPopup.ShowUiStandardPopup1(title, content, onCreated);
		}
		public static void ShowStandardPopup(this VRCUiPopupManager vrcUiPopupManager, string title, string content, string buttonText, System.Action buttonAction, System.Action<VRCUiPopup> onCreated = null)
		{
			QMPopup.ShowUiStandardPopup2(title, content, buttonText, buttonAction, onCreated);
		}
		public static void ShowStandardPopup(this VRCUiPopupManager vrcUiPopupManager, string title, string content, string button1Text, System.Action button1Action, string button2Text, System.Action button2Action, System.Action<VRCUiPopup> onCreated = null)
		{
			QMPopup.ShowUiStandardPopup3(title, content, button1Text, button1Action, button2Text, button2Action, onCreated);
		}
		public static void ShowStandardPopupV2(this VRCUiPopupManager vrcUiPopupManager, string title, string content, string buttonText, System.Action buttonAction, System.Action<VRCUiPopup> onCreated = null)
		{
			QMPopup.ShowUiStandardPopupV21(title, content, buttonText, buttonAction, onCreated);
		}
		public static void ShowStandardPopupV2(this VRCUiPopupManager vrcUiPopupManager, string title, string content, string button1Text, System.Action button1Action, string button2Text, System.Action button2Action, System.Action<VRCUiPopup> onCreated = null)
		{
			QMPopup.ShowUiStandardPopupV22(title, content, button1Text, button1Action, button2Text, button2Action, onCreated);
		}
		public static void ShowInputPopup(this VRCUiPopupManager vrcUiPopupManager, string title, string preFilledText, InputField.InputType inputType, bool keypad, string buttonText, Il2CppSystem.Action<string, List<KeyCode>, Text> buttonAction, Il2CppSystem.Action cancelAction, string boxText = "Enter text....", bool closeOnAccept = true, System.Action<VRCUiPopup> onCreated = null, bool startOnLeft = false, int characterLimit = 0)
		{
			QMPopup.ShowUiInputPopup(title, preFilledText, inputType, keypad, buttonText, buttonAction, cancelAction, boxText, closeOnAccept, onCreated, startOnLeft, characterLimit);
		}
		public static void ShowAlert(this VRCUiPopupManager vrcUiPopupManager, string title, string content, float timeout)
		{
			QMPopup.ShowUiAlertPopup(title, content, timeout);
		}
		public static QMPopup.ShowUiInputPopupAction ShowUiInputPopup
		{
			get
			{
				if (QMPopup.ourShowUiInputPopupAction != null)
				{
					return QMPopup.ourShowUiInputPopupAction;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(delegate (MethodInfo it)
				{
					if (it.GetParameters().Length == 12)
					{
						return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
						{
							if (jt.Type == XrefType.Global)
							{
								Il2CppSystem.Object @object = jt.ReadAsObject();
								return ((@object != null) ? @object.ToString() : null) == "UserInterface/MenuContent/Popups/InputPopup";
							}
							return false;
						});
					}
					return false;
				});
				QMPopup.ourShowUiInputPopupAction = (QMPopup.ShowUiInputPopupAction)System.Delegate.CreateDelegate(typeof(QMPopup.ShowUiInputPopupAction), VRCUiPopupManager.prop_VRCUiPopupManager_0, method);
				return QMPopup.ourShowUiInputPopupAction;
			}
		}
		public static QMPopup.ShowUiStandardPopup1Action ShowUiStandardPopup1
		{
			get
			{
				if (QMPopup.ourShowUiStandardPopup1Action != null)
				{
					return QMPopup.ourShowUiStandardPopup1Action;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(delegate (MethodInfo it)
				{
					if (it.GetParameters().Length == 3 && !it.Name.Contains("PDM"))
					{
						return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
						{
							if (jt.Type == XrefType.Global)
							{
								Il2CppSystem.Object @object = jt.ReadAsObject();
								return ((@object != null) ? @object.ToString() : null) == "UserInterface/MenuContent/Popups/StandardPopup";
							}
							return false;
						});
					}
					return false;
				});
				QMPopup.ourShowUiStandardPopup1Action = (QMPopup.ShowUiStandardPopup1Action)System.Delegate.CreateDelegate(typeof(QMPopup.ShowUiStandardPopup1Action), VRCUiPopupManager.prop_VRCUiPopupManager_0, method);
				return QMPopup.ourShowUiStandardPopup1Action;
			}
		}
		public static QMPopup.ShowUiStandardPopup2Action ShowUiStandardPopup2
		{
			get
			{
				if (QMPopup.ourShowUiStandardPopup2Action != null)
				{
					return QMPopup.ourShowUiStandardPopup2Action;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(delegate (MethodInfo it)
				{
					if (it.GetParameters().Length == 5 && !it.Name.Contains("PDM"))
					{
						return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
						{
							if (jt.Type == XrefType.Global)
							{
								Il2CppSystem.Object @object = jt.ReadAsObject();
								return ((@object != null) ? @object.ToString() : null) == "UserInterface/MenuContent/Popups/StandardPopup";
							}
							return false;
						});
					}
					return false;
				});
				QMPopup.ourShowUiStandardPopup2Action = (QMPopup.ShowUiStandardPopup2Action)System.Delegate.CreateDelegate(typeof(QMPopup.ShowUiStandardPopup2Action), VRCUiPopupManager.prop_VRCUiPopupManager_0, method);
				return QMPopup.ourShowUiStandardPopup2Action;
			}
		}

		public static QMPopup.ShowUiStandardPopup3Action ShowUiStandardPopup3
		{
			get
			{
				if (QMPopup.ourShowUiStandardPopup3Action != null)
				{
					return QMPopup.ourShowUiStandardPopup3Action;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(delegate (MethodInfo it)
				{
					if (it.GetParameters().Length == 7 && !it.Name.Contains("PDM"))
					{
						return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
						{
							if (jt.Type == XrefType.Global)
							{
								Il2CppSystem.Object @object = jt.ReadAsObject();
								return ((@object != null) ? @object.ToString() : null) == "UserInterface/MenuContent/Popups/StandardPopup";
							}
							return false;
						});
					}
					return false;
				});
				QMPopup.ourShowUiStandardPopup3Action = (QMPopup.ShowUiStandardPopup3Action)System.Delegate.CreateDelegate(typeof(QMPopup.ShowUiStandardPopup3Action), VRCUiPopupManager.prop_VRCUiPopupManager_0, method);
				return QMPopup.ourShowUiStandardPopup3Action;
			}
		}
		public static QMPopup.ShowUiStandardPopupV21Action ShowUiStandardPopupV21
		{
			get
			{
				if (QMPopup.ourShowUiStandardPopupV21Action != null)
				{
					return QMPopup.ourShowUiStandardPopupV21Action;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(delegate (MethodInfo it)
				{
					if (it.GetParameters().Length == 5 && !it.Name.Contains("PDM"))
					{
						return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
						{
							if (jt.Type == XrefType.Global)
							{
								Il2CppSystem.Object @object = jt.ReadAsObject();
								return ((@object != null) ? @object.ToString() : null) == "UserInterface/MenuContent/Popups/StandardPopupV2";
							}
							return false;
						});
					}
					return false;
				});
				QMPopup.ourShowUiStandardPopupV21Action = (QMPopup.ShowUiStandardPopupV21Action)System.Delegate.CreateDelegate(typeof(QMPopup.ShowUiStandardPopupV21Action), VRCUiPopupManager.prop_VRCUiPopupManager_0, method);
				return QMPopup.ourShowUiStandardPopupV21Action;
			}
		}
		public static QMPopup.ShowUiStandardPopupV22Action ShowUiStandardPopupV22
		{
			get
			{
				if (QMPopup.ourShowUiStandardPopupV22Action != null)
				{
					return QMPopup.ourShowUiStandardPopupV22Action;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(delegate (MethodInfo it)
				{
					if (it.GetParameters().Length == 7 && !it.Name.Contains("PDM"))
					{
						return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
						{
							if (jt.Type == XrefType.Global)
							{
								Il2CppSystem.Object @object = jt.ReadAsObject();
								return ((@object != null) ? @object.ToString() : null) == "UserInterface/MenuContent/Popups/StandardPopupV2";
							}
							return false;
						});
					}
					return false;
				});
				QMPopup.ourShowUiStandardPopupV22Action = (QMPopup.ShowUiStandardPopupV22Action)System.Delegate.CreateDelegate(typeof(QMPopup.ShowUiStandardPopupV22Action), VRCUiPopupManager.prop_VRCUiPopupManager_0, method);
				return QMPopup.ourShowUiStandardPopupV22Action;
			}
		}
		public static QMPopup.ShowUiAlertPopupAction ShowUiAlertPopup
		{
			get
			{
				if (QMPopup.ourShowUiAlertPopupAction != null)
				{
					return QMPopup.ourShowUiAlertPopupAction;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(delegate (MethodInfo it)
				{
					if (it.GetParameters().Length == 3)
					{
						return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
						{
							if (jt.Type == XrefType.Global)
							{
								Il2CppSystem.Object @object = jt.ReadAsObject();
								return ((@object != null) ? @object.ToString() : null) == "UserInterface/MenuContent/Popups/AlertPopup";
							}
							return false;
						});
					}
					return false;
				});
				QMPopup.ourShowUiAlertPopupAction = (QMPopup.ShowUiAlertPopupAction)System.Delegate.CreateDelegate(typeof(QMPopup.ShowUiAlertPopupAction), VRCUiPopupManager.prop_VRCUiPopupManager_0, method);
				return QMPopup.ourShowUiAlertPopupAction;
			}
		}

		private static QMPopup.ShowUiInputPopupAction ourShowUiInputPopupAction;
		private static QMPopup.ShowUiStandardPopup1Action ourShowUiStandardPopup1Action;
		private static QMPopup.ShowUiStandardPopup2Action ourShowUiStandardPopup2Action;
		private static QMPopup.ShowUiStandardPopup3Action ourShowUiStandardPopup3Action;
		private static QMPopup.ShowUiStandardPopupV21Action ourShowUiStandardPopupV21Action;
		private static QMPopup.ShowUiStandardPopupV22Action ourShowUiStandardPopupV22Action;
		private static QMPopup.ShowUiAlertPopupAction ourShowUiAlertPopupAction;
		public delegate void ShowUiInputPopupAction(string title, string initialText, InputField.InputType inputType, bool isNumeric, string confirmButtonText, Il2CppSystem.Action<string, List<KeyCode>, Text> onComplete, Il2CppSystem.Action onCancel, string placeholderText = "Enter text...", bool closeAfterInput = true, Il2CppSystem.Action<VRCUiPopup> onPopupShown = null, bool startOnLeft = false, int characterLimit = 0);
		public delegate void ShowUiStandardPopup1Action(string title, string body, Il2CppSystem.Action<VRCUiPopup> onPopupShown = null);
		public delegate void ShowUiStandardPopup2Action(string title, string body, string middleButtonText, Il2CppSystem.Action middleButtonAction, Il2CppSystem.Action<VRCUiPopup> onPopupShown = null);
		public delegate void ShowUiStandardPopup3Action(string title, string body, string leftButtonText, Il2CppSystem.Action leftButtonAction, string rightButtonText, Il2CppSystem.Action rightButtonAction, Il2CppSystem.Action<VRCUiPopup> onPopupShown = null);
		public delegate void ShowUiStandardPopupV21Action(string title, string body, string middleButtonText, Il2CppSystem.Action middleButtonAction, Il2CppSystem.Action<VRCUiPopup> onPopupShown = null);
		public delegate void ShowUiStandardPopupV22Action(string title, string body, string leftButtonText, Il2CppSystem.Action leftButtonAction, string rightButtonText, Il2CppSystem.Action rightButtonAction, Il2CppSystem.Action<VRCUiPopup> onPopupShown = null);
		public delegate void ShowUiAlertPopupAction(string title, string body, float timeout);
	}
}

