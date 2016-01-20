/*   
 *     檔案位置 : Sam33UML/Core/CoreDraw.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : CoreDraw為所有繪圖相關的動作提供了一個整合的介面,所有的繪圖動作理應呼叫CoreDraw來代為處理，
 *                CoreDraw中所有的繪圖區域皆是以CoreRectRange類別來定義，並加上座標、Style等等的參數，用來繪
 *                出指定的圖形，此架構能夠方便程式的移植。
 *                
 *     繼承架構 : None
 *                
 *     最後修改 : 2015/01/20
 * 
 */

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
using System.Globalization;


namespace Sam33UML.Core
{
    public class CoreDraw
    {
        public const int BorderStyle_StraightLine = 1;
        public const int BorderStyle_MultipleSegment = 2;
        public const int LineStyle_StraightLine = 1;
        public const int LineStyle_MultipleSegment = 2;


        public static void DrawLine(DrawingContext dc, Point _Start, Point _End, double LineWidth, Brush LineColor, int LineStyle)
        {
            Pen lp = new Pen(LineColor, LineWidth);

            switch (LineStyle)
            {
                case LineStyle_StraightLine :
                    dc.DrawLine(lp, _Start, _End);
                    break;
                case LineStyle_MultipleSegment :
                    double dx = Math.Abs(_Start.X - _End.X);
                    double dy = Math.Abs(_Start.Y - _End.Y);
                    double d1 = Math.Pow(dx * dx + dy * dy, 0.5);
                    double d2 = Math.Pow(5 * 5 + 5 * 5, 0.5);
                    Vector v1 = new Vector(_End.X - _Start.X, _End.Y - _Start.Y);
                    Vector v2 = v1 / (d1 / d2);
                    int count = (int)(d1 / d2);
                    List<Point> S = new List<Point>();
                    List<Point> E = new List<Point>();
                    for (int i = 0; i < count; i += 2)
                    {
                        S.Add(_Start + v2 * i);
                        E.Add(_Start + v2 * i + v2);
                    }
                    for (int i = 0; i < S.Count; i++)
                    {
                        dc.DrawLine(lp, S.ElementAt(i), E.ElementAt(i));
                    }
                    break;
            }
        }

        public static void DrawEllipse(DrawingContext dc, Point Center, double Width, double Height, Brush EllipseColor)
        {
            dc.DrawEllipse(EllipseColor, new Pen(), Center, Width / 2, Height / 2);
        }

        public static void DrawEllipseBorder(DrawingContext dc, Point Center, double Width, double Height,  double BorderWidth, Brush EllipseColor)
        {
            Pen p = new Pen(EllipseColor, BorderWidth);
            dc.DrawEllipse(Brushes.Transparent, p, Center, Width / 2, Height / 2);
        }


        public static void DrawRect(DrawingContext dc, CoreRectRange Range, Brush RectColor)
        {
            dc.DrawRectangle(RectColor, new Pen(), new Rect(Range.TopLeft, Range.BottomRight));
        }

        public static void DrawText(DrawingContext dc, String Text, CoreRectRange Range, Brush TextColor, int FontSize)
        {
            FormattedText formattedText = new FormattedText(Text, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), FontSize, TextColor);
            formattedText.MaxTextHeight = Range.Height;
            formattedText.MaxTextWidth = Range.Width;
            dc.DrawText(formattedText, Range.TopLeft);
        }

        public static void DrawBorder(DrawingContext dc, CoreRectRange Range, double BorderWidth, Brush BorderColor, int BorderStyle)
        {
            Brush lb = Brushes.CadetBlue;
            Pen lp = new Pen(BorderColor, BorderWidth);
            Point TopLeft = Range.TopLeft;
            Point TopRight = new Point(Range.BottomRight.X, Range.TopLeft.Y);
            Point BottomRight = Range.BottomRight;
            Point BottomLeft = new Point(Range.TopLeft.X, Range.BottomRight.Y);
            switch(BorderStyle)
            {
                case BorderStyle_StraightLine:
                    DrawLine(dc, TopLeft, TopRight, BorderWidth, BorderColor, LineStyle_StraightLine);
                    DrawLine(dc, TopLeft, BottomLeft, BorderWidth, BorderColor, LineStyle_StraightLine);
                    DrawLine(dc, BottomRight, TopRight, BorderWidth, BorderColor, LineStyle_StraightLine);
                    DrawLine(dc, BottomRight, BottomLeft, BorderWidth, BorderColor, LineStyle_StraightLine);
                    break;
                
                case BorderStyle_MultipleSegment:
                    DrawLine(dc, TopLeft, TopRight, BorderWidth, BorderColor, LineStyle_MultipleSegment);
                    DrawLine(dc, TopLeft, BottomLeft, BorderWidth, BorderColor, LineStyle_MultipleSegment);
                    DrawLine(dc, BottomRight, TopRight, BorderWidth, BorderColor, LineStyle_MultipleSegment);
                    DrawLine(dc, BottomRight, BottomLeft, BorderWidth, BorderColor, LineStyle_MultipleSegment);
                    break;
            }

        }
    }

    public class CoreRectRange
    {
        public Point TopLeft;
        public Point BottomRight;
        public double Width;
        public double Height;
        public static CoreRectRange Default()
        {
            return new CoreRectRange(0,0,0,0);
        }

        private void Init(List<Point> Points)
        {
            double minx = double.MaxValue;
            double miny = double.MaxValue;
            double maxx = double.MinValue;
            double maxy = double.MinValue;
            foreach(Point point in Points)
            {
                if (point.X < minx)
                    minx = point.X;
                if (point.Y < miny)
                    miny = point.Y;
                if (point.X > maxx)
                    maxx = point.X;
                if (point.Y > maxy)
                    maxy = point.Y;
            }
            Init(new Point(minx, miny), new Point(maxx, maxy));
        }

        private void Init(Point _P1, Point _P2)
        {
            double tlx = Math.Min(_P1.X, _P2.X);
            double tly = Math.Min(_P1.Y, _P2.Y);
            double brx = Math.Max(_P1.X, _P2.X);
            double bry = Math.Max(_P1.Y, _P2.Y);
            TopLeft = new Point(tlx, tly);
            BottomRight = new Point(brx, bry);
            Width = BottomRight.X - TopLeft.X;
            Height = BottomRight.Y - TopLeft.Y;
        }

        public CoreRectRange(List<CoreRectRange> Ranges)
        {
            List<Point> Points = new List<Point>();
            foreach (CoreRectRange range in Ranges)
            {
                Points.Add(range.TopLeft);
                Points.Add(range.BottomRight);
            }
            Init(Points);
        }

        public CoreRectRange(List<Point> Points)
        {
            Init(Points);
        }

        public CoreRectRange(Point _P1, Point _P2)
        {
            Init(_P1, _P2);
        }

        public CoreRectRange(double _x1, double _y1, double _x2, double _y2)
        {
            Init(new Point(_x1, _y1), new Point(_x2, _y2));
        }
    }
}
