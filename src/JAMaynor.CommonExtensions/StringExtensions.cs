using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace JAMaynor
{
    public static class StringExtensionMethods
    {

        /// <summary>Indicates whether the specified string is null or a <see cref="string.Empty"/>.</summary>
        public static bool IsNullOrEmpty(this string? seed) { return string.IsNullOrEmpty(seed); }

        /// <summary>Indicates whether the specified string is null or a <see cref="string.Empty"/> or just spacebar characters.</summary>
        public static bool IsNullOrWhitespace(this string? seed) { return string.IsNullOrWhiteSpace(seed); }



        /// <summary>Returns true if the string is formatted as a valid email address.</summary>
        public static bool IsValidEmailAddress(this string? seed)
        {
            if(seed.IsNullOrEmpty()) return false;

            bool isValid = false;
            MailAddress address;

            try
            {
                address = new MailAddress(seed!);
                isValid = true;
            }
            catch { }
            return isValid;
        }

        
        /// <summary>Encodes all the characters in the specified string into a sequence of bytes.</summary>
        public static byte[] ToBytes(this string? startString, Encoding encoding)
        {
            if (startString.IsNullOrEmpty()) return new byte[0];

            if (encoding is null) encoding = Encoding.UTF8;
            return encoding.GetBytes(startString!);
        }
        /// <summary>Encodes all the characters in the specified string into a sequence of bytes.</summary>
        public static byte[] ToBytes(this string? startString) { return ToBytes(startString, Encoding.UTF8); }



        /// <summary>Decodes the bytes in an array into a string using a specified encoder.</summary>
        public static string FromBytes(this byte[]? sourceData, Encoding encoding)
        {
            if (sourceData is null) return string.Empty;
            if (sourceData.Length== 0) return string.Empty;

            if (encoding is null) encoding = Encoding.UTF8;
            return encoding.GetString(sourceData);
        }
        /// <summary>Decodes the bytes in an array into a string using a specified encoder.</summary>
        public static string FromBytes(this byte[]? sourceData) { return FromBytes(sourceData, Encoding.UTF8); }



        /// <summary>Converts plain text to a Base64 encoded string using the UTF8 encoding suitable for the web and obfuscation.</summary>        
        public static string ToBase64String(this string? utf8String)
        {
            if (utf8String.IsNullOrEmpty()) return string.Empty;

            return Convert.ToBase64String(utf8String.ToBytes());
        }
        /// <summary>Decodes a Base64 string rendering UTF-8 plain text.</summary>        
        public static string FromBase64String(this string? base64EncodedData)
        {
            if (base64EncodedData.IsNullOrEmpty()) return string.Empty;

            return Convert.FromBase64String(base64EncodedData!).FromBytes();
        }


        /// <summary>Converts a plain text string to a hexidecimal encoded srting.</summary>        
        public static string ToHexString(this string? utf8String)
        {
            if (utf8String.IsNullOrEmpty()) return string.Empty;

            return Convert.ToHexString(utf8String.ToBytes());
        }
        /// <summary>Converts a hexidecimal encoded srtingto a UTF8 encoded plain text string.</summary>        
        public static string FromHexString(this string? hexEncodedString)
        {
            if (hexEncodedString.IsNullOrEmpty()) return string.Empty;

            return Convert.FromHexString(hexEncodedString).FromBytes();
        }

    }
}
