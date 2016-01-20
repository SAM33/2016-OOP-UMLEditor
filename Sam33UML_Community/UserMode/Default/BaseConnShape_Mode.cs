/*   
 *     檔案位置 : Sam33UML/UserMode/Default/BaseConnShape_Mode.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : BaseConnShape_Mode為畫線工具模式的基底類別，透過覆寫BaseUMLMode並將其加入畫線條所該有的
 *                滑鼠事件處理，並提供ProcessPreviewConnShape、ProcessAddConnShape兩個接口給子類別覆寫，
 *                當使用者正在拉線條，以及線條被新增時，會分別將必要參數丟給ProcessPreviewConnShape、
 *                ProcessAddConnShape，子類別只需時作此部分就能完成各種畫線模式的實作。
 *                
 *     繼承架構 : BaseUMLMode
 *                |
 *                |__BaseConnShape_Mode
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
    public abstract class BaseConnShape_Mode : BaseUMLMode
    {
        private BaseUMLObject LastStartTarget = null;
        private BaseUMLObject LastEndTarget = null;
        public BaseConnShape_Mode(MainPanel _Panel)
            : base(_Panel)
        {

        }
        protected abstract void ProcessPreviewConnShape(DrawingContext dc, Point Start, Point End);
        protected abstract void ProcessAddConnShape(ConnectionPort StartPort, ConnectionPort EndPort);
        

        public override void ProcessEvent(CoreMouseEventArgs e)
        {
            switch (e.Type)
            {
                case CoreMouseEventProcessor.MouseDown:
                    {
                        CoreMouseDownEventArgs args = e as CoreMouseDownEventArgs;
                        BaseUMLObject LastStartTarget = Panel.TopBaseUMLObjectsAt(args.Location);
                        break;
                    }
                case CoreMouseEventProcessor.MouseDragging:
                    {
                        CoreMouseDraggingEventArgs args = e as CoreMouseDraggingEventArgs;
                        //Console.WriteLine("BaseConnShape_Mode::MouseDragging{(" + args.Start.X + "," + args.Start.Y + ")->(" + args.End.X + "," + args.End.Y + ")," + args.Distance + ",key=" + args.Button + "}");
                        BaseUMLObject StartTarget = Panel.TopBaseUMLObjectsAt(args.Start);
                        BaseUMLObject EndTarget = Panel.TopBaseUMLObjectsAt(args.End);
                        ConnectionPort StartPort=null,EndPort=null;
                        DrawingGroup PanelForeground = Panel.getForeground();
                        if(StartTarget!=null)
                            StartPort = StartTarget.NearConnectionPort(args.Start);
                        if (EndTarget != null)
                            EndPort = EndTarget.NearConnectionPort(args.End);
                        if (StartTarget != null)  //開始點在BaseUMLObject上
                        {
                            if (LastEndTarget != null)  //上次的EndTarget不為空,將他的Ports顏色還原並取消選取
                            {
                                foreach (ConnectionPort port in LastEndTarget.AllConnectionPort())
                                    port.setHighLight(false);
                                LastEndTarget.setSelected(false);
                                LastEndTarget = null;
                            }
                            if (EndTarget != null)  //開始點在BaseUMLObject上,結束點在BaseUMLObject上,把開始BaseUMLObject和結束BaseUMLObject的目標Port來HighLight 
                            {
                                foreach (ConnectionPort port in StartTarget.AllConnectionPort())  //將開始BaseUMLObject顏色還原
                                    port.setHighLight(false);
                                if (StartPort != null)  //設定開始BaseUMLObject目標Port顏色
                                    StartPort.setHighLight(true);
                                StartTarget.setSelected(true);
                                if (EndTarget != StartTarget)  //結束和開始不相同
                                {
                                    foreach (ConnectionPort port in EndTarget.AllConnectionPort())  //將結束BaseUMLObject顏色還原
                                        port.setHighLight(false);
                                    if (EndPort != null)  //設定結束BaseUMLObject目標Port顏色
                                        EndPort.setHighLight(true);
                                    EndTarget.setSelected(true);
                                    LastEndTarget = EndTarget;  //設定上次的EndTarget為此次的EndTarget
                                }
                            }
                            else  //開始點在BaseUMLObject上,結束點不在BaseUMLObject上
                            {
                                //開始點在BaseUMLObject上，結束點不在BaseUMLObject上
                                //將開始BaseUMLObject顏色還原
                                foreach (ConnectionPort port in StartTarget.AllConnectionPort())
                                    port.setHighLight(false);
                                //選取開始BaseUMLObject讓其能繪出Port顏色
                                StartTarget.setSelected(true);

                            }
                            using (DrawingContext dc = PanelForeground.Open())  //要求子類別畫出預覽圖形
                            {
                                ProcessPreviewConnShape(dc, args.Start, args.End);
                            }
                        }
                        else
                        {
                            if (LastStartTarget != null)  //開始點不在BaseUMLObject上,將上次StartTarget的Port顏色還原
                            {
                                foreach (ConnectionPort port in LastStartTarget.AllConnectionPort())
                                    port.setHighLight(false);
                                LastStartTarget = null;
                            }
                        }
                        break;
                    }
                case CoreMouseEventProcessor.MouseDragEnd:
                    {
                        CoreMouseDragEndEventArgs args = e as CoreMouseDragEndEventArgs;
                        //Console.WriteLine("BaseConnShape_Mode::MouseDragEnd{(" + args.Start.X + "," + args.Start.Y + ")->(" + args.End.X + "," + args.End.Y + ")," + args.Distance + ",key=" + args.Button + "}");
                        BaseUMLObject StartTarget = Panel.TopBaseUMLObjectsAt(args.Start);
                        BaseUMLObject EndTarget = Panel.TopBaseUMLObjectsAt(args.End);
                        ConnectionPort StartPort = null, EndPort = null;
                        DrawingGroup PanelForeground = Panel.getForeground();
                        if (StartTarget != null)
                        {
                            foreach (ConnectionPort port in StartTarget.AllConnectionPort())  //將開始BaseUMLObject顏色還原
                                port.setHighLight(false);
                            StartTarget.setSelected(false);  //將開始BaseUMLObject取消選取
                            StartPort = StartTarget.NearConnectionPort(args.Start);
                        }
                        if (EndTarget != null)
                        {
                            foreach (ConnectionPort port in EndTarget.AllConnectionPort())  //將開始BaseUMLObject顏色還原
                                port.setHighLight(false);
                            EndTarget.setSelected(false);  //將開始BaseUMLObject取消選取
                            EndPort = EndTarget.NearConnectionPort(args.End);
                        }
                        if (StartPort != null && EndPort != null && StartTarget != EndTarget)
                        {
                            ProcessAddConnShape(StartPort, EndPort);
                        }
                        break;
                    }
                case CoreMouseEventProcessor.MouseUp:
                    {
                        Panel.getForeground().Children.Clear();
                        break;
                    }
            }
        }
    }
}
