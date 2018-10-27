using System;
using System.Linq;

namespace GenRandomData
{
    public static class RandomData
    {
        /// <summary>
        /// generate random string based on [A-Za-z0-9]
        /// </summary>
        /// <param name="length">random string length</param>
        /// <returns>random string</returns>
        public static string GenRandomString(int length)
        {
            var charArr = Enumerable.Repeat(_character, length).Select(i =>
            i[_random.Next(_character.Length)]).ToArray();

            return new string(charArr);
        }
        /// <summary>
        /// generate random integer [min,max) like Age or CodeNumber
        /// </summary>
        public static int GenRandomInt(int minInclusive , int maxexclusive)
        {
            return _random.Next(minInclusive, maxexclusive);
        }
        /// <summary>
        /// given zero and one , randomly returns either
        /// by default , zero -- "Male" , "One" -- "Female"
        /// </summary>
        /// <param name="zero">represents zero obj , default -- "Male"</param>
        /// <param name="one">represents one obj , default -- "Female"</param>
        /// <returns>zero or one .ToString()</returns>
        public static string GenRandom_0or1(object zero = null, object one = null)
        {
            var result = _random.Next(2);
            if (result == 0)
            {
                return zero?.ToString() ?? "Male";
            }
            else
            {
                return one?.ToString() ?? "Female";
            }
        }

        const string _character = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz";
        static readonly Random _random = new Random();
    }
}
