using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janken
{
    class Program
    {
        static void Main(string[] args)
        {
            Cp player1 = new Cp();
            Man player2 = new Man();
            Judge judge = new Judge(player1, player2);
            for (int i = 1; i <= 5; i++)
            {
                judge.Game(i);
            }
            judge.Winner();
            Console.Read();
        }
    }

    abstract class Player
    {
        public int wincount;
        public String name;
        public String Name
        {
            get { return name; }
        }
        public Player()
        {
            this.wincount = 0;
        }
        public abstract int ShowHand();
        public void Count()
        {
            wincount++;
        }
        public int WinCount
        {
            get { return wincount; }
        }
    }

    class Cp : Player
    {
        public Cp()
        {
            this.name = "Computer";
        }
        public override int ShowHand()
        {
            Random rnd = new Random();
            return rnd.Next(0, 3);
        }
    }

    class Man : Player
    {
        public Man()
        {
            Console.Write("あなたの名前を入力して下さい：");
            this.name = Console.ReadLine();
            if (name == "")
            {
                name = "名無し";
            }
        }
        public override int ShowHand()
        {
            int n;
            do
            {
                Console.Write(this.name + "の手を入力して下さい（0:グー, 1:チョキ, 2:パー）：");
                try
                {
                    n = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("数字を入れて下さい");
                    n = -1;
                }
            } while (n != 0 && n != 1 && n != 2);
            return n;
        }
    }

    class Judge
    {
        public Player player1;
        public Player player2;
        public Judge(Player p1, Player p2)
        {
            this.player1 = p1;
            this.player2 = p2;
            Console.WriteLine(player1.Name + "　対　" + player2.Name + "　：じゃんけん開始\n");
        }
        public int hand1, hand2;
        public void Game(int n)
        {
            Console.WriteLine("*** {0}回戦 ***", n);
            hand1 = player1.ShowHand();
            hand2 = player2.ShowHand();
            Judgement(hand1, hand2);
        }
        private void Judgement(int h1, int h2)
        {
            Player winner = player1;
            Console.Write(Hand(h1) + "　対　" + Hand(h2) + "で　");
            if (h1 == h2)
            {
                Console.WriteLine("引き分けです。");
                return;
            }
            else if ((3 + h1 - h2) % 3 == 1)
            {
                winner = player2;
            }
            Console.WriteLine(winner.Name + "の勝ちです。");
            winner.Count();
        }
        private string Hand(int h)
        {
            string[] hs = { "グー", "チョキ", "パー" };
            return hs[h];
        }
        public void Winner()
        {
            int p1, p2;
            p1 = player1.WinCount;
            p2 = player2.WinCount;
            Player finalwinner = player1;
            Console.Write("\n*** 最終結果 ***\n{0} 対 {1} で　", p1, p2);
            if (p1 == p2)
            {
                Console.WriteLine("引き分けです。");
                return;
            }
            else if (p1 < p2)
            {
                finalwinner = player2;
            }
            Console.WriteLine(finalwinner.Name + "の勝ちです。");
        }
    }
}
