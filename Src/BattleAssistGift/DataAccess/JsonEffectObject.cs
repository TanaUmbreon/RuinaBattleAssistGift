using BattleAssistGift.Models;

namespace BattleAssistGift.DataAccess
{
    /// <summary>
    /// 各キャラクターに適用できる効果を格納します。
    /// </summary>
    public class JsonEffectObject
    {
        /// <summary>
        /// 効果名を取得または設定します。
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// ステータスに加算するボーナスを取得または設定します。
        /// </summary>
        public JsonStatBonusObject StatBonus { get; set; } = new JsonStatBonusObject();

        /// <summary>
        /// 毎幕回復するステータスを取得または設定します。
        /// </summary>
        public JsonRecoveringStatOnRoundStartObject RecoveringStatOnRoundStart { get; set; } = new JsonRecoveringStatOnRoundStartObject();

        /// <summary>
        /// 毎幕付与する状態とその付与数を取得または設定します。
        /// </summary>
        public JsonAddingBufsOnRoundStartObject AddingBufsOnRoundStart { get; set; } = new JsonAddingBufsOnRoundStartObject();

        /// <summary>
        /// 値が null のプロパティを初期化します。
        /// </summary>
        public void InitializeNullProperties()
        {
            if (Name == null)
            {
                Name = "";
            }

            if (StatBonus == null)
            {
                StatBonus = new JsonStatBonusObject();
            }

            if (RecoveringStatOnRoundStart == null)
            {
                RecoveringStatOnRoundStart = new JsonRecoveringStatOnRoundStartObject();
            }

            if (AddingBufsOnRoundStart == null)
            {
                AddingBufsOnRoundStart = new JsonAddingBufsOnRoundStartObject();
            }
        }

        /// <summary>
        /// 現在のインスタンスの値を元に <see cref="EffectModel"/> のインスタンスを生成します。
        /// </summary>
        /// <returns></returns>
        public EffectModel CreateEffectModel()
        {
            return new EffectModel(
                name: Name,
                hpAdder: StatBonus.HpAdder,
                breakGageAdder: StatBonus.BreakGageAdder,
                playPointAdder: StatBonus.PlayPointAdder,
                hpRecover: RecoveringStatOnRoundStart.HpRecover,
                breakRecover: RecoveringStatOnRoundStart.BreakRecover,
                playPointRecover: RecoveringStatOnRoundStart.PlayPointRecover,
                strengthStack: AddingBufsOnRoundStart.Strength,
                weakStack: AddingBufsOnRoundStart.Weak,
                enduranceStack: AddingBufsOnRoundStart.Endurance,
                disarmStack: AddingBufsOnRoundStart.Disarm,
                quicknessStack: AddingBufsOnRoundStart.Quickness,
                bindingStack: AddingBufsOnRoundStart.Binding,
                protectionStack: AddingBufsOnRoundStart.Protection,
                vulnerableStack: AddingBufsOnRoundStart.Vulnerable,
                breakProtectionStack: AddingBufsOnRoundStart.BreakProtection,
                burnStack: AddingBufsOnRoundStart.Burn,
                paralysisStack: AddingBufsOnRoundStart.Paralysis,
                bleedingStack: AddingBufsOnRoundStart.Bleeding
            );
        }
    }
}
