using System;
using System.Linq;
using System.Reflection;

namespace BattleAssistGift.DataAccess
{
    public static class Normalizer
    {
        /// <summary>
        /// 指定したオブジェクトの状態を正規化します。
        /// </summary>
        /// <param name="obj">正規化するオブジェクト。</param>
        /// <param name="recursively">
        ///   <paramref name="obj"/> に <see cref="INormalizable"/> 
        ///   を実装する非 null なフィールドが存在する時、そのフィールドに対して再帰的に
        ///   <see cref="INormalizable.Normalize"/> メソッドを呼び出す事を示す値。
        /// </param>
        public static void Normalize(INormalizable obj, bool recursively = true)
        {
            if (obj == null) { throw new ArgumentNullException(nameof(obj)); }

            obj.Normalize();
            if (!recursively) { return; }

            Type normalizable = typeof(INormalizable);
            BindingFlags allInstanceMember = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var normalizableFieldValues = obj.GetType().GetFields(allInstanceMember)
                .Where(f => f.FieldType.GetInterface(normalizable.FullName) != null)
                .Select(f => f.GetValue(obj) as INormalizable)
                .Where(v => v != null);

            foreach (var fieldValue in normalizableFieldValues)
            {
                Normalize(fieldValue, recursively);
            }
        }
    }
}
