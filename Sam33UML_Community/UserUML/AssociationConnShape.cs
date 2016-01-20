/*   
 *     檔案位置 : Sam33UML/UserUML/AssociationConnShape.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : UML的Association圖形
 *                
 *     繼承架構 : BaseConnShape
 *                |
 *                |__AssociationConnShape
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
    public class AssociationConnShape : BaseConnShape
    {
        public AssociationConnShape(Connection _StartConn, Connection _EndConn)
            : base(_StartConn, _EndConn)
        {

        }

        public AssociationConnShape()
            : base()
        {

        }

        public override bool Inside(CoreRectRange r)
        {
            return false;
        }

        public override bool Contain(Point p)
        {
            return false;
        }

        public override void Draw(DrawingContext dc)
        {

            if (HasConnection())
                CoreDraw.DrawLine(dc, StartConn.getPort().getCenter(), EndConn.getPort().getCenter(), LineWidth, Brushes.Blue, CoreDraw.LineStyle_StraightLine);

        }

    }
}
