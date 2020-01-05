using System;
using System.Buffers.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace review_api.Util
{
    public static class GuidToBase64
    {
        private const byte ForwardSlashByte = (byte)'/';
        private const byte PlusByte = (byte)'+';
        private const char Underscore = '_';
        private const char Dash = '-';

        public static string EncodeBase64String(this Guid guid)
        {
            Span<byte> guidBytes = stackalloc byte[16];
            Span<byte> encodedBytes = stackalloc byte[24];

            MemoryMarshal.TryWrite(guidBytes, ref guid);
            Base64.EncodeToUtf8(guidBytes, encodedBytes, out _, out _);

            Span<char> chars = stackalloc char[22];

            for (var i = 0; i < 22; i++)
            {
                chars[i] = (encodedBytes[i]) switch
                {
                    ForwardSlashByte => Dash,
                    PlusByte => Underscore,
                    _ => (char)encodedBytes[i],
                };
            }

            var final = new string(chars);

            return final;
        }
    }
}
