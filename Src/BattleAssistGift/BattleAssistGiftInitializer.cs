using System;
using System.IO;
using BaseMod;
using BattleAssistGift.Keys;
using BattleAssistGift.Properties;
using BattleAssistGift.Refrection;
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
                var harmony = new Harmony(AssemblyInfo.Name);
                harmony.PatchAll();

                CreateSettingsFile();
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }

        /// <summary>
        /// 既定の設定ファイルを作成します。設定ファイルが既に存在する場合は何もしません。
        /// </summary>
        private void CreateSettingsFile()
        {
            if (File.Exists(ReferencePath.ModSettingsFile)) { return; }

            File.AppendAllText(ReferencePath.ModSettingsFile, Resources.ModSettings);
        }

#pragma warning disable IDE0051 // 使用されていないプライベート メンバーを削除する

        /// <summary>
        /// カスタム戦闘表象の入手に関わる、 HarmonyPatch の割り込み処理を実装します。
        /// </summary>
        [Harmony]
        private static class CustomGiftPatch
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
                _hasGotGift = false;
                return true;
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
                    if (_hasGotGift)
                    {
                        Tools.SetAlarmText(ExtraTextID.GotGifts);
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.ErrorOnExceptionThrown(ex);
                }
            }
        }

#pragma warning restore IDE0051 // 使用されていないプライベート メンバーを削除する
    }
}
