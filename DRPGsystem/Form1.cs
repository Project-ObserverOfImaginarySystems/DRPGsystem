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
                            recivery.GetContext(0).AddBox(new Box(0, 0, "grade1manual", "キズぐすり\nHPを20かいふくする"));

                        recivery.AddBox(new Box(1, 0, "grade2", "すごいキズぐすり"));
                            recivery.GetContext(1).AddBox(new Box(0, 0, "grade1manual", "すごいキズぐすり\nHPを50かいふくする"));

                        recivery.AddBox(new Box(2, 0, "grade3", "かいふくスプレー"));
                            recivery.GetContext(2).AddBox(new Box(0, 0, "grade1manual", "かいふくスプレー\nHPやじょうたい　いじょうもすべてかいふくする"));

                        recivery.AddBox(new Box(3, 0, "grade4", "まんたんスプレー"));
                            recivery.GetContext(3).AddBox(new Box(0, 0, "grade1manual", "まんたんスプレー\nHPを20かいふくする"));

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

            Bitmap canvas = new Bitmap(500, 500);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics sg = Graphics.FromImage(canvas);



            BufferedGraphicsContext currentContext;
            BufferedGraphics bg;

            currentContext = BufferedGraphicsManager.Current;
            if (this.Width > 50 && this.Height > 39)
            {
                bg = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);//このコントロールのグラフィックスを作成し、このウィンドウのサイズを与える。
                                                                                           //return BufferGraphics
            }
            else
            {
                bg = currentContext.Allocate(this.CreateGraphics(), new Rectangle(0, 0, 5, 5));//このコントロールのグラフィックスを作成し、このウィンドウのサイズを与える。
            }


            Console.WriteLine("描画します");

            // g.Clear(Color.FromArgb(250, 250, 250));



            Console.WriteLine("描画します");
            bg.Graphics.Clear(Color.FromArgb(255, 255, 255, 255));
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
                    string[] output = new String[buf.GetNumber()];
                    for (int i = 0; i < buf.GetNumber(); ++i)
                    {
                        int margin = 10;
                        int height = 20;
                        
                        output[i] = buf.GetContext(i).GetContent();

                        string mae = "";
                        string ushiro = "";
                        int length = buf.GetContext(i).GetContent().Length;
                        int l = 0;
                        int kaigyo = 8;
                        Size nopadSize;


                        // width  <  char_width[]   -->   \n
                        //sumWidth < buf.GetContext(i).GetReRect().Width
                        int sumWidth = 0;
                        int row = 1;

                        int escapeId = -1;
                        bool escapeFlag = false;


                        for (int counter = 0;counter < output[i].Length;++counter)
                        {

                            if ("\n" == output[i].Substring(counter, 1) && escapeFlag == false)//改行が意図的に入っていればリセット
                            {
                                sumWidth = 0;
                                escapeId = -1;
                                ++row;
                            }
                            else {

                            nopadSize = TextRenderer.MeasureText(sg, output[i].Substring(counter, 1), buf.GetContext(i).GetReFont(), new Size(500, 500), TextFormatFlags.NoPadding);

                            sumWidth += nopadSize.Width;//横幅を足していく

                                // A 20   B 20   C 20   D 20   E 20    <60>    A, B, C  <60>  ///  D -> <80>

                                /*
                                 * 
                                 * Paddingありで
                                 * [ ABCDEF ] [ GHIJKLM ] [ ASDSAD ]
                                 * みたいな感じに文字列を読み取ってやってみればより高精度になりそう
                                 * 
                                 */

                                if (sumWidth > buf.GetContext(i).GetReRect().Width)
                                {
                                    if (escapeFlag == false)
                                    {
                                        escapeId = counter;
                                        escapeFlag = true;

                                        --counter;

                                        string s1s = output[i].Substring(0, counter);

                                        string s2s = output[i].Substring(counter, output[i].Length - counter);
                                        output[i] = s1s + "\n" + s2s;
                                        ++counter;
                                        ++row;//列を増やす
                                        sumWidth = 0;
                                    }
                                    else
                                    {
                                        if (escapeId == counter)//無限ループ発生
                                        {

                                            escapeId = -1;
                                            escapeFlag = false;


                                            string es1 = output[i].Substring(0, counter);

                                            string es2 = output[i].Substring(counter, output[i].Length - counter);

                                            Console.WriteLine(es1);
                                            Console.WriteLine(es2);
                                            output[i] = es1 + "\n" + es2;
                                            ++counter;

                                            sumWidth = 0;
                                        }
                                        else
                                        {
                                            escapeId = -1;
                                            escapeFlag = false;

                                            --counter;

                                            string ls1 = output[i].Substring(0, counter);

                                            string ls2 = output[i].Substring(counter, output[i].Length - counter);
                                            output[i] = ls1 + "\n" + ls2;
                                            ++counter;
                                            ++row;//列を増やす
                                            sumWidth = 0;
                                        }
                                    }
                                }
                            }

                        }


                        if (i == 0) {
                            buf.GetContext(i).SetRect(new Rectangle(margin, margin + (height * i) + (margin * i), 160, height * (row)));
                        }
                        else
                        {
                            buf.GetContext(i).SetRect(new Rectangle(margin, margin + buf.GetContext(i - 1).GetRect().Y + buf.GetContext(i - 1).GetRect().Height, 160, height * (row)));
                        }
                        


                        buf.GetContext(i).SetReRect();
                        Config cfg = new Config();
                        if (height != 0 && cfg.GetHeight() != 0 && (int)(height * (cfg.GetHeight() / (double)cfg.GetDefaultHeight()) * 2) / 3 != 0) {
                            buf.GetContext(i).SetReFont(new Font("MS UI Gothic", (int)(height * (cfg.GetHeight() / (double)cfg.GetDefaultHeight()) * 2) / 3));
                        }


                    }



                    int reMarginX = (int)(buf.GetContext(0).GetReRect().X);
                    int reMarginY = (int)(buf.GetContext(0).GetReRect().Y);
                    int reHeight = 0;
                    for(int a = 0; a < buf.GetNumber(); ++a)
                    {
                        reHeight += buf.GetContext(a).GetReRect().Height;
                    }

                    Console.WriteLine("menu : " + buf.GetContext(buf.GetNumber() - 1).GetReRect().Height + "  :     " + (buf.GetContext(buf.GetNumber() - 1).GetReRect().Y + 20));
                    bg.Graphics.FillRectangle(menubr, new Rectangle(0 + addx, 0 + addy, buf.GetContext(0).GetReRect().Width + (reMarginX * 2), reHeight + (reMarginY * (2 + (buf.GetNumber() - 1)))) /* サイズの取得 */);



                    for (int i = 0; i < buf.GetNumber(); ++i)
                    {
                        bg.Graphics.FillRectangle(br, new Rectangle(buf.GetContext(i).GetReRect().X + addx, buf.GetContext(i).GetReRect().Y + addy, buf.GetContext(i).GetReRect().Width, buf.GetContext(i).GetReRect().Height) /* サイズの取得 */);

                        if (buf.GetContext(i).GetState() == 2)
                        {
                            bg.Graphics.DrawString("" + output[i]/* コンテンツ */, buf.GetContext(i).GetReFont()/* フォント */, Brushes.White /* ブラシ */, new Rectangle(buf.GetContext(i).GetReRect().X + addx, buf.GetContext(i).GetReRect().Y + addy, buf.GetContext(i).GetReRect().Width, buf.GetContext(i).GetReRect().Height) /* サイズの取得 */);
                        }
                        /*
                         * ">"
                         * " "
                         */
                        else
                        {
                            bg.Graphics.DrawString("" + output[i]/* コンテンツ */, buf.GetContext(i).GetReFont()/* フォント */, Brushes.White /* ブラシ */, new Rectangle(buf.GetContext(i).GetReRect().X + addx, buf.GetContext(i).GetReRect().Y + addy, buf.GetContext(i).GetReRect().Width, buf.GetContext(i).GetReRect().Height) /* サイズの取得 */);
                        }
                        Console.WriteLine("x : " + buf.GetContext(i).GetReX() + "y : " + buf.GetContext(i).GetReY());



                    }
                    addx += 30;

                    addy += 10;

                }

                /*
                //mr.Graphics.FillRectangle(br, new Rectangle(0, 0, 1000, 1000));
                //mr.Graphics.DrawString(output, new Font("MS UI Gothic", 10), wh, new Rectangle(0, 0, 800, 800));
                */
                bg.Render();
                // Renders the contents of the buffer to the specified drawing surface.
                bg.Render(this.CreateGraphics());
                /*
                string s = "DOBON.NET";

                //描画先とするImageオブジェクトを作成する
                Bitmap canvas = new Bitmap(this.Width, this.Height);
                //ImageオブジェクトのGraphicsオブジェクトを作成する
                Graphics g = Graphics.FromImage(canvas);

                //フォントオブジェクトの作成
                Font fnt = new Font("Arial", 25);

                //文字列を描画する
                TextRenderer.DrawText(g, s, fnt, new Point(0, 0), Color.Black);
                //グラフィック - 文字列 - font - 表示場所 - 文字の色


                //文字列を描画するときの大きさを計測する
                Size strSize = TextRenderer.MeasureText(g, s, fnt);
                //       サイズ     <-    グラフィック - 文字列 - Font

                

                //取得した文字列の大きさを使って四角を描画する
                g.DrawRectangle(Pens.Red, 0, 0, strSize.Width, strSize.Height);

                //NoPaddingにして、文字列を描画する
                TextRenderer.DrawText(g, s, fnt, new Point(0, 50), Color.Black, TextFormatFlags.NoPadding);
                    //グラフィック - 文字列 - Font - 表示場所 - 表示する文字の色 - 余白無し

                //大きさを計測して、四角を描画する
                Size nopadSize = TextRenderer.MeasureText(g, s, fnt, new Size(500, 500), TextFormatFlags.NoPadding);


                g.DrawRectangle(Pens.Blue, 0, 50, nopadSize.Width, nopadSize.Height);

                e.Graphics.DrawImage(canvas, new PointF(0, 0));
                */


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
                    if(GetStackBoxContext().GetContext(GetStackCorsor()).GetNumber() > 0)
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
