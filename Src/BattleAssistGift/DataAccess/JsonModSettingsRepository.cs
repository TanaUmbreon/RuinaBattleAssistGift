using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BattleAssistGift.Models;
using Newtonsoft.Json;

namespace BattleAssistGift.DataAccess
{
    /// <summary>
    /// JSON ファイル形式で永続化された MOD 設定データ (MOD 設定ファイル) へのアクセスを実装します。
    /// </summary>
    public class JsonModSettingsRepository : IModSettingsRepository
    {
        /// <summary>MOD 設定ファイル</summary>
        private readonly FileInfo _file;
        /// <summary>「月光の祝福」の効果名と、それに対応する効果のディクショナリー</summary>
        private readonly Dictionary<string, EffectModel> _effects;

        /// <summary>
        /// <see cref="JsonModSettingsRepository"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="path"></param>
        public JsonModSettingsRepository(string path)
        {
            _file = new FileInfo(path);
            _effects = new Dictionary<(Faction, int), EffectModel>();
        }

        /// <summary>
        /// MOD 設定データを MOD 設定ファイルから再読み込みます。
        /// </summary>
        public void Reload()
        {
            _effects.Clear();
            JsonModSettingsObject settings = Load();

        }

        private JsonModSettingsObject Load()
        {
            try
            {
                if (!_file.Exists) { throw new FileNotFoundException($"Mod settings file '{_file.FullName}' is not found."); }

                string json = File.ReadAllText(_file.FullName);
                JsonModSettingsObject settings = JsonConvert.DeserializeObject<JsonModSettingsObject>(json);
                Normalizer.Normalize(settings);
                return settings;
            }
            catch (Exception ex)
            {
                Log.Instance.WarningWithCaller("Default values are used because the mod settings file could not be loaded.");
                Log.Instance.ErrorOnExceptionThrown(ex);
                return new JsonModSettingsObject();
            }
        }

        /// <summary>
        /// 指定したキャラクターに対応する「月光の祝福」の効果を取得します。
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public EffectModel GetEffect(BattleUnitModel target)
        {
            if (!settings.Enabled) { return EffectModel.None; }

            switch (target.faction)
            {
                case Faction.Enemy:
                    switch (target.index)
                    {
                        case 0:
                            return GetEffect(settings.ApplyingEffectName.Enemy1);
                        case 1:
                            return GetEffect(settings.ApplyingEffectName.Enemy2);
                        case 2:
                            return GetEffect(settings.ApplyingEffectName.Enemy3);
                        case 3:
                            return GetEffect(settings.ApplyingEffectName.Enemy4);
                        case 4:
                            return GetEffect(settings.ApplyingEffectName.Enemy5);
                    }
                    break;

                case Faction.Player:
                    switch (target.index)
                    {
                        case 0:
                            return GetEffect(settings.ApplyingEffectName.Player1);
                        case 1:
                            return GetEffect(settings.ApplyingEffectName.Player2);
                        case 2:
                            return GetEffect(settings.ApplyingEffectName.Player3);
                        case 3:
                            return GetEffect(settings.ApplyingEffectName.Player4);
                        case 4:
                            return GetEffect(settings.ApplyingEffectName.Player5);
                    }
                    break;
            }

            Log.Instance.WarningWithCaller($"{target.UnitData.unitData.name}のキャラクター位置 ({target.index}) が範囲外です。規定値を使用します。");
            return EffectModel.None;
        }

        /// <summary>
        /// 指定した効果名に一致する効果を返します。
        /// </summary>
        /// <param name="effectName">取得する効果の名前。</param>
        /// <returns>指定した名前に完全一致する効果。該当する効果が複数ある場合は最初に一致した効果を返します。該当する効果がない場合は既定の効果を返します。</returns>
        private EffectModel GetEffect(string effectName)
        {
            if (effectName == null) { return EffectModel.None; }

            JsonEffectObject effect = settings.Effects.FirstOrDefault(e => e.Name == effectName);
            if (effect == null)
            {
                Log.Instance.WarningWithCaller($"効果名 '{effectName}' は定義されていません。規定値を使用します。");
                return EffectModel.None;
            }

            return effect.CreateEffectModel();
        }
    }
}
