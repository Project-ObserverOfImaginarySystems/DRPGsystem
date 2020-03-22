using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRPGsystem
{
    class 第97回円卓会議
    {
        String title;
        String[,] item;
        public 第97回円卓会議()
        {
            //textbox,checkbox
            this.title = "\n\n\n仕事の再割り振りと楓キュンへの話の共有";
            item = new String [20,6]{
                {"ギミックをもとにシナリオを作成する。", "", "","","",""},
                {"", "", "","","",""},
                {"", "", "","","",""},
                {"", "", "","","",""},
                {"SAKURA", "プログラミング。", "箱作り。","","",""},
                {"楽器", "シナリオを文字に書き起こし、キャラクターを作成していく。", "","","",""},
                {"NASA", "SAKURAが終わる前にC#の勉強をしておく。", "","","",""}, 
                {"かえでキュン", "ガッキーにシナリオのお手伝いをする。", "ギミックも考えてもらう。","","",""},
                {"おこねこ", "SAKURAが終わる前にC#で遊んでおく。", "後　重火器系の攻撃力などのパラメータの設定。","プロやシナで具体的な案が思いつかない時などはリア友に相談してディスコのほうに上げる","",""},
                {"ゆぴ", "実習を優先しながらシナリオ", "","","",""},
                {"俊介", "お兄ちゃん(藁)と一緒にシナリオ考えてみる。", "","","",""},
                
                
                
                {"", "", "","","",""},
                {"", "", "","","",""},
                {"", "", "","","",""},
                {"", "", "","","",""},
                {"", "", "","","",""},
                {"", "", "","","",""},
                {"", "", "","","",""},
                {"", "", "","","",""},
                {"\n\n\n", "", "","","",""}};
        }

       public string getString()
        {
            string output = "";
            output += title;
            for(int i = 0;i < item.GetLength(0); ++i)
            {
                Console.WriteLine("i : " + i);
                if (item[i,0] != "")
                {
                    output += "\n　〇" + item[i, 0];
                }
                for (int n = 1; n < item.GetLength(1); ++n)
                {
                    Console.WriteLine("n : " + n);
                    if (item[i, n] != "")
                    {
                        output += "\n　　→" + item[i, n];
                    }
                }
                
            }
            Console.WriteLine("output : ");
            return output;
        }
    }
}
