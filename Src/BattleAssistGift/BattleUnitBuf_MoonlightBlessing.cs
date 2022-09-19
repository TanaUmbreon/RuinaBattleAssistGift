using System;
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
        private MoonlightBlessingEffectModel _effect;
        /// <summary>状態の付与数更新を行うことができる事を示すフラグ</summary>
        private bool _canUpdateBufs = false;

        public override BufPositiveType positiveType => BufPositiveType.None;
        public override bool Hide => _effect == MoonlightBlessingEffectModel.None;
        public override string bufActivatedText => Singleton<BattleEffectTextsXmlList>.Instance.GetEffectTextDesc(keywordId, _effect.Name);
        protected override string keywordId => "MoonlightBlessing";
        protected override string keywordIconId => "MoonlightBlessingBuf";

        /// <summary>
        /// <see cref="BattleUnitBuf_MoonlightBlessing"/> の新しいインスタンスを生成します。
        /// </summary>
        public BattleUnitBuf_MoonlightBlessing()
        {
            _effect = MoonlightBlessingEffectModel.None;
            stack = MaxStack;
        }

        public override void OnAddBuf(int addedStack)
            => stack = MaxStack;

        public override StatBonus GetStatBonus()
            => _effect.StatBonus;

        public override int MaxPlayPointAdder()
            => _effect.PlayPointAdder;

        public override void OnRoundStart()
        {
            try
            {
                _effect.RecoverTo(_owner);
                _effect.AddBufsTo(_owner);
                _canUpdateBufs = true;
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
        public void Apply(MoonlightBlessingEffectModel newEffect)
        {
            if (newEffect == null) { throw new ArgumentNullException(nameof(newEffect)); }

            MoonlightBlessingEffectModel oldEffect = ChangeEffect(newEffect);
            newEffect.UpdateStat(_owner, oldEffect);

            // 最初の幕はOnWaveStartとOnRoundStartで二重に状態が付与されてしまうのでそれを回避
            if (_canUpdateBufs)
            {
                newEffect.UpdateBufs(_owner, oldEffect);
            }
        }

        /// <summary>
        /// <see cref="_effect"/> フィールドの値を指定した効果に変更し、状態アイコンの表示・非表示を切り替えます。
        /// </summary>
        /// <param name="newEffect"></param>
        /// <returns>変更前の効果。</returns>
        private MoonlightBlessingEffectModel ChangeEffect(MoonlightBlessingEffectModel newEffect)
        {
            MoonlightBlessingEffectModel oldEffect = _effect;
            _effect = newEffect;

            new InstanceControler(this).SetField("_iconInit", false);
            GetBufIcon();

            return oldEffect;
        }
    }
}
