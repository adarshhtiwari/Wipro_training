Console.WriteLine("Enter a value:");
string input = Console.ReadLine();
object value;

if (int.TryParse(input, out int i))
    value = i;
else if (double.TryParse(input, out double d))
    value = d;
else
    value = input;


switch (value)
{
    case int intValue:
        Console.WriteLine($"The input is an integer: {intValue}");
        break;
    case double doubleValue:
        Console.WriteLine($"The input is a double: {doubleValue}");
        break;
    case string strValue:
        Console.WriteLine($"The input is a string: {strValue}");
        break;
    default:
        Console.WriteLine("The input is of an unknown type.");
        break;
}
