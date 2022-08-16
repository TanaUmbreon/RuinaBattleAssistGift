namespace BattleAssistGift.Json
{
    /// <summary>
    /// JSON のオブジェクト型を表す基底クラスです。
    /// </summary>
    public abstract class JsonObject
    {
        /// <summary>
        /// JSON オブジェクトからデシリアライズした時に呼び出され、このインスタンスの状態を正規化します。
        /// </summary>
        public virtual void OnDeserialize() { }
    }
}
