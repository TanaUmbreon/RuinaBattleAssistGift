using System;
using System.Collections.Generic;
using BattleAssistGift.Refrection;
using TMPro;
using UI;
using UnityEngine;

namespace BattleAssistGift
{
    public static class Tools
    {
		/// <summary>
		/// アラーム テキストを画面上にポップアップさせます。
		/// </summary>
		/// <param name="textId">表示するアラーム テキストのテキスト ID。</param>
		/// <param name="buttonType">ユーザーに押させるボタンの種類。省略した場合は OK ボタンを使用します。</param>
		/// <param name="confirmEvent">confirmEvent (OK 時に呼び出されるイベント？)。省略した場合は null。</param>
		/// <param name="args">アラーム テキストで使用されるプレース ホルダを置き換える値の配列。</param>
		public static void ShowAlarmText(string textId, UIAlarmButtonType buttonType = UIAlarmButtonType.Default, ConfirmEvent confirmEvent = null, params object[] args)
		{
			if (UIAlarmPopup.instance.IsOpened()) { UIAlarmPopup.instance.Close(); }

			new EnumControler(LoadedAssembly.AssemblyCSharp, "UI.UIAlarmPopup+UIAlarmAnimState")
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
				.SetField("buttonNumberType", buttonType)
				.SetField("currentmode", AnimatorUpdateMode.Normal)
				.GetField("anim", out Animator anim)
				.GetField("txt_alarm", out TextMeshProUGUI txtAlarm);
			anim.updateMode = AnimatorUpdateMode.Normal;
			txtAlarm.text = TextDataModel.GetText(textId, args ?? Array.Empty<object>());

			uiAlarmPopup.SetField("_confirmEvent", confirmEvent);
			buttonRoots[(int)buttonType].gameObject.SetActive(true);
			UIAlarmPopup.instance.Open();

			switch (buttonType)
			{
				case UIAlarmButtonType.Default:
					UIControlManager.Instance.SelectSelectableForcely(UIAlarmPopup.instance.OkButton, false);
					break;
				case UIAlarmButtonType.YesNo:
					uiAlarmPopup.GetField("yesButton", out UICustomSelectable yesButton);
					UIControlManager.Instance.SelectSelectableForcely(yesButton, false);
					break;
			}
        }
	}
}
