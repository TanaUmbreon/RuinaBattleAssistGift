using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BattleAssistGift.Refrection;
using HarmonyLib;
using TMPro;
using UI;
using UnityEngine;

namespace BattleAssistGift
{
    public static class Tools
    {
		public static void SetAlarmText(string alarmtype, UIAlarmButtonType btnType = UIAlarmButtonType.Default, ConfirmEvent confirmFunc = null, params object[] args)
		{
			if (UIAlarmPopup.instance.IsOpened()) { UIAlarmPopup.instance.Close(); }

			Assembly assembly = GetAssembly("Assembly-CSharp");
			new EnumControler(assembly, "UI.UIAlarmPopup+UIAlarmAnimState")
				.GetValue("Normal", out object value);

			var uiAlarmPopup = new InstanceControler(UIAlarmPopup.instance)
				.SetField("currentAnimState", value)
				.GetField("ob_blue", out GameObject obBlue)
				.GetField("ob_normal", out GameObject obNormal)
				.GetField("ob_Reward", out GameObject obReward)
				.GetField("ob_BlackBg", out GameObject obBlackBg)
				.GetField("ButtonRoots", out List<GameObject> buttonRoots);

			if (obBlue.activeSelf) { obBlue.gameObject.SetActive(false); }
			if (!obNormal.activeSelf) { obNormal.gameObject.SetActive(true); }
			if (obReward.activeSelf) { obReward.SetActive(false); }
			if (obBlackBg.activeSelf) { obBlackBg.SetActive(false); }

			foreach (GameObject button in buttonRoots)
			{
				button.gameObject.SetActive(false);
			}

			uiAlarmPopup
				.SetField("currentAlarmType", UIAlarmType.Default)
				.SetField("buttonNumberType", btnType)
				.SetField("currentmode", AnimatorUpdateMode.Normal)
				.GetField("anim", out Animator anim)
				.GetField("txt_alarm", out TextMeshProUGUI txtAlarm);
			anim.updateMode = AnimatorUpdateMode.Normal;

			if (args == null)
			{
				txtAlarm.text = TextDataModel.GetText(alarmtype, Array.Empty<object>());
			}
			else
			{
				txtAlarm.text = TextDataModel.GetText(alarmtype, args);
			}

			typeof(UIAlarmPopup).GetField("_confirmEvent", AccessTools.all).SetValue(UIAlarmPopup.instance, confirmFunc);
			buttonRoots[(int)btnType].gameObject.SetActive(true);
			UIAlarmPopup.instance.Open();
			if (btnType == UIAlarmButtonType.Default)
			{
				UIControlManager.Instance.SelectSelectableForcely(UIAlarmPopup.instance.OkButton, false);
			}
			else
			{
				if (btnType == UIAlarmButtonType.YesNo)
				{
					uiAlarmPopup.GetField("yesButton", out UICustomSelectable yesButton);
					UIControlManager.Instance.SelectSelectableForcely(yesButton, false);
				}
			}
		}

		/// <summary>
		/// 実行中のアプリケーションで読み込まれているアセンブリから、指定した名前に一致するアセンブリを取得します。
		/// </summary>
		/// <param name="name">取得するアセンブリの簡易名。通常は、アセンブリ ファイルの名前から拡張子を取り除いたものになります。</param>
		/// <returns></returns>
		private static Assembly GetAssembly(string name)
		{
			Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == name);
			if (assembly == null) { throw new DllNotFoundException($"指定した名前 '{name}' に一致するアセンブリが見つかりません。"); }
			return assembly;
		}
	}
}
