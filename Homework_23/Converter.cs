public static class Converter
{
    public static double StringToDouble(string str)
    {
        bool isNegative;

        var (intPart, fractPart) = StringFiltering(str, out isNegative);

        int integer = StringToInt(intPart);
        double fraction = IntToFract(StringToInt(fractPart));

        double number = integer + fraction;

        if (isNegative)
            number *= -1;

        return number;
    }

    public static (string, string) StringFiltering(string input, out bool isNegative)
    {
        List<char> charList = new();

        bool commaFlag = false;
        bool firstZeroFlag = false;
        bool firstZeroNegativeFlag = false;
        isNegative = false;

        for (int i = 0; i < input.Length; i++)
        {
            if (!isNegative && input[0] == '-')
            {
                isNegative = true;
            }

            if (!firstZeroFlag && (input[0] == '.' || input[0] == ','))
            {
                charList.Add('0');
                firstZeroFlag = true;
            }

            if (isNegative && !firstZeroNegativeFlag && (input[1] == '.' || input[1] == ','))
            {
                charList.Add('0');
                firstZeroNegativeFlag = true;
            }

            if (!commaFlag && (input[i] == '.' || input[i] == ','))
            {
                charList.Add(input[i]);
                commaFlag = true;
                continue;
            }

            if (char.IsDigit(input[i]))
            {
                charList.Add(input[i]);
            }
        }

        string numberInString = string.Join("", charList);

        string intPart = numberInString.Split(new char[] { ',', '.' })[0];
        string fractPart = numberInString.Split(new char[] { ',', '.' })[1];

        return (intPart, fractPart);
    }

    public static int StringToInt(string str)
    {
        if (str.Length == 1)
            return (str[0] - '0');

        int next = StringToInt(str.Substring(1));
        int res = str[0] - '0';

        res = (int)(res * Math.Pow(10, str.Length - 1) + next);

        return res;
    }

    public static int StringToInt2(string str)
    {
        int res = 0;

        for (int i = 0; i < str.Length; ++i)
            res = res * 10 + str[i] - '0';

        return res;
    }

    public static double IntToFract(int number)
    {
        int length = (int)Math.Floor(Math.Log10(number) + 1);
        long factor = (long)Math.Pow(10, length);
        double result = (double)number / factor;

        return result;
    }
}

