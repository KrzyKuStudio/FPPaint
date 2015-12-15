using NUnit.Framework;
using FPPaint.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPPaint.Converters.Tests
{
    [TestFixture()]
    public class IntegerToStringOnlyNumbersConverterTests
    {
        [Test()]
        public void ConvertTest_Integer2String()
        {
            int integer1 = 1;
            int integer2 = -1;
            int integer3 = 98999999;

            IntegerToStringOnlyNumbersConverter convert = new IntegerToStringOnlyNumbersConverter();
            object obj1 = convert.Convert(integer1, typeof(string), null, null);
            object obj2 = convert.Convert(integer2, typeof(string), null, null);
            object obj3 = convert.Convert(integer3, typeof(string), null, null);

            Assert.AreEqual("1", obj1);
            Assert.AreEqual("-1", obj2);
            Assert.AreEqual("98999999", obj3);
        }

        [Test()]
        public void ConvertBackTest_String2Integer()
        {
            string string1 = "1";
            string string2 = "-1";
            string string3 = "98999999";
            string string4 = "0000";
            string string5 = "1 2";
            string string6 = "5 2b";
            string string7 = "-dup2 25b";

            IntegerToStringOnlyNumbersConverter convert = new IntegerToStringOnlyNumbersConverter();
            object obj1 = convert.ConvertBack(string1, typeof(int), null, null);
            object obj2 = convert.ConvertBack(string2, typeof(int), null, null);
            object obj3 = convert.ConvertBack(string3, typeof(int), null, null);
            object obj4 = convert.ConvertBack(string4, typeof(int), null, null);
            object obj5 = convert.ConvertBack(string5, typeof(int), null, null);
            object obj6 = convert.ConvertBack(string6, typeof(int), null, null);
            object obj7 = convert.ConvertBack(string7, typeof(int), null, null);

            Assert.AreEqual(1, obj1);
            Assert.AreEqual(1, obj2);
            Assert.AreEqual(98999999, obj3);
            Assert.AreEqual(0, obj4);
            Assert.AreEqual(12, obj5);
            Assert.AreEqual(52, obj6);
            Assert.AreEqual(225, obj7);
        }
    }
}