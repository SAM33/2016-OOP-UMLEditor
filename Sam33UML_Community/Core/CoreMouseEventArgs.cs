/*   
 *     檔案位置 : Sam33UML/Core/CoreMouseEventArgs.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : CoreMouseEventArgs為本程式封裝的滑鼠事件參數，將本程式會用到的滑鼠事件參數統一管理，為MVC
 *                架構中的C層，此架構能夠方便程式的移植。
 *                
 *     繼承架構 : System.EventArgs
 *                |
 *                |__CoreMouseEventArgs
 *                   |
 *                   |__CoreMouseEnterEventArgs             
 *                   |__CoreMouseLeaveEventArgs
 *                   |__CoreMouseDownEventArgs
 *                   |__CoreMouseUpEventArgs   
 *                   |__CoreMouseMoveEventArgs             
 *                   |__CoreMouseDraggingEventArgs
 *                   |__CoreMouseDragEndEventArgs   
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
    public abstract class CoreMouseEventArgs : EventArgs
    {
        public int Type;
        public CoreMouseEventArgs(int _Type)
        {
            Type = _Type;
        }
    }

    public class CoreMouseEnterEventArgs : CoreMouseEventArgs
    {
        public Point Location;
        public CoreMouseEnterEventArgs(int _Type, Point _Location)
            : base(_Type)
        {
            Location = _Location;
        }
    }

    public class CoreMouseLeaveEventArgs : CoreMouseEventArgs
    {
        public Point Location;
        public CoreMouseLeaveEventArgs(int _Type, Point _Location)
            : base(_Type)
        {
            Location = _Location;
        }
    }

    public class CoreMouseDownEventArgs : CoreMouseEventArgs
    {
        public const int RightButton = 0;
        public const int LeftButton = 1;
        public int Button;
        public int Count;
        public Point Location;
        public CoreMouseDownEventArgs(int _Type, int _Button, Point _Location, int _Count)
            : base(_Type)
        {
            Button = _Button;
            Count = _Count;
            Location = _Location;
        }
    }

    public class CoreMouseUpEventArgs : CoreMouseEventArgs
    {
        public const int RightButton = 0;
        public const int LeftButton = 1;
        public int Button;
        public Point Location;
        public CoreMouseUpEventArgs(int _Type, int _Button, Point _Location)
            : base(_Type)
        {
            Button = _Button;
            Location = _Location;
        }
    }

    public class CoreMouseMoveEventArgs : CoreMouseEventArgs
    {
        public Point Location;
        public CoreMouseMoveEventArgs(int _Type, Point _Location)
            : base(_Type)
        {
            Location = _Location;
        }
    }

    public class CoreMouseDraggingEventArgs : CoreMouseEventArgs
    {
        public const int RightButton = 0;
        public const int LeftButton = 1;
        public int Button;
        public Point End;
        public Point Start;
        public double Distance;
        public CoreMouseDraggingEventArgs(int _Type, Point _Start, int _Button, Point _End, double _Distance)
            : base(_Type)
        {
            Distance = _Distance;
            Start = _Start;
            End = _End;
            Button = _Button;
        }
    }

    public class CoreMouseDragEndEventArgs : CoreMouseEventArgs
    {
        public const int RightButton = 0;
        public const int LeftButton = 1;
        public int Button;
        public Point End;
        public Point Start;
        public double Distance;
        public CoreMouseDragEndEventArgs(int _Type, Point _Start, int _Button, Point _End, double _Distance)
            : base(_Type)
        {
            Distance = _Distance;
            Start = _Start;
            End = _End;
            Button = _Button;
        }
    }

}
