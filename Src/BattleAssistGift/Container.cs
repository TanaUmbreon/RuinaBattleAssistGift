using BattleAssistGift.Models;
using BattleAssistGift.Resources;

namespace BattleAssistGift.DataAccess
{
    /// <summary>
    /// リポジトリのインスタンスを生成するファクトリー クラスです。
    /// </summary>
    public static class Container
    {
        /// <summary>
        /// <see cref="IModSettingsRepository"/> を実装するリポジトリのインスタンスを生成します。
        /// </summary>
        /// <returns><see cref="IModSettingsRepository"/> を実装するリポジトリ。</returns>
        public static IModSettingsRepository CreateModSettingsRepository()
        {
            return new JsonModSettingsRepository(ReferencePath.ModSettingsFile);
        }
    }
}
