#pragma warning disable IDE0051 // 使用されていないプライベート メンバーを削除する

using System;
using System.Collections.Generic;
using BaseMod;
using BattleAssistGift.Keys;
using BattleAssistGift.Refrection;
using HarmonyLib;

namespace BattleAssistGift.Patches
{
    [Harmony]
    internal class GiftAcquiringPatch
    {
        /// <summary>誰か一人でも戦闘表象を入手した事を示すフラグ</summary>
        private static bool _hasGotGift = false;

        /// <summary>
        /// 戦闘表象の入手フラグを初期化します。
        /// </summary>
        /// <returns>本来呼び出されるメソッドも呼び出す場合は true、呼び出さない場合は false。</returns>
        [HarmonyPatch(typeof(LibraryModel), "LoadFromSaveData")]
        [HarmonyPrefix]
        private static bool LibraryModel_LoadFromSaveData_Prefix()
        {
            try
            {
                _hasGotGift = false;
                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
                return true;
            }
        }

        /// <summary>
        /// 指定司書に戦闘表象を入手させます。
        /// </summary>
        /// <param name="__instance">メソッドを呼び出したインスタンス。</param>
        [HarmonyPatch(typeof(UnitDataModel), "LoadFromSaveData")]
        [HarmonyPostfix]
        private static void UnitDataModel_LoadFromSaveData_Postfix(UnitDataModel __instance)
        {
            try
            {
                if (!__instance.isSephirah) { return; }

                GiftModel addedGift = __instance.giftInventory.AddGift(GiftID.MoonlightBlessing);
                if (addedGift != null)
                {
                    _hasGotGift = true;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }

        /// <summary>
        /// 誰か一人でも戦闘表象を入手した場合、入手した事を知らせるアラート テキストを表示します。
        /// </summary>
        [HarmonyPatch(typeof(LibraryModel), "LoadFromSaveData")]
        [HarmonyPostfix]
        private static void LibraryModel_LoadFromSaveData_Postfix()
        {
            try
            {
                if (!_hasGotGift) { return; }

                Tools.SetAlarmText(ExtraTextID.GotGifts);
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }
    }
}
