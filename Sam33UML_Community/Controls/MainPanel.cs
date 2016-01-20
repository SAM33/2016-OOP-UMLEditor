/*   
 *     檔案位置 : Sam33UML/Controls/MainPanel.cs
 *     
 *     作者資訊 : Sam33 (史碩三)
 *     作者聲明 : 此檔案為作者中央大學資工所物件導向分析設計期末專題的一部分，允許做任何
 *                修改與重製來作為其他用途，唯本人不付任何衍伸的法律責任，由修改者或重製
 *                者自行承擔。
 *     Github   : https://github.com/SAM33/Sam33UML (將會再期末專題繳交結束後開放)
 *     
 *     檔案說明 : MainPanel為使用者所看到的主要繪圖板。
 *                
 *     繼承架構 : System.Windows.Controls.Canvas
 *                |
 *                |__MainPanel
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
using Sam33UML.Others;
namespace Sam33UML.Controls
{
    public class MainPanel : Canvas
    {
        private List<BaseUMLShape> UMLShapes;
        private CoreMouseEventProcessor EventProcessor;
        private BaseUMLMode Mode;
        private DrawingGroup Foreground;

        /* 用來設定Mode,此部分為多型,可以傳入任何BaseUMLMode的子類別,並都過多型來展現其功能 */
        public void setMode(BaseUMLMode _Mode)
        {
            Mode = _Mode;
        }
        /* Constructor */
        public MainPanel()
            : base()
        {
            /* 由於WPF架構中背景為NULL不能觸發任何事件,所以要將透明色填入背景 */
            BrushConverter bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFrom("Transparent");
            /* 建立前景繪圖區域,用來繪製移動,選取等等滑鼠操作的特效 */
            Foreground = new DrawingGroup();
            UMLShapes = new List<BaseUMLShape>();
            /* MVC架構,把自身的滑鼠事件(V)全權交由封裝的CoreMouseEventProcessor事件處理器(C)來分析 */
            EventProcessor = new CoreMouseEventProcessor(this);
            /* MVC架構,當CoreMouseEventProcessor(C)分析完之後自動觸發下面的函式,並將自定義的事件參數送入函式(V) */
            EventProcessor.MouseEvent += EventProcessor_MouseEvent;
            /* 透過多型將Mode設為None_Mode(無任何動作),需要傳入本身的ref來讓Mode的子類別來取得MainPanel持有的相關資訊 */
            Mode = new None_Mode(this);
        }
        /* 刷新Depth的排序,將Depth越大的搬到List最前方,方便取得Top元素,Depth越小會覆蓋其他繪圖 */
        public void UpdateDepth()
        {
            UMLShapes.Sort((s2, s1) => s1.getDepth().CompareTo(s2.getDepth()));  //Depth越大的會被放到List最前方
        }

        public void AddShape(BaseUMLShape s,bool keepdepth=false)
        {
            if (!keepdepth)
            {
                s.setDepth(0);
                foreach (BaseUMLShape t in UMLShapes)
                    t.setDepth(t.getDepth() + 1);
            }
            UMLShapes.Add(s);
            UpdateDepth();
        }

        public DrawingGroup getForeground()
        {
            return Foreground;
        }

        public List<BaseUMLShape> AllBaseUMLShapes()
        {
            return UMLShapes;
        }


        public List<BaseUMLObject> AllBaseUMLObjects()
        {
            List<BaseUMLObject> Objects = new List<BaseUMLObject>();
            if (UMLShapes != null)
            {
                foreach (BaseUMLShape shape in UMLShapes)
                {
                    if (shape as BaseUMLObject != null)
                    {
                        Objects.Add(shape as BaseUMLObject);
                    }
                }
                if (Objects.Count > 0)
                    return Objects;
            }
            return null;
        }


        public List<BaseUMLShape> AllBaseUMLShapesAt(Point p)
        {
            List<BaseUMLShape> Shapes = new List<BaseUMLShape>();
            if (UMLShapes != null)
            {
                foreach (BaseUMLShape s in UMLShapes)
                    if (s.Contain(p))
                        Shapes.Add(s);
                if (Shapes.Count > 0)
                    return Shapes;
            }
            return null;
        }

        public List<BaseUMLObject> AllBaseUMLObjectsAt(Point p)
        {
            List<BaseUMLObject> Objects = new List<BaseUMLObject>();
            List<BaseUMLObject> All = AllBaseUMLObjects();
            if (All != null)
            {
                foreach (BaseUMLObject o in All)
                    if (o.Contain(p))
                        Objects.Add(o);
                if (Objects.Count > 0)
                    return Objects;
            }
            return null;
        }

        public BaseUMLShape TopBaseUMLShapesAt(Point p)
        {
            UpdateDepth();
            List<BaseUMLShape> Shapes = AllBaseUMLShapesAt(p);
            if (Shapes != null)
                return Shapes.ElementAt(Shapes.Count - 1);
            return null;
        }

        public BaseUMLObject TopBaseUMLObjectsAt(Point p)
        {
            UpdateDepth();
            List<BaseUMLObject> Objects = AllBaseUMLObjectsAt(p);
            if (Objects !=null)
                return Objects.ElementAt(Objects.Count - 1);
            return null;
        }

        /* 當封裝的滑鼠事件被觸發的時候,將事件轉交給Mode去自行處理 */
        void EventProcessor_MouseEvent(object sender, CoreMouseEventArgs e)
        {
            if (Mode != null)
                Mode.ProcessEvent(e);
            this.InvalidateVisual();
        }

        /* 複寫掉Canvas的繪圖處理,在此加入UML編輯區的繪圖主回圈 */
        protected override void OnRender(DrawingContext dc)
        {
            /* 畫出基本Canvas */
            base.OnRender(dc);
            /* 畫出Canvas邊界 */
            CoreDraw.DrawBorder(dc, new CoreRectRange(new Point(0, 0), new Point(this.ActualWidth, this.ActualHeight)), 5, Brushes.Blue, CoreDraw.BorderStyle_MultipleSegment);
            /* 多型的應用,畫出BaseUMLShape的List,並透過多型來呼叫真正實體類別該呈現的繪圖效果 */
            foreach (BaseUMLShape s in UMLShapes)
            {
                s.Draw(dc);
            }
            /* 繪出前景,前景覆蓋在所有圖層上,用來顯示特殊,或使用工具時的相關繪圖特效 */
            dc.DrawDrawing(Foreground);
        }

        public void Do_ChangeObjectName()
        {
            List<BaseUMLObject> Target = new List<BaseUMLObject>();
            foreach (BaseUMLShape shape in UMLShapes)
            {
                if (shape as BaseUMLObject != null)
                {
                    BaseUMLObject obj = shape as BaseUMLObject;
                    if (obj.getSelected())
                        Target.Add(obj);
                }
            }
            if(Target.Count==1)
            {
                if (Target.ElementAt(0) as ClassUMLObject != null)
                {
                    ClassUMLObject temp = Target.ElementAt(0) as ClassUMLObject;
                    BasicInputDialog inputDialog = new BasicInputDialog("Please enter your new class name:", temp.getTitleText());
                    if (inputDialog.ShowDialog() == true)
                    temp.setTitleText(inputDialog.Answer);
                }
                if (Target.ElementAt(0) as UseCaseUMLObject != null)
                {
                    UseCaseUMLObject temp = Target.ElementAt(0) as UseCaseUMLObject;
                    BasicInputDialog inputDialog = new BasicInputDialog("Please enter your new usecase name:", temp.getName());
                    if (inputDialog.ShowDialog() == true)
                        temp.setName(inputDialog.Answer);
                }
                this.InvalidateVisual();
            }
        }
        public void Do_Ungroup()
        {
            List<GroupUMLObject> Target = new List<GroupUMLObject>();
            foreach (BaseUMLShape shape in UMLShapes)
            {
                if (shape as GroupUMLObject != null)
                {
                    GroupUMLObject gp = shape as GroupUMLObject;
                    if (gp.getSelected())
                        Target.Add(gp);
                }
            }
            foreach (GroupUMLObject gp in Target)
            {
                foreach (BaseUMLObject obj in gp.getChildrens())
                    AddShape(obj,true);
                UMLShapes.Remove(gp);
            }
        }

        public void Do_Group()
        {
            List<BaseUMLObject> Target = new List<BaseUMLObject>();
            foreach(BaseUMLShape shape in UMLShapes)
            {
                if(shape as BaseUMLObject != null)
                {
                    BaseUMLObject obj = shape as BaseUMLObject;
                    if(obj.getSelected())
                        Target.Add(obj);
                }
            }
            foreach (BaseUMLObject obj in Target)
            {
                obj.setSelected(false);
                UMLShapes.Remove(obj);
            }
            GroupUMLObject Group = new GroupUMLObject(CoreRectRange.Default());
            Group.SetChildrens(Target);
            AddShape(Group);
        }
    }
}
