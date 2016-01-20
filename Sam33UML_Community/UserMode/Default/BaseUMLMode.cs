/*   
 *     檔案位置 : Sam33UML/UserMode/Default/BaseUMLMode.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : BaseUMLMode為所有工具模式的基底類別，當來子自MVC架構的C層(CoreMouseEventProcessor)發出滑鼠事件時
 *                將會透過多形呼叫ProcessEvent，子類別需要覆寫ProcessEvent來加入工具模式該有的表現，並透過MainPanel
 *                的reference 'Panel'來對MainPanel進行存取。
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
using Sam33UML.Core;
using Sam33UML.Controls;

namespace Sam33UML.UserMode.Default
{
    public abstract class BaseUMLMode
    {
        public abstract void ProcessEvent(CoreMouseEventArgs args);
        protected MainPanel Panel;
        protected String Description;
        public String getDescription()
        {
            return Description;
        }
        public BaseUMLMode(MainPanel _Panel)
        {
            Description = "";
            Panel = _Panel;
        }
    }
}
