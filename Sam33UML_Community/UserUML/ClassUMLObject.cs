/*   
 *     檔案位置 : Sam33UML/UserUML/ClassUMLOject.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : UML的Class圖形
 *                
 *     繼承架構 : BaseUMLObject
 *                |
 *                |__ClassUMLOject
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
    public class ClassUMLObject : BaseUMLObject
    {
        String TitleText;
        String MemberText;
        String MethodText;

        public ClassUMLObject(CoreRectRange Range)
            : base(Range)
        {
            TitleText = "UMLClass(" + ID + ")";
            MemberText = "Members:";
            MethodText = "Methods:";
        }

        public void setTitleText(String _TitleText)
        {
            TitleText = _TitleText;
        }

        public String getTitleText()
        {
            return TitleText;
        }

        public void setMemberText(String _MemberText)
        {
            MemberText = _MemberText;
        }

        public void setMethodText(String _MethodText)
        {
            MemberText = _MethodText;
        }

        public override bool Contain(Point p)
        {
            if (Range.TopLeft.X <= p.X && Range.TopLeft.Y <= p.Y && Range.BottomRight.X >= p.X && Range.BottomRight.Y >= p.Y)
                return true;
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
            Point Title_Top_LeftPoint = new Point(Range.TopLeft.X, Range.TopLeft.Y);
            Point Title_Top_RightPoint = new Point(Range.BottomRight.X, Range.TopLeft.Y);
            Point Title_Member_LeftPoint = new Point(Range.TopLeft.X,Range.TopLeft.Y+Height*0.25);
            Point Title_Member_RightPoint = new Point(Range.BottomRight.X,Range.TopLeft.Y+Height*0.25);
            Point Member_Method_LeftPoint = new Point(Range.TopLeft.X,Range.TopLeft.Y+Height*0.65);
            Point Member_Method_RightPoint = new Point(Range.BottomRight.X,Range.TopLeft.Y+Height*0.65);
            Point Method_Bottom_LeftPoint = new Point(Range.TopLeft.X, Range.TopLeft.Y + Height);
            Point Method_Bottom_RightPoint = new Point(Range.BottomRight.X, Range.TopLeft.Y + Height);
            //畫出整個外觀
            CoreDraw.DrawRect(dc, Range, Brushes.AliceBlue);
            //畫出第一條分隔線
            CoreDraw.DrawLine(dc, Title_Member_LeftPoint, Title_Member_RightPoint, BorderWidth, Brushes.Black, CoreDraw.LineStyle_StraightLine);
            //畫出第二條分隔線
            CoreDraw.DrawLine(dc, Member_Method_LeftPoint, Member_Method_RightPoint, BorderWidth, Brushes.Black, CoreDraw.LineStyle_StraightLine);
            //畫出Title
            CoreRectRange TitleRange = new CoreRectRange(Title_Top_LeftPoint.X + BorderWidth , Title_Top_LeftPoint.Y + BorderWidth , Title_Member_RightPoint.X - BorderWidth, Title_Member_RightPoint.Y - BorderWidth );
            //CoreDraw.DrawBorder(dc, TitleRange, 2, Brushes.Red, CoreDraw.BorderStyle_StraightLine);
            CoreDraw.DrawText(dc, TitleText, TitleRange, Brushes.Black, 12);
            //畫出Member
            CoreRectRange MembersRange = new CoreRectRange(Title_Member_LeftPoint.X + BorderWidth, Title_Member_LeftPoint.Y + BorderWidth , Member_Method_RightPoint.X - BorderWidth , Member_Method_RightPoint.Y - BorderWidth );
            //CoreDraw.DrawBorder(dc, MembersRange, 2, Brushes.Red, CoreDraw.BorderStyle_StraightLine);
            CoreDraw.DrawText(dc, MemberText, MembersRange, Brushes.Black, 12);
            //畫出Methods
            CoreRectRange Methodsange = new CoreRectRange(Member_Method_LeftPoint.X + BorderWidth , Member_Method_LeftPoint.Y + BorderWidth , Method_Bottom_RightPoint.X - BorderWidth , Method_Bottom_RightPoint.Y - BorderWidth );
            //CoreDraw.DrawBorder(dc, Methodsange, 2, Brushes.Red, CoreDraw.BorderStyle_StraightLine);
            CoreDraw.DrawText(dc, MethodText, Methodsange, Brushes.Black, 12);
            //劃出外框
            CoreDraw.DrawBorder(dc, Range, BorderWidth, Brushes.Black, CoreDraw.BorderStyle_StraightLine);
            //畫出選擇點
            if (Selected)
                foreach (ConnectionPort Port in Ports)
                    Port.Draw(dc);
            return;
        }
    }
}
