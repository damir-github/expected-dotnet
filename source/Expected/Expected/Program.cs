using ExpectedLib;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

static class Program
{
	static void Main()
	{
		Console.WriteLine("Using the TryGet pattern to get the expected value.");

		var result = GetExpectedValue();
		if (result.HasValue)
		{
			Console.WriteLine($"Value: {result.Value}");
		}
		else
		{
			Debug.Assert(result.Error != null);
			Console.WriteLine($"Error: {result.Error.Message}");
		}

		Console.WriteLine("Using the Get pattern to get the expected value.");

		result = GetExpectedValue();
		try
		{
			Console.WriteLine($"Value: {result.Value}");
		}
		catch (InvalidOperationException e)
		{
			Debug.Assert(e != null);
			Console.WriteLine($"Error: {e.Message}");
		}

		Console.WriteLine("Using the TryGet pattern to get the unexpected value.");

		result = GetUnexpectedValue();
		if (result.HasValue)
		{
			Console.WriteLine($"Value: {result.Value}");
		}
		else
		{
			Debug.Assert(result.Error != null);
			Console.WriteLine($"Error: {result.Error.Message}");
		}

		Console.WriteLine("Using the Get pattern to get the unexpected value.");

		result = GetUnexpectedValue();
		try
		{
			Console.WriteLine($"Value: {result.Value}");
		}
		catch (InvalidOperationException e)
		{
			Debug.Assert(e != null);
			Console.WriteLine($"Error: {e.Message}");
		}
	}

	private static Expected<int, InvalidOperationException> GetExpectedValue() => Expected.FromValue<int, InvalidOperationException>(42);
	private static Expected<int, InvalidOperationException> GetUnexpectedValue() => Expected.FromError<int, InvalidOperationException>(new InvalidOperationException("Error retrieving value."));
}