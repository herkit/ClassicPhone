using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Should;

namespace Arasoft.ClassicPhone.Tests
{
    [TestFixture]
    public class OTABitmapTests
    {
        [Test]
        public void Height_and_width_should_be_set()
        {
            var bmp = OTABitmap.FromUserData("0004040133CC");
            bmp.Width.ShouldEqual(4);
            bmp.Height.ShouldEqual(4);
        }

        [Test]
        public void Bits_should_be_set_properly()
        {
            var bmp = OTABitmap.FromUserData("0004040133CC");
            /*
             * - - x x
             * - - x x
             * x x - -
             * x x - -
             */

            bmp.Bits[0, 0].ShouldBeFalse();
            bmp.Bits[1, 0].ShouldBeFalse();
            bmp.Bits[2, 0].ShouldBeTrue();
            bmp.Bits[3, 0].ShouldBeTrue();

            bmp.Bits[0, 1].ShouldBeFalse();
            bmp.Bits[1, 1].ShouldBeFalse();
            bmp.Bits[2, 1].ShouldBeTrue();
            bmp.Bits[3, 1].ShouldBeTrue();

            bmp.Bits[0, 2].ShouldBeTrue();
            bmp.Bits[1, 2].ShouldBeTrue();
            bmp.Bits[2, 2].ShouldBeFalse();
            bmp.Bits[3, 2].ShouldBeFalse();

            bmp.Bits[0, 3].ShouldBeTrue();
            bmp.Bits[1, 3].ShouldBeTrue();
            bmp.Bits[2, 3].ShouldBeFalse();
            bmp.Bits[3, 3].ShouldBeFalse();

        }
    }
}
