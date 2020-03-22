using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRPGsystem
{
    class class3
    {
    }

    interface Ibox : Itext, Iname , Icorsor, IgraphicalUserInterface
    {
        Ibox GetContext();//このBox

        Ibox GetContext(int n);//このBoxの中のBox

        void SetContext(Ibox setbox);//このBoxの変更...全ての箱の型を入れる...？

        void SetContext(int n, Ibox setbox);//このBoxの中のBoxの変更

        /// <summary>
        /// 保有している箱の数の取得
        /// </summary>
        /// <returns></returns>
        int GetNumber();
        /// <summary>
        /// 保有している箱の数の代入
        /// </summary>
        /// <returns></returns>
        void SetNumber(int n);//個数一個        
        /// <summary>
        /// 全ての値をString既定の形のStringで返す
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        string ToString(string level);
        /// <summary>
        /// 内包しているすべての値を既定の形のStringで返す
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        string ToStringAll(string level);
        /// <summary>
        /// 実際に表示する値の取得
        /// </summary>
        /// <returns></returns>
        String GetContent();
        /// <summary>
        /// 内包している特定の場所の、実際に表示する値の取得
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        String GetContent(int i);
        /// <summary>
        /// 内包している場所の、実際に表示する全ての値を取得
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        String GetContentAll();

        String GetContentAll(string level);



        /// <summary>
        /// 実際に表示する値の代入
        /// </summary>
        /// <returns></returns>
        void SetContent(string setcontet);
        /// <summary>
        /// 内包している特定の場所へ、実際に表示する値の代入
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        void SetContent(int i, string setcontet);
        /// <summary>
        /// MenuBoxの追加
        /// </summary>
        /// <param name="ibox">The ibox.</param>
        void AddBox(Ibox ibox);
        /// <summary>
        /// nameを入れるすべての値を検索して引っかかるものを深度、場所の行列？で返してくる
        /// </summary>
        /// <returns></returns>
        int[] SerchName(string serchName);
        /// <summary>
        /// nameを入れるすべての値を検索して引っかかるものを深度、場所の行列？で返してくる
        /// </summary>
        /// <returns></returns>
        int[] SerchName(int[] level, string serchName);

        int[] SerchContent(string serchContent);
        /// <summary>
        /// content(Itext)を入れるすべての値を検索して引っかかるものを深度、場所の行列？で返してくる
        /// </summary>
        /// <returns></returns>
        int[] SerchContent(int[] level, string serchContent);

    }

    class Box : Ibox
    {
        String[] text;

        Ibox[] box;

        int number;

        int id;
        string name;
        public Box(int i, int n, string name, string setText)
        {
            SetBox(i, n, name, setText);
            SetBox(n, null,null);
            SetRect(0, 0, 0, 0, 0, 0);
        }
        public Box(int i, int n, string name, string setText, string[] box_name, string[] box_set_text)
        {
            SetBox(i, n, name, setText);
            SetBox(n, box_name, box_set_text);//初期化
            SetRect(0, 0, 0, 0, 0, 0);
        }

        public Box(int i, int n, string name, string setText, int x, int y, int width, int height, int defaultWidth, int defaultHeight)
        {
            SetBox(i, n, name, setText);
            SetRect(x, y, width, height, defaultWidth, defaultHeight);
        }

        protected void SetBox(int i, int n, string name, string setText)
        {
            text = new String[4];
            box = new Box[n];
            SetNumber(n);
            this.SetText(3, n.ToString());
            if (name == null || name == "")
            {
                SetName("ヌルポ");
                this.SetText(0, "ヌルポ");
            }
            else
            {
                SetName(name);
                this.SetText(0, name);
            }
            if (setText == null || setText == "")
            {
                this.SetText(2, "ヌルポ");
            }
            else
            {
                this.SetText(2, setText);
            }
            this.SetText(1, "" + i);
            SetId(i);
        }
        /// <summary>
        /// メニュー用
        /// </summary>
        /// <param name="i"></param>
        /// <param name="name"></param>
        /// <param name="set_text"></param>
        protected void SetBox(int i, string[] name, string[] set_text)
        {
            for (int c = 0; c < i; ++c)
            {
                if (name == null)
                {
                    name = new String[i];
                }
                if (name[c] == null || name[c] == "")
                {
                    name[c] = ("Iname" + (c + 1));
                }
                if (set_text == null)
                {
                    set_text = new String[i];
                }
                if (set_text[c] == null || set_text[c] == "")
                {
                    set_text[c] = ("Item" + (c + 1));
                }

                box[c] = new Box(c, 0, name[c], set_text[c]);
            }
        }

        protected void SetRect(int x, int y, int width, int height, int defaultWidth, int defaultHeight)
        {
            if(x == 0)
            {
                x = 5;
            }
            if (y == 0)
            {
                y = 5 + (55 * GetId());
                Console.WriteLine("y : " + y);
            }
            if(width == 0)
            {
                width = 200;
            }
            if (height == 0)
            {
                height = 50;
            }

            rect = new Rectangle(x, y, width, height);

            Config cfg = new Config();

            if (defaultWidth == 0)
            {
                defaultWidth = cfg.GetDefaultWidth();
            }
            if (defaultHeight == 0)
            {
                defaultHeight = cfg.GetDefaultHeight();//デフォルト値の自動入力
            }

            this.defaultWidth = defaultWidth;
            this.defaultHeight = defaultHeight;
            SetFont(null);
            
        }





        public Ibox GetContext()
        {
            return this;
        }
        public Ibox GetContext(int n)
        {
            if (0 <= n && n < GetNumber())
            {
                return box[n];
            }
            Console.WriteLine("TextBox : GetContext(" + n + ") : インデックスの境界外です。");
            return this;
        }
        public void SetContext(Ibox setbox)//削除する場合はdelete
        {
            if (setbox != null)
            {
                for (int i = 0; i < text.Length; ++i)
                {
                    if (setbox.GetText(i) == null)
                    {
                        SetText(i, "");
                    }
                    else
                    {
                        SetText(i, setbox.GetText(i));
                    }
                }
                for (int i = 0; i < setbox.GetNumber(); ++i)
                {
                    if (GetNumber() != setbox.GetNumber())
                    {
                        Array.Resize(ref box, setbox.GetNumber() - 1);//サイズが異なる場合に作成しなおす
                    }
                    if (setbox.GetContext(i) == null)
                    {
                        box[i] = new Box(i, 0, "", "");
                    }
                    else
                    {
                        box[i] = setbox.GetContext(i);
                    }
                }
                this.SetNumber(setbox.GetNumber());
                this.SetId(setbox.GetId());
                this.SetName(setbox.GetName());
            }


        }

        public void SetContext(int n, Ibox setbox)
        {
            if (0 <= n && n < GetNumber())
            {
                box[n] = setbox;
            }
            Console.WriteLine("TextBox : SetContext(" + n + "] : インデックスの境界外です。");

        }
        public int GetNumber()
        {
            return this.number;
        }

        public void SetNumber(int n)
        {
            this.number = n;
        }

        public string ToString(string level)
        {
            Console.WriteLine(this.GetType() + "のtoString()");
            string output = "";
            output += "\n" + level + "ボックスName　name : " + GetName();
            output += "\n" + level + "ボックスId　id: " + GetId();
            output += "\n" + level + "保有ボックス数　number : " + GetNumber();
            output += "\n" + level + "ボックステキスト Itext[2] : " + GetText(2);
            return output + "\n";

        }

        public string ToStringAll(string level)
        {
            Console.WriteLine(this.GetType() + "のtoStringall()");
            String output = "";
            if (GetNumber() == 0)
            {
                return ToString(level);
            }
            else
            {
                level += GetContent() + " / ";
                for (int i = 0; i < GetNumber(); ++i)
                {
                    output += GetContext(i).ToStringAll(level);
                }

            }
            return output + "\n";
        }

        public string GetContent()
        {
            return this.GetText(2);
        }

        public string GetContent(int n)
        {
            if (0 <= n && n < this.GetNumber())
            {
                return this.GetContext(n).GetText(2);
            }
            else
            {
                Console.WriteLine(this.GetType() + " : GetContent(" + n + ") : インデックスの境界外です。");
                return "";
            }

        }
        public string GetContentAll()
        {
            Console.WriteLine(this.GetType() + "のgetContentall()");
            String output = "";
            if (GetNumber() == 0)
            {
                return GetContent() + "\n";
            }
            else
            {
                for (int i = 0; i < GetNumber(); ++i)
                {
                    output += "./" + GetContext(i).GetContentAll();
                }

            }
            return output + "\n";
        }

        public string GetContentAll(string level)
        {

            Console.WriteLine(this.GetType() + "のgetContentall(" + level + ")");
            String output = "";
            if (GetNumber() == 0)
            {
                return level + " /" + GetContent() + "\n";
            }
            else
            {
                for (int i = 0; i < GetNumber(); ++i)
                {
                    output += GetContext(i).GetContentAll(level + " /" + GetContent());
                }

            }
            return output;
        }

        public void SetContent(string setcontent)
        {
            this.SetText(2, setcontent);
        }

        public void SetContent(int n, string setcontent)
        {
            if (0 <= n && n < this.GetNumber())
            {
                this.box[n].SetText(2, setcontent);
            }
            else
            {
                Console.WriteLine(this.GetType() + " : SetContent(" + n + ", " + setcontent + "インデックスの境界外です。");
            }

        }

        public void AddBox(Ibox add_box)
        {
            if (add_box != null)
            {
                if (GetNumber() == 0)
                {//所持数がゼロの場合
                    SetId(0);
                }
                else
                {
                    if (add_box.GetId() != GetContext(GetNumber() - 1).GetId() + 1)//現在保有しているBoxの最大Id + 1に設定する
                    {
                        add_box.SetId(GetContext(GetNumber() - 1).GetId() + 1);
                        Console.WriteLine("id 変更 : " + (GetContext(GetNumber() - 1).GetId() + 1));
                    }
                }
            }
            else
            {
                add_box = new Box(GetNumber() + 1, 0, "自動作成されたヌルポ", "自動作成されたヌルポ");
            }
            Array.Resize(ref box, box.Length + 1);
            box[GetNumber()] = (Ibox)(add_box);
            SetNumber(GetNumber() + 1);
        }



        // Iname
        public int GetId()
        {
            return id;
        }
        public void SetId(int setId)
        {
            id = setId;
        }
        public string GetName()
        {
            return name;
        }
        public void SetName(string setName)
        {
            name = setName;
        }



        // Itext
        public string GetText()
        {
            return text[0];
        }

        public string GetText(int i)
        {
            if (0 <= i && i < text.Length)
            {
                return text[i];
            }
            else
            {
                Console.WriteLine(this.GetType() + " : GetText(" + i + ") : インデックスの境界外です。");
                return "";
            }
        }
        public void SetText(string tex)
        {
            text[0] = tex;
        }
        public void SetText(int i, string tex)
        {
            if (0 <= i && i < text.Length)
            {
                text[i] = tex;
            }
            else
            {
                Console.WriteLine(this.GetType() + " : SetText(" + i + " , + " + tex + ") : インデックスの境界外です。");
            }

        }

        int corsorState = 1;
        

        // Icorsor
        public bool GetEnable()
        {
            throw new NotImplementedException();
        }

        public void SetEnable()
        {
            throw new NotImplementedException();
        }

        public void SetState(int i)
        {
            corsorState = i;
        }

        public int GetState()
        {
            return corsorState;
        }

        public int GetEventState()
        {
            throw new NotImplementedException();
        }

        public string GetEvent()
        {
            throw new NotImplementedException();
        }




        // GUI

        int defaultWidth = 0;
        int defaultHeight = 0;

        Rectangle rect = new Rectangle(0, 0, 0, 0);

        Rectangle reRect = new Rectangle(0, 0, 0, 0);

        bool setReSize = false;

        Font font;

        Font reFont;

        public void SetDefault(int width, int height)
        {
            defaultWidth = width;
            defaultHeight = height;
        }

        public int GetDefaultWidth()
        {
            return defaultWidth;
        }

        public int GetDefaultHeight()
        {
            return defaultHeight;
        }

        public Rectangle GetRect()
        {
            return rect;
        }


        public int GetX()
        {
            return rect.X;
        }

        public int GetY()
        {
            return rect.Y;
        }

        public int GetWidth()
        {
            return rect.Width;
        }

        public int GetHeight()
        {
            return rect.Height;
        }

        public void SetRect(Rectangle setRect)
        {
            rect = setRect;
            SetFont(null);
        }
        public void SetRect(Rectangle setRect, Font font)
        {
            rect = setRect;
            SetFont(font);
        }

        public Rectangle GetReRect()
        {
            if(setReSize == false)
            {
                Console.WriteLine(GetContent() + "Boxのリサイズ値を決めてください。");
                SetReRect();
                setReSize = false;
            }
            return reRect;
        }
        public int GetReX()
        {
            return reRect.X;
        }

        public int GetReY()
        {
            return reRect.Y;
        }

        public int GetReWidth()
        {
            return reRect.Width;
        }

        public int GetReHeight()
        {
            return reRect.Height;
        }

        public void SetReRect(int width, int height)
        {
            reRect = new Rectangle((int)(rect.X * ((double)width) / defaultWidth), (int)(rect.Y * ((double)height) / defaultHeight), (int)(rect.Width * ((double)width) / defaultWidth), (int)(rect.Height * ((double)height / defaultHeight)));
            SetReFont(null);
            setReSize = true;
        }
        public void SetReRect(int width, int height, Font reFont)
        {
            SetReRect(width, height);
            SetReFont(reFont);
            setReSize = true;
        }

        public void SetReRect()
        {
            Config cfg = new Config();
            Console.WriteLine("cfg wid " + cfg.GetWidth());
            reRect = new Rectangle((int)(rect.X * ((double)cfg.GetWidth()) / defaultWidth), (int)(rect.Y * ((double)cfg.GetHeight())/ defaultHeight), (int)(rect.Width * ((double)cfg.GetWidth()) / defaultWidth), (int)(rect.Height * ((double)cfg.GetHeight() / defaultHeight)));
            SetReFont(null);
            setReSize = true;
        }

        public void SetFont(Font font)
        {
            if (font == null)
            {
                this.font = new Font("MS UI Gothic", (rect.Height * 2) / 3);
            }
            else
            {
                this.font = font;
            }
        }

        public Font GetFont()
        {
            return this.font;
        }

        public void SetReFont(Font font)
        {
            if (font == null)
            {
                this.reFont = new Font("MS UI Gothic", (reRect.Height * 2) / 3);
            }
            else
            {
                this.reFont = font;
            }
        }

        public Font GetReFont()
        {
            return this.reFont;
        }








        public int[] SerchName(string serchName)
        {
            return SerchName(new int[] { }, serchName);
        }

        public int[] SerchName(int[] level, string serchName)
        {
            int[] output = level;
            if (GetName() == serchName)
            {
                Array.Resize(ref output, level.Length + 1);
                output[level.Length] = GetId();
                return output;
            }
            else
            {
                //Console.WriteLine(GetId() + " : " + GetName() + "  で見つかりませんでしたが、検索します");
                for (int i = 0; i < GetNumber(); ++i)
                {
                    Array.Resize(ref output, level.Length + 1);
                    output[level.Length] = GetId();
                    if (output.Length < GetContext(i).SerchName(output, serchName).Length)
                    {

                         //Console.WriteLine(GetId() + " : " + GetName() + " : の保有ボックス内で見つかりました");
                         return GetContext(i).SerchName(output, serchName);
                    }
                }
            }
            //Console.WriteLine(GetId() + " : " + GetName() + "  で見つかりませんでした");
            return level;
        }

        public int[] SerchContent(string serchContent)
        {
            return SerchContent(new int[] { }, serchContent);
        }

        public int[] SerchContent(int[] level, string serchContent)
        {
            int[] output = level;
            if (GetContent() == serchContent)
            {
                Array.Resize(ref output, level.Length + 1);
                output[level.Length] = GetId();
                return output;
            }
            else
            {
                //Console.WriteLine(GetId() + " : " + SerchContent() + "  で見つかりませんでしたが、検索します");
                for (int i = 0; i < GetNumber(); ++i)
                {
                    Array.Resize(ref output, level.Length + 1);
                    output[level.Length] = GetId();
                    if (output.Length < GetContext(i).SerchContent(output, serchContent).Length)
                    {

                        //Console.WriteLine(GetId() + " : " + GetContent() + " : の保有ボックス内で見つかりました");
                        return GetContext(i).SerchContent(output, serchContent);
                    }
                }
            }
            //Console.WriteLine(GetId() + " : " + SerchContent() + "  で見つかりませんでした");
            return level;
        }
    }











    class TextBox : Ibox
    {
        String[] text;

        Ibox[] box;

        int number;

        int id;
        string name;
        public TextBox(int i, int n, string name, string setText)
        {
            SetBox(i, n, name, setText);
            SetBox(n, null, null);
            SetRect(0, 0, 0, 0, 0, 0);
        }
        public TextBox(int i, int n, string name, string setText, string[] box_name, string[] box_set_text)
        {
            SetBox(i, n, name, setText);
            SetBox(n, box_name, box_set_text);//初期化
            SetRect(0, 0, 0, 0, 0, 0);
        }

        public TextBox(int i, int n, string name, string setText, int x, int y, int width, int height, int defaultWidth, int defaultHeight)
        {
            SetBox(i, n, name, setText);
            SetRect(x, y, width, height, defaultWidth, defaultHeight);
        }

        protected void SetBox(int i, int n, string name, string setText)
        {
            text = new String[4];
            box = new Box[n];
            SetNumber(n);
            this.SetText(3, n.ToString());
            if (name == null || name == "")
            {
                SetName("ヌルポ");
                this.SetText(0, "ヌルポ");
            }
            else
            {
                SetName(name);
                this.SetText(0, name);
            }
            if (setText == null || setText == "")
            {
                this.SetText(2, "ヌルポ");
            }
            else
            {
                this.SetText(2, setText);
            }
            this.SetText(1, "" + i);
            SetId(i);
        }
        /// <summary>
        /// メニュー用
        /// </summary>
        /// <param name="i"></param>
        /// <param name="name"></param>
        /// <param name="set_text"></param>
        protected void SetBox(int i, string[] name, string[] set_text)
        {
            for (int c = 0; c < i; ++c)
            {
                if (name == null)
                {
                    name = new String[i];
                }
                if (name[c] == null || name[c] == "")
                {
                    name[c] = ("Iname" + (c + 1));
                }
                if (set_text == null)
                {
                    set_text = new String[i];
                }
                if (set_text[c] == null || set_text[c] == "")
                {
                    set_text[c] = ("Item" + (c + 1));
                }
                box[c] = new Box(c, 0, name[c], set_text[c]);
            }
        }

        protected void SetRect(int x, int y, int width, int height, int defaultWidth, int defaultHeight)
        {
            if (x == 0)
            {
                x = 5;
            }
            if (y == 0)
            {
                y = 5 + (55 * GetId());
                Console.WriteLine("y : " + y);
            }
            if (width == 0)
            {
                width = 200;
            }
            if (height == 0)
            {
                height = 50;
            }

            rect = new Rectangle(x, y, width, height);

            Config cfg = new Config();

            if (defaultWidth == 0)
            {
                defaultWidth = cfg.GetDefaultWidth();
            }
            if (defaultHeight == 0)
            {
                defaultHeight = cfg.GetDefaultHeight();//デフォルト値の自動入力
            }

            this.defaultWidth = defaultWidth;
            this.defaultHeight = defaultHeight;
            SetFont(null);

        }


        public Ibox GetContext()
        {
            return this;
        }
        public Ibox GetContext(int n)
        {
            if (0 <= n && n < GetNumber())
            {
                return box[n];
            }
            Console.WriteLine("TextBox : GetContext(" + n + "] : インデックスの境界外です。");
            return this;
        }
        public void SetContext(Ibox setbox)//削除する場合はdelete
        {
            if (setbox != null)
            {
                for (int i = 0; i < text.Length; ++i)
                {
                    if (setbox.GetText(i) == null)
                    {
                        SetText(i, "");
                    }
                    else
                    {
                        SetText(i, setbox.GetText(i));
                    }
                }
                for (int i = 0; i < setbox.GetNumber(); ++i)
                {
                    if (GetNumber() != setbox.GetNumber())
                    {
                        Array.Resize(ref box, setbox.GetNumber() - 1);//サイズが異なる場合に作成しなおす
                    }
                    if (setbox.GetContext(i) == null)
                    {
                        box[i] = new Box(i, 0, "", "");
                    }
                    else
                    {
                        box[i] = setbox.GetContext(i);
                    }
                }
                this.SetNumber(setbox.GetNumber());
                this.SetId(setbox.GetId());
                this.SetName(setbox.GetName());
            }


        }

        public void SetContext(int n, Ibox setbox)
        {
            if (0 <= n && n < GetNumber())
            {
                box[n] = setbox;
            }
            Console.WriteLine("TextBox : SetContext(" + n + "] : インデックスの境界外です。");

        }
        public int GetNumber()
        {
            return this.number;
        }

        public void SetNumber(int n)
        {
            this.number = n;
        }

        public string ToString(string level)
        {
            Console.WriteLine(this.GetType() + "のtoString()");
            string output = "";
            output += "\n" + level + "ボックスName　name : " + GetName();
            output += "\n" + level + "ボックスId　id: " + GetId();
            output += "\n" + level + "保有ボックス数　number : " + GetNumber();
            output += "\n" + level + "ボックステキスト Itext[2] : " + GetText(2);
            return output + "\n";

        }

        public string ToStringAll(string level)
        {
            Console.WriteLine(this.GetType() + "のtoStringall()");
            String output = "";
            if (GetNumber() == 0)
            {
                return ToString(level);
            }
            else
            {
                level += GetContent() + " / ";
                for (int i = 0; i < GetNumber(); ++i)
                {
                    output += GetContext(i).ToStringAll(level);
                }

            }
            return output + "\n";
        }

        public string GetContent()
        {
            return this.GetText(2);
        }

        public string GetContent(int n)
        {
            if (0 <= n && n < this.GetNumber())
            {
                return this.box[n].GetText(2);
            }
            else
            {
                Console.WriteLine(this.GetType() + " : GetContent(" + n + ") : インデックスの境界外です。");
                return "";
            }

        }
        public string GetContentAll()
        {
            Console.WriteLine(this.GetType() + "のgetContentall()");
            String output = "";
            if (GetNumber() == 0)
            {
                return GetContent(2) + "\n";
            }
            else
            {
                for (int i = 0; i < GetNumber(); ++i)
                {
                    output += "./" + GetContext(i).GetContentAll();
                }

            }
            return output + "\n";
        }
        public string GetContentAll(string level)
        {

            Console.WriteLine(this.GetType() + "のgetContentall(" + level + ")");
            String output = "";
            if (GetNumber() == 0)
            {
                return level + " /" + GetContent() + "\n";
            }
            else
            {
                for (int i = 0; i < GetNumber(); ++i)
                {
                    output += GetContext(i).GetContentAll(level + " /" + GetContent());
                }

            }
            return output;
        }

        public void SetContent(string setcontent)
        {
            this.SetText(2, setcontent);
        }

        public void SetContent(int n, string setcontent)
        {
            if (0 <= n && n < this.GetNumber())
            {
                this.box[n].SetText(2, setcontent);
            }
            else
            {
                Console.WriteLine(this.GetType() + " : SetContent(" + n + ", " + setcontent + "インデックスの境界外です。");
            }

        }

        public void AddBox(Ibox add_box)
        {
            if (add_box != null)
            {
                if (GetNumber() == 0)
                {//所持数がゼロの場合
                    SetId(0);
                }
                else
                {
                    if (add_box.GetId() != GetContext(GetNumber() - 1).GetId() + 1)//現在保有しているBoxの最大Id + 1に設定する
                    {
                        add_box.SetId(GetContext(GetNumber() - 1).GetId() + 1);
                        Console.WriteLine("id 変更 : " + (GetContext(GetNumber() - 1).GetId() + 1));
                    }
                }
            }
            else
            {
                add_box = new Box(GetNumber() + 1, 0, "自動作成されたヌルポ", "自動作成されたヌルポ");
            }
            Array.Resize(ref box, box.Length + 1);
            box[GetNumber()] = (Ibox)(add_box);
            SetNumber(GetNumber() + 1);
        }



        // Iname
        public int GetId()
        {
            return id;
        }
        public void SetId(int setId)
        {
            id = setId;
        }
        public string GetName()
        {
            return name;
        }
        public void SetName(string setName)
        {
            name = setName;
        }



        // Itext
        public string GetText()
        {
            return text[0];
        }

        public string GetText(int i)
        {
            if (0 <= i && i < text.Length)
            {
                return text[i];
            }
            else
            {
                Console.WriteLine(this.GetType() + " : GetText(" + i + ") : インデックスの境界外です。");
                return "";
            }
        }
        public void SetText(string tex)
        {
            text[0] = tex;
        }
        public void SetText(int i, string tex)
        {
            if (0 <= i && i < text.Length)
            {
                text[i] = tex;
            }
            else
            {
                Console.WriteLine(this.GetType() + " : SetText(" + i + " , + " + tex + ") : インデックスの境界外です。");
            }

        }




        int corsorState = 1;

        // Icorsor
        public bool GetEnable()
        {
            throw new NotImplementedException();
        }

        public void SetEnable()
        {
            throw new NotImplementedException();
        }


        public void SetState(int i)
        {
            corsorState = i;
        }

        public int GetState()
        {
            return corsorState;
        }

        public int GetEventState()
        {
            throw new NotImplementedException();
        }

        public string GetEvent()
        {
            throw new NotImplementedException();
        }



        // GUI

        int defaultWidth = 0;
        int defaultHeight = 0;

        Rectangle rect = new Rectangle(0, 0, 0, 0);

        Rectangle reRect = new Rectangle(0, 0, 0, 0);

        bool setReSize = false;

        Font font;

        Font reFont;

        public void SetDefault(int width, int height)
        {
            defaultWidth = width;
            defaultHeight = height;
        }

        public int GetDefaultWidth()
        {
            return defaultWidth;
        }

        public int GetDefaultHeight()
        {
            return defaultHeight;
        }

        public Rectangle GetRect()
        {
            return rect;
        }


        public int GetX()
        {
            return rect.X;
        }

        public int GetY()
        {
            return rect.Y;
        }

        public int GetWidth()
        {
            return rect.Width;
        }

        public int GetHeight()
        {
            return rect.Height;
        }

        public void SetRect(Rectangle setRect)
        {
            rect = setRect;
            SetFont(null);
        }
        public void SetRect(Rectangle setRect, Font font)
        {
            rect = setRect;
            SetFont(font);
        }

        public Rectangle GetReRect()
        {
            if (setReSize == false)
            {
                Console.WriteLine(GetContent() + "Boxのリサイズ値を決めてください。");
                SetReRect();
                setReSize = false;
            }
            return reRect;
        }
        public int GetReX()
        {
            return reRect.X;
        }

        public int GetReY()
        {
            return reRect.Y;
        }

        public int GetReWidth()
        {
            return reRect.Width;
        }

        public int GetReHeight()
        {
            return reRect.Height;
        }

        public void SetReRect(int width, int height)
        {
            reRect = new Rectangle((int)(rect.X * ((double)width) / defaultWidth), (int)(rect.Y * ((double)height) / defaultHeight), (int)(rect.Width * ((double)width) / defaultWidth), (int)(rect.Height * ((double)height / defaultHeight)));
            SetReFont(null);
            setReSize = true;
        }
        public void SetReRect(int width, int height, Font reFont)
        {
            SetReRect(width, height);
            SetReFont(reFont);
            setReSize = true;
        }

        public void SetReRect()
        {
            Config cfg = new Config();
            Console.WriteLine("cfg wid " + cfg.GetWidth());
            reRect = new Rectangle((int)(rect.X * ((double)cfg.GetWidth()) / defaultWidth), (int)(rect.Y * ((double)cfg.GetHeight()) / defaultHeight), (int)(rect.Width * ((double)cfg.GetWidth()) / defaultWidth), (int)(rect.Height * ((double)cfg.GetHeight() / defaultHeight)));
            SetReFont(null);
            setReSize = true;
        }

        public void SetFont(Font font)
        {
            if (font == null)
            {
                this.font = new Font("MS UI Gothic", (rect.Height * 2) / 3);
            }
            else
            {
                this.font = font;
            }
        }

        public Font GetFont()
        {
            return this.font;
        }

        public void SetReFont(Font font)
        {
            if (font == null)
            {
                this.reFont = new Font("MS UI Gothic", (reRect.Height * 2) / 3);
            }
            else
            {
                this.reFont = font;
            }
        }

        public Font GetReFont()
        {
            return this.reFont;
        }




        public int[] SerchName(string serchName)
        {
            return SerchName(new int[] { }, serchName);
        }

        public int[] SerchName(int[] level, string serchName)
        {
            int[] output = level;
            if (GetName() == serchName)
            {
                //Console.WriteLine(GetId() + " : " + GetName() + " : で見つかりました");
                Array.Resize(ref output, level.Length + 1);
                output[level.Length] = GetId();
                return output;
            }
            else
            {
                //Console.WriteLine(GetId() + " : " + GetName() + "  で見つかりませんでしたが、検索します");
                for (int i = 0; i < GetNumber(); ++i)
                {
                    Array.Resize(ref output, level.Length + 1);
                    output[level.Length] = GetId();
                    if (output.Length < GetContext(i).SerchName(output, serchName).Length)
                    {

                        //Console.WriteLine(GetId() + " : " + GetName() + " : の保有ボックス内で見つかりました");
                        return GetContext(i).SerchName(output, serchName);
                    }
                }
            }
            //Console.WriteLine(GetId() + " : " + GetName() + "  で見つかりませんでした");
            return level;
        }
        public int[] SerchContent(string serchContent)
        {
            return SerchContent(new int[] { }, serchContent);
        }

        public int[] SerchContent(int[] level, string serchContent)
        {
            int[] output = level;
            if (GetContent() == serchContent)
            {
                Array.Resize(ref output, level.Length + 1);
                output[level.Length] = GetId();
                return output;
            }
            else
            {
                //Console.WriteLine(GetId() + " : " + SerchContent() + "  で見つかりませんでしたが、検索します");
                for (int i = 0; i < GetNumber(); ++i)
                {
                    Array.Resize(ref output, level.Length + 1);
                    output[level.Length] = GetId();
                    if (output.Length < GetContext(i).SerchContent(output, serchContent).Length)
                    {

                        //Console.WriteLine(GetId() + " : " + GetContent() + " : の保有ボックス内で見つかりました");
                        return GetContext(i).SerchContent(output, serchContent);
                    }
                }
            }
            //Console.WriteLine(GetId() + " : " + SerchContent() + "  で見つかりませんでした");
            return level;
        }
    }














    class MenuBox : Ibox
    {
        String[] text;

        public Ibox[] box;



        int id;
        string name;

        int number;
        public MenuBox(int i, int n, string name, string setText)
        {
            SetBox(i, n, name, setText);
            SetBox(n, null, null);
            SetRect(0, 0, 0, 0, 0, 0);
        }
        public MenuBox(int i, int n, string name, string setText, string[] box_name, string[] box_set_text)
        {
            SetBox(i, n, name, setText);
            SetBox(n, box_name, box_set_text);//初期化
            SetRect(0, 0, 0, 0, 0, 0);
        }

        public MenuBox(int i, int n, string name, string setText, int x, int y, int width, int height, int defaultWidth, int defaultHeight)
        {
            SetBox(i, n, name, setText);
            SetRect(x, y, width, height, defaultWidth, defaultHeight);
        }

        protected void SetBox(int i, int n, string name, string setText)
        {
            text = new String[4];
            box = new Box[n];
            SetNumber(n);
            this.SetText(3, n.ToString());
            if (name == null || name == "")
            {
                SetName("ヌルポ");
                this.SetText(0, "ヌルポ");
            }
            else
            {
                SetName(name);
                this.SetText(0, name);
            }
            if (setText == null || setText == "")
            {
                this.SetText(2, "ヌルポ");
            }
            else
            {
                this.SetText(2, setText);
            }
            this.SetText(1, "" + i);
            SetId(i);
        }
        /// <summary>
        /// メニュー用
        /// </summary>
        /// <param name="i"></param>
        /// <param name="name"></param>
        /// <param name="set_text"></param>
        protected void SetBox(int n, string[] name, string[] set_text)
        {
            for (int c = 0; c < n; ++c)
            {
                if (name == null)
                {
                    name = new String[n];
                }
                if (name[c] == null || name[c] == "")
                {
                    name[c] = ("Iname" + (c + 1));
                }
                if (set_text == null)
                {
                    set_text = new String[n];
                }
                if (set_text[c] == null || set_text[c] == "")
                {
                    set_text[c] = ("Item" + (c + 1));
                }
                box[c] = new Box(c, 0, name[c], set_text[c]);
            }
        }

        protected void SetRect(int x, int y, int width, int height, int defaultWidth, int defaultHeight)
        {
            if (x == 0)
            {
                x = 5;
            }
            if (y == 0)
            {
                y = 5 + (55 * GetId());
                Console.WriteLine("y : " + y);
            }
            if (width == 0)
            {
                width = 200;
            }
            if (height == 0)
            {
                height = 50;
            }

            rect = new Rectangle(x, y, width, height);

            Config cfg = new Config();

            if (defaultWidth == 0)
            {
                defaultWidth = cfg.GetDefaultWidth();
            }
            if (defaultHeight == 0)
            {
                defaultHeight = cfg.GetDefaultHeight();//デフォルト値の自動入力
            }

            this.defaultWidth = defaultWidth;
            this.defaultHeight = defaultHeight;
            SetFont(null);

        }




        public Ibox GetContext()
        {
            return this;
        }
        public Ibox GetContext(int n)
        {
            if (0 <= n && n < GetNumber())
            {
                return box[n];
            }
            Console.WriteLine("TextBox : GetContext(" + n + "] : インデックスの境界外です。");
            return this;
        }


        public void SetContext(Ibox setbox)//削除する場合はdelete
        {
            if (setbox != null)
            {
                for (int i = 0; i < text.Length; ++i)
                {
                    if (setbox.GetText(i) == null)
                    {
                        SetText(i, "");
                    }
                    else
                    {
                        SetText(i, setbox.GetText(i));
                    }
                }
                for (int i = 0; i < setbox.GetNumber(); ++i)
                {
                    if (GetNumber() != setbox.GetNumber())
                    {
                        Array.Resize(ref box, setbox.GetNumber() - 1);//サイズが異なる場合に作成しなおす
                    }
                    if (setbox.GetContext(i) == null)
                    {
                        box[i] = new Box(i, 0, "", "");
                    }
                    else
                    {
                        box[i] = setbox.GetContext(i);
                    }
                }
                this.SetNumber(setbox.GetNumber());
                this.SetId(setbox.GetId());
                this.SetName(setbox.GetName());
            }


        }

        public void SetContext(int n, Ibox setbox)
        {
            if (0 <= n && n < GetNumber())
            {
                box[n] = setbox;
            }
            Console.WriteLine("TextBox : SetContext(" + n + "] : インデックスの境界外です。");

        }
        public int GetNumber()
        {
            return this.number;
        }

        public void SetNumber(int n)
        {
            this.number = n;
        }

        public string ToString(string level)
        {
            Console.WriteLine(this.GetType() + "のtoString()");
            string output = "";
            output += "\n" + level + "ボックスName　name : " + GetName();
            output += "\n" + level + "ボックスId　id: " + GetId();
            output += "\n" + level + "保有ボックス数　number : " + GetNumber();
            output += "\n" + level + "ボックステキスト Itext[2] : " + GetText(2);
            return output + "\n";

        }

        public string ToStringAll(string level)
        {
            Console.WriteLine(this.GetType() + "のtoStringall()");
            String output = "";
            if (GetNumber() == 0)
            {
                return ToString(level);
            }
            else
            {
                level += GetContent() + " / ";
                for (int i = 0; i < GetNumber(); ++i)
                {
                    output += GetContext(i).ToStringAll(level);
                }

            }
            return output + "\n";
        }

        public string GetContent()
        {
            return this.GetText(2);
        }

        public string GetContent(int n)
        {
            if (0 <= n && n < this.GetNumber())
            {
                return this.box[n].GetText(2);
            }
            else
            {
                Console.WriteLine(this.GetType() + " : GetContent(" + n + ") : インデックスの境界外です。");
                return "";
            }

        }
        public string GetContentAll()
        {
            Console.WriteLine(this.GetType() + "のgetContentall()");
            String output = "";
            if (GetNumber() == 0)
            {
                return GetContent(2) + "\n";
            }
            else
            {
                for (int i = 0; i < GetNumber(); ++i)
                {
                    output += "./" + GetContext(i).GetContentAll();
                }

            }
            return output + "\n";
        }

        public string GetContentAll(string level)
        {

            Console.WriteLine(this.GetType() + "のgetContentall(" + level + ")");
            String output = "";
            if (GetNumber() == 0)
            {
                return level + " /" + GetContent() + "\n";
            }
            else
            {
                // メニュー /メニュー2 /武器 /片手剣 /ハンターナイフ
                for (int i = 0; i < GetNumber(); ++i)
                {
                    output += GetContext(i).GetContentAll(level + " /" + GetContent());
                }

            }
            return output;
        }

        public void SetContent(string setcontent)
        {
            this.SetText(2, setcontent);
        }

        public void SetContent(int n, string setcontent)
        {
            if (0 <= n && n < this.GetNumber())
            {
                this.box[n].SetText(2, setcontent);
            }
            else
            {
                Console.WriteLine(this.GetType() + " : SetContent(" + n + ", " + setcontent + "インデックスの境界外です。");
            }

        }

        public void AddBox(Ibox add_box)
        {
            if (add_box != null)
            {
                if (GetNumber() == 0) {//所持数がゼロの場合
                    SetId(0);
                }
                else
                {
                    if (add_box.GetId() != GetContext(GetNumber() - 1).GetId() + 1)//現在保有しているBoxの最大Id + 1に設定する
                    {
                        add_box.SetId(GetContext(GetNumber() - 1).GetId() + 1);
                        Console.WriteLine("id 変更 : " + (GetContext(GetNumber() - 1).GetId() + 1));
                    }
                }
            }
            else
            {
                add_box = new Box(GetNumber() + 1, 0, "自動作成されたヌルポ", "自動作成されたヌルポ");
            }
            Array.Resize(ref box, box.Length + 1);
            box[GetNumber()] = (Ibox)(add_box);
            SetNumber(GetNumber() + 1);
        }



        // Iname
        public int GetId()
        {
            return id;
        }
        public void SetId(int setId)
        {
            id = setId;
        }
        public string GetName()
        {
            return name;
        }
        public void SetName(string setName)
        {
            name = setName;
        }



        // Itext
        public string GetText()
        {
            return text[0];
        }

        public string GetText(int i)
        {
            if (0 <= i && i < text.Length)
            {
                return text[i];
            }
            else
            {
                Console.WriteLine(this.GetType() + " : GetText(" + i + ") : インデックスの境界外です。");
                return "";
            }
        }
        public void SetText(string tex)
        {
            text[0] = tex;
        }
        public void SetText(int i, string tex)
        {
            if (0 <= i && i < text.Length)
            {
                text[i] = tex;
            }
            else
            {
                Console.WriteLine(this.GetType() + " : SetText(" + i + " , + " + tex + ") : インデックスの境界外です。");
            }

        }

        int corsorState = 1;

        // Icorsor
        public bool GetEnable()
        {
            throw new NotImplementedException();
        }

        public void SetEnable()
        {
            throw new NotImplementedException();
        }


        public void SetState(int i)
        {
            corsorState = i;
        }

        public int GetState()
        {
            return corsorState;
        }

        public int GetEventState()
        {
            throw new NotImplementedException();
        }

        public string GetEvent()
        {
            throw new NotImplementedException();
        }



        // GUI

        int defaultWidth = 0;
        int defaultHeight = 0;

        Rectangle rect = new Rectangle(0, 0, 0, 0);

        Rectangle reRect = new Rectangle(0, 0, 0, 0);

        bool setReSize = false;

        Font font;

        Font reFont;

        public void SetDefault(int width, int height)
        {
            defaultWidth = width;
            defaultHeight = height;
        }

        public int GetDefaultWidth()
        {
            return defaultWidth;
        }

        public int GetDefaultHeight()
        {
            return defaultHeight;
        }

        public Rectangle GetRect()
        {
            return rect;
        }


        public int GetX()
        {
            return rect.X;
        }

        public int GetY()
        {
            return rect.Y;
        }

        public int GetWidth()
        {
            return rect.Width;
        }

        public int GetHeight()
        {
            return rect.Height;
        }

        public void SetRect(Rectangle setRect)
        {
            rect = setRect;
            SetFont(null);
        }
        public void SetRect(Rectangle setRect, Font font)
        {
            rect = setRect;
            SetFont(font);
        }

        public Rectangle GetReRect()
        {
            if (setReSize == false)
            {
                Console.WriteLine(GetContent() + " Boxのリサイズ値を決めてください。");
                SetReRect();
                setReSize = false;
            }
            return reRect;
        }
        public int GetReX()
        {
            return reRect.X;
        }

        public int GetReY()
        {
            return reRect.Y;
        }

        public int GetReWidth()
        {
            return reRect.Width;
        }

        public int GetReHeight()
        {
            return reRect.Height;
        }

        public void SetReRect(int width, int height)
        {
            reRect = new Rectangle((int)(rect.X * ((double)width) / defaultWidth), (int)(rect.Y * ((double)height) / defaultHeight), (int)(rect.Width * ((double)width) / defaultWidth), (int)(rect.Height * ((double)height / defaultHeight)));
            SetReFont(null);
            setReSize = true;
        }
        public void SetReRect(int width, int height, Font reFont)
        {
            SetReRect(width, height);
            SetReFont(reFont);
            setReSize = true;
        }

        public void SetReRect()
        {
            Config cfg = new Config();
            Console.WriteLine("cfg wid " + cfg.GetWidth());
            reRect = new Rectangle((int)(rect.X * ((double)cfg.GetWidth()) / defaultWidth), (int)(rect.Y * ((double)cfg.GetHeight()) / defaultHeight), (int)(rect.Width * ((double)cfg.GetWidth()) / defaultWidth), (int)(rect.Height * ((double)cfg.GetHeight() / defaultHeight)));
            SetReFont(null);
            setReSize = true;
        }

        public void SetFont(Font font)
        {
            if (font == null)
            {
                this.font = new Font("MS UI Gothic", (rect.Height * 2) / 3);
            }
            else
            {
                this.font = font;
            }
        }

        public Font GetFont()
        {
            return this.font;
        }

        public void SetReFont(Font font)
        {
            if (font == null)
            {
                Console.WriteLine("reFont : height - " + reRect.Height);
                this.reFont = new Font("MS UI Gothic", (reRect.Height * 2) / 3);
            }
            else
            {
                this.reFont = font;
            }
        }

        public Font GetReFont()
        {
            return this.reFont;
        }







        public int[] SerchName(string serchName)
        {
            return SerchName(new int[] { }, serchName);
        }

        public int[] SerchName(int[] level, string serchName)
        {
            int[] output = level;
            if (GetName() == serchName)
            {
                //Console.WriteLine(GetId() + " : " + GetName() + " : で見つかりました");
                Array.Resize(ref output, level.Length + 1);
                output[level.Length] = GetId();
                //Console.WriteLine(GetId() + " : " + GetName() + "  : output.length : " + output.Length);
                return output;
            }
            else
            {
                //Console.WriteLine(GetId() + " : " + GetName() + "  で見つかりませんでしたが、検索します");
                for (int i = 0; i < GetNumber(); ++i)
                {
                    Array.Resize(ref output, level.Length + 1);
                    output[level.Length] = GetId();
                    if (output.Length < GetContext(i).SerchName(output, serchName).Length)
                    {
                        
                        //Console.WriteLine(GetId() + " : " + GetName() + " : の保有ボックス内で見つかりました");
                        return GetContext(i).SerchName(output, serchName);
                    }
                }
            }
            //Console.WriteLine(GetId() + " : " + GetName() + "  で見つかりませんでした");
            return level;
        }

        public int[] SerchContent(string serchContent)
        {
            return SerchContent(new int[] { }, serchContent);
        }

        public int[] SerchContent(int[] level, string serchContent)
        {
            int[] output = level;
            if (GetContent() == serchContent)
            {
                Array.Resize(ref output, level.Length + 1);
                output[level.Length] = GetId();
                return output;
            }
            else
            {
                //Console.WriteLine(GetId() + " : " + SerchContent() + "  で見つかりませんでしたが、検索します");
                for (int i = 0; i < GetNumber(); ++i)
                {
                    Array.Resize(ref output, level.Length + 1);
                    output[level.Length] = GetId();
                    if (output.Length < GetContext(i).SerchContent(output, serchContent).Length)
                    {

                        //Console.WriteLine(GetId() + " : " + GetContent() + " : の保有ボックス内で見つかりました");
                        return GetContext(i).SerchContent(output, serchContent);
                    }
                }
            }
            //Console.WriteLine(GetId() + " : " + SerchContent() + "  で見つかりませんでした");
            return level;
        }
    }

}