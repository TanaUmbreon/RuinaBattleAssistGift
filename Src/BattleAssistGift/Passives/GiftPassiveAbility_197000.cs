using System;

namespace BattleAssistGift.Passives
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
                    Log.Instance.WarningWithCaller($"指定司書以外が装着しているため効果は発揮しません。 (OwnerSephirah: {owner.UnitData.unitData.OwnerSephirah}, name: '{owner.UnitData.unitData.name}')");
                    return;
                }

                var blessing = new MoonlightBlessing(Factory.CreateModSettingsRepository());
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
