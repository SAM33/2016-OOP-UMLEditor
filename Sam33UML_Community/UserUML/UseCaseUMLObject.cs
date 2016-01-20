/*   
 *     檔案位置 : Sam33UML/UserUML/UseCaseUMLOject.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : UML的UseCase圖形
 *                
 *     繼承架構 : BaseUMLObject
 *                |
 *                |__UseCaseUMLOject
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
using Sam33UML.Controls;
using Sam33UML.Core;
using Sam33UML.UserUML.Default;


namespace Sam33UML.UserUML
{
    public class UseCaseUMLObject : BaseUMLObject
    {
        String Name;
        public UseCaseUMLObject(CoreRectRange Range)
            : base(Range)
        {
            Name = "UMLUseCase(" + ID + ")";
        }
        public void setName(String _Name)
        {
            Name = _Name;
        }

        public String getName()
        {
            return Name;
        }

        public override bool Contain(Point p)
        {
            //計算橢圓公式
            double sr = Math.Min(Range.Height, Range.Width)/2;    //短邊半徑
            double lr = Math.Max(Range.Height, Range.Width)/2;    //長邊半徑
            Point Center = new Point((Range.TopLeft.X + Range.BottomRight.X) / 2, (Range.TopLeft.Y + Range.BottomRight.Y) / 2 );
            double x = p.X - Center.X;
            double y = p.Y - Center.Y;
            if (lr == Range.Width/2) //水平橢圓  (x*x)/(lr*lr)+(y*y)/(sr*sr)-1 = 0
            {
                double d = (x * x) / (lr * lr) + (y * y) / (sr * sr) - 1;
                if (d < 0)
                    return true;
            }
            else  //垂直橢圓  (x*x)/(sr*sr)+(y*y)/(lr*lr)-1 = 0
            {
                double d = (x * x) / (sr * sr) + (y * y) / (lr * lr) - 1;
                if (d < 0)
                    return true;
            }
            return false;
        }

        public override bool Inside(CoreRectRange r)
        {
            if (Range.TopLeft.X > r.TopLeft.X && Range.TopLeft.Y > r.TopLeft.Y && Range.BottomRight.X < r.BottomRight.X && Range.BottomRight.Y < r.BottomRight.Y)
                return true;
            return false;
        }

        public override void Draw(DrawingContext dc)
        {
            const double BorderWidth = 3;
            double Width = Range.Width;
            double Height = Range.Height;
            Point Center = new Point((Range.TopLeft.X + Range.BottomRight.X) / 2, (Range.TopLeft.Y + Range.BottomRight.Y) / 2);
            double a = Math.Max(Range.Height, Range.Width) / 2;  //長邊半徑
            double b = Math.Min(Range.Height, Range.Width) / 2;  //短邊半徑
            double c = 2 * b * b / a;  //計算正焦距
            //s+w=2a, (2c)^2+w^2=s^2 我們現在要求垂直於左焦點的線長w*2,以及c*2為內接四邊形的長和寬
            //s^2-w^2=(2c)^2 --> (s+w)(s-w)=(2c)^2 --> (s-w)=(2c)^2/(s+w) --> (s-w)=(2c)^2/(2a) --> (s-w)=2c^2/a --> 2w = 2*a - ((2*c*c)/a)
            double w = (2 * a - ((2 * c * c) / a)) / 2;
            Point InsideRectTopLeft = new Point(Center.X - c, Center.Y - w);
            Point InsideRectBottomRight = new Point(Center.X + c, Center.Y + w);
            //畫出整個外觀
            CoreDraw.DrawEllipse(dc, Center, Width, Height, Brushes.AliceBlue);
            //畫出Title
            CoreRectRange TitleRange = new CoreRectRange(InsideRectTopLeft, InsideRectBottomRight);
            //CoreDraw.DrawBorder(dc, TitleRange, 2, Brushes.Red, CoreDraw.BorderStyle_StraightLine);
            CoreDraw.DrawText(dc, Name, TitleRange, Brushes.Black, 12);
            //劃出外框
            CoreDraw.DrawEllipseBorder(dc, Center, Width, Height, BorderWidth, Brushes.Black);
            //畫出選擇點
            if (Selected)
                foreach (ConnectionPort Port in Ports)
                    Port.Draw(dc);
            return;
        }
    }
}
