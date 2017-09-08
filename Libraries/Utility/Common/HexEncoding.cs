using System;

namespace Kalendar.Utility.Common
{
    /// <summary>
    /// 字节编码
    /// </summary>
    public class HexEncoding
    {
        /// <summary>
        /// Gets the byte count.
        /// </summary>
        /// <param name="hexString">The hex string.</param>
        /// <returns></returns>
        public static int GetByteCount(string hexString)
        {
            int numHexChars = 0;
            // remove all none A-F, 0-9, characters
            for (int i = 0; i < hexString.Length; i++)
            {
                char c = hexString[i];
                if (IsHexDigit(c))
                    numHexChars++;
            }
            // if odd number of characters, discard last character
            if (numHexChars % 2 != 0)
            {
                numHexChars--;
            }
            return numHexChars / 2; // 2 characters per byte
        }


        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="hexString">The hex string.</param>
        /// <returns></returns>
        public static byte[] GetBytes(string hexString)
        {
            int byteLength = hexString.Length / 2;
            var bytes = new byte[byteLength];
            int j = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                var hex = new String(new[] { hexString[j], hexString[j + 1] });
                bytes[i] = HexToByte(hex);
                j = j + 2;
            }
            return bytes;
        }

        /// <summary>
        /// Toes the string.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static string ToString(byte[] bytes)
        {
            string hexString = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                hexString += bytes[i].ToString("X2");
            }
            return hexString;
        }

        /// <summary>
        /// Ins the hex format.
        /// </summary>
        /// <param name="hexString">The hex string.</param>
        /// <returns></returns>
        public static bool InHexFormat(string hexString)
        {
            bool hexFormat = true;

            foreach (char digit in hexString)
            {
                if (!IsHexDigit(digit))
                {
                    hexFormat = false;
                    break;
                }
            }
            return hexFormat;
        }

        /// <summary>
        /// Determines whether [is hex digit] [the specified c].
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>
        /// 	<c>true</c> if [is hex digit] [the specified c]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsHexDigit(Char c)
        {
            int numA = Convert.ToInt32('A');
            int num1 = Convert.ToInt32('0');
            c = Char.ToUpper(c);
            int numChar = Convert.ToInt32(c);
            if (numChar >= numA && numChar < (numA + 6))
                return true;
            if (numChar >= num1 && numChar < (num1 + 10))
                return true;
            return false;
        }

        /// <summary>
        /// Hexes to byte.
        /// </summary>
        /// <param name="hex">The hex.</param>
        /// <returns></returns>
        private static byte HexToByte(string hex)
        {
            if (hex.Length > 2 || hex.Length <= 0)
                throw new ArgumentException("hex must be 1 or 2 characters in length");
            byte newByte = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return newByte;
        }
    }
}
