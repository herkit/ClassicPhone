using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class BinaryTools
{
    public static byte[] FromHexString(this string hex)
    {
        try
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
        catch
        {
            throw new ArgumentException("Must be a hex string with even number of characters");
        }
    }

}
