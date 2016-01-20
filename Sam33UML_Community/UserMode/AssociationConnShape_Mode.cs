/*   
 *     檔案位置 : Sam33UML/UserMode/AssociationConnShape_Mode.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : AssociationConnShape_Mode用來讓使用者新透過滑鼠操作來新增AssociationConnShape
 *                
 *     繼承架構 : BaseConnShape_Mode
 *                |
 *                |__AssociationConnShape_Mode
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
using Sam33UML.UserUML;
using Sam33UML.UserUML.Default;
using Sam33UML.UserMode;
using Sam33UML.UserMode.Default;

namespace Sam33UML.UserMode
{
    public class AssociationConnShape_Mode : BaseConnShape_Mode
    {
        protected int DefaultDepth;
        protected double DefaultLineWidth;
        public AssociationConnShape_Mode(MainPanel _Panel)
            : base(_Panel)
        {
            DefaultDepth = 0;
            DefaultLineWidth = 2;
            Description = "Create a association line";
        }

        protected override void ProcessPreviewConnShape(DrawingContext dc, Point Start, Point End)
        {
            CoreDraw.DrawLine(dc, Start, End, 2, Brushes.Blue, CoreDraw.LineStyle_MultipleSegment);
        }

        protected override void ProcessAddConnShape(ConnectionPort StartPort, ConnectionPort EndPort)
        {
            BaseConnShape Line = new AssociationConnShape();
            Connection StartConn = new Connection(StartPort, Line);
            Connection EndConn = new Connection(EndPort, Line);
            StartPort.AddConnection(StartConn);
            EndPort.AddConnection(EndConn);
            Line.UpdateConeection(StartConn, EndConn);
            Panel.AddShape(Line);
            Line.setDepth(DefaultDepth);
            Panel.UpdateDepth();      
        }
    }
}
