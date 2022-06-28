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
