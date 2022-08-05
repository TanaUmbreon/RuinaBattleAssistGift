namespace BattleAssistGift.DataAccess
{
    /// <summary>
    /// 毎幕付与する状態とその付与数を格納します。
    /// </summary>
    public class JsonAddingBufsOnRoundStartObject
    {
        /// <summary>
        /// 毎幕付与するパワーの数を取得または設定します。
        /// </summary>
        public int Strength { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する虚弱の数を取得または設定します。
        /// </summary>
        public int Weak { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する忍耐の数を取得または設定します。
        /// </summary>
        public int Endurance { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する武装解除の数を取得または設定します。
        /// </summary>
        public int Disarm { get; set; } = 0;

        /// <summary>
        /// 毎幕付与するクイックの数を取得または設定します。
        /// </summary>
        public int Quickness { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する束縛の数を取得または設定します。
        /// </summary>
        public int Binding { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する保護の数を取得または設定します。
        /// </summary>
        public int Protection { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する脆弱の数を取得または設定します。
        /// </summary>
        public int Vulnerable { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する混乱保護の数を取得または設定します。
        /// </summary>
        public int BreakProtection { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する火傷の数を取得または設定します。
        /// </summary>
        public int Burn { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する麻痺の数を取得または設定します。
        /// </summary>
        public int Paralysis { get; set; } = 0;

        /// <summary>
        /// 毎幕付与する出血の数を取得または設定します。
        /// </summary>
        public int Bleeding { get; set; } = 0;
    }
}
