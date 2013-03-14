using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace EmulatorHelper
{
    [TestFixture]
    public class SendPDUTests
    {
        [Test]
        public void Pdu()
        {
            var pdu = "0011000891097891150000AA0BCA309B1D0689C36C7618";
            var telnet = new MinimalisticTelnet.TelnetConnection("localhost", 5554);
            Console.WriteLine(telnet.Read());
            telnet.WriteLine("sms pdu " + pdu);
            Console.WriteLine(telnet.Read());

        }

        [Test]
        public void SendSMS()
        {
            var telnet = new MinimalisticTelnet.TelnetConnection("localhost", 5554);
            telnet.WriteLine("sms send 908719512 \"ABC\"");
            Console.WriteLine(telnet.Read());

        }

        [Test]
        public void SendSMSPDU()
        {
            var telnet = new MinimalisticTelnet.TelnetConnection("localhost", 5554);
            Console.WriteLine(telnet.Read());

            var time = DateTime.Now;
            var pdu = "04 20 08 81 09 78 91 15 00 00" + time.ToPDUTimeStamp().ToHexString();

            var udh = "06 05 04 15 82 00 00";
            var opl = "00 04 04 01 33 CC";
            //var opl = "00 48 1C 01 7F FF EF FF EF FF FB FF FE 40 3F E8 38 2F FF FB FF FE 48 3F A8 38 2F 9F FB FF FE 4C FF A9 FF 2F 8F FA DA DA 4E FF 29 01 2F 80 FA 52 52 5E 7F 69 31 2F BF 7B 07 06 4F FF 69 79 2F BE FB 77 76 47 FF 69 79 2F BE 7B 07 06 47 FE EF 7D EF BE 7B FF FE 47 FC EF 7D E7 BC F1 FF FC 40 F0 EF 7D E7 7C F1 ED BC 21 E7 C9 79 27 98 F1 E5 3C 21 E7 C9 39 27 C8 F1 F0 7C 16 6F 89 39 23 E6 E0 F7 78 15 2F 88 82 23 F3 E0 F0 78 08 3F 04 44 43 D7 E0 FF F8 04 3E 02 28 81 EF C0 7F F0 02 3C 01 39 00 FF 80 3F E0 01 38 00 BA 00 7F 00 1F C0 00 F0 00 7C 00 3E 00 0F 80 FF C0 00 38 00 1C 00 07 FF 55 FF FF FF FF FF FF FF AA 2A F3 87 87 3F 1E 67 0F 54 15 F3 93 9F 3E 4E 27 27 A8 2A F3 87 8F 3E 4E 07 27 54 55 F3 93 9F 3E 0E 47 27 AA FF F3 9B 87 0E 4E 67 0F FF 00 FF FF FF FF FF FF FF 00";
            var userdata = (udh + opl).Replace(" ", "");

            pdu = pdu.Replace(" ", "") + (userdata.Length / 2).ToString("X2") + userdata;


            telnet.WriteLine("sms pdu 0420098109789115F200003130416101304005A2A0702802");
            //telnet.WriteLine("sms pdu " + pdu);
            Console.WriteLine(telnet.Read());
        }

        [Test]
        public void sdfasdf()
        {
            var phonebytes = PhoneNumberUtils.NetworkPortionToCalledPartyBCD("90871951");
            Assert.AreEqual("088109789115", phonebytes.ToHexString());

        }
    }

}


public static class PhoneNumberUtils
{
    public static byte[] NetworkPortionToCalledPartyBCD(string number) {
        var bytes = new List<byte>();

        bytes.Add((byte)(number.Length & 0xFF));
        bytes.Add(0x81);
        
        byte curbyte = 0;
        for (var idx = 0; idx < number.Length; idx++) {
            var num = int.Parse(number.Substring(idx, 1));
            curbyte = (byte)(curbyte | (idx % 2 == 0 ? num : num << 4));
            if (idx % 2 == 1 || idx == number.Length - 1)
            {
                if (idx % 2 == 0) curbyte = (byte)(curbyte | 0xF0);
                bytes.Add(curbyte);
                curbyte = 0;
            }
        }

        return bytes.ToArray();
    }

    public static byte ToSemiOctet(this byte b) {
        return (byte)((b & 0xF0 >> 4) | (b & 0x0F << 4));
    }

    public static byte[] ToPDUTimeStamp(this DateTime dt)
    {
        var result = new byte[7];

        result[0] = (dt.Year % 100).ToPDUOctets();
        result[1] = dt.Month.ToPDUOctets();
        result[2] = dt.Day.ToPDUOctets();
        result[3] = dt.Hour.ToPDUOctets();
        result[4] = dt.Minute.ToPDUOctets();
        result[5] = dt.Second.ToPDUOctets();

        return result;
    }

    private static byte ToPDUOctets(this int b)
    {
        return (byte)(((b % 10) << 4) + ((int)Math.Floor((float)b / 10)));
    }
}