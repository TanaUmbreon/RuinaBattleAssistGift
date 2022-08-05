using System;
using BattleAssistGift.Models;
using BattleAssistGift.Refrection;

namespace BattleAssistGift
{
    /// <summary>
    /// 状態「月光の祝福」
    /// カスタマイズされた様々な効果を発揮する。
    /// </summary>
    public class BattleUnitBuf_MoonlightBlessing : BattleUnitBuf
    {
        /// <summary>付与数の上限値</summary>
        private const int MaxStack = 0;

        /// <summary>適用中の効果</summary>
        private EffectModel effect;
        /// <summary>状態の付与数更新を行うことができる事を示すフラグ</summary>
        private bool canUpdateBufs = false;

        public override BufPositiveType positiveType => BufPositiveType.None;
        public override bool Hide => effect == EffectModel.None;
        public override string bufActivatedText => Singleton<BattleEffectTextsXmlList>.Instance.GetEffectTextDesc(keywordId, effect.Name);
        protected override string keywordId => "MoonlightRing";
        protected override string keywordIconId => "MoonlightRingBuf";

        /// <summary>
        /// <see cref="BattleUnitBuf_MoonlightBlessing"/> の新しいインスタンスを生成します。
        /// </summary>
        public BattleUnitBuf_MoonlightBlessing()
        {
            effect = EffectModel.None;
            stack = MaxStack;
        }

        public override void OnAddBuf(int addedStack)
            => stack = MaxStack;

        public override StatBonus GetStatBonus()
            => effect.StatBonus;

        public override int MaxPlayPointAdder()
            => effect.PlayPointAdder;

        public override void OnRoundStart()
        {
            try
            {
                effect.RecoverTo(_owner);
                effect.AddBufsTo(_owner);
                canUpdateBufs = true;
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }

        /// <summary>
        /// 指定した効果を適用します。
        /// </summary>
        /// <param name="newEffect"></param>
        public void Apply(EffectModel newEffect)
        {
            if (newEffect == null) { throw new ArgumentNullException(nameof(newEffect)); }

            EffectModel oldEffect = ChangeEffect(newEffect);
            newEffect.UpdateStat(_owner, oldEffect);

            // 最初の幕はOnWaveStartとOnRoundStartで二重に状態が付与されてしまうのでそれを回避
            if (canUpdateBufs)
            {
                newEffect.UpdateBufs(_owner, oldEffect);
            }
        }

        /// <summary>
        /// <see cref="effect"/> フィールドの値を指定した効果に変更し、状態アイコンの表示・非表示を切り替えます。
        /// </summary>
        /// <param name="newEffect"></param>
        /// <returns>変更前の効果。</returns>
        private EffectModel ChangeEffect(EffectModel newEffect)
        {
            EffectModel oldEffect = effect;
            effect = newEffect;

            new InstanceControler(this).SetField("_iconInit", false);
            GetBufIcon();

            return oldEffect;
        }
    }
}
