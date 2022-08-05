namespace BattleAssistGift.DataAccess
{
    /// <summary>
    /// 各キャラクター毎に適用する効果名を格納します。
    /// </summary>
    public class JsonApplyingEffectNameObject
    {
        /// <summary>
        /// 1 番目の味方キャラクターに適用する効果名を取得または設定します。
        /// </summary>
        public string Player1 { get; set; } = null;

        /// <summary>
        /// 2 番目の味方キャラクターに適用する効果名を取得または設定します。
        /// </summary>
        public string Player2 { get; set; } = null;

        /// <summary>
        /// 3 番目の味方キャラクターに適用する効果名を取得または設定します。
        /// </summary>
        public string Player3 { get; set; } = null;

        /// <summary>
        /// 4 番目の味方キャラクターに適用する効果名を取得または設定します。
        /// </summary>
        public string Player4 { get; set; } = null;

        /// <summary>
        /// 5 番目の味方キャラクターに適用する効果名を取得または設定します。
        /// </summary>
        public string Player5 { get; set; } = null;

        /// <summary>
        /// 1 番目の敵キャラクターに適用する効果名を取得または設定します。
        /// </summary>
        public string Enemy1 { get; set; } = null;

        /// <summary>
        /// 2 番目の敵キャラクターに適用する効果名を取得または設定します。
        /// </summary>
        public string Enemy2 { get; set; } = null;

        /// <summary>
        /// 3 番目の敵キャラクターに適用する効果名を取得または設定します。
        /// </summary>
        public string Enemy3 { get; set; } = null;

        /// <summary>
        /// 4 番目の敵キャラクターに適用する効果名を取得または設定します。
        /// </summary>
        public string Enemy4 { get; set; } = null;

        /// <summary>
        /// 5 番目の敵キャラクターに適用する効果名を取得または設定します。
        /// </summary>
        public string Enemy5 { get; set; } = null;
    }
}
