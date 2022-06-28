using System;
using System.IO;
using BattleAssistGift.Properties;
using HarmonyLib;
using BattleAssistGift.Keys;

namespace BattleAssistGift
{
    /// <summary>
    /// MOD を初期化する機能を提供します。
    /// </summary>
    [Harmony]
    public class BattleAssistGiftInitializer : ModInitializer
    {
        /// <summary>設定ファイルのパス</summary>
        private static readonly string SettingsFilePath = Path.Combine(AssemblyInfo.DirectoryPath, @"..\ModSettings.json");

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
            if (File.Exists(SettingsFilePath)) { return; }

            File.AppendAllText(SettingsFilePath, Resources.ModSettings);
        }

        [HarmonyPatch(typeof(LibraryModel), "LoadFromSaveData")]
        [HarmonyPostfix]
        public static void LibraryModel_LoadFromSaveData_Postfix()
        {
            try
            {
                Tools.ShowAlarmText(ExtraTextID.GotGifts);
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }

	}
}
