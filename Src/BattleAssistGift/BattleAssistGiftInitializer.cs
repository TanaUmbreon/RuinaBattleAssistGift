using System;
using System.IO;
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
                new Harmony(AssemblyInfo.Name).PatchAll();

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
    }
}
