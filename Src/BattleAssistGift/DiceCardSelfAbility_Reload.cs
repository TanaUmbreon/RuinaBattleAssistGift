using System;

namespace BattleAssistGift.CardAbilities
{
    /// <summary>
    /// バトルページ効果「設定リロード」
    /// 「月光の祝福」の効果設定を再ロードして、全てのキャラクターに効果を適用しなおす。
    /// </summary>
    public class DiceCardSelfAbility_Reload : DiceCardSelfAbilityBase
    {
        public override string[] Keywords => new[] { "Reload" };

        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            try
            {
                var blessing = new MoonlightBlessingController();
                blessing.ApplyEffectAll();
                blessing.AddReloadCard(unit);
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }

        #region ターゲット可能なキャラクターの設定

        // リロード可能な状況を増やす為、自分自身含めて全てのキャラクターを選択可能にしておく。

        public override bool IsTargetableAllUnit() => true;

        public override bool IsTargetableSelf() => true;

        #endregion
    }
}
