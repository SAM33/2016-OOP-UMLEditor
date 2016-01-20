/*   
 *     檔案位置 : Sam33UML/UserUML/GeneralizationConnShape.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : UML的Generalization圖形
 *                
 *     繼承架構 : BaseConnShape
 *                |
 *                |__GeneralizationConnShape
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
    public class GeneralizationConnShape : BaseConnShape
    {
        public GeneralizationConnShape(Connection _StartConn, Connection _EndConn)
            : base(_StartConn, _EndConn)
        {

        }

        public GeneralizationConnShape()
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
            const double ArrowSize = 15;
            Point Arrow_Top = EndConn.getPort().getCenter();
            double tempx1 = StartConn.getPort().getCenter().X;
            double tempy1 = StartConn.getPort().getCenter().Y;
            double tempx2 = EndConn.getPort().getCenter().X;
            double tempy2 = EndConn.getPort().getCenter().Y;
            double LineDistance = Math.Pow(Math.Pow(tempx1 - tempx2, 2) + Math.Pow(tempy1 - tempy2, 2), 0.5);
            Vector LineVector = new Vector(tempx2 - tempx1, tempy2 - tempy1);
            Vector UnitArrowVector = LineVector / LineDistance;
            Vector ArrowVector = UnitArrowVector * ArrowSize;
            Point Arrow_Bottom = Arrow_Top - ArrowVector;
            Vector RightDotVector = new Vector(ArrowVector.Y * (-1), ArrowVector.X);
            Vector LeftDotVector = RightDotVector * (-1);
            Point Arrow_BottomLeft = Arrow_Bottom + LeftDotVector;
            Point Arrow_BottomRight = Arrow_Bottom + RightDotVector;
            if (HasConnection())
            {
                CoreDraw.DrawLine(dc, StartConn.getPort().getCenter(), Arrow_Bottom, LineWidth, Brushes.Blue, CoreDraw.LineStyle_StraightLine);
                CoreDraw.DrawLine(dc, Arrow_Bottom, Arrow_BottomRight, LineWidth, Brushes.Blue, CoreDraw.LineStyle_StraightLine);
                CoreDraw.DrawLine(dc, Arrow_BottomRight, Arrow_Top, LineWidth, Brushes.Blue, CoreDraw.LineStyle_StraightLine);
                CoreDraw.DrawLine(dc, Arrow_Top, Arrow_BottomLeft, LineWidth, Brushes.Blue, CoreDraw.LineStyle_StraightLine);
                CoreDraw.DrawLine(dc, Arrow_BottomLeft, Arrow_Bottom, LineWidth, Brushes.Blue, CoreDraw.LineStyle_StraightLine);
            }
        }

    }
}
