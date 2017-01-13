using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerRun
{
    class CardHelper
    {
        public const int WRONGCARD = 0;
        public const int DANCARD = 1;
        public const int DUICARD = 2;
        public const int SANDANCARD = 3;


        public static int getCardType(List<string> arrCard)
        {
            //将牌面字符串数组转化为整型数组
            List<int> arrCardTemp = new List<int> { };
            int  playCardCount = arrCard.Count;
            arrCardTemp = handleStringToInt(arrCard);
            int cardType = WRONGCARD;
            switch (playCardCount)
            {
                case 1:
                    cardType =  DANCARD;
                break;
                case 2:
                    if (isSame(arrCardTemp))
                    {
                        cardType = DUICARD;
                    }
                break;
                case 3:
                    if (isSame(arrCardTemp))
                    {
                        cardType = SANDANCARD;
                    }
                break;
                default:
                break;
            }
            return cardType;
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

        /// <summary>
        /// 检查牌面是否相同
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static bool isSame(List<int> arr)
        {
            if (arr.Count <= 0)
            {
                return false;
            }
            bool b = true;
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

       


      
    }
}
