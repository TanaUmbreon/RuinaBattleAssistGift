using System;
using System.Reflection;

namespace BattleAssistGift.Refrection
{
	/// <summary>
	/// インスタンスの private なメンバーにアクセスする機能を提供します。
	/// </summary>
	public class InstanceControler
	{
		private readonly object _target;
		/// <summary>アクセス対象の列挙型の型宣言</summary>
		private readonly Type _targetType;

		/// <summary>
		/// 指定したインスタンスを対象とした <see cref="InstanceControler"/> の新しいインスタンスを生成します。
		/// </summary>
		/// <param name="target">対象のインスタンス。</param>
		public InstanceControler(object target)
		{
			_target = target ?? throw new ArgumentNullException(nameof(target));
			_targetType = target.GetType();
		}

		/// <summary>
		/// 指定した名前に一致する private フィールドに指定した値を設定します。
		/// </summary>
		/// <param name="fieldName">設定するフィールドの名前。</param>
		/// <param name="value">設定する値。</param>
		/// <returns>このインスタンス。</returns>
		public InstanceControler SetField(string fieldName, object value)
		{
			_targetType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_target, value);
			return this;
		}

		/// <summary>
		/// 指定した名前に一致する private フィールドの値を取得します。
		/// </summary>
		/// <typeparam name="T">フィールドの型。</typeparam>
		/// <param name="fieldName">取得するフィールドの名前。</param>
		/// <param name="value">名前に一致するフィールドの値を out パラメーターとして返します。</param>
		/// <returns>このインスタンス。</returns>
		public InstanceControler GetField<T>(string fieldName, out T value) where T : class
		{
			value = _targetType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(_target) as T;
			return this;
		}
	}
}
