using System;
using System.Collections.Generic;
using BaseMod;
using BattleAssistGift.Refrection;
using BattleAssistGift.Statics;
using HarmonyLib;

namespace BattleAssistGift
{
    /// <summary>
    /// MOD を初期化する機能を提供します。
    /// </summary>
    public class BattleAssistGiftInitializer : ModInitializer
    {
        public override void OnInitializeMod()
        {
            try
            {
                // カスタム戦闘表象の画像データ読み込みはBaseModで行われていないのでここで行う
                if (CustomGiftAppearance.GiftArtWork == null)
                {
                    CustomGiftAppearance.GetGiftArtWork();
                }

                new Harmony(AssemblyInfo.Name).PatchAll();
                new MoonlightBlessingController().CreateSettingsFile();
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }
    }

#pragma warning disable IDE0051 // 使用されていないプライベート メンバーを削除する

    /// <summary>
    /// カスタム戦闘表象の入手を行う為の HarmonyPatch 割り込み処理を実装します。
    /// </summary>
    [Harmony]
    internal class GiftAcquiringPatch
    {
        /// <summary>誰か一人でも戦闘表象を入手した事を示すフラグ</summary>
        private static bool _hasGotGift = false;
        /// <summary>BaseMod を使用することで発生する <see cref="GiftXmlList"/> の不具合を解決した事を示すフラグ</summary>
        private static bool _fixedGiftXmlList = false;

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
                FixGiftXmlList();
                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
                return true;
            }
        }

        /// <summary>
        /// BaseMod を使用することで発生する、カスタム戦闘表象を参照できない不具合を解決します。
        /// </summary>
        private static void FixGiftXmlList()
        {
            if (_fixedGiftXmlList) { return; }

            // カスタム戦闘表象を「GiftXmlList.Instance.GetData(int)」で参照できない不具合を解決 (2022-07-03時点で発生する不具合)
            new InstanceControler(GiftXmlList.Instance).GetField("_list", out List<GiftXmlInfo> list);
            GiftXmlList.Instance.Init(list);

            _fixedGiftXmlList = true;
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

                GiftModel addedGift = __instance.giftInventory.AddGift(GiftId.MoonlightBlessing);
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

                Tools.SetAlarmText(ExtraTextId.GotGifts);
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }
    }
}
