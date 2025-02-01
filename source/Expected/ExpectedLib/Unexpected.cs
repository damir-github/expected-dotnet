using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpectedLib
{
	/// <summary>
	/// Represents an unexpected error.
	/// </summary>
	public class Unexpected
	{
		/// <summary>
		/// Creates an <see cref="Unexpected{E}"/> with an error.
		/// </summary>
		/// <typeparam name="E"></typeparam>
		/// <param name="error"></param>
		/// <returns></returns>
		public static Unexpected<E> From<E>(E error) where E : Exception
		{
			return new Unexpected<E>(error);
		}
	}

	/// <summary>
	/// Represents an unexpected error.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	public class Unexpected<E> : Unexpected where E : Exception
	{
		/// <summary>
		/// The error.
		/// </summary>
		private readonly E _error;

		/// <summary>
		/// Initializes a new instance of the <see cref="Unexpected{E}"/> class with an error.
		/// </summary>
		/// <param name="error"></param>
		internal Unexpected(E error)
		{
			_error = error;
		}

		/// <summary>
		/// Gets the error.
		/// </summary>
		public E Error => _error;
	}
}
