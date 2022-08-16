using System;
using System.IO;
using BaseMod;
using BattleAssistGift.Json;
using BattleAssistGift.Statics;

namespace BattleAssistGift
{
    /// <summary>
    /// 戦闘表象「月光の祝福」の機能を操作します。
    /// </summary>
    public class MoonlightBlessingController
    {
        /// <summary>MOD 設定ファイル</summary>
        private readonly FileInfo _file;

        /// <summary>
        /// <see cref="MoonlightBlessingController"/> の新しいインスタンスを生成します。
        /// </summary>
        public MoonlightBlessingController()
        {
            _file = new FileInfo(ReferencePath.ModSettingsFile);
        }

        /// <summary>
        /// 既定の内容で MOD 設定ファイルを作成します。既に存在する場合は何もしません。
        /// </summary>
        public void CreateSettingsFile()
        {
            if (_file.Exists)
            {
                Log.Instance.DebugWithCaller("Mod settings file exsits.");
                return;
            }

            File.WriteAllText(_file.FullName, Properties.Resources.ModSettings);
            Log.Instance.DebugWithCaller("Mod settings file does not exsit, so a new one was created.");
        }

        /// <summary>
        /// 敵味方全ての生存キャラクターに「月光の祝福」の効果を適用します。
        /// </summary>
        public void ApplyEffectAll()
        {
            JsonModSettingsObject settings = LoadModSettings();

            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
            {
                var buf = target.bufListDetail.FindBuf<BattleUnitBuf_MoonlightBlessing>();
                if (buf == null)
                {
                    buf = new BattleUnitBuf_MoonlightBlessing();
                    target.bufListDetail.AddBuf(buf);
                }

                buf.Apply(settings.GetEffect(target));
            }
        }

        /// <summary>
        /// MOD 設定ファイルを読み込み、オブジェクトにデシリアライズして返します。デシリアライズに失敗した場合は既定の値で返します。
        /// </summary>
        /// <returns>デシリアライズされた MOD 設定ファイル。</returns>
        private JsonModSettingsObject LoadModSettings()
        {
            try
            {
                CreateSettingsFile();
                return JsonDeserialize.FromFile<JsonModSettingsObject>(_file.FullName);
            }
            catch (Exception ex)
            {
                Log.Instance.WarningWithCaller("Default values are used because the mod settings file could not be loaded.");
                Log.Instance.ErrorOnExceptionThrown(ex);
                return new JsonModSettingsObject();
            }
        }

        /// <summary>
        /// 味方全ての生存キャラクターに個人特殊ページ「設定リロード」を追加します。
        /// </summary>
        public void AddReloadCardAllPlayers()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList(Faction.Player))
            {
                AddReloadCard(target);
            }
        }

        /// <summary>
        /// 指定したキャラクターに個人特殊ページ「設定リロード」を追加します。
        /// </summary>
        /// <param name="target"></param>
        public void AddReloadCard(BattleUnitModel target)
        {
            target.personalEgoDetail.RemoveCard(CardId.Reload);
            target.personalEgoDetail.AddCard(CardId.Reload);
        }
    }
}
