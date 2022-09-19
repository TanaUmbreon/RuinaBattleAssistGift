using System;

namespace BattleAssistGift
{
    /// <summary>
    /// 戦闘表象「月光の祝福」のパッシブ。
    /// 全てのキャラクターに対してカスタマイズされた様々な効果を発揮する。
    /// </summary>
    public class GiftPassiveAbility_197000 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            try
            {
                if (!owner.UnitData.unitData.isSephirah)
                {
                    Log.Instance.WarningWithCaller($"Moonlight Blessing is not effective because Assistant Librarian is equipped with it. (unitData: {{ OwnerSephirah: {owner.UnitData.unitData.OwnerSephirah}, name: '{owner.UnitData.unitData.name}' }})");
                    return;
                }

                var blessing = new MoonlightBlessingController();
                blessing.ApplyEffectAll();
                blessing.AddReloadCardAllPlayers();
            }
            catch (Exception ex)
            {
                Log.Instance.ErrorOnExceptionThrown(ex);
            }
        }
    }
}
