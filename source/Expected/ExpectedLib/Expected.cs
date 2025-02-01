using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpectedLib
{
	/// <summary>
	/// Represents a value that may or may not be present.
	/// </summary>
	public class Expected
	{
		/// <summary>
		/// Creates an <see cref="Expected{T, E}"/> with a value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="E"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Expected<T, E> FromValue<T, E>(T value) where E : Exception
		{
			return new Expected<T, E>(value);
		}

		/// <summary>
		/// Creates an <see cref="Expected{T, Exception}"/> with a value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Expected<T, Exception> FromValue<T>(T value) => FromValue<T, Exception>(value);

		/// <summary>
		/// Creates an <see cref="Expected{T, E}"/> with an error.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="E"></typeparam>
		/// <param name="error"></param>
		/// <returns></returns>
		public static Expected<T, E> FromError<T, E>(E error) where E : Exception
		{
			return new Expected<T, E>(Unexpected.From(error));
		}
	}

	/// <summary>
	/// Represents a value that may or may not be present.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="E"></typeparam>
	public class Expected<T, E> : Expected where E : Exception
	{
		/// <summary>
		/// The value of the <see cref="Expected{T, E}"/>.
		/// </summary>
		private readonly T? _value;
		/// <summary>
		/// The error of the <see cref="Expected{T, E}"/>.
		/// </summary>
		private readonly Unexpected<E>? _unexpected;
		/// <summary>
		/// A value indicating whether the <see cref="Expected{T, E}"/> has a value.
		/// </summary>
		private readonly bool _hasValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="Expected{T, E}"/> class with a value.
		/// </summary>
		/// <param name="value"></param>
		internal Expected(T value)
		{
			_value = value;
			_hasValue = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Expected{T, E}"/> class with an error.
		/// </summary>
		/// <param name="unexpected"></param>
		internal Expected(Unexpected<E> unexpected)
		{
			_unexpected = unexpected;
			_hasValue = false;
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="Expected{T, E}"/> has a value.
		/// </summary>
		public bool HasValue => _hasValue;

		/// <summary>
		/// Gets the value of the <see cref="Expected{T, E}"/>.
		/// </summary>
		public T Value
		{
			get
			{
				if (_hasValue) return _value!;
				else
				{
					Debug.Assert(_unexpected != null);
					throw _unexpected.Error;
				}
			}
		}

		/// <summary>
		/// Gets the error of the <see cref="Expected{T, E}"/>.
		/// </summary>
		public E? Error
		{
			get
			{
				if (_hasValue) return null;
				else
				{
					Debug.Assert(_unexpected != null);
					return _unexpected.Error;
				}
			}
		}

		/// <summary>
		/// Gets the value of the <see cref="Expected{T, E}"/> or a default value.
		/// </summary>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public T GetValueOrDefault(T defaultValue) => _hasValue ? _value! : defaultValue;

		/// <summary>
		/// Gets the value of the <see cref="Expected{T, E}"/> or a default value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool TryGetValue(out T? value)
		{
			value = _value;
			return _hasValue;
		}
	}
}
