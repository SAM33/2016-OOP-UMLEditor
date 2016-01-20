/*   
 *     檔案位置 : Sam33UML/UserUML/Default/BaseUMLShape.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : BaseUMLShape為本程式所有圖形的基底類別，擁有成員Depth來決定繪圖的優先程度。
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
using Sam33UML.Controls;
using Sam33UML.Core;

namespace Sam33UML.UserUML.Default
{
    public abstract class BaseUMLShape
    {
        protected String ID;
        protected int Depth;
        protected bool Selected;
        public BaseUMLShape()
            : base()
        {
            ID = "ID=" + this.GetHashCode();
            Selected = false;
            Depth = 0; //Top
        }
        public int getDepth()
        {
            return Depth;
        }
        public void setDepth(int _Depth)
        {
            Depth = _Depth;
        }
        public String getID()
        {
            return ID;
        }
        public bool getSelected()
        {
            return Selected;
        }
        public void setSelected(bool _Selected)
        {
            Selected = _Selected;
        }
        public abstract void Draw(DrawingContext dc);
        public abstract bool Contain(Point p);
        public abstract bool Inside(CoreRectRange r);
    }
}
