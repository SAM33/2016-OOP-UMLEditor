/*   
 *     檔案位置 : Sam33UML/UserMode/None_Mode.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : 不做任何事情的工具
 *                
 *     繼承架構 : BaseUMLMode
 *                |
 *                |__None__Mode
 *                
 *     最後修改 : 2015/01/20
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sam33UML.UserMode.Default;
using Sam33UML.Controls;

namespace Sam33UML.UserMode
{
    public class None_Mode : BaseUMLMode
    {
        public None_Mode(MainPanel _Panel)
            : base(_Panel)
        {
            Description = "Not select any tool";
        }
        public override void ProcessEvent(Core.CoreMouseEventArgs args)
        {
            return;
        }
    }
}
