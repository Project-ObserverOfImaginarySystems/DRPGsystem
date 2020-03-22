using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRPGsystem
{
    /// <summary>
    /// 実装クラスはgetText()ですべての情報をテキストにして返します。
    /// </summary>
    public interface Itext
    {
        string GetText();
        /// <summary>
        /// テキスト情報の特定の情報を引き出します。
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        string GetText(int i);
        void SetText(string tex);
        void SetText(int i, string tex);

    }
    public interface Iname
    {

        void SetId(int setid);
        int GetId();
        void SetName(string setname);
        string GetName();
    }
    public interface Icorsor
    {
        /// <summary>
        /// カーソルが有効かどうかのフラグの取得
        /// </summary>
        bool GetEnable();
        /// <summary>
        /// カーソルが有効かどうかのフラグの設定
        /// </summary>
        /// <returns></returns>
        void SetEnable();
        /// <summary>
        /// カーソルの現状の代入
        /// </summary>
        /// <param name="i">The i.</param>
        void SetState(int i);
        /// <summary>
        /// カーソルの現状の取得
        /// </summary>
        /// <returns></returns>
        int GetState();
        /// <summary>
        /// カーソルイベントの取得
        /// 0 移動不能
        /// 1 通常通りの移動
        /// 2 特殊移動 --> 
        /// </summary>
        /// <returns></returns>
        int GetEventState();
        /// <summary>
        /// 仮 : 実際のイベント内容をstringで返す
        /// 例 {for} {UP} {10} {UP} -->↑キー１０回で↑
        ///    {rec} {UP} {1} {DOWN} ->↑キーを押せば↓
        /// </summary>
        /// <returns></returns>
        string GetEvent();
    }
    interface IgraphicalUserInterface
    {
        void SetDefault(int width, int height);

        int GetDefaultWidth();

        int GetDefaultHeight();

        Rectangle GetRect();

        int GetX();

        int GetY();

        int GetWidth();

        int GetHeight();

        void SetRect(Rectangle setrect);

        void SetRect(Rectangle setrect, Font font);

        Rectangle GetReRect();

        int GetReX();

        int GetReY();

        int GetReWidth();

        int GetReHeight();

        void SetReRect(int width, int height);

        void SetReRect(int height, int width, Font reFont);
        /// <summary>
        /// Systemから値をとる
        /// </summary>
        void SetReRect();//Systemから持ってくるため、コンフィグ設定ができない。

        void SetFont(Font font);

        Font GetFont();

        void SetReFont(Font font);

        Font GetReFont();


    }

    class Config
    {
        static int defaultWidth = 500;

        static int defaultHeight = 500;

        static int Width = 500;

        static int Height = 500;

        public int GetDefaultWidth()
        {
            return defaultWidth;
        }
        public int GetDefaultHeight()
        {
            return defaultHeight;
        }
        public void SetDefaultWidth(int setWidth)
        {
            defaultWidth = setWidth;
        }
        public void SetDefaultHeight(int setHeight)
        {
            defaultHeight = setHeight;
        }

        public int GetWidth()
        {
            return Width;
        }
        public int GetHeight()
        {
            return Height;
        }
        public void SetWidth(int setWidth)
        {
            Width = setWidth;
        }
        public void SetHeight(int setHeight)
        {
            Height = setHeight;
        }
    }

}
