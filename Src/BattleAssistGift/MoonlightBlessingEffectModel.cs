using System;
using UnityEngine;

namespace BattleAssistGift
{
    /// <summary>
    /// キャラクターに適用させる「月光の祝福」の効果を実装します。
    /// </summary>
    public class MoonlightBlessingEffectModel
    {
        /// <summary>効果なしを表します。</summary>
        public static readonly MoonlightBlessingEffectModel None = new MoonlightBlessingEffectModel("None");

        private const int MinHpAdder = 0;
        private const int MaxHpAdder = 500;
        private const int MinBreakGageAdder = 0;
        private const int MaxBreakGageAdder = 500;
        private const int MinPlayPointAdder = 0;
        private const int MaxPlayPointAdder = 5;
        private const int MinHpRecover = 0;
        private const int MaxHpRecover = 1000;
        private const int MinBreakRecover = 0;
        private const int MaxBreakRecover = 1000;
        private const int MinPlayPointRecover = 0;
        private const int MaxPlayPointRecover = 10;
        private const int MinBufStack = 0;
        private const int MaxBufStack = 50;

        /// <summary>
        /// 効果名を取得します。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 能力値ボーナスを取得します。
        /// </summary>
        public StatBonus StatBonus { get; }

        /// <summary>
        /// 光の最大値の増加量を取得します。
        /// </summary>
        public int PlayPointAdder { get; }

        /// <summary>
        /// 毎幕の体力の回復量を取得します。
        /// </summary>
        public int HpRecover { get; }

        /// <summary>
        /// 毎幕の混乱抵抗値の回復量を取得します。
        /// </summary>
        public int BreakRecover { get; }

        /// <summary>
        /// 毎幕の光の回復量を取得します。
        /// </summary>
        public int PlayPointRecover { get; }

        /// <summary>
        /// 毎幕のパワーの付与数を取得します。
        /// </summary>
        public int StrengthStack { get; }

        /// <summary>
        /// 毎幕の虚弱の付与数を取得します。
        /// </summary>
        public int WeakStack { get; }

        /// <summary>
        /// 毎幕の忍耐の付与数を取得します。
        /// </summary>
        public int EnduranceStack { get; }

        /// <summary>
        /// 毎幕の武装解除の付与数を取得します。
        /// </summary>
        public int DisarmStack { get; }

        /// <summary>
        /// 毎幕のクイックの付与数を取得します。
        /// </summary>
        public int QuicknessStack { get; }

        /// <summary>
        /// 毎幕の束縛の付与数を取得します。
        /// </summary>
        public int BindingStack { get; }

        /// <summary>
        /// 毎幕の保護の付与数を取得します。
        /// </summary>
        public int ProtectionStack { get; }

        /// <summary>
        /// 毎幕の脆弱の付与数を取得します。
        /// </summary>
        public int VulnerableStack { get; }

        /// <summary>
        /// 毎幕の混乱保護の付与数を取得します。
        /// </summary>
        public int BreakProtectionStack { get; }

        /// <summary>
        /// 毎幕の火傷の付与数を取得します。
        /// </summary>
        public int BurnStack { get; }

        /// <summary>
        /// 毎幕の麻痺の付与数を取得します。
        /// </summary>
        public int ParalysisStack { get; }

        /// <summary>
        /// 毎幕の出血の付与数を取得します。
        /// </summary>
        public int BleedingStack { get; }

