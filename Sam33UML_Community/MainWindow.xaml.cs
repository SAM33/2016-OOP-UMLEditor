/*   
 *     檔案位置 : Sam33UML/MainWindowL.xaml.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : 本程式的主視窗，用來將工具插入工具箱中，以及存放MainPanel以及ToolPanel，或是遞送來自Menu的事件。
 *                
 *     繼承架構 : System.Windows
 *                |
 *                |__MainWindow
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
using Sam33UML.Others;
using Sam33UML.Controls;
using Sam33UML.UserMode;
using Sam33UML.UserMode.Default;



namespace Sam33UML
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        MainPanel Main;
        const double ToolBoxWidth = 200;
        const double ToolBoxHeight = 600;

        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            double ProgramHeight = System.Windows.SystemParameters.PrimaryScreenHeight * 0.5;
            double ProgramWidth = System.Windows.SystemParameters.PrimaryScreenWidth * 0.5;
            Console.WriteLine(ProgramWidth + "x" + ProgramHeight);
            this.MinWidth = ProgramWidth;
            this.MinHeight = ProgramHeight;
            Scroll.Width = ProgramWidth - ToolBoxWidth - 20;
            Scroll.Height = ProgramHeight - Menu.Height;
            Main = new MainPanel();
            Main.Width = 2048;
            Main.Height = 1536;
            DrawBorder.Child = Main;

            ToolsPanel toolspanel = new ToolsPanel();
            toolspanel.Width = ToolBoxWidth;
            toolspanel.Height = ToolBoxHeight;
            ToolsPanelBorder.Child = toolspanel;
            toolspanel.AddTool(new None_Mode(Main), new Uri(@"/Sam33UML;component/Resources/None.png", UriKind.Relative), new Uri(@"/Sam33UML;component/Resources/None_Selected.png", UriKind.Relative), toolspanel.RowBox0);
            toolspanel.AddSeparator(2, toolspanel.RowBox0);
            toolspanel.AddTool(new Selection_Mode(Main), new Uri(@"/Sam33UML;component/Resources/Selection.png", UriKind.Relative), new Uri(@"/Sam33UML;component/Resources/Selection_Selected.png", UriKind.Relative), toolspanel.RowBox0);
            toolspanel.AddTool(new ClassUMLObject_Mode(Main), new Uri(@"/Sam33UML;component/Resources/UMLClass_Normal.png", UriKind.Relative), new Uri(@"/Sam33UML;component/Resources/UMLClass_Selected.png", UriKind.Relative), toolspanel.RowBox1);
            toolspanel.AddSeparator(2, toolspanel.RowBox1);
            toolspanel.AddTool(new UseCaseUMLObject_Mode(Main), new Uri(@"/Sam33UML;component/Resources/UMLUseCase_Normal.png", UriKind.Relative), new Uri(@"/Sam33UML;component/Resources/UMLUseCase_Selected.png", UriKind.Relative), toolspanel.RowBox1);
            toolspanel.AddTool(new AssociationConnShape_Mode(Main), new Uri(@"/Sam33UML;component/Resources/UMLAssociationLine.png", UriKind.Relative), new Uri(@"/Sam33UML;component/Resources/UMLAssociationLine_Selected.png", UriKind.Relative), toolspanel.RowBox2);
            toolspanel.AddSeparator(2, toolspanel.RowBox2);
            toolspanel.AddTool(new CompositionConnShape_Mode(Main), new Uri(@"/Sam33UML;component/Resources/UMLCompositionLine.png", UriKind.Relative), new Uri(@"/Sam33UML;component/Resources/UMLCompositionLine_Selected.png", UriKind.Relative), toolspanel.RowBox2);
            toolspanel.AddTool(new GeneralizationConnShape_Mode(Main), new Uri(@"/Sam33UML;component/Resources/UMLGeneralizationLine.png", UriKind.Relative), new Uri(@"/Sam33UML;component/Resources/UMLGeneralizationLine_Selected.png", UriKind.Relative), toolspanel.RowBox3);
            toolspanel.ModeChange += toolspanel_ModeChange;
            //source.UriSource = new Uri("pack://application:,,,/Sam33UML;component/Resources/UserIcon.png");
        }

        void toolspanel_ModeChange(object sender, ModeChangeEventArgs e)
        {
            Main.setMode(e.getMode());
        }

        private void Windows_File_New_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window(object sender, RoutedEventArgs e)
        {

        }

        private void Windows_Edit_ChangeObjectName_Click(object sender, RoutedEventArgs e)
        {
            Main.Do_ChangeObjectName();
        }

        private void Windows_Edit_Group_Click(object sender, RoutedEventArgs e)
        {
            Main.Do_Group();
        }

        private void Windows_Edit_Ungroup_Click(object sender, RoutedEventArgs e)
        {
            Main.Do_Ungroup();
        }

        private void Windows_File_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Windows_About_AboutSam33UML_Click(object sender, RoutedEventArgs e)
        {
            About_Sam33UML About = new About_Sam33UML();
            About.ShowDialog();
        }

        private void Windows_About_AboutAuthor_Click(object sender, RoutedEventArgs e)
        {
            About_Author About = new About_Author();
            About.ShowDialog();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double ProgramHeight = e.NewSize.Height;
            double ProgramWidth = e.NewSize.Width;
            Console.WriteLine(ProgramWidth + "x" + ProgramHeight);
            this.Width = ProgramWidth;
            this.Height = ProgramHeight;
            Scroll.Width = ProgramWidth - ToolBoxWidth - 20;
            Scroll.Height = ProgramHeight - Menu.Height;
        }

    }
}
