#pragma warning disable IDE0051 // 使用されていないプライベート メンバーを削除する

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using BattleAssistGift.Keys;
using BattleAssistGift.Properties;
using BattleAssistGift.Refrection;
using HarmonyLib;
using LOR_XML;

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
                CustomGiftStartup.LoadGiftText(ReferencePath.GiftTextFile);
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
            /// 指定した戦闘表象情報ファイルを読み込みます。戦闘表象 ID が重複する場合は末尾に出現した要素で上書きします。
            /// </summary>
            /// <param name="path">読み込む戦闘表象情報ファイルのパス。</param>
            public static void LoadGiftInfo(string path)
            {
                if (!File.Exists(path)) { throw new FileNotFoundException($"戦闘表象情報ファイル '{path}' が見つかりません。"); }

                GiftXmlRoot root = LoadXml<GiftXmlRoot>(path);
                AddGifts(root.giftXmlList);
            }

            /// <summary>
            /// 指定した戦闘表象ローカライズ ファイルを読み込みます。戦闘表象ローカライズ ID が重複する場合は末尾に出現した要素で上書きします。
            /// </summary>
            /// <param name="path"></param>
            public static void LoadGiftText(string path)
            {
                if (!File.Exists(path)) { throw new FileNotFoundException($"戦闘表象ローカライズ ファイル '{path}' が見つかりません。"); }

                GiftTextRoot root = LoadXml<GiftTextRoot>(path);
                AddGiftTexts(root.giftList);
            }

            /// <summary>
            /// 指定した戦闘表象情報を読み込み済みリストに追加します。戦闘表象 ID が重複する場合は末尾に出現した要素で上書きします。
            /// </summary>
            /// <param name="gifts">追加する戦闘表象情報のコレクション。</param>
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

            /// <summary>
            /// 指定した戦闘表象ローカライズを読み込み済みリストに追加します。戦闘表象ローカライズ ID が重複する場合は末尾に出現した要素で上書きします。
            /// </summary>
            /// <param name="texts">追加する戦闘表象ローカライズのコレクション。</param>
            private static void AddGiftTexts(IEnumerable<GiftText> texts)
            {
                new InstanceControler(Singleton<GiftDescXmlList>.Instance)
                    .GetField("_dictionary", out Dictionary<string, GiftText> dictionary);

                foreach (GiftText text in texts)
                {
                    dictionary[text.id] = text;
                }
            }

            /// <summary>
            /// 指定したパスの XML ファイルを読み込み、指定した型にデシリアライズして返します。
            /// </summary>
            /// <typeparam name="T">デシリアライズする型。</typeparam>
            /// <param name="path">読み込む XML ファイルのパス。</param>
            /// <returns>デシリアライズされた XML ファイルのデータ。</returns>
            private static T LoadXml<T>(string path)
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var reader = new StringReader(path))
                {
                    return (T)serializer.Deserialize(reader);
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
