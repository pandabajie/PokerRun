using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerRun
{
    class Player
    {
        //分发牌片
        public List<string> arrCard { get; set; }
        //打出牌片的值
        public List<string> arrPlayCard { get; set; }
        //打出牌片的索引
        public List<int> arrPlayCardIndex { get; set; }
        //出牌令牌
        public bool playCardToken { get; set; }
        //叫牌令牌
        public bool callCardToken { get; set; }
        //昵称
        public string nickName { get; set; }
    }
}
