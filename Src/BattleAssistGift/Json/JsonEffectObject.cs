using BattleAssistGift.Models;

namespace BattleAssistGift.Json
{
    /// <summary>
    /// 各キャラクターに適用できる効果を格納します。
    /// </summary>
    public class JsonEffectObject : JsonObject
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
        public override void OnDeserialize()
        {
            Name = Name ?? "";

            StatBonus = StatBonus ?? new JsonStatBonusObject();
            StatBonus.OnDeserialize();

            RecoveringStatOnRoundStart = RecoveringStatOnRoundStart ?? new JsonRecoveringStatOnRoundStartObject();
            RecoveringStatOnRoundStart.OnDeserialize();

            AddingBufsOnRoundStart = AddingBufsOnRoundStart ?? new JsonAddingBufsOnRoundStartObject();
            AddingBufsOnRoundStart.OnDeserialize();
        }

        /// <summary>
        /// 現在のインスタンスの値を元に <see cref="MoonlightBlessingEffectModel"/> のインスタンスを生成します。
        /// </summary>
        /// <returns>このインスタンスの内容と同等の <see cref="MoonlightBlessingEffectModel"/>。</returns>
        public MoonlightBlessingEffectModel CreateEffectModel()
        {
            return new MoonlightBlessingEffectModel(
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
