/*   
 *     檔案位置 : Sam33UML/Others/About_Author.xaml.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : 關於本程式的作者GitHub以及部落格。
 *                
 *     繼承架構 : System.Windows
 *                |
 *                |__About_Author
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
using System.Windows.Shapes;


namespace Sam33UML.Others
{
    /// <summary>
    /// About_Sam33UML.xaml 的互動邏輯
    /// </summary>
    public partial class About_Author : Window
    {
        public About_Author()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            
        }

        private void GithubURL_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/SAM33");
        }

        private void GithubURL_MouseEnter(object sender, MouseEventArgs e)
        {
            GithubURL.Foreground = Brushes.Blue;
            GithubURL.Cursor = Cursors.Hand;
        }

        private void GithubURL_MouseLeave(object sender, MouseEventArgs e)
        {
            GithubURL.Foreground = Brushes.Green;
            GithubURL.Cursor = null;
        }

        private void BloggerURL_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://sam33-home.blogspot.tw/");
        }

        private void BloggerURL_MouseEnter(object sender, MouseEventArgs e)
        {
            BloggerURL.Foreground = Brushes.Blue;
            BloggerURL.Cursor = Cursors.Hand;
        }

        private void BloggerURL_MouseLeave(object sender, MouseEventArgs e)
        {
            BloggerURL.Foreground = Brushes.Green;
            BloggerURL.Cursor = null;
        }
    }
}
