#pragma warning disable IDE0051 // 使用されていないプライベート メンバーを削除する

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using BattleAssistGift.Keys;
using BattleAssistGift.Properties;
using HarmonyLib;

namespace BattleAssistGift
{
    /// <summary>
    /// MOD を初期化する機能を提供します。
    /// </summary>
    [Harmony]
    public class BattleAssistGiftInitializer : ModInitializer
    {
        #region 初期化

        public override void OnInitializeMod()
        {
            try
            {
                var harmony = new Harmony(AssemblyInfo.Name);
                harmony.PatchAll();

                CustomGiftStartup.LoadGiftInfo(ReferencePath.GiftInfoFile);
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

        #endregion

        /// <summary>
        /// カスタム戦闘表象のスタートアップを行います。
        /// </summary>
        private static class CustomGiftStartup
        {
            /// <summary>
            /// 指定した戦闘表象情報ファイル (GiftInfo.xml) を読み込みます。
            /// </summary>
            /// <param name="path">読み込む戦闘表象情報ファイルのパス。</param>
            public static void LoadGiftInfo(string path)
            {
                if (!File.Exists(path)) { throw new FileNotFoundException($"戦闘表象情報ファイル '{path}' が見つかりません。"); }

                GiftXmlRoot root = LoadGiftXmlRoot(path);
                AddGifts(root.giftXmlList);
            }

            private static GiftXmlRoot LoadGiftXmlRoot(string path)
            {
                var serializer = new XmlSerializer(typeof(GiftXmlRoot));
                using (var reader = new StringReader(path))
                {
                    return (GiftXmlRoot)serializer.Deserialize(reader);
                }
            }

            private static void AddGifts(IEnumerable<GiftXmlInfo> gifts)
            {
                List<GiftXmlInfo> addedGifts = Singleton<GiftXmlList>.Instance.GetAvailableList();

                foreach (GiftXmlInfo adding in gifts)
                {
                    var added = addedGifts.FirstOrDefault(g => g.id == adding.id);
                    if (added != null)
                    {
                        addedGifts.Remove(added);
                    }

                    addedGifts.Add(adding);
                }
            }
        }

        #region HarmonyPatch による割り込み処理 (戦闘表象の入手)

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

                GiftModel addedGift = __instance.giftInventory.AddGift(GiftID.MoonlightRing);
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
                    Tools.ShowAlarmText(ExtraTextID.GotGifts);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }

        #endregion
    }
}
