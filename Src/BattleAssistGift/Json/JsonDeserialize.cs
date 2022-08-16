using System.IO;
using Newtonsoft.Json;

namespace BattleAssistGift.Json
{
    /// <summary>
    /// JSON のデータ型を .NET のデータ型にデシリアライズする機能を提供します。
    /// </summary>
    public static class JsonDeserialize
    {
        /// <summary>
        /// 指定したパスの JSON ファイルを読み込み、指定した型にデシリアライズして返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T FromFile<T>(string path) where T : JsonObject
        {
            string text = File.ReadAllText(path);
            T obj = JsonConvert.DeserializeObject<T>(text);
            obj.OnDeserialize();
            return obj;
        }
    }
}
