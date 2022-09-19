using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleAssistGift.Json
{
    /// <summary>
    /// MOD 設定データを格納する JSON オブジェクトです。
    /// </summary>
    public class JsonModSettingsObject : JsonObject
    {
        /// <summary>
        /// 戦闘表象「月光の輪」の効果が有効であることを表すフラグを取得または設定します。
        /// </summary>
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// 各キャラクター毎に適用する効果を取得または設定します。
        /// </summary>
        public JsonApplyingEffectNameObject ApplyingEffectName { get; set; } = new JsonApplyingEffectNameObject();

        /// <summary>
        /// 各キャラクターに適用できる効果のコレクションを取得または設定します。
        /// </summary>
        public IEnumerable<JsonEffectObject> Effects { get; set; } = Array.Empty<JsonEffectObject>();

        public override void OnDeserialize()
        {
            ApplyingEffectName = ApplyingEffectName ?? new JsonApplyingEffectNameObject();
            ApplyingEffectName.OnDeserialize();

            Effects = Effects ?? Array.Empty<JsonEffectObject>();
            foreach (var effect in Effects)
            {
                effect.OnDeserialize();
            }
        }

        /// <summary>
        /// 指定したキャラクターに対応する「月光の祝福」の効果を取得します。
        /// </summary>
        /// <param name="target">効果を取得する対象のキャラクター。</param>
        /// <returns>キャラクターに対応する効果。<see cref="Enabled">Enabled</see> プロパティの値が false または該当する効果がない場合は既定の効果。</returns>
        public MoonlightBlessingEffectModel GetEffect(BattleUnitModel target)
        {
            if (!Enabled) { return MoonlightBlessingEffectModel.None; }

            switch (target.faction)
            {
                case Faction.Enemy:
                    switch (target.index)
                    {
                        case 0:
                            return GetEffect(ApplyingEffectName.Enemy1);
                        case 1:
                            return GetEffect(ApplyingEffectName.Enemy2);
                        case 2:
                            return GetEffect(ApplyingEffectName.Enemy3);
                        case 3:
                            return GetEffect(ApplyingEffectName.Enemy4);
                        case 4:
                            return GetEffect(ApplyingEffectName.Enemy5);
                    }
                    break;

                case Faction.Player:
                    switch (target.index)
                    {
                        case 0:
                            return GetEffect(ApplyingEffectName.Player1);
                        case 1:
                            return GetEffect(ApplyingEffectName.Player2);
                        case 2:
                            return GetEffect(ApplyingEffectName.Player3);
                        case 3:
                            return GetEffect(ApplyingEffectName.Player4);
                        case 4:
                            return GetEffect(ApplyingEffectName.Player5);
                    }
                    break;
            }

            Log.Instance.WarningWithCaller($"{target.UnitData.unitData.name}'s character index ({target.index}) is out of range. Use the default value.");
            return MoonlightBlessingEffectModel.None;
        }

        /// <summary>
        /// 指定した効果名に一致する「月光の祝福」の効果を返します。
        /// </summary>
        /// <param name="effectName">取得する効果の名前。</param>
        /// <returns>指定した名前に完全一致する効果。該当する効果が複数ある場合は最初に一致した効果を返します。<paramref name="effectName"/> が null または該当する効果がない場合は既定の効果を返します。</returns>
        private MoonlightBlessingEffectModel GetEffect(string effectName)
        {
            if (effectName == null) { return MoonlightBlessingEffectModel.None; }

            JsonEffectObject effect = Effects.FirstOrDefault(e => e.Name == effectName);
            if (effect == null)
            {
                Log.Instance.WarningWithCaller($"Effect name '{effectName}' is not defined. Use the default value.");
                return MoonlightBlessingEffectModel.None;
            }

            return effect.CreateEffectModel();
        }
    }
}
