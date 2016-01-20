/*   
 *     檔案位置 : Sam33UML/Controls/ToolPanel.xaml.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : 此檔案為使用者的工具箱，使用者點選工具箱時會觸發工具箱的ToolChange事件，
 *                如此一來就能通知MainPanel切換工具，此物件繼承了UserControl故有一個xml檔
 *                案來定義其外觀。
 *                
 *     繼承架構 : System.Windows.Controls.UserControl
 *                |
 *                |__ToolPanel
 *                
 *                System.EventArgs
 *                |
 *                |__ModeChangeEventArgs
 *                
 *     最後修改 : 2015/1/20
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
using Sam33UML.UserMode.Default;
using Sam33UML.UserMode;


namespace Sam33UML.Controls
{
    /// <summary>
    /// ToolsPanel.xaml 的互動邏輯
    /// </summary>
    public partial class ToolsPanel : UserControl
    {
        //This is the event which trigger when user select another tool
        public event EventHandler<ModeChangeEventArgs> ModeChange;
        private List<ToolButton> Buttons;

        //Add new tool to toolsbox
        public void AddTool(BaseUMLMode Mode, Uri icon1, Uri icon2, ToolBar location)
        {
            ToolButton UIButton = new Controls.ToolButton(Mode, icon1, icon2);
            UIButton.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(ToolsButton_MouseLeftButtonDown), true);
            UIButton.AddHandler(MouseEnterEvent, new MouseEventHandler(ToolsButton_MouseEnter), true);
            UIButton.AddHandler(MouseLeaveEvent, new MouseEventHandler(ToolsPanel_MouseEnter), true);
            location.Items.Add(UIButton);
            Buttons.Add(UIButton);
        }
        public void AddSeparator(double width,ToolBar location)
        {
            Separator sp = new Separator();
            sp.Width = width;
            BrushConverter bc = new BrushConverter();
            sp.Background = (Brush)bc.ConvertFrom("Transparent");
            location.Items.Add(sp);
        }

        //Constructor
        public ToolsPanel()
        {
            InitializeComponent();
            Buttons = new List<ToolButton>();
            AddHandler(MouseEnterEvent, new MouseEventHandler(ToolsPanel_MouseEnter), false);
        }

        void ToolsPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            this.InfoBox.Content = "Info:";
        }

        void ToolsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Controls.ToolButton obj = ( Controls.ToolButton)sender;
            this.InfoBox.Content = "Info:"+obj.getMode().getDescription();
        }

        void ToolsButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (ToolButton btn in Buttons)
                btn.setSelected(false);
            ToolButton t = (ToolButton)sender;
            t.setSelected(true);
            OnToolChange(new ModeChangeEventArgs(t.getMode()));
        }

        protected void OnToolChange(ModeChangeEventArgs e)
        {
            EventHandler<ModeChangeEventArgs> handler = ModeChange;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    public class ModeChangeEventArgs : EventArgs
    {
        private BaseUMLMode Mode;
        public BaseUMLMode getMode()
        {
            return Mode;
        }
        public ModeChangeEventArgs(BaseUMLMode m)
        {
            Mode = m;
        }
    }
}
