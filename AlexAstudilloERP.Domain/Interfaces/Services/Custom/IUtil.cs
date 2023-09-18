using System.Text;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Custom;

public interface IUtil
{
    public string GenerateCode(byte length = 20)
    {
        char[] _chars = new char[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
            'k', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
            'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'o',
            'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y',
            'z', '0', '1', '2', '3', '4', '5', '6', '7', '8',
            '9'
        };
        StringBuilder sb = new();
        Random random = new();
        for (int i = 0; i < length; i++) sb.Append(_chars[random.Next(0, _chars.Length)]);
        return sb.ToString();
    }
}
