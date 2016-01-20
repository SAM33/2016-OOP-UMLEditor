/*   
 *     檔案位置 : Sam33UML/UserUML/Default/ConnectionPort.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : ConnectionPort每一個BaseUMLObject所持有的部件，用於建立Connection。
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
using Sam33UML.Core;
namespace Sam33UML.UserUML.Default
{
    public class ConnectionPort
    {
        public enum Location { TopConnectionPort, LeftConnectionPort, RightConnectionPort, BottomConnectionPort }
        private List<Connection> Conns;
        private BaseUMLObject Parent;
        private Location PortLocation;
        private const double Width = 10;
        private const double Height = 10;
        bool HighLight = false;

        public BaseUMLObject getParent()
        {
            return Parent;
        }

        public ConnectionPort(BaseUMLObject _Parent,Location _Location)
        {
            Conns = new List<Connection>();
            Parent = _Parent;
            PortLocation = _Location;
        }

        public void setHighLight(bool _HighLight)
        {
            HighLight = _HighLight;
        }

        public void AddConnection(Connection conn)
        {
            Conns.Add(conn);
        }

        public List<Connection> AllConnection()
        {
            return Conns;
        }
        
        public Point getCenter()
        {
            Point Center = new Point(0,0);
            switch (PortLocation)
            {
                case Location.TopConnectionPort:
                    {
                        Center = new Point(Parent.GetLocation().X + Parent.GetWidth() / 2, Parent.GetLocation().Y);

                        break;
                    }
                case Location.RightConnectionPort:
                    {
                        Center = new Point(Parent.GetLocation().X + Parent.GetWidth(), Parent.GetLocation().Y + Parent.GetHeight() / 2);

                        break;
                    }
                case Location.BottomConnectionPort:
                    {
                        Center = new Point(Parent.GetLocation().X + Parent.GetWidth() / 2, Parent.GetLocation().Y + Parent.GetHeight());

                        break;
                    }
                case Location.LeftConnectionPort:
                    {
                        Center = new Point(Parent.GetLocation().X, Parent.GetLocation().Y + Parent.GetHeight() / 2);
                        break;
                    }
            }
            return Center;
        }

        public CoreRectRange getRange()
        {
            Point Center = getCenter();
            CoreRectRange Range = new CoreRectRange(Center.X - Width / 2, Center.Y - Height / 2, Center.X + Width / 2, Center.Y + Height / 2);
            return Range;
        }

        public void Draw(DrawingContext dc)
        {
            if(HighLight)
                CoreDraw.DrawRect(dc, getRange(), Brushes.Blue);
            else
                CoreDraw.DrawRect(dc, getRange(), Brushes.Red);
        }

    }
}