        /// <summary>
        /// <see cref="MoonlightBlessingEffectModel"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="name">効果名。</param>
        /// <param name="hpAdder">体力の最大値の増加量。</param>
        /// <param name="breakGageAdder">混乱抵抗値の最大値の増加量。</param>
        /// <param name="playPointAdder">光の最大値の増加量。</param>
        /// <param name="hpRecover">毎幕の体力の回復量。</param>
        /// <param name="breakRecover">毎幕の混乱抵抗値の回復量。</param>
        /// <param name="playPointRecover">毎幕の光の回復量。</param>
        /// <param name="strengthStack">毎幕のパワーの付与数。</param>
        /// <param name="weakStack">毎幕の虚弱の付与数。</param>
        /// <param name="enduranceStack">毎幕の忍耐の付与数。</param>
        /// <param name="disarmStack">毎幕の武装解除の付与数。</param>
        /// <param name="quicknessStack">毎幕のクイックの付与数。</param>
        /// <param name="bindingStack">毎幕の束縛の付与数。</param>
        /// <param name="protectionStack">毎幕の保護の付与数。</param>
        /// <param name="vulnerableStack">毎幕の脆弱の付与数。</param>
        /// <param name="breakProtectionStack">毎幕の混乱保護の付与数。</param>
        /// <param name="burnStack">毎幕の火傷の付与数。</param>
        /// <param name="paralysisStack">毎幕の麻痺の付与数。</param>
        /// <param name="bleedingStack">毎幕の出血の付与数。</param>
        public MoonlightBlessingEffectModel(
            string name,
            int hpAdder = 0,
            int breakGageAdder = 0,
            int playPointAdder = 0,
            int hpRecover = 0,
            int breakRecover = 0,
            int playPointRecover = 0,
            int strengthStack = 0,
            int weakStack = 0,
            int enduranceStack = 0,
            int disarmStack = 0,
            int quicknessStack = 0,
            int bindingStack = 0,
            int protectionStack = 0,
            int vulnerableStack = 0,
            int breakProtectionStack = 0,
            int burnStack = 0,
            int paralysisStack = 0,
            int bleedingStack = 0)
        {
            Name = name ?? "";

            StatBonus = new StatBonus()
            {
                hpAdder = Mathf.Clamp(hpAdder, MinHpAdder, MaxHpAdder),
                breakGageAdder = Mathf.Clamp(breakGageAdder, MinBreakGageAdder, MaxBreakGageAdder),
            };
            PlayPointAdder = Mathf.Clamp(playPointAdder, MinPlayPointAdder, MaxPlayPointAdder);

            HpRecover = Mathf.Clamp(hpRecover, MinHpRecover, MaxHpRecover);
            BreakRecover = Mathf.Clamp(breakRecover, MinBreakRecover, MaxBreakRecover);
            PlayPointRecover = Mathf.Clamp(playPointRecover, MinPlayPointRecover, MaxPlayPointRecover);

            StrengthStack = Mathf.Clamp(strengthStack, MinBufStack, MaxBufStack);
            WeakStack = Mathf.Clamp(weakStack, MinBufStack, MaxBufStack);
            EnduranceStack = Mathf.Clamp(enduranceStack, MinBufStack, MaxBufStack);
            DisarmStack = Mathf.Clamp(disarmStack, MinBufStack, MaxBufStack);
            QuicknessStack = Mathf.Clamp(quicknessStack, MinBufStack, MaxBufStack);
            BindingStack = Mathf.Clamp(bindingStack, MinBufStack, MaxBufStack);
            ProtectionStack = Mathf.Clamp(protectionStack, MinBufStack, MaxBufStack);
            VulnerableStack = Mathf.Clamp(vulnerableStack, MinBufStack, MaxBufStack);
            BreakProtectionStack = Mathf.Clamp(breakProtectionStack, MinBufStack, MaxBufStack);
            BurnStack = Mathf.Clamp(burnStack, MinBufStack, MaxBufStack);
            ParalysisStack = Mathf.Clamp(paralysisStack, MinBufStack, MaxBufStack);
            BleedingStack = Mathf.Clamp(bleedingStack, MinBufStack, MaxBufStack);
        }

        /// <summary>
        /// 指定したキャラクターのステータスを設定されている値で回復します。
        /// </summary>
        /// <param name="target"></param>
        public void RecoverTo(BattleUnitModel target)
        {
            if (HpRecover > 0)
            {
                target.RecoverHP(HpRecover);
            }

            if (BreakRecover > 0 && !target.IsBreakLifeZero())
            {
                target.breakDetail.RecoverBreak(BreakRecover);
            }

            if (PlayPointRecover > 0)
            {
                target.cardSlotDetail.RecoverPlayPoint(PlayPointRecover);
            }
        }

        /// <summary>
        /// 指定したキャラクターに設定されている状態を付与します。
        /// </summary>
        /// <param name="target"></param>
        public void AddBufsTo(BattleUnitModel target)
        {
            AddBufTo(target, KeywordBuf.Strength, StrengthStack);
            AddBufTo(target, KeywordBuf.Weak, WeakStack);
            AddBufTo(target, KeywordBuf.Endurance, EnduranceStack);
            AddBufTo(target, KeywordBuf.Disarm, DisarmStack);
            AddBufTo(target, KeywordBuf.Quickness, QuicknessStack);
            AddBufTo(target, KeywordBuf.Binding, BindingStack);
            AddBufTo(target, KeywordBuf.Protection, ProtectionStack);
            AddBufTo(target, KeywordBuf.Vulnerable, VulnerableStack);
            AddBufTo(target, KeywordBuf.BreakProtection, BreakProtectionStack);
            AddBufTo(target, KeywordBuf.Burn, BurnStack);
            AddBufTo(target, KeywordBuf.Paralysis, ParalysisStack);
            AddBufTo(target, KeywordBuf.Bleeding, BleedingStack);
        }

        /// <summary>
        /// 指定したキャラクターに指定した状態を付与します。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="bufType"></param>
        /// <param name="stack"></param>
        private void AddBufTo(BattleUnitModel target, KeywordBuf bufType, int stack)
        {
            if (stack <= 0) { return; }

            target.bufListDetail.AddKeywordBufThisRoundByEtc(bufType, stack);
        }

