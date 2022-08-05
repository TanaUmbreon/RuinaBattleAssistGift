using System;
using System.Collections.Generic;

namespace BattleAssistGift.DataAccess
{
    /// <summary>
    /// MOD 設定データを格納します。
    /// </summary>
    public class JsonModSettingsObject
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

        /// <summary>
        /// 値が null のプロパティを初期化します。
        /// </summary>
        public void InitializeNullProperties()
        {
            if (ApplyingEffectName == null)
            {
                ApplyingEffectName = new JsonApplyingEffectNameObject();
            }

            if (Effects == null)
            {
                Effects = Array.Empty<JsonEffectObject>();
            }
            foreach (var effect in Effects)
            {
                effect.InitializeNullProperties();
            }
        }
    }
}
