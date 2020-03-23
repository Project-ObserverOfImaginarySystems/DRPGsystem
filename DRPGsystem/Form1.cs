using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DRPGsystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //第97回円卓会議 kaigi = new 第97回円卓会議();
            //Console.WriteLine(kaigi.getString());
            
            UserBox();

            Config cfg = new Config();
            cfg.SetDefaultWidth(this.Width);
            cfg.SetDefaultHeight(this.Height);
        }
        Ibox box;


        int[] stack;

        public void Stack(int stack)
        {
            if (this.stack == null)
            {
                this.stack = new int[] { };
            }
            Console.WriteLine("Stackします - " + stack);
            Array.Resize(ref this.stack, this.stack.Length + 1);

            Console.WriteLine(GetStackString());


            this.stack[this.stack.Length - 1] = stack;


            Console.WriteLine(GetStackString());
        }


        public void SetStack(int setStack)
        {
            if (this.stack == null)
            {
                this.stack = new int[] { };
            }
            Console.WriteLine("Setします - " + setStack);
            stack[stack.Length - 1] = setStack;
        }
        public void Pop()
        {
            if (this.stack == null)
            {
                this.stack = new int[] { };
            }
            Array.Resize(ref this.stack, this.stack.Length - 1);
        }

        public int GetStack(int n)
        {
            if (this.stack == null)
            {
                this.stack = new int[] { };
            }
            if (n >= GetStackLength()) //0, 1, 2 -> length 3
            {
                return -1;
            }
            return stack[n];
        }

        public int GetStackLength()
        {
            if (this.stack == null)
            {
                this.stack = new int[] { };
            }
            return stack.Length;
        }

        Ibox GetStackBoxContext()
        {
            if (this.stack == null)
            {
                this.stack = new int[] { };
            }
            Ibox buffer = box;
            for(int i = 0;i < GetStackLength();++i)
            {
                buffer = buffer.GetContext(GetStack(i));
            }
            return buffer;
        }

        Ibox GetStackBoxContext(int n)
        {
            if (this.stack == null)
            {
                this.stack = new int[] { };
            }
            Ibox buffer = box;
            for (int i = 0; i < n; ++i)
            {
                if (n > GetStackLength())
                {
                    Console.WriteLine("GetStackBoxContext : インデックスの境界外です。 - " + n);
                }
                else
                {
                    buffer = buffer.GetContext(GetStack(i));
                }
            }
            return buffer;
        }
        /// <summary>
        /// StackBox下のBoxのカーソル状態が２を指しているBoxのIdと同値のものを返す
        /// </summary>
        /// <returns></returns>
        int GetStackCorsor()
        {
            if (this.stack == null)
            {
                this.stack = new int[] { };
            }
            Ibox buf = GetStackBoxContext();
            for (int i = 0; i < buf.GetNumber();++i)
            {
                if(buf.GetContext(i).GetState() == 2)
                {
                    return i;
                }
            }
            return -1;
        }
        String GetStackString()
        {
            string output = "stack : [ ";
            for (int i = 0; i < stack.Length; ++i)
            {
                output += GetStack(i) + " - ";
            }
            output += " ]";
            return output;
        }




        public void UserBox()
        {
            
            MenuBox mb1 = new MenuBox(0, 0, "menu1", "メニュー1", new string[] { "" }, new string[] { "" });
                mb1.AddBox((new Box(0, 0, "なーまーえ", "手動追加Box")));
                MenuBox mb2 = new MenuBox(1, 0, "menu2", "メニュー2", new string[] { "" }, new string[] { "" });
                    mb2.AddBox((new MenuBox(0, 3, "itembox2", "アイテム", null, new string[] { "回復薬", "回復薬グレート", "秘薬"})));
                    mb2.AddBox((new MenuBox(1, 3, "weponbox2", "武器", null, new string[] { "鉄の剣", "金の剣", "魔鉱の剣" })));
            mb1.AddBox(mb2);

            Console.WriteLine("階層テスト : " + mb1.GetContext(1).GetContext(0).GetContext(1).GetContent());


            Console.WriteLine(mb1.ToStringAll(""));
            Console.WriteLine(mb1.GetContentAll());
            int[] output = mb1.SerchName("武器入れ");
            //Console.WriteLine("\n\nserch \"回復薬グレート\" : ");

            box = new Box(0, 0, "all", "まとめボックス");
            Ibox menu = new MenuBox(1, 0, "menu", "メニュー", null, null);
                Ibox item = new MenuBox(0, 0, "item.", "アイテム", null, null);
                    Ibox recivery = new MenuBox(0, 0, "reciavery", "回復薬", null, null);
                        recivery.AddBox(new Box(0, 0, "grade1", "キズぐすり"));
                        recivery.AddBox(new Box(1, 0, "grade2", "すごいキズぐすり"));
                        recivery.AddBox(new Box(2, 0, "grade3", "かんぜんかいふくスプレー"));
                        recivery.AddBox(new Box(3, 0, "grade4", "かいふくスプレー"));
                    Ibox trickDisck  = new MenuBox(1, 0, "trickDisck", "わざマシン", null, null);
                        trickDisck.AddBox(new Box(0, 0, "iwa", "いわくだき"));
                        trickDisck.AddBox(new Box(1, 0, "lock", "ロッククライミング"));
                        trickDisck.AddBox(new Box(2, 0, "wave", "なみのり"));
                    Ibox ball = new MenuBox(2, 0, "ball", "ボール", null, null);
                        ball.AddBox(new Box(0, 0, "monster", "モンスターボール"));
                        ball.AddBox(new Box(1, 0, "super", "スーパーボール"));
                        ball.AddBox(new Box(2, 0, "hyper", "ハイパーボール"));
                Ibox wepon = new MenuBox(1, 0, "wepon", "武器", null, null);
                    Ibox handSword = new MenuBox(0, 0, "handSword", "片手剣", null, null);
                        handSword.AddBox(new Box(0, 0, "hanterKnigh", "ハンターナイフ"));
                        handSword.AddBox(new Box(1, 0, "hanterKnighMk2", "ハンターナイフ改"));
                        handSword.AddBox(new Box(2, 0, "hanterKnighMk3", "ハンターカリンガ"));
                    Ibox Sword = new MenuBox(1, 0, "Sword", "太刀", null, null);
                        Sword.AddBox(new Box(0, 0, "ironSword", "鉄刀"));
                        Sword.AddBox(new Box(1, 0, "ironSwordMk2", "鉄塔[神楽]"));
                        Sword.AddBox(new Box(2, 0, "sanderSword", "真・王我刀[天滅]"));
                    Ibox twinSword = new MenuBox(2, 0, "twinSword", "双剣", null, null);
                        twinSword.AddBox(new Box(0, 0, "twinDagger", "ツインダガー	"));
                        twinSword.AddBox(new Box(1, 0, "twinDaggerMk2", "ツインダガー改"));
                        twinSword.AddBox(new Box(2, 0, "tornadoTomahawk", "トルネードトマホーク"));

            
            item.AddBox(recivery);
            item.AddBox(trickDisck);
            item.AddBox(ball);

            wepon.AddBox(handSword);
            wepon.AddBox(Sword);
            wepon.AddBox(twinSword);

            menu.AddBox(item);
            menu.AddBox(wepon);

            box.AddBox(mb1);
            box.AddBox(menu);
            

            Console.WriteLine(box.ToStringAll(""));
            //Console.WriteLine(box.GetContentAll(""));



            Console.WriteLine("アイテム\"tornadoTomahawk\"を検索します");
            output = box.SerchName("tornadoTomahawk");


            string depth = "検索したアイテム\"tornadoTomahawk\"は[";
            for (int i = 0; i < output.Length; ++i)
            {
                depth += (" " + output[i] + ",");
            }
            depth += " ] にあります";

            Console.WriteLine(depth);

            depth = "検索したアイテム\"tornadoTomahawk\"は[";
            Ibox buffer = new Box(0, 0, null, null);
            buffer = box.GetContext();
            depth += (" " + buffer.GetContent() + ",");
            for (int i = 1; i < output.Length; ++i)
            {
                buffer = buffer.GetContext(output[i]);
                depth += (" " + buffer.GetContent() + ",");
            }

            depth += " ] にあります";

            Console.WriteLine(depth);




            Console.WriteLine("アイテム\"魔鉱の剣\"を検索します");
            output = box.SerchContent("魔鉱の剣");


            depth = "検索したアイテム\"tornadoTomahawk\"は[";
            for (int i = 0; i < output.Length; ++i)
            {
                depth += (" " + output[i] + ",");
            }
            depth += " ] にあります";

            Console.WriteLine(depth);

            depth = "検索したアイテム\"tornadoTomahawk\"は[";
            buffer = new Box(0, 0, null, null);
            buffer = box.GetContext();
            depth += (" " + buffer.GetContent() + ",");
            for (int i = 1; i < output.Length; ++i)
            {
                buffer = buffer.GetContext(output[i]);
                depth += (" " + buffer.GetContent() + ",");
            }

            depth += " ] にあります";

            Console.WriteLine(depth);

            GetStackBoxContext().GetContext(0).SetState(2);//上のとこを選択
            //Stack(0);



            this.Invalidate(new Rectangle(0, 0, 1000, 2000));//再描画
        }
        Config cfg = new Config();
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            Console.WriteLine("描画します");
            e.Graphics.Clear(Color.FromArgb(255, 255, 255, 255));
            if (box != null)
            {
                //Ibox buf = GetStackBoxContext();
                //Console.WriteLine(buf.GetContentAll(""));
                SolidBrush br = new SolidBrush(Color.FromArgb(0, 0, 0));
                SolidBrush menubr = new SolidBrush(Color.FromArgb(150, 150, 150));
                int addx = 0;
                int addy = 0;
                for (int n = 0;n < GetStackLength() + 1;++n)
                {
                    Ibox buf = GetStackBoxContext(n);
                    for (int i = 0; i < buf.GetNumber(); ++i)
                    {
                        int margin = 10;

                        buf.GetContext(i).SetRect(new Rectangle(margin, margin + (30 * i) + (margin * i), 160, 30));
                        

                        buf.GetContext(i).SetReRect();

                    }



                    int remarginX = (int)(buf.GetContext(0).GetReRect().X);
                    int remarginY = (int)(buf.GetContext(0).GetReRect().Y);

                    Console.WriteLine("menu : " + buf.GetContext(buf.GetNumber() - 1).GetReRect().Height + "  :     " + (buf.GetContext(buf.GetNumber() - 1).GetReRect().Y + 20));
                    e.Graphics.FillRectangle(menubr, new Rectangle(0 + addx, 0 + addy, buf.GetContext(0).GetReRect().Width + (remarginX * 2), buf.GetContext(buf.GetNumber() - 1).GetReRect().Height + buf.GetContext(buf.GetNumber() - 1).GetReRect().Y + remarginY) /* サイズの取得 */);



                    for (int i = 0; i < buf.GetNumber(); ++i)
                    {
                        e.Graphics.FillRectangle(br, new Rectangle(buf.GetContext(i).GetReRect().X + addx, buf.GetContext(i).GetReRect().Y + addy, buf.GetContext(i).GetReRect().Width, buf.GetContext(i).GetReRect().Height) /* サイズの取得 */);

                        if (buf.GetContext(i).GetState() == 2)
                        {
                            e.Graphics.DrawString("▶" + buf.GetContext(i).GetContent()/* コンテンツ */, buf.GetContext(i).GetReFont()/* フォント */, Brushes.White /* ブラシ */, new Rectangle(buf.GetContext(i).GetReRect().X + addx, buf.GetContext(i).GetReRect().Y + addy, buf.GetContext(i).GetReRect().Width, buf.GetContext(i).GetReRect().Height) /* サイズの取得 */);
                        }
                        else
                        {
                            e.Graphics.DrawString("▷" + buf.GetContext(i).GetContent()/* コンテンツ */, buf.GetContext(i).GetReFont()/* フォント */, Brushes.White /* ブラシ */, new Rectangle(buf.GetContext(i).GetReRect().X + addx, buf.GetContext(i).GetReRect().Y + addy, buf.GetContext(i).GetReRect().Width, buf.GetContext(i).GetReRect().Height) /* サイズの取得 */);
                        }
                        Console.WriteLine("x : " + buf.GetContext(i).GetReX() + "y : " + buf.GetContext(i).GetReY());



                    }
                    addx += 30;

                    addy += 10;

                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            
            cfg.SetHeight(this.Height);
            cfg.SetWidth(this.Width);
            Console.WriteLine("cfg width " + cfg.GetWidth() + " height " + cfg.GetHeight());
            this.Invalidate(new Rectangle(0, 0, this.Width, this.Height));//再描画

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            switch (e.KeyCode)
            {
                //矢印キーが押されたことを示す

                case Keys.Up:
                    int i = GetStackCorsor();
                    if (i > 0)//カーソルが2の場所が一番上(0)以外
                    {

                        Console.WriteLine(GetStackString());
                        GetStackBoxContext().GetContext(i).SetState(1);//今のとこを1に
                        GetStackBoxContext().GetContext(i - 1).SetState(2);//一個上を2に

                    }
                    break;
                case Keys.Left:
                    break;
                case Keys.Right:
                    break;
                case Keys.Down:
                    i = GetStackCorsor();
                    if (i < GetStackBoxContext().GetNumber() - 1)//カーソルが2の場所が一番下(GetNumber())以外  0 1 2 -> 3
                    {
                        Console.WriteLine(GetStackString());

                        GetStackBoxContext().GetContext(i).SetState(1);//今のとこを1に
                        GetStackBoxContext().GetContext(i + 1).SetState(2);//一個下を2に

                    }
                    break;
                //Tabキーが押されてもフォーカスが移動しないようにする
                case Keys.Enter:
                    if(GetStackBoxContext().GetContext(GetStackCorsor()).GetNumber() > 1)
                    {
                        Console.WriteLine(GetStackCorsor());

                        Console.WriteLine(GetStackString());
                        if (GetStackCorsor() != -1)//対象がなかった時
                        {
                            Stack(GetStackCorsor());//今のカーソルをスタックする
                        }
                        if (GetStackCorsor() == -1)//次の欄に対象がなかった時
                        {
                            GetStackBoxContext().GetContext(0).SetState(2);//上のとこを選択
                        }
                        else
                        {
                            GetStackBoxContext().GetContext(GetStackCorsor()).SetState(1);//現在選ばれているのを1に
                            GetStackBoxContext().GetContext(0).SetState(2);//上のとこを選択
                        }
                    }
                    break;
                case Keys.Escape:
                    if (GetStackLength() > 0)
                    {
                        Pop();
                    }
                    



                    break;
                case Keys.Tab:
                    e.IsInputKey = true;
                    break;
            }
            this.Invalidate(new Rectangle(0, 0, this.Width, this.Height));//再描画
        }
        /*
Graphics g = e.Graphics;
e.Graphics.Clear(Color.FromArgb(255, 255, 255, 255));
//his.g.DrawString("x : " + x + "y : " + y + " ", fnt, Brushes.Blue, 100, 100);


int r = 0;
for (int i = 0; i < 10; ++i)
{
if (t != null)
{

SolidBrush br = new SolidBrush(Color.FromArgb(255, 240, 240));

SolidBrush cframe = new SolidBrush(Color.FromArgb(150, 100, 150, 250));

SolidBrush frame = new SolidBrush(Color.FromArgb(255, 0, 0, 0));

g.FillRectangle(br, t[i].reRect);
if (t[i].cursor >= 1)
{
this.g.DrawString(t[i].GetText(), t[i].reFont, Brushes.Blue, t[i].reRect);
r += 20;
int s = 0;
switch (t[i].cursor)
{
case 2:
//Console.WriteLine("枠を作成");
s = 6;
g.DrawRectangle(new Pen(cframe, s), new Rectangle(t[i].reRect.X + (s / 2), t[i].reRect.Y + (s / 2), t[i].reRect.Width - s, t[i].reRect.Height - s)); ;
goto case 1;//流れ落ち
case 1:
//Console.WriteLine("枠を作成");
s = 2;
g.DrawRectangle(new Pen(frame, s), new Rectangle(t[i].reRect.X + (s / 2), t[i].reRect.Y + (s / 2), t[i].reRect.Width - s, t[i].reRect.Height - s)); ;
break;

}
}
}
}
}

private void Form1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
{

mousepointer = e;

//   this.Invalidate(new Rectangle(100, 100, 200, 20));//再描画

this.Invalidate(new Rectangle(0, 0, 1000, 1000));
Console.WriteLine("再描画");
if (t == null)
{
Console.WriteLine("nullなので作りなおします");
t = new TextBox[10];
int wx = this.Width;
int hy = this.Height;
Config cf = new Config();
int x = (int)(50 * (wx / cf.wD));
int y = (int)(0 * (hy / cf.hD));
int w = (int)(300 * (wx / cf.wD));
int h = (int)(30 * (hy / cf.hD));
fnt = new Font("MS UI Gothic", (h * 2) / 3);
for (int i = 0; i < t.Length; ++i)
{
y += (int)(40 * (hy / 500.0));
t[i] = new TextBox(x, y, w, h, new Font("MS UI Gothic", 20));
}
}
else
{
Console.WriteLine("nullじゃなかった");
}
for (int i = 0; i < t.Length; ++i)
{
t[i].reSise(this.Width, this.Height);
}
/*
for (int i = 0; i < t.Length; ++i)
{
this.Invalidate(t[i].rect);//再描画(テキスト部分のみ)  //なんかわからんけど動くからヨシ!!
}
//////////
Console.WriteLine("width : " + t[0].reRect.Width + "    height : " + t[0].reRect.Height);
for (int i = 0; i < t.Length; ++i)
{
//t[i].SetText("id : " + t[i].Id);

//Console.WriteLine("判定します...");
//Console.WriteLine("x : " + t[i].reRect.X + "y : " + t[i].reRect.Y);
if (t[i].reRect.X <= e.X && e.X <= (t[i].reRect.X + t[i].reRect.Width))
{
//Console.WriteLine("Xの判定に乗りました");
//t[i].SetText("Xが入った");
//x座標が指定範囲に入る
if (t[i].reRect.Y <= e.Y && e.Y <= (t[i].reRect.Y + t[i].reRect.Height))
{
//t[i].SetText("選択されました。");
t[i].cursor = 2;

}
else
{
t[i].cursor = 1;
}
}
else
{
t[i].cursor = 1;
}
}



}
*/
    }

}
