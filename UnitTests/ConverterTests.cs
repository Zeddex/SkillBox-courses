using NUnit.Framework;
using FluentAssertions;

public class ConverterTests
{
    [Test]
    public void StringFilteringTest1()
    {
        string input = "1,235346";
        string expected = "235346";
        bool isNegative;

        (string integer, string fraction) result = Converter.StringFiltering(input, out isNegative);

        result.fraction.Should().Be(expected);
    }

    [Test]
    public void StringFilteringTest2()
    {
        string input = ".567";
        string expected = "0";
        bool isNegative;

        (string integer, string fraction) result = Converter.StringFiltering(input, out isNegative);

        result.integer.Should().Be(expected);
    }

    [Test]
    public void StringFilteringTest3()
    {
        string input = "10,5,45";
        string expected = "545";
        bool isNegative;

        (string integer, string fraction) result = Converter.StringFiltering(input, out isNegative);

        result.fraction.Should().Be(expected);
    }

    [Test]
    public void StringFilteringTest4()
    {
        string input = "10,5,45";
        string expected = "10";
        bool isNegative;

        (string integer, string fraction) result = Converter.StringFiltering(input, out isNegative);

        result.integer.Should().Be(expected);
    }

    [Test]
    public void StringFilteringTest5()
    {
        string input = ".dfg5ss9";
        string expected = "59";
        bool isNegative;

        (string integer, string fraction) result = Converter.StringFiltering(input, out isNegative);

        result.fraction.Should().Be(expected);
    }

    [Test]
    public void StringFilteringTest6()
    {
        string input = "-1,357";
        bool isNegative;

        (string integer, string fraction) result = Converter.StringFiltering(input, out isNegative);

        isNegative.Should().BeTrue();
    }

    [Test]
    public void StringFilteringTest7()
    {
        string input = "-.555";
        string expected = "0";
        bool isNegative;

        (string integer, string fraction) result = Converter.StringFiltering(input, out isNegative);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(result.integer, expected);
            Assert.IsTrue(isNegative);
        });
    }

    [Test]
    public void StringFilteringTest8()
    {
        string input = "45,56ee9";
        string expectedInt = "45";
        string expectedFract = "569";
        bool isNegative;

        (string integer, string fraction) result = Converter.StringFiltering(input, out isNegative);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(result.integer, expectedInt);
            Assert.AreEqual(result.fraction, expectedFract);
            Assert.IsFalse(isNegative);
        });
    }

    [Test]
    public void StringToIntTest1()
    {
        string input = "123456";
        int expected = 123456;

        int result = Converter.StringToInt2(input);

        result.Should().Be(expected);
    }

    [Test]
    public void StringToIntTest2()
    {
        string input = "001122";
        int expected = 1122;

        int result = Converter.StringToInt2(input);

        result.Should().Be(expected);
    }

    [Test]
    public void StringToIntTest3()
    {
        string input = "0";
        int expected = 0;

        int result = Converter.StringToInt2(input);

        result.Should().Be(expected);
    }

    [Test]
    public void IntToFractTest1()
    {
        int input = 1234;
        double expected = 0.1234;

        double result = Converter.IntToFract(input);

        result.Should().Be(expected);
    }

    [Test]
    public void IntToFractTest2()
    {
        int input = 1234567;
        double expected = 0.1234567;

        double result = Converter.IntToFract(input);

        result.Should().Be(expected);
    }

    [Test]
    public void StringToDoubleTest1()
    {
        string input = "1,57,3p4";
        double expected = 1.5734;

        double result = Converter.StringToDouble(input);

        result.Should().Be(expected);
    }

    [Test]
    public void StringToDoubleTest2()
    {
        string input = "-1,57,3p4";
        double expected = -1.5734;

        double result = Converter.StringToDouble(input);

        result.Should().Be(expected);
    }

    [Test]
    public void StringToDoubleTest3()
    {
        string input = ",57u3";
        double expected = 0.573;

        double result = Converter.StringToDouble(input);

        result.Should().Be(expected);
    }
}