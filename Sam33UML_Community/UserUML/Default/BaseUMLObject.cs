/*   
 *     檔案位置 : Sam33UML/UserUML/Default/BaseUMLObject.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : BaseUMLObject為所有實體UML圖形的基底類別，BaseUMLObject提供ConnectionPort方便以BaseConnShape為基底
 *                的類別們與其建立連線。
 *                
 *     繼承架構 : BaseUMLShape
 *                |
 *                |__BaseUMLObject
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

namespace Sam33UML.UserUML.Default
{
    public abstract class BaseUMLObject : BaseUMLShape
    {
        public const int Move_Normal = 0;
        public const int Move_Relative = 1;
        protected CoreRectRange Range;
        protected List<ConnectionPort> Ports;

        //enum GraphicDetail {DrawID,DrawText,DrawBorder,DrawBorder,DrawObject}


        public BaseUMLObject(CoreRectRange _Range)
            : base()
        {
            Range = _Range;
            Ports = new List<ConnectionPort>();
            Ports.Add(new ConnectionPort(this,ConnectionPort.Location.TopConnectionPort));
            Ports.Add(new ConnectionPort(this, ConnectionPort.Location.RightConnectionPort));
            Ports.Add(new ConnectionPort(this, ConnectionPort.Location.LeftConnectionPort));
            Ports.Add(new ConnectionPort(this, ConnectionPort.Location.BottomConnectionPort));
        }

        public virtual void Move(Point Location, int Type = Move_Normal)
        {
            if (Type == Move_Normal)
            {
                Range = new CoreRectRange(Location.X, Location.Y, Location.X + Range.Width, Location.Y+Range.Height);
            }
            else if(Type == Move_Relative)
            {
                Point p = new Point(Range.TopLeft.X + Location.X, Range.TopLeft.Y + Location.Y);
                Range = new CoreRectRange(p.X,p.Y , p.X+Range.Width, p.Y+Range.Height);
            }
        }

        public CoreRectRange getRange()
        {
            return Range;
        }
        public Point GetLocation()
        {
            return Range.TopLeft;
        }

        public double GetWidth()
        {
            return Range.Width;
        }

        public double GetHeight()
        {
            return Range.Height;
        }

        public List<ConnectionPort> AllConnectionPort()
        {
            if (Ports.Count == 0)
                return null;
            return Ports;
        }

        public ConnectionPort NearConnectionPort(Point Location)
        {
            if (Ports.Count == 0)
                return null;
            double[] distance = new double[Ports.Count];
            for(int i=0 ; i<Ports.Count ; i++)
            {
                CoreRectRange range = Ports.ElementAt(i).getRange();
                Point center = new Point((range.TopLeft.X + range.BottomRight.X) / 2, (range.TopLeft.Y + range.BottomRight.Y) / 2);
                double dx = Math.Abs(Location.X - center.X);
                double dy = Math.Abs(Location.Y - center.Y);
                distance[i] = Math.Pow(dx * dx + dy * dy, 0.5);
            }
            double min = distance[0];
            int index = 0;
            for (int i = 0; i < Ports.Count; i++)
            {
                if (distance[i] < min)
                {
                    min = distance[i];
                    index = i;
                }
            }
            return Ports.ElementAt(index);
        }

    }
}