        /// <summary>
        /// 前回適用した効果からの変更量が釣り合うように、指定したキャラクターの能力値を更新します。
        /// </summary>
        /// <param name="target">能力値を更新する対象のキャラクター。</param>
        /// <param name="oldEffect">前回適用した効果。</param>
        public void UpdateStat(BattleUnitModel target, MoonlightBlessingEffectModel oldEffect)
        {
            UpdateHp(target, StatBonus.hpAdder - oldEffect.StatBonus.hpAdder);
            UpdateBreak(target, StatBonus.breakAdder - oldEffect.StatBonus.breakAdder);
            UpdatePlayPoint(target, PlayPointAdder - oldEffect.PlayPointAdder);
        }

        /// <summary>
        /// 指定したキャラクターの体力を指定した差分で更新します。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="hpAdderDiff"></param>
        private void UpdateHp(BattleUnitModel target, int hpAdderDiff)
        {
            int newHp = Mathf.Clamp(
                Convert.ToInt32(target.hp) + hpAdderDiff,
                target.Book.DeadLine + 1,
                target.MaxHp);

            target.SetHp(newHp);
        }

        /// <summary>
        /// 指定したキャラクターの混乱抵抗値を指定した差分で更新します。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="breakAdderDiff"></param>
        private void UpdateBreak(BattleUnitModel target, int breakAdderDiff)
        {
            target.breakDetail.breakGauge = target.breakDetail.GetDefaultBreakGauge();
            if (target.breakDetail.IsBreakLifeZero()) { return; }

            int newBreak = Mathf.Clamp(
                target.breakDetail.breakLife + breakAdderDiff,
                1,
                target.breakDetail.breakGauge);

            target.breakDetail.breakLife = newBreak;
        }

        /// <summary>
        /// 指定したキャラクターの光を指定した差分で更新します。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="playPointAdderDiff"></param>
        private void UpdatePlayPoint(BattleUnitModel target, int playPointAdderDiff)
        {
            int newPlayPoint = Mathf.Clamp(
                target.cardSlotDetail.PlayPoint + playPointAdderDiff,
                0,
                target.cardSlotDetail.GetMaxPlayPoint());

            target.cardSlotDetail.SetPlayPoint(newPlayPoint);
        }

        /// <summary>
        /// 前回適用した効果からの変更量が釣り合うように、指定したキャラクターの状態の付与数を更新します。
        /// </summary>
        /// <param name="target">状態の付与数を更新する対象のキャラクター。</param>
        /// <param name="oldEffect">前回適用した効果。</param>
        public void UpdateBufs(BattleUnitModel target, MoonlightBlessingEffectModel oldEffect)
        {
            UpdateBuf(target, KeywordBuf.Strength, StrengthStack - oldEffect.StrengthStack);
            UpdateBuf(target, KeywordBuf.Weak, WeakStack - oldEffect.WeakStack);
            UpdateBuf(target, KeywordBuf.Endurance, EnduranceStack - oldEffect.EnduranceStack);
            UpdateBuf(target, KeywordBuf.Disarm, DisarmStack - oldEffect.DisarmStack);
            UpdateBuf(target, KeywordBuf.Quickness, QuicknessStack - oldEffect.QuicknessStack);
            UpdateBuf(target, KeywordBuf.Binding, BindingStack - oldEffect.BindingStack);
            UpdateBuf(target, KeywordBuf.Protection, ProtectionStack - oldEffect.ProtectionStack);
            UpdateBuf(target, KeywordBuf.Vulnerable, VulnerableStack - oldEffect.VulnerableStack);
            UpdateBuf(target, KeywordBuf.BreakProtection, BreakProtectionStack - oldEffect.BreakProtectionStack);
            UpdateBuf(target, KeywordBuf.Burn, BurnStack - oldEffect.BurnStack);
            UpdateBuf(target, KeywordBuf.Paralysis, ParalysisStack - oldEffect.ParalysisStack);
            UpdateBuf(target, KeywordBuf.Bleeding, BleedingStack - oldEffect.BleedingStack);
        }

        private void UpdateBuf(BattleUnitModel target, KeywordBuf bufType, int stackDiff)
        {
            if (stackDiff == 0) { return; }
            if (stackDiff > 0)
            {
                target.bufListDetail.AddKeywordBufThisRoundByEtc(bufType, stackDiff);
                return;
            }

            var buf = target.bufListDetail.GetActivatedBuf(bufType);
            if (buf == null) { return; }
            buf.stack += stackDiff;
            if (buf.stack > 0) { return; }
            target.bufListDetail.RemoveBuf(buf);
        }
    }
}
