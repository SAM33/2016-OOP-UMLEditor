/*   
 *     檔案位置 : Sam33UML/UserMode/UseCaseUMLObject_Mode.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : UseCaseUMLObject_Mode用來讓使用者新透過滑鼠操作來新增UseCaseUMLObject
 *                
 *     繼承架構 : BaseUMLMode
 *                |
 *                |__UseCaseUMLObject_Mode
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
    public class UseCaseUMLObject_Mode : BaseUMLMode
    {
        int DefaultDepth = 0;
        public UseCaseUMLObject_Mode(MainPanel _Panel)
            : base(_Panel)
        {
            Description = "Create a class object";
        }

        public override void ProcessEvent(CoreMouseEventArgs e)
        {
            switch (e.Type)
            {
                case CoreMouseEventProcessor.MouseDown:
                    {
                        CoreMouseDownEventArgs args = e as CoreMouseDownEventArgs;
                        if (args.Button == CoreMouseDownEventArgs.LeftButton)
                        {
                            Vector size = new Vector(150, 100);
                            UseCaseUMLObject temp = new UseCaseUMLObject(new CoreRectRange(args.Location, args.Location + size));
                            Panel.AddShape(temp);
                            temp.setDepth(DefaultDepth);
                            Panel.UpdateDepth();
                        }
                        break;
                    }
            }
        }
    }
}
