namespace BattleAssistGift.DataAccess
{
    /// <summary>
    /// 毎幕回復するステータスを格納します。
    /// </summary>
    public class JsonRecoveringStatOnRoundStartObject
    {
        /// <summary>
        /// 体力の回復量を取得または設定します。
        /// </summary>
        public int HpRecover { get; set; } = 0;

        /// <summary>
        /// 混乱抵抗値の回復量を取得または設定します。
        /// </summary>
        public int BreakRecover { get; set; } = 0;

        /// <summary>
        /// 光の回復量を取得または設定します。
        /// </summary>
        public int PlayPointRecover { get; set; } = 0;
    }
}
