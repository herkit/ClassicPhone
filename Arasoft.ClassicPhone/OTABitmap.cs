using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arasoft.ClassicPhone
{
    public class OTABitmap
    {
        private readonly int _width;
        private readonly int _height;
        public bool[,] Bits { get; private set; }

        public OTABitmap(int width, int height)
        {
            _height = height;
            _width = width;
            Bits = new bool[width, height];
        }


        public int Width
        {
            get
            {
                return _width;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
        }

        public static OTABitmap FromUserData(string hexdata)
        {
            return FromUserData(hexdata.FromHexString());
        }

        private static OTABitmap FromUserData(byte[] bytes)
        {
            var w = bytes[1];
            var h = bytes[2];

            var bmp = new OTABitmap(w, h);

            var bit = 0;
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    var mask = 1 << (7 - (bit % 8));
                    var bt = bit / 8 + 4;
                    var state = ((int)bytes[bt] & mask) == mask;
                    bmp.Bits[x, y] = state;
                    bit++;
                }
            }

            return bmp;
        }        
    }

}
