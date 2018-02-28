﻿using System;
using System.IO;
using System.Runtime.InteropServices;

namespace TestBase
{
    public static class StreamExtensions
    {
        public static byte[] ToBuffer(this Stream stream, int throwIfLengthGreaterThan)
        {
            if (throwIfLengthGreaterThan > 0 && stream.Length > throwIfLengthGreaterThan)
            {
                throw new ArgumentException(string.Format("Stream length {0} was bigger than {1} specified maximum", stream.Length, throwIfLengthGreaterThan), "stream");
            }

            return ToBuffer(stream);
        }

        public static byte[] ToBuffer<T>(this T stream) where T : Stream
        {
            var buffer = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(buffer, 0, (int)stream.Length);
            return buffer;
        }

        public static void Copy(this Stream source, Stream destination)
        {
            Copy(source, destination, 4096);
        }

        public static void Copy(this Stream source, Stream destination, int bufferSize)
        {
            int num;
            byte[] buffer = new byte[bufferSize];
            while ((num = source.Read(buffer, 0, buffer.Length)) != 0)
            {
                destination.Write(buffer, 0, num);
            }
        }

        public static bool IsEqualByValue(this Stream @this, Stream expectedValue, [Optional] string message, params object[] args)
        {
            @this.Position = 0;
            expectedValue.Position = 0;
            var left = new BufferedStream(@this);
            var right = new BufferedStream(expectedValue);
            byte[] bufLeft = new byte[1];
            byte[] bufRight = new byte[1];
            long l = 0;

            while (left.Read(bufLeft, 0, 1) != 0 && right.Read(bufRight, 0, 1) != 0)
            {
                l++;
                if (bufLeft[0] != bufRight[0]) return false;
            }

            return @this.Length == expectedValue.Length;
        }
    }
}