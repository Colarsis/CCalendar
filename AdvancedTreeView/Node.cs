using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColarsisUserControls.AdvancedTreeView
{
    public class Node
    {
        private Image img;
        private string name;
        private bool opened = false;

        private TreeView treeView = new TreeView();

        public TreeView TreeView
        {
            get {return treeView; }
            private set { treeView = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Opened
        {
            get { return opened; }
            set { opened = value; }
        }

        public Image Image
        {
            get { return img; }
            set { img = value; }
        }

        public Node(string name, Image img)
        {
            this.img = img;
            this.name = name;

            treeView.FullRowSelect = true;
            treeView.ShowLines = false;
        }

        public void addNode(TreeNode n)
        {
            treeView.Nodes.Add(n);
        }

        public void deleteNode(TreeNode n)
        {
            treeView.Nodes.Remove(n);
        }
    }
}
