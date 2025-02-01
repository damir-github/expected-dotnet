using ExpectedLib;

namespace ExpectedTest
{
	public class ExpectedUnitTest
	{
		[Fact]
		public void TestIntValue()
		{
			int value = 42;
			var result = Expected.FromValue<int, InvalidOperationException>(value);
			Assert.True(result.HasValue);
			Assert.Equal(value, result.Value);
			Assert.Null(result.Error);
		}

		[Fact]
		public void TestIntNullValue()
		{
			var result = Expected.FromValue<int?, InvalidOperationException>(null);
			Assert.True(result.HasValue);
			Assert.Null(result.Value);
			Assert.Null(result.Error);
		}

		[Fact]
		public void TestIntError()
		{
			var result = Expected.FromError<int, InvalidOperationException>(new InvalidOperationException());
			Assert.False(result.HasValue);
			Assert.Throws<InvalidOperationException>(() => result.Value);
			Assert.NotNull(result.Error);
		}

		[Fact]
		public void TestStingValue()
		{
			string value = "Hello";
			var result = Expected.FromValue<string, InvalidOperationException>(value);
			Assert.True(result.HasValue);
			Assert.Equal(value, result.Value);
			Assert.Null(result.Error);
		}

		[Fact]
		public void TestStringNullValue()
		{
			var result = Expected.FromValue<string?, InvalidOperationException>(null);
			Assert.True(result.HasValue);
			Assert.Null(result.Value);
			Assert.Null(result.Error);
		}

		[Fact]
		public void TestStringError()
		{
			var result = Expected.FromError<string, InvalidOperationException>(new InvalidOperationException());
			Assert.False(result.HasValue);
			Assert.Throws<InvalidOperationException>(() => result.Value);
			Assert.NotNull(result.Error);
		}

		[Fact]
		public void TestCustomValue()
		{
			var value = new TestClass(new object());
			var result = Expected.FromValue<TestClass, InvalidOperationException>(value);
			Assert.True(result.HasValue);
			Assert.Equal(value, result.Value);
			Assert.Null(result.Error);
		}

		[Fact]
		public void TestCustomNullValue()
		{
			var result = Expected.FromValue<TestClass?, InvalidOperationException>(null);
			Assert.True(result.HasValue);
			Assert.Null(result.Value);
			Assert.Null(result.Error);
		}

		[Fact]
		public void TestCustomError()
		{
			var result = Expected.FromError<TestClass, InvalidOperationException>(new InvalidOperationException());
			Assert.False(result.HasValue);
			Assert.Throws<InvalidOperationException>(() => result.Value);
			Assert.NotNull(result.Error);
		}

		private class TestClass
		{
			private readonly object _value;

			public TestClass(object value)
			{
				_value = value;
			}

			public object Value => _value;
		}
	}
}