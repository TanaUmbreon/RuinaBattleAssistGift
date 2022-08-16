namespace BattleAssistGift.Json
{
    /// <summary>
    /// 能力値ボーナスを格納します。
    /// </summary>
    public class JsonStatBonusObject : JsonObject
    {
        /// <summary>
        /// 体力の増加ボーナス量を取得または設定します。
        /// </summary>
        public int HpAdder { get; set; } = 0;

        /// <summary>
        /// 混乱抵抗値の増加ボーナス量を取得または設定します。
        /// </summary>
        public int BreakGageAdder { get; set; } = 0;

        /// <summary>
        /// 光の増加ボーナス量を取得または設定します。
        /// </summary>
        public int PlayPointAdder { get; set; } = 0;
    }
}
