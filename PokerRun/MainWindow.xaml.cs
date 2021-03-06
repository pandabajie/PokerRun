﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PokerRun
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //一副牌片的总数量
        int cardCount = 45;
        //每人发的牌片数量
        int perCardCount = 15;
        //牌的宽度
        int cardWidth = 140;
        //牌的高度
        int cardHeight = 190;
        //每张牌片的牌面值数组或者可以保存上一轮打下来的所有牌数组
        List<string> arrPreCard = new List<string> {
         "03d", "04d", "05d","06d","07d", "08d", "09d", "10d", "11d", "12d", "13d"
        ,"03c", "04c", "05c","06c","07c", "08c", "09c", "10c", "11c", "12c", "13c"
        ,"03b", "04b", "05b","06b","07b", "08b", "09b", "10b", "11b", "12b", "13b"
        ,"03a", "04a", "05a","06a","07a", "08a", "09a", "10a", "11a", "12a"
        ,"14d","15d"};
        //洗完牌后的牌片数组
        List<string> arrNextCard = new List<string>();
        //底牌或者本轮打下来的牌
        List<string> arrBottomCard = new List<string>();
        //本轮打下牌的牌型
        int bottomCardType;

        Player A = new Player();
        Player B = new Player();
        Player C = new Player();

        public MainWindow()
        {
            InitializeComponent();
            init();
        }

        /// <summary>
        /// 开局初始化
        /// </summary>
        private void init()
        {
            this.shuffleCards();
            this.dealingCards();

        }

        /// <summary>
        /// 洗牌
        /// </summary>
        private void shuffleCards()
        {
            Random rand = new Random();
            for (int i = 0; i < this.cardCount; i++)
            {
                int index = rand.Next(0, this.arrPreCard.Count);
                //重新组合
                this.arrNextCard.Insert(i, this.arrPreCard[index]);
                this.arrPreCard.RemoveAt(index);
            }
        }

        /// <summary>
        /// 发牌
        /// </summary>
        private void dealingCards()
        {

            //抽出3张底牌
            //for (int i = 0; i < this.bottomCardCount; i++)
            //{
            //    this.arrBottomCard.Insert(i, this.arrNextCard[i]);
            //    this.arrNextCard.RemoveAt(i);
            //}
            int j = 0;
            //发牌
            this.A.arrCard = new List<string> { };
            this.B.arrCard = new List<string> { };
            this.C.arrCard = new List<string> { };
            do
            {
                this.A.arrCard.Insert(j, this.arrNextCard[0]);
                this.B.arrCard.Insert(j, this.arrNextCard[1]);
                this.C.arrCard.Insert(j, this.arrNextCard[2]);
                arrNextCard.RemoveRange(0, 3);
                j++;
            } while (j < this.perCardCount);
            //我方牌片UI
            this.initMyGrid();
            this.initHisGrid(true);
            this.initHerGrid(true);
        }

        /// <summary>
        /// 准备出牌，预处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardOut_click(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            int index = (int)img.GetValue(UIManager.CartValueProperty);
            int flag = (int)img.GetValue(UIManager.CartFlagProperty);
            if (flag == 0)
            {
                Canvas.SetTop(img, -20);
                img.SetValue(UIManager.CartFlagProperty, 1);
            }
            else
            {
                Canvas.SetTop(img, 0);
                img.SetValue(UIManager.CartFlagProperty, 0);
            }
            flag = (int)img.GetValue(UIManager.CartFlagProperty);
            Console.WriteLine("此牌的出牌标记是："+flag);
        }

        /// <summary>
        /// 重新洗牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            this.myCanvas.Children.Clear();
            this.hisCanvas.Children.Clear();
            this.herCanvas.Children.Clear();
            this.bottomCanvas.Children.Clear();
            this.arrPreCard = new List<string> { "03d", "04d", "05d","06d","07d", "08d", "09d", "10d", "11d", "12d", "13d"
            ,"03c", "04c", "05c","06c","07c", "08c", "09c", "10c", "11c", "12c", "13c"
            ,"03b", "04b", "05b","06b","07b", "08b", "09b", "10b", "11b", "12b", "13b"
            ,"03a", "04a", "05a","06a","07a", "08a", "09a", "10a", "11a", "12a"
            ,"14d","15d"};
            this.A.arrCard = new List<string> { };
            this.B.arrCard = new List<string> { };
            this.C.arrCard = new List<string> { };
            this.arrBottomCard = new List<string> { };
            this.init();
        }

 

        /// <summary>
        /// 开始出牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            this.labInfo.Content = "";
            //检查出牌令牌
            if (this.A.playCardToken == false)
            {
                this.labInfo.Content = "还没轮到您出牌";
                return;
            }
            //初始化
            this.arrBottomCard = new List<string> { };
            this.A.arrPlayCard = new List<string> { };
            this.A.arrPlayCardIndex = new List<int> { };
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(this.myCanvas); i++)
            {
                var child = VisualTreeHelper.GetChild(this.myCanvas, i);
                if (child is Image)
                {
                    Image img = (child as Image);
                    int index = (int)img.GetValue(UIManager.CartValueProperty);
                    int flag = (int)img.GetValue(UIManager.CartFlagProperty);
                    if (flag == 1)
                    {
                        Console.WriteLine("出牌的索引号是："+index);
                        //将打出的牌放入临时数组
                        this.A.arrPlayCard.Add(this.A.arrCard[index]);
                        this.A.arrPlayCardIndex.Add(index);
                    }
                    
                }
            }
            //检查牌型
            if (this.A.arrPlayCard.Count >= 2 && A.arrPlayCard.Contains("15d"))
            {
                this.labInfo.Content = "您的牌型不正确！";
                return;
            }
            //我的牌型
            Dictionary<int, int> myCardTypeMaxValue = new Dictionary<int, int> { };
            myCardTypeMaxValue =  CardHelper.getCardTypeMaxValue(this.A.arrPlayCard);
            int myCardType = myCardTypeMaxValue.Keys.First();
            int myCardMaxValue = myCardTypeMaxValue.Values.First();
            if (myCardType == CardHelper.WRONGCARD)
            {
                this.labInfo.Content = "您的牌型不正确a！";
                return;
            }
            //如果手里的牌多于三个，则只打三个相同单牌无效
            if (this.A.arrCard.Count > 3 && myCardType == CardHelper.SANDANCARD)
            {
                this.labInfo.Content = "您的牌型不正确3>b！";
                return;
            }

            //如果桌面中央有牌，且自己牌型和桌面牌型匹配的话，则比较大小,
            if (this.bottomCanvas.Children.Count > 0 && myCardType == this.bottomCardType)
            {

            }
            Console.WriteLine("您的牌型是："+ myCardType + "牌的最大值是:"+ myCardMaxValue+"------------");
            //清除底牌
            this.bottomCanvas.Children.Clear();
            //删除打了出的牌面，需要倒序循环
            int c = this.A.arrPlayCardIndex.Count;
            for (int i = c - 1; i >= 0; i--)
            {
                var child = VisualTreeHelper.GetChild(this.myCanvas, this.A.arrPlayCardIndex[i]);
                if (child is Image)
                {
                    Image img = (child as Image);
                    this.myCanvas.Children.Remove((Image)child);
                }
                this.arrBottomCard.Add(this.A.arrPlayCard[i]);
                this.A.arrCard.RemoveAt(this.A.arrPlayCardIndex[i]);
            }
            this.initbottomGrid(false);
            this.bottomCardType = myCardType;
            this.initMyGrid();
            //本轮打完以后，令牌给下一家了
            this.A.playCardToken = false;
            this.B.playCardToken = true;
            if (this.B.arrCard.Count > 0)
            {
                DispatcherTimer readDataTimer = new DispatcherTimer();
                readDataTimer.Tick += new EventHandler(playerBCardAITime);
                readDataTimer.Interval = new TimeSpan(0, 0, 0, 1);
                readDataTimer.Start();
            }
             

        }

        /// <summary>
        /// 下家机器人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerBCardAITime(object sender, EventArgs e)
        {
            if (this.B.arrCard.Count <= 0)
            {
                return;
            }
            DispatcherTimer readDataTimer = sender as DispatcherTimer;
            readDataTimer.Stop();


            Console.WriteLine("他是我下家");
            this.arrBottomCard = new List<string> { };
            this.bottomCanvas.Children.Clear();
            //随便出一张牌
            Random rand = new Random();
            int index = rand.Next(0, this.B.arrCard.Count);
            Console.WriteLine("下家的牌长："+ this.B.arrCard.Count);
            Console.WriteLine("下家的索引号：" + index);
            this.arrBottomCard.Add(this.B.arrCard[index]);
            this.B.arrCard.RemoveAt(index);
            this.initHisGrid(true);
            this.initbottomGrid(false);


            //改变令牌
            this.B.playCardToken = false;
            this.C.playCardToken = true;

            if (this.C.arrCard.Count > 0)
            {
                DispatcherTimer readDataTimer1 = new DispatcherTimer();
                readDataTimer1.Tick += new EventHandler(playerCCardAITime);
                readDataTimer1.Interval = new TimeSpan(0, 0, 0, 1);
                readDataTimer1.Start();
            }

        }

        /// <summary>
        /// 对家机器人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerCCardAITime(object sender, EventArgs e)
        {
            Console.WriteLine("她是我对家");
            this.arrBottomCard = new List<string> { };
            this.bottomCanvas.Children.Clear();
            //随便出一张牌
            Random rand = new Random();
            int index = rand.Next(0, this.C.arrCard.Count);
            this.arrBottomCard.Add(this.C.arrCard[index]);
            this.C.arrCard.RemoveAt(index);
            this.initHerGrid(true);
            this.initbottomGrid(false);
            //改变令牌
            this.C.playCardToken = false;
            this.A.playCardToken = true;

            DispatcherTimer readDataTimer1 = sender as DispatcherTimer;
            readDataTimer1.Stop();
        }


        /// <summary>
        /// 重新整理我方牌局
        /// </summary>
        private void initMyGrid()
        {
            //牌片排序
            this.A.arrCard.Sort((x, y) => -x.CompareTo(y));
            //清除我方桌面
            this.myCanvas.Children.Clear();
            //我方牌片UI
            for (int ai = 0; ai < this.A.arrCard.Count; ai++)
            {
                Image img = new Image();
                img.Height = this.cardHeight;
                img.Width = this.cardWidth;
                //设置牌片的附加属性值  
                img.SetValue(UIManager.CartValueProperty, ai);
                //初始化出牌状态
                img.SetValue(UIManager.CartFlagProperty, 0);
                img.Source = new BitmapImage(new Uri("pack://application:,,,/images/" + this.A.arrCard[ai] + ".png"));
                Canvas.SetLeft(img, ai * 40);
                img.MouseLeftButtonDown += new MouseButtonEventHandler(cardOut_click);
                this.myCanvas.Children.Add(img);
            }
            //出牌令牌
            this.A.playCardToken = true;
        }

        private void initHisGrid(bool isCover)
        {
            this.B.arrCard.Sort((x, y) => -x.CompareTo(y));
            this.hisCanvas.Children.Clear();
            RotateTransform rotateTransform = new RotateTransform(90);//90度
            for (int bi = 0; bi < this.B.arrCard.Count; bi++)
            {
                Image img = new Image();
                img.Height = this.cardHeight;
                img.Width = this.cardWidth;
                if (isCover)
                {
                    img.Source = new BitmapImage(new Uri("pack://application:,,,/images/back.png"));

                }
                else
                {
                    img.Source = new BitmapImage(new Uri("pack://application:,,,/images/" + this.B.arrCard[bi] + ".png"));
                }
                img.RenderTransform = rotateTransform;//图片控件旋转
                Canvas.SetBottom(img, (bi - 1) * 40);
                Canvas.SetRight(img, -140);
                this.hisCanvas.Children.Add(img);
            }
            //出牌令牌
            this.B.playCardToken = false;
        }

        private void initHerGrid(bool isCover)
        {
            this.C.arrCard.Sort((x, y) => -x.CompareTo(y));
            this.herCanvas.Children.Clear();
            for (int ci = 0; ci < this.C.arrCard.Count; ci++)
            {
                Image img = new Image();
                img.Height = this.cardHeight;
                img.Width = this.cardWidth;
                if (isCover)
                {
                    img.Source = new BitmapImage(new Uri("pack://application:,,,/images/back.png"));

                }
                else
                {
                    img.Source = new BitmapImage(new Uri("pack://application:,,,/images/" + this.C.arrCard[ci] + ".png"));
                }
                Canvas.SetRight(img, (ci * 40));
                this.herCanvas.Children.Add(img);
            }
            //出牌令牌
            this.C.playCardToken = false;
        }

        private void initbottomGrid(bool isCover)
        {
            this.C.arrCard.Sort((x, y) => -x.CompareTo(y));
            //底牌UI
            for (int mi = 0; mi < this.arrBottomCard.Count; mi++)
            {
                Image img = new Image();
                img.Height = this.cardHeight;
                img.Width = this.cardWidth;
                if (isCover)
                {
                    img.Source = new BitmapImage(new Uri("pack://application:,,,/images/back.png"));

                }
                else
                {
                    img.Source = new BitmapImage(new Uri("pack://application:,,,/images/" + this.arrBottomCard[mi] + ".png"));
                }
                Canvas.SetLeft(img, mi * 40);
                this.bottomCanvas.Children.Add(img);
            }
        }

    }
}
