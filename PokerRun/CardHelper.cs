using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerRun
{
    class CardHelper
    {
        //错误牌型
        public const int WRONGCARD = 0;
        //单牌
        public const int DANCARD = 1;
        //一个对子
        public const int YIDUICARD = 2;
        //三张相同牌
        public const int SANDANCARD = 3;
        //炸弹
        public const int ZHADANCARD = 4;
        //三带二
        public const int SANDAIER = 5;
        //单个顺子
        public const int SHUNZICARD = 6;
        //双顺对（4张牌）
        public const int ERDUICARD = 7;
        //三顺对（6张牌）
        public const int SANDUICARD = 8;
        //四顺对（8张牌）
        public const int SIDUICARD = 9;
        //五顺对（10张牌）
        public const int WUDUICARD = 10;
        //六顺对（12张牌）
        public const int LIUDUICARD = 11;
        //七顺对（14张牌）
        public const int QIDUICARD = 12;
        //两个三张（6张牌）
        public const int ERSANSHUNCARD = 13;
        //三个三张（9张牌）
        public const int SANSANSHUNCARD = 14;
        //四个三张(12张牌)
        public const int SISANSHUNCARD = 15;
        //五个三张(15张牌)
        public const int WUSANSHUNCARD = 16;
        //两个三带二(10张牌)
        public const int DOUBLESANDAIER = 17;
        //三个三带二(15张牌)
        public const int SANSANDAIER = 18;


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
        /// 根据牌面值获取牌型+最大值
        /// </summary>
        /// <param name="arr"></param>
        /// <returns>牌型+最大值</returns>
        public static Dictionary<int, int> getCardTypeMaxValue(List<string> arrCard)
        {
            List<int> arr = new List<int> { };
            arr = handleStringToInt(arrCard);
            Dictionary<int, int> ret = new Dictionary<int, int> { };
            //如果牌数大于等于2张，且包含1，2，直接返回牌型错误
            if (arr.Count >= 2 && (arr.Contains(1) || arr.Contains(2)))
            {
                ret[WRONGCARD] = 0;
                return ret;
            }
            Dictionary<int, List<int>> arrTemp = new Dictionary<int, List<int>> { };
            //排序
            arr.Sort();

            //将打出的牌片分组
            for (int i = 0; i < arr.Count; i++)
            {
                if (arrTemp.ContainsKey(arr[i]))
                {
                    arrTemp[arr[i]].Add(arr[i]);
                }
                else
                {
                    arrTemp[arr[i]] = new List<int> { arr[i] };
                }
            }
            //统计牌片，获取结果类似于
            //3:2
            //4:2
            //其中Key键名是牌面值，Value值是这张牌面值的数量
            Dictionary<int, int> cardStat = new Dictionary<int, int> { };
            foreach (KeyValuePair<int, List<int>> kvp in arrTemp)
            {
                cardStat[kvp.Key] = kvp.Value.Count;
                Console.WriteLine(kvp.Key + ":" + kvp.Value.Count);
            }
            //开始检查牌型+最大值
            int firstKey;
            if (arr.Count == 1)
            {
                firstKey = arr[0];
                ret[DANCARD] = firstKey;
            }
            //两张牌，检查对牌 即 88
            else if (arr.Count == 2 && cardStat.Values.Max() == 2)
            {
                firstKey = cardStat.FirstOrDefault(q => q.Value == 2).Key;
                ret[YIDUICARD] = firstKey;
            }
            //三张牌 即 888
            else if (arr.Count == 3 && cardStat.Values.Max() == 3)
            {
                firstKey = cardStat.FirstOrDefault(q => q.Value == 3).Key;
                ret[SANDANCARD] = firstKey;
            }
            else if (arr.Count == 4)
            {
                //炸弹 即 8888
                if (cardStat.Count == 1 && cardStat.Values.Max() == 4)
                {
                    firstKey = cardStat.FirstOrDefault(q => q.Value == 4).Key;
                    ret[ZHADANCARD] = firstKey;
                }
                //双顺对 即 88 99
                else if (cardStat.Count == 2 && cardStat.Values.Max() == 2 && isShunzi(cardStat))
                {
                    ret[ERDUICARD] = cardStat.Keys.Max();
                }
                else
                {
                    ret[WRONGCARD] = 0;
                }
            }
            else if (arr.Count == 5)
            {
                //三带一对对牌或者三带二张单牌  即 888 55 或 888 56
                if (cardStat.Values.Max() == 3 && (cardStat.Count == 2 || cardStat.Count == 3))
                {
                    firstKey = cardStat.FirstOrDefault(q => q.Value == 3).Key;
                    ret[SANDAIER] = firstKey;
                }
                //一个单顺子 即 8 9 10 J Q
                else if (cardStat.Count == 5 && cardStat.Values.Max() == 1 && isShunzi(cardStat))
                {
                    ret[SHUNZICARD] = cardStat.Keys.Max();
                }
                else
                {
                    ret[WRONGCARD] = 0;
                }
            }
            else if (arr.Count == 6)
            {
                //单个顺子
                if (cardStat.Count == 6 && cardStat.Values.Max() == 1 && isShunzi(cardStat))
                {
                    ret[SHUNZICARD] = cardStat.Keys.Max();
                }
                //三连对，三连顺
                else if (cardStat.Count == 3 && cardStat.Values.Max() == 2 && isShunzi(cardStat))
                {
                    ret[SANDUICARD] = cardStat.Keys.Max();
                }
                //两对三张的，即 555 666
                else if (cardStat.Count == 2 && cardStat.Values.Max() == 3 && isShunzi(cardStat))
                {
                    ret[ERSANSHUNCARD] = cardStat.Keys.Max();
                }
                else
                {
                    ret[WRONGCARD] = 0;
                }
            }
            else if (arr.Count == 7)
            {
                //单个顺子 即 8 9 10 J Q K A
                if (cardStat.Count == 7 && cardStat.Values.Max() == 1 && isShunzi(cardStat))
                {
                    ret[SHUNZICARD] = cardStat.Keys.Max();
                }
                else
                {
                    ret[WRONGCARD] = 0;
                }
            }
            else if (arr.Count == 8)
            {
                //单个顺子
                if (cardStat.Count == 8 && cardStat.Values.Max() == 1 && isShunzi(cardStat))
                {
                    ret[SHUNZICARD] = cardStat.Keys.Max();
                }
                //四顺对 即 88 99 1010 JJ
                else if (cardStat.Count == 4 && cardStat.Values.Max() == 2 && isShunzi(cardStat))
                {
                    ret[SIDUICARD] = cardStat.Keys.Max();
                }
                else
                {
                    ret[WRONGCARD] = 0;
                }
            }
            else if (arr.Count == 9)
            {
                //单个顺子
                if (cardStat.Count == 9 && cardStat.Values.Max() == 1 && isShunzi(cardStat))
                {
                    ret[SHUNZICARD] = cardStat.Keys.Max();
                }
                //三对三张的，即 555 666 777
                else if (cardStat.Count == 3 && cardStat.Values.Max() == 3 && isShunzi(cardStat))
                {
                    ret[SANSANSHUNCARD] = cardStat.Keys.Max();
                }
                else
                {
                    ret[WRONGCARD] = 0;
                }
            }
            else if (arr.Count == 10)
            {
                //单个顺子
                if (cardStat.Count == 10 && cardStat.Values.Max() == 1 && isShunzi(cardStat))
                {
                    ret[SHUNZICARD] = cardStat.Keys.Max();
                }
                //两个三带二  即 888 999 3456 或 888 999 3356  或 888 999 3355 或 888 999 101010 J 
                else if (cardStat.Values.Max() == 3 && (cardStat.Count == 3 || cardStat.Count == 4 || cardStat.Count == 5 || cardStat.Count == 6))
                {
                    Dictionary<int, int> dicTemp = new Dictionary<int, int> { };
                    dicTemp = dicPlaneWing(cardStat);
                    if (isShunzi(dicTemp))
                    {
                        ret[DOUBLESANDAIER] = dicTemp.Keys.Max();
                    }
                    else
                    {
                        ret[WRONGCARD] = 0;
                    }
                }
                else if (cardStat.Count == 5 && cardStat.Values.Max() == 2 && isShunzi(cardStat))
                {
                    ret[SIDUICARD] = cardStat.Keys.Max();
                }
                else
                {
                    ret[WRONGCARD] = 0;
                }
            }
            else
            {
                ret[WRONGCARD] = 0;
            }
            return ret;
        }

        /// <summary>
        /// 检查是不是顺子
        /// </summary>
        /// <param name="cardStat">
        /// 类似于  
        /// 3:1
        /// 4:1
        /// 5:1
        /// 6:1
        /// 7:1
        /// 8:1
        /// </param>
        /// <returns></returns>
        public static bool isShunzi(Dictionary<int, int> cardStat)
        {
            bool b = false;
            int min = cardStat.Keys.Min();
            int max = cardStat.Keys.Max();
            if (min + cardStat.Count - 1 == max)
            {
                b = true;
            }
            return b;
        }

        /// <summary>
        /// 提取三带二，即飞机带翅膀的牌型
        /// </summary>
        /// <param name="cardStat"></param>
        /// <returns></returns>
        public static Dictionary<int, int> dicPlaneWing(Dictionary<int, int> cardStat)
        {
            Dictionary<int, int> dicTemp = new Dictionary<int, int> { };
            foreach (KeyValuePair<int, int> kvp in cardStat)
            {
                if (kvp.Value == 3)
                {
                    dicTemp[kvp.Key] = kvp.Value;
                }
            }
            return dicTemp;
        }


    }
}
