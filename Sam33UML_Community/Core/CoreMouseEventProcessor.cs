/*   
 *     檔案位置 : Sam33UML/Core/CoreMouseEventProcessor.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : CoreMouseEventProcessor為本程式的滑鼠事件管理核心，為MVC架構中的C以及M層，能夠將V層的WPF滑鼠事件
 *                作為輸入，以M層的邏輯來進行解析，找出本程式中對應的滑鼠事件並發出CoreMouseEventArgs通知C層。
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

namespace Sam33UML.Core
{
    public class CoreMouseEventProcessor
    {
        private const int TypeAmount = 7;
        public const int MouseMove = 0;
        public const int MouseDown = 1;
        public const int MouseUp = 2;
        public const int MouseDragging = 3;
        public const int MouseDragEnd = 4;
        public const int MouseEnter = 5;
        public const int MouseLeave = 6;
        bool[] Enable;
        Point MousePressedPoint;
        double DragThreshold;
        public event EventHandler<CoreMouseEventArgs> MouseEvent;
        bool MouseLeftPressed = false;
        bool MouseRightPressed = false;
        public void setDragThreshold(double value)
        {
            DragThreshold = value;
        }

        public void EnableFuntion(int ID)
        {
            if (ID < 0 || ID >= TypeAmount)
                throw new ArgumentException("CoreMouseEvent.cs/CoreMouseEventProcessor/EnableEventProcess : ID out of range");
            Enable[ID] = true;
        }

         public void DisableFuntion(int ID)
        {
            if (ID < 0 || ID >= TypeAmount)
                throw new ArgumentException("CoreMouseEvent.cs/CoreMouseEventProcessor/EnableEventProcess : ID out of range");
            Enable[ID] = false;
        }

        private void OnMouseEvent(CoreMouseEventArgs e, Object obj)
        {
            EventHandler<CoreMouseEventArgs> handler = this.MouseEvent;
            if (handler != null)
                handler(obj, e);
        }

        public CoreMouseEventProcessor(FrameworkElement Target)
        {
            Enable = new bool[TypeAmount];
            for(int i=0 ; i<TypeAmount ; i++)
                Enable[i] = true;
            DragThreshold = 10;
            Target.PreviewMouseDown += Target_PreviewMouseDown;
            Target.PreviewMouseUp += Target_PreviewMouseUp;
            Target.PreviewMouseMove += Target_PreviewMouseMove;
            Target.MouseEnter += Target_MouseEnter;
            Target.MouseLeave += Target_MouseLeave;
        }

        void Target_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender as FrameworkElement == null)
                throw new ArgumentException("CoreMouseEvent.cs/CoreMouseEventProcessor/Target_MouseLeave : Can't cover sender to FrameworkElement");
            FrameworkElement Target = sender as FrameworkElement;
            if (Enable[MouseLeave])
            {
                CoreMouseEventArgs info = new CoreMouseLeaveEventArgs(CoreMouseEventProcessor.MouseLeave, e.GetPosition(Target));
                OnMouseEvent(info, Target);
            }
        }

        void Target_MouseEnter(object sender, MouseEventArgs e)
        {
            if(sender as FrameworkElement == null)
                throw new ArgumentException("CoreMouseEvent.cs/CoreMouseEventProcessor/Target_MouseEnter : Can't cover sender to FrameworkElement");
            FrameworkElement Target = sender as FrameworkElement;
            if (Enable[MouseEnter])
            {
                CoreMouseEventArgs info = new CoreMouseEnterEventArgs(CoreMouseEventProcessor.MouseEnter, e.GetPosition(Target));
                OnMouseEvent(info, Target);
            }
        }

        void Target_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (sender as FrameworkElement == null)
                throw new ArgumentException("CoreMouseEvent.cs/CoreMouseEventProcessor/Target_PreviewMouseMove : Can't cover sender to FrameworkElement");
            FrameworkElement Target = sender as FrameworkElement;
            if (Enable[MouseMove])
            {
                CoreMouseEventArgs info = new CoreMouseMoveEventArgs(CoreMouseEventProcessor.MouseMove, e.GetPosition(Target));
                OnMouseEvent(info, Target);
            }
            if (MouseLeftPressed)
            {
                if(Enable[MouseDragging])
                {
                    Point NowPosition = e.GetPosition(Target);
                    double Distance = Math.Pow(((MousePressedPoint.X - NowPosition.X) * (MousePressedPoint.X - NowPosition.X) + (MousePressedPoint.Y - NowPosition.Y) * (MousePressedPoint.Y - NowPosition.Y)), 0.5);
                    if (Distance > DragThreshold)
                    {
                        CoreMouseEventArgs info = new CoreMouseDraggingEventArgs(CoreMouseEventProcessor.MouseDragging, MousePressedPoint, CoreMouseDraggingEventArgs.LeftButton, NowPosition, Distance);
                        OnMouseEvent(info, Target);
                    }
                }
            }
            if (MouseRightPressed)
            {
                if (Enable[MouseDragging])
                {
                    Point NowPosition = e.GetPosition(Target);
                    double Distance = Math.Pow(((MousePressedPoint.X - NowPosition.X) * (MousePressedPoint.X - NowPosition.X) + (MousePressedPoint.Y - NowPosition.Y) * (MousePressedPoint.Y - NowPosition.Y)), 0.5);
                    if (Distance > DragThreshold)
                    {
                        CoreMouseEventArgs info = new CoreMouseDraggingEventArgs(CoreMouseEventProcessor.MouseDragging, MousePressedPoint, CoreMouseDraggingEventArgs.RightButton, NowPosition, Distance);
                        OnMouseEvent(info, Target);
                    }
                }
            }
        }
        

        void Target_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender as FrameworkElement == null)
                throw new ArgumentException("CoreMouseEvent.cs/CoreMouseEventProcessor/Target_PreviewMouseUp : Can't cover sender to FrameworkElement");
            FrameworkElement Target = sender as FrameworkElement;
            if (Enable[MouseUp])
            {
                CoreMouseEventArgs info1 = new CoreMouseUpEventArgs(CoreMouseEventProcessor.MouseUp, CoreMouseDownEventArgs.LeftButton, e.GetPosition(Target));
                CoreMouseEventArgs info2 = new CoreMouseUpEventArgs(CoreMouseEventProcessor.MouseUp, CoreMouseDownEventArgs.RightButton, e.GetPosition(Target));
                if (e.ChangedButton == MouseButton.Left)
                    OnMouseEvent(info1, Target);
                if (e.ChangedButton == MouseButton.Right)
                    OnMouseEvent(info2, Target);
            }
            if(MouseLeftPressed)
            {
                if(Enable[MouseDragEnd])
                {
                    Point NowPosition = e.GetPosition(Target);
                    double Distance = Math.Pow(((MousePressedPoint.X - NowPosition.X) * (MousePressedPoint.X - NowPosition.X) + (MousePressedPoint.Y - NowPosition.Y) * (MousePressedPoint.Y - NowPosition.Y)), 0.5);
                    if (Distance > DragThreshold)
                    {
                        CoreMouseEventArgs info = new CoreMouseDragEndEventArgs(CoreMouseEventProcessor.MouseDragEnd, MousePressedPoint, CoreMouseDragEndEventArgs.LeftButton, NowPosition, Distance);
                        OnMouseEvent(info, Target);
                    }
                }
            }
            if (MouseRightPressed)
            {
                if (Enable[MouseDragEnd])
                {
                    Point NowPosition = e.GetPosition(Target);
                    double Distance = Math.Pow(((MousePressedPoint.X - NowPosition.X) * (MousePressedPoint.X - NowPosition.X) + (MousePressedPoint.Y - NowPosition.Y) * (MousePressedPoint.Y - NowPosition.Y)), 0.5);
                    if (Distance > DragThreshold)
                    {
                        CoreMouseEventArgs info = new CoreMouseDragEndEventArgs(CoreMouseEventProcessor.MouseDragEnd, MousePressedPoint, CoreMouseDragEndEventArgs.RightButton, NowPosition, Distance);
                        OnMouseEvent(info, Target);
                    }
                }
            }
            if(e.ChangedButton == MouseButton.Left)
                MouseLeftPressed = false;
            if(e.ChangedButton == MouseButton.Right)
                MouseRightPressed = false;
        }

        void Target_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender as FrameworkElement == null)
                throw new ArgumentException("CoreMouseEvent.cs/CoreMouseEventProcessor/Target_PreviewMouseDown : Can't cover sender to FrameworkElement");
            FrameworkElement Target = sender as FrameworkElement;
            MousePressedPoint = e.GetPosition(Target);
            if (Enable[MouseDown])
            {
                CoreMouseEventArgs info1 = new CoreMouseDownEventArgs(CoreMouseEventProcessor.MouseDown, CoreMouseDownEventArgs.LeftButton, e.GetPosition(Target), e.ClickCount);
                CoreMouseEventArgs info2 = new CoreMouseDownEventArgs(CoreMouseEventProcessor.MouseDown, CoreMouseDownEventArgs.RightButton, e.GetPosition(Target), e.ClickCount);
                if (e.ChangedButton == MouseButton.Left)
                    OnMouseEvent(info1, Target);
                if (e.ChangedButton == MouseButton.Right)
                    OnMouseEvent(info2, Target);
            }
            if (e.ChangedButton == MouseButton.Left)
                MouseLeftPressed = true;
            if (e.ChangedButton == MouseButton.Right)
                MouseRightPressed = true;
        }



        

    }
}
