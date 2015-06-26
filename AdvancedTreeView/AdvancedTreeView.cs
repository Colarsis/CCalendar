using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColarsisUserControls.AdvancedTreeView
{
    public partial class AdvancedTreeView : UserControl
    {

        private List<Node> nodeList = new List<Node>();
        private List<TreeView> treeList = new List<TreeView>();
        private List<Button> btnList = new List<Button>();

        public event TreeNodeMouseClickEventHandler TreeNodeClicked;
        public event TreeNodeMouseClickEventHandler TreeNodeDoubleClicked;

        private Size size;

        public List<Node> NodeList
        {
            get { return nodeList; }
            private set { nodeList = value; }
        }

        public AdvancedTreeView()
        {
            InitializeComponent();

            size = this.Size;
            
            draw();
        }

        public void draw()
        {
            foreach (TreeView tr in treeList)
            {
                tr.NodeMouseClick -= new TreeNodeMouseClickEventHandler(nodeClicked);
                tr.NodeMouseDoubleClick -= new TreeNodeMouseClickEventHandler(nodeDoubleClicked);
            }

            btnList.Clear();
            treeList.Clear();

            panel1.Controls.Clear();

            int panelHeight = 0;

            int id = 0;

            int totalHeight = 0;

            foreach (Node n in nodeList)
            {
                totalHeight += 40;

                Button btn = new Button();

                btn.Text = n.Name;
                btn.Image = n.Image;

                btn.Height = 40;

                btn.Name = id.ToString();

                btn.Left = 0;
                btn.Top = btnList.Count * 40 + panelHeight;

                btn.Width = this.Width;

                btn.Click += new EventHandler(buttonClicked);

                TreeView t = new TreeView();

                t = n.TreeView;

                t.Width = this.Width;

                bool isOpened = false;

                if (n.Opened)
                {
                    t.Height = 0;

                    t.Top = btn.Top + 40;

                    foreach (TreeNode no in t.Nodes)
                    {
                        t.Height += 20;
                    }

                    int leftSpace = nodeList.Count * 40 - (id + 1) * 40;

                    if (leftSpace + t.Top + t.Height > panel1.Height)
                    {
                        panel1.Height = leftSpace + t.Top + t.Height;
                        panel1.MinimumSize = new Size(panel1.MinimumSize.Width, leftSpace + t.Top + t.Height);
                    }
                    else
                    {
                        if (leftSpace + t.Top + t.Height <= this.Height)
                        {
                            panel1.MinimumSize = new Size(panel1.MinimumSize.Width, leftSpace + t.Top + t.Height);
                            panel1.Height = this.Height;
                        }
                    }

                    t.Height = panel1.Height - (t.Top + leftSpace);

                    totalHeight += t.Height;

                    isOpened = true;
                }
                else
                {
                    t.Height = 0;
                }

                if (!isOpened)
                {
                    panel1.MinimumSize = new Size(panel1.MinimumSize.Width, 100);
                    panel1.Height = this.Height;
                }

                t.Name = id.ToString();

                treeList.Add(t);

                panelHeight += t.Height;

                btnList.Add(btn);

                id++;
            }

            foreach (Button b in btnList)
            {
                panel1.Height = totalHeight;

                panel1.Controls.Add(b);
                panel1.Controls.Add(treeList[btnList.IndexOf(b, 0, btnList.Count)]);

                treeList[btnList.IndexOf(b, 0, btnList.Count)].NodeMouseClick += new TreeNodeMouseClickEventHandler(nodeClicked);
                treeList[btnList.IndexOf(b, 0, btnList.Count)].NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(nodeDoubleClicked);
            }


        }

        public void addNode(Node n)
        {
            nodeList.Add(n);
            draw();
        }

        public void deleteNode(Node n)
        {
            nodeList.Remove(n);

            draw();
        }

        private void buttonClicked(object o, EventArgs e)
        {
            int i = 0;

            i = btnList.FindIndex(
                delegate(Button b)
                {
                    if (b.Name == ((Button)o).Name)
                    {
                       return true;
                    }      
                    else
                    {
                        return false;
                    }
                 });

            Node n = nodeList[i];

            if (!n.Opened)
            {
                foreach (Node no in NodeList)
                {
                    no.Opened = false;
                }

                n.Opened = true;
            }
            else
            {
                n.Opened = false;
            }

            draw();
        }

        private void nodeClicked(object o, TreeNodeMouseClickEventArgs e)
        {
            if (TreeNodeClicked != null)
            {
                TreeNodeClicked(o, e);
            }
        }

        private void nodeDoubleClicked(object o, TreeNodeMouseClickEventArgs e)
        {
            if (TreeNodeDoubleClicked != null)
            {
                TreeNodeDoubleClicked(o, e);
            }
        }

        private void AdvancedTreeView_Resize(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;

            if (ctrl.Size != size)
            {
                panel1.Width = this.Width;
                panel1.Height = this.Height;

                size = ctrl.Size;

                draw();
            }

            
        }
    }
}
