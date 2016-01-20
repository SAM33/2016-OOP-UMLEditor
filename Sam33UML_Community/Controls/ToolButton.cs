/*   
 *     檔案位置 : Sam33UML/Controls/ToolButton.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分, 允許做任何
 *                修改與重製來作為其他用途, 唯本人不付任何衍伸的法律責任, 由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : 此檔案為使用者在工具箱上所看到的工具按鈕, 能夠讓使用者點選。
 *                
 *     繼承架構 : System.Windows.Controls.Button
 *                |
 *                |__ToolButton
 *                
 *     最後修改 : 2015/01/17
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
    class ToolButton : Button
    {
        private BaseUMLMode Mode;
        //  一般狀態的圖片
        private Uri uri1;
        //  被點選狀態的圖片
        private Uri uri2;
        //  true:設定是被選取狀態; false:設定是取消選取狀態
        public void setSelected(bool b)
        {
            if (b == false)
            {
                Image img = new Image();
                BitmapImage source = new BitmapImage();
                source.BeginInit();
                source.UriSource = uri1;
                source.DecodePixelHeight = 70;
                source.DecodePixelWidth = 70;
                source.EndInit();
                img.Source = source;
                StackPanel stackPnl = new StackPanel();
                stackPnl.Orientation = Orientation.Horizontal;
                //stackPnl.Margin = new Thickness(10);
                stackPnl.Children.Add(img);
                this.Content = stackPnl;
                this.InvalidateVisual();  //排程重新繪圖事件
            }
            else
            {
                Image img = new Image();
                BitmapImage source = new BitmapImage();
                source.BeginInit();
                source.UriSource = uri2;
                source.DecodePixelHeight = 70;
                source.DecodePixelWidth = 70;
                source.EndInit();
                img.Source = source;
                StackPanel stackPnl = new StackPanel();
                stackPnl.Orientation = Orientation.Horizontal;
                //stackPnl.Margin = new Thickness(10);
                stackPnl.Children.Add(img);
                this.Content = stackPnl;
                this.InvalidateVisual();  //排程重新繪圖事件
            }
        }
        //  建構子,傳入按鈕所附帶的工具模式, 按鈕正常狀態的圖片URI, 按鈕被點選狀態的URI
        public ToolButton(BaseUMLMode _Mode, Uri _uri1, Uri _uri2)
            : base()
        {
            Mode = _Mode;
            uri1 = _uri1;
            uri2 = _uri2;
            setSelected(false);
        }
        //  取得工具按鈕裡面的工具模式
        public BaseUMLMode getMode()
        {
            return Mode;
        }
    }
}
