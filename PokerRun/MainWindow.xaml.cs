using System;
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

namespace PokerRun
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        int cardCount = 45;
        int perCardCount = 14;
        //底牌数量
        int bottomCardCount = 3;
        //是否可以出牌
        bool isStartCard = false;

        int cardWidth = 140;
        int cardHeight = 190;
        List<string> arrPreCard = new List<string> { "03d", "04d", "05d","06d","07d", "08d", "09d", "10d", "11d", "12d", "13d"
        ,"03c", "04c", "05c","06c","07c", "08c", "09c", "10c", "11c", "12c", "13c"
        ,"03b", "04b", "05b","06b","07b", "08b", "09b", "10b", "11b", "12b", "13b"
        ,"03a", "04a", "05a","06a","07a", "08a", "09a", "10a", "11a", "12a"
        ,"01d","02d"};
        List<string> arrNextCard = new List<string>();
        //3张底牌
        List<string> arrBottomCard = new List<string>();

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
            for (int i = 0; i < this.bottomCardCount; i++)
            {
                this.arrBottomCard.Insert(i, this.arrNextCard[i]);
                this.arrNextCard.RemoveAt(i);
            }
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
            //桌面底牌UI
            this.initbottomGrid(true);
            //我方牌片UI
            this.initMyGrid();
            this.initHisGrid(false);
            this.initHerGrid(false);


            //Console.WriteLine("--------------------------------");
            //foreach (String s in A.arrCard)
            //{
            //    Console.WriteLine(s);
            //}
            //Console.WriteLine("--------------------------------");
            //foreach (String s in B.arrCard)
            //{
            //    Console.WriteLine(s);
            //}
            //Console.WriteLine("--------------------------------");
            //A.playCardToken = this.token;
            //foreach (String s in C.arrCard)
            //{
            //    Console.WriteLine(s);
            //}

            //Console.WriteLine("--------------------------------");
            //if (A.playCardToken)
            //{
            //    Console.WriteLine("该我出了");
            //}
            //else
            //{
            //    Console.WriteLine("不是我出");
            //}
            //Console.WriteLine("--------------------------------");
            //A.playCardToken = false;
            //if (A.playCardToken)
            //{
            //    Console.WriteLine("该我出了");
            //}
            //else
            //{
            //    Console.WriteLine("不是我出");
            //}
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
            ,"01d","02d"};
            this.A.arrCard = new List<string> { };
            this.B.arrCard = new List<string> { };
            this.C.arrCard = new List<string> { };
            this.arrBottomCard = new List<string> { };
            this.init();
            this.btnCall.Visibility = Visibility.Visible;
            this.isStartCard = false;
        }

        /// <summary>
        /// 庄家叫牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCall_Click(object sender, RoutedEventArgs e)
        {
            this.labInfo.Content = "";
            this.bottomCanvas.Children.Clear();
            this.A.arrCard.AddRange(this.arrBottomCard);
            this.arrBottomCard.RemoveRange(0, this.bottomCardCount);
            this.initMyGrid();
            //隐藏“庄家叫牌”的按钮
            Button btn = sender as Button;
            btn.Visibility = Visibility.Hidden;
            //做好可以出牌的标记
            this.isStartCard = true;
        }

        /// <summary>
        /// 开始出牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            this.labInfo.Content = "";
            if (!this.isStartCard)
            {
                this.labInfo.Content = "桌面还有底牌，您还不能出牌";
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
            int playCardCount = this.A.arrPlayCard.Count;
            if (playCardCount >= 2 && (A.arrPlayCard.Contains("01d") || A.arrPlayCard.Contains("02d")))
            {
                this.labInfo.Content = "您的牌型不正确s！";
                return;
            }
            switch (playCardCount)
            {
                case 1:
                    this.labInfo.Content = "您的牌型是单牌";
                break;
                case 2:
                    if (CardHelper.isSame(this.A.arrPlayCard))
                    {
                        this.labInfo.Content = "您的牌型是一个对子";
                    }
                    else
                    {
                        this.labInfo.Content = "您的牌型不正确，请出对子！";
                        return;
                    }
                break;
                default:
                break;
            }

            //清除底牌
            this.bottomCanvas.Children.Clear();
            //删除打了出的牌面，需要倒序循环
            int c = this.A.arrPlayCardIndex.Count;
            for (int i = c-1; i >= 0; i--)
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
            
            this.initMyGrid();
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
        }

        private void initHisGrid(bool isCover)
        {
            this.B.arrCard.Sort((x, y) => -x.CompareTo(y));
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
                Canvas.SetBottom(img, bi * 40);
                Canvas.SetRight(img, -140);
                this.hisCanvas.Children.Add(img);
            }
        }

        private void initHerGrid(bool isCover)
        {
            this.C.arrCard.Sort((x, y) => -x.CompareTo(y));
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
