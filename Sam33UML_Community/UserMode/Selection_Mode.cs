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
    public class Selection_Mode : BaseUMLMode
    {
        BaseUMLObject LockTarget = null;
        double LockTargetShiftX;
        double LockTargetShiftY;

        public Selection_Mode(MainPanel _Panel)
            : base(_Panel)
        {
            Description = "Select or move objects";
        }

        public override void ProcessEvent(CoreMouseEventArgs e)
        {
            switch (e.Type)
            {
                case CoreMouseEventProcessor.MouseEnter:
                    {
                        CoreMouseEnterEventArgs args = e as CoreMouseEnterEventArgs;
                        //Console.WriteLine("SelectMode::MouseEnter{" + args.Location.X + "," + args.Location.Y + "}");
                        break;
                    }
                case CoreMouseEventProcessor.MouseLeave:
                    {
                        CoreMouseLeaveEventArgs args = e as CoreMouseLeaveEventArgs;
                        //Console.WriteLine("SelectMode::MouseLeave{" + args.Location.X + "," + args.Location.Y + "}");
                        break;
                    }

                case CoreMouseEventProcessor.MouseDown:
                    {
                        CoreMouseDownEventArgs args = e as CoreMouseDownEventArgs;
                        if (args.Button == CoreMouseDownEventArgs.LeftButton)
                        {
                            //Console.WriteLine("SelectMode::MouseLeftButtonDown{" + args.Location.X + "," + args.Location.Y + "} : " + args.Count);
                            BaseUMLObject TopUMLObject = Panel.TopBaseUMLObjectsAt(args.Location);
                            List<BaseUMLShape> Shapes = Panel.AllBaseUMLShapes();
                            foreach(BaseUMLShape s in Shapes)
                                s.setSelected(false);
                            if (TopUMLObject != null)
                            {
                                TopUMLObject.setSelected(true);
                                LockTarget = TopUMLObject;
                                LockTargetShiftX = args.Location.X - LockTarget.GetLocation().X;
                                LockTargetShiftY = args.Location.Y - LockTarget.GetLocation().Y;
                            }
                            else
                            {
                                LockTarget = null;
                            }
                        }
                        if (args.Button == CoreMouseDownEventArgs.RightButton)
                        {
                            //Console.WriteLine("SelectMode::MouseRightButtonDown{" + args.Location.X + "," + args.Location.Y + "} : " + args.Count);
                        }
                        break;
                    }
                case CoreMouseEventProcessor.MouseUp:
                    {
                        CoreMouseUpEventArgs args = e as CoreMouseUpEventArgs;
                        if (args.Button == CoreMouseUpEventArgs.LeftButton)
                        {
                            Panel.getForeground().Children.Clear();
                            //Console.WriteLine("SelectMode::MouseLeftButtonUp{" + args.Location.X + "," + args.Location.Y + "}");
                        }
                        if (args.Button == CoreMouseUpEventArgs.RightButton)
                        {
                            //Console.WriteLine("SelectMode::MouseRightButtonUp{" + args.Location.X + "," + args.Location.Y + "}");
                        }
                        break;
                    }
                case CoreMouseEventProcessor.MouseMove:
                    {
                        CoreMouseMoveEventArgs args = e as CoreMouseMoveEventArgs;
                        //BaseUMLShape TopUMLShape = Panel.getTopShapesAt(args.Location);
                        //if(TopUMLShape==null)
                        //    return;
                        //Console.WriteLine("TopUMLShape:" + TopUMLShape.getID() + " Depth:" + TopUMLShape.getDepth());
                        //Console.WriteLine("SelectMode::MouseMove{" + args.Location.X + "," + args.Location.Y + "}");
                        break;
                    }
                case CoreMouseEventProcessor.MouseDragging:
                    {
                        CoreMouseDraggingEventArgs args = e as CoreMouseDraggingEventArgs;
                        //Console.WriteLine("SelectMode::MouseDragging{(" + args.Start.X + "," + args.Start.Y + ")->(" + args.End.X + "," + args.End.Y + ")," + args.Distance + ",key=" + args.Button + "}");
                        if (args.Button == CoreMouseDragEndEventArgs.LeftButton)
                        {
                            if (LockTarget != null)
                            {
                                double dx = args.End.X - LockTargetShiftX - LockTarget.GetLocation().X;
                                double dy = args.End.Y - LockTargetShiftY - LockTarget.GetLocation().Y;
                                LockTarget.Move(new Point(dx, dy), BaseUMLObject.Move_Relative);
                                //鄰居連帶機制啟動
                                /*
                                foreach(ConnectionPort port in LockTarget.AllConnectionPort())
                                {
                                    List<Connection> conns = port.AllConnection();
                                    foreach(Connection conn in conns)
                                    {
                                        if(conn.getConnShape().IsStartConn(conn))
                                        {
                                            BaseUMLObject UndirectlyObj = conn.getConnShape().getEndConn().getPort().getParent();
                                            if(UndirectlyObj != LockTarget)
                                                UndirectlyObj.Move(new Point(dx, dy), BaseUMLObject.Move_Relative);
                                        }
                                        else
                                        {
                                            BaseUMLObject UndirectlyObj = conn.getConnShape().getStartConn().getPort().getParent();
                                            if (UndirectlyObj != LockTarget)
                                                UndirectlyObj.Move(new Point(dx, dy), BaseUMLObject.Move_Relative);
                                        }
                                    
                                    }
                                }*/
                            }
                            else
                            {
                                DrawingGroup Group = Panel.getForeground();
                                using (DrawingContext dc = Group.Open())
                                {
                                    CoreDraw.DrawBorder(dc, new CoreRectRange(args.Start, args.End), 1, Brushes.Black, CoreDraw.LineStyle_StraightLine);
                                }
                            }
                        }
                        break;
                    }
                case CoreMouseEventProcessor.MouseDragEnd:
                    {
                        CoreMouseDragEndEventArgs args = e as CoreMouseDragEndEventArgs;
                        //Console.WriteLine("SelectMode::MouseDragEnd{(" + args.Start.X + "," + args.Start.Y + ")->("+args.End.X + "," + args.End.Y + "),"+args.Distance+",key="+args.Button+"}");
                        if (args.Button == CoreMouseDragEndEventArgs.LeftButton)
                        {
                            if (LockTarget == null)
                            {
                                List<BaseUMLObject> Objects = Panel.AllBaseUMLObjects();
                                if (Objects != null)
                                {
                                    if (Objects.Count != 0)
                                    {
                                        foreach (BaseUMLObject s in Objects)
                                            if (s.Inside(new CoreRectRange(args.Start, args.End)))
                                                s.setSelected(true);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                LockTarget = null;
                            }
                        }
                        break;
                    }
            }
        }
    }
}
