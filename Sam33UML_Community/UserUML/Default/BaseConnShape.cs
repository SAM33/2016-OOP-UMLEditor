/*   
 *     檔案位置 : Sam33UML/UserUML/Default/BaseConnShape.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : BaseConnShape為所有線型UML圖形的基底類別，BaseConnShape運作原理為透過Connection成員找到開始
 *                與結束的ConnectionPort，並取得其中央座標，將開始座標與結束座標之間透過多型呼叫正確的Draw。
 *                
 *     繼承架構 : BaseUMLShape
 *                |
 *                |__BaseConnShape
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
    public abstract class BaseConnShape : BaseUMLShape
    {
        protected int Direction;
        protected Connection StartConn;
        protected Connection EndConn;
        protected double LineWidth;
        public const int Direction_NULL = 0;
        public const int Direction_W_E = 1;
        public const int Direction_NW_SE = 2;
        public const int Direction_N_S = 3;
        public const int Direction_NE_SW = 4;
        public const int Direction_E_W = 5;
        public const int Direction_SE_NW = 6;
        public const int Direction_S_N = 7;
        public const int Direction_SW_NE = 8;


        public Connection getStartConn()
        {
            return StartConn;
        }

        public Connection getEndConn()
        {
            return EndConn;
        }

        public bool HasConnection()
        {
            if (StartConn == null || EndConn == null)
                return false;
            return true;
        }

        public BaseConnShape()
        {
            LineWidth = 2;
        }

        public BaseConnShape(Connection _StartConn, Connection _EndConn)
        {
            LineWidth = 2;
            UpdateConeection(_StartConn, _EndConn);
        }

        public int getDirection()
        {
            return Direction;
        }

        public double getLineWidth()
        {
            return LineWidth;
        }

        public void setLineWidth(double _LineWidth)
        {
            LineWidth = _LineWidth;
        }

        public bool IsStartConn(Connection Conn)
        {
            if (StartConn == Conn)
                return true;
            return false;
        }

        public void UpdateConeection(Connection _StartConn, Connection _EndConn)
        {
            StartConn = _StartConn;
            EndConn = _EndConn;
            if (HasConnection())
            {
                Point Start = StartConn.getPort().getCenter();
                Point End = EndConn.getPort().getCenter();
                double tlx = Math.Min(Start.X, End.X);
                double tly = Math.Min(Start.Y, End.Y);
                double brx = Math.Max(Start.X, End.X);
                double bry = Math.Max(Start.Y, End.Y);
                if (Start.X == End.X && Start.Y == End.Y)
                    Direction = Direction_NULL;
                if (Start.X == End.X && Start.Y > End.Y)
                    Direction = Direction_S_N;
                if (Start.X == End.X && Start.Y < End.Y)
                    Direction = Direction_N_S;
                if (Start.X < End.X && Start.Y == End.Y)
                    Direction = Direction_W_E;
                if (Start.X < End.X && Start.Y > End.Y)
                    Direction = Direction_SW_NE;
                if (Start.X < End.X && Start.Y < End.Y)
                    Direction = Direction_NW_SE;
                if (Start.X > End.X && Start.Y == End.Y)
                    Direction = Direction_E_W;
                if (Start.X > End.X && Start.Y > End.Y)
                    Direction = Direction_SE_NW;
                if (Start.X > End.X && Start.Y < End.Y)
                    Direction = Direction_NE_SW;
            }
        }
    }
}
