using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerRun
{
    class CardHelper
    {
        public static bool isSame(List<string> arrCard)
        {
            if (arrCard.Count <= 0)
            {
                return false;
            }
            bool b = true;
            List<int> arr = new List<int> { };
            arr = handleStringToInt(arrCard);
            for (int i = 0; i < arr.Count; i++)
            {
                var tmp = arr[0];
                Console.WriteLine(arr[i]);
                if (tmp != arr[i])
                {
                    b = false;
                    break;
                }
            }
            return b;
        }

        /// <summary>
        /// 将牌面值的字符串转换成整型
        /// </summary>
        /// <param name="arrCard">牌面值的字符串数组</param>
        /// <returns></returns>
        public static List<int> handleStringToInt(List<string> arrCard)
        {
            List<int> arr = new List<int> { };
            int c = arrCard.Count;
            for (int i = 0; i < c; i++)
            {
                arr.Add(Convert.ToInt32(arrCard[i].Substring(0, 2)));
            }
            return arr;

        }
    }
}
