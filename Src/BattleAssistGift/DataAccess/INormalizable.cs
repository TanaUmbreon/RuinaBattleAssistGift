namespace BattleAssistGift.DataAccess
{
    /// <summary>
    /// インスタンスの状態を任意のルールに従って正規化する機能を実装します。
    /// </summary>
    public interface INormalizable
    {
        /// <summary>
        /// このインスタンスを正規化します。
        /// </summary>
        void Normalize();
    }
}
