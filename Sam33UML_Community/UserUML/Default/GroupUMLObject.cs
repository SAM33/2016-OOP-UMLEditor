/*   
 *     檔案位置 : Sam33UML/UserUML/Default/GroupUMLObject.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : GroupUMLObject能夠裝入任意數量以BaseUMLObject為基底的物件，包含GroupUMLObject本身。
 *                
 *     繼承架構 : BaseUMLShape
 *                |
 *                |__GroupUMLObject
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
    public class GroupUMLObject : BaseUMLObject
    {
        List<BaseUMLObject> Childrens;

        public List<BaseUMLObject> getChildrens()
        {
            return Childrens;
        }

        public GroupUMLObject(CoreRectRange Range) : base(Range)
        {
            
        }

        public void SetChildrens(List<BaseUMLObject> Objects)
        {
            Childrens = Objects;
            List<CoreRectRange> Ranges = new List<CoreRectRange>();
            foreach (BaseUMLObject obj in Objects)
            {
                Ranges.Add(obj.getRange());
            }
            Range = new CoreRectRange(Ranges);
        }

        public override void Move(Point _Location, int Type = Move_Normal)
        {
            base.Move(_Location, Type);
            if(Type == Move_Relative)
            {
                foreach (BaseUMLObject obj in Childrens)
                    obj.Move(_Location, Move_Relative);
            }
            else
            {
                double dx = _Location.X - GetLocation().X;
                double dy = _Location.Y - GetLocation().Y;
                foreach (BaseUMLObject obj in Childrens)
                    obj.Move(new Point(dx,dy), Move_Relative);
            }
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
            double BorderWidth = 1;
            foreach (BaseUMLObject obj in Childrens)
                obj.Draw(dc);
            CoreDraw.DrawBorder(dc, Range, BorderWidth, Brushes.Red, CoreDraw.BorderStyle_MultipleSegment);
            CoreRectRange TextRange = new CoreRectRange(Range.TopLeft.X + BorderWidth * 1.5, Range.TopLeft.Y + BorderWidth * 1.5 - 30, Range.BottomRight.X - BorderWidth * 1.5, Range.TopLeft.Y + Range.Height / 3 - BorderWidth * 1.5 - 30);
            CoreDraw.DrawText(dc, "UMLGroup(" + ID + ")", TextRange, Brushes.Black, 12);
            if (Selected)
                foreach (ConnectionPort Port in Ports)
                    Port.Draw(dc);
            return;
        }
    }
}
