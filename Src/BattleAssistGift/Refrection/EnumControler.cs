using System;
using System.Reflection;

namespace BattleAssistGift.Refrection
{
	/// <summary>
	/// private な列挙型とそのフィールドにアクセスする機能を提供します。
	/// </summary>
	public class EnumControler
	{
		/// <summary>アクセス対象の列挙型の型宣言</summary>
		private readonly Type _targetType;

		/// <summary>
		/// 指定したアセンブリに含まれる、指定した名前の列挙型を対象とした
		/// <see cref="EnumControler"/> の新しいインスタンスを生成します。
		/// </summary>
		/// <param name="assembly">列挙型が定義されたアセンブリ。</param>
		/// <param name="targetTypeName">列挙型の完全名。名前空間を含めて名前を指定します。</param>
		public EnumControler(Assembly assembly, string targetTypeName)
		{
			if (assembly == null) { throw new ArgumentNullException(nameof(assembly)); }
			if (targetTypeName == null) { throw new ArgumentNullException(nameof(targetTypeName)); }

			_targetType = assembly.GetType(targetTypeName)
				?? throw new TypeLoadException($"列挙型 '{targetTypeName}' の型宣言を取得できません。");
		}

		/// <summary>
		/// 指定した名前に一致する値を取得します。
		/// </summary>
		/// <param name="name">取得する値の名前。</param>
		/// <param name="value">名前に一致した値を out パラメーターとして返します。</param>
		/// <returns>このインスタンス。</returns>
		public EnumControler GetValue(string name, out object value)
		{
			value = Enum.Parse(_targetType, name);
			return this;
		}
	}
}
