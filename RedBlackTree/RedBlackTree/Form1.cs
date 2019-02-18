using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedBlackTree
{
    public partial class Form1 : Form
    {
        public RedBlackTree tree;
        Font tagFont = new Font("Helvetica", 14, FontStyle.Bold);

        public Form1()
        {
            InitializeComponent();
            TreeView.DrawNode +=
                new DrawTreeNodeEventHandler(myTreeView_DrawNode);
            TreeView.MouseDown += new MouseEventHandler(myTreeView_MouseDown);

            InitTree();
            RefreshTree();
        }

        void InitTree()
        {
            tree = new RedBlackTree(3);
            tree.AddNode(5);
        }

        void RefreshTree() //first right, then left
        {
            TreeView.Nodes.Clear();
            TreeView.Nodes.Add(AddNode(tree.Root));
            TreeView.ExpandAll();
        }

        TreeNode AddNode(m_TreeNode node)
        {
            if (node == null)
            {
                TreeNode newNode = new TreeNode("null");
                newNode.BackColor = Color.Black;
                return newNode;
            }
            else
            {
                TreeNode newNode = new TreeNode(node.data.ToString());
                if (node.isBlack)
                    newNode.BackColor = Color.Black;
                else
                    newNode.BackColor = Color.Red;
                newNode.Nodes.Add(AddNode(node.rightNode));
                newNode.Nodes.Add(AddNode(node.leftNode));
                return newNode;
            }
        }

        private void myTreeView_DrawNode(
        object sender, DrawTreeNodeEventArgs e)
        {
            Font nodeFont = e.Node.NodeFont;
            if (nodeFont == null) nodeFont = ((TreeView)sender).Font;
            if (e.Node.BackColor == Color.Black)
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.Black,
                Rectangle.Inflate(e.Bounds, 2, 0));
            else
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.Red,
                Rectangle.Inflate(e.Bounds, 2, 0));
        }

        // Selects a node that is clicked on its label or tag text.
        private void myTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode clickedNode = TreeView.GetNodeAt(e.X, e.Y);
            if (NodeBounds(clickedNode).Contains(e.X, e.Y))
            {
                TreeView.SelectedNode = clickedNode;
            }
        }

        // Returns the bounds of the specified node, including the region 
        // occupied by the node label and any node tag displayed.
        private Rectangle NodeBounds(TreeNode node)
        {
            // Set the return value to the normal node bounds.
            Rectangle bounds = node.Bounds;
            if (node.Tag != null)
            {
                // Retrieve a Graphics object from the TreeView handle
                // and use it to calculate the display width of the tag.
                Graphics g = TreeView.CreateGraphics();
                int tagWidth = (int)g.MeasureString
                    (node.Tag.ToString(), tagFont).Width + 6;

                // Adjust the node bounds using the calculated value.
                bounds.Offset(tagWidth / 2, 0);
                bounds = Rectangle.Inflate(bounds, tagWidth / 2, 0);
                g.Dispose();
            }

            return bounds;

        }

        bool CheckInput(out int result)
        {
            return int.TryParse(InputBox.Text, out result);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!CheckInput(out int result))
            {
                MessageBox.Show("Incorrect input!", "Error");
                return;
            }
            tree.AddNode(result);
            RefreshTree();
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            if (!CheckInput(out int result))
            {
                MessageBox.Show("Incorrect input!", "Error");
                return;
            }
            if (TreeView.Nodes.Count == 0)
                return;
            TreeNode current = TreeView.Nodes[0];
            while(current.Text != "null")
            {
                int data = int.Parse(current.Text);
                if (data == result)
                {
                    TreeView.SelectedNode = current;
                    TreeView.Select();
                    return;
                }
                else
                {
                    current = data < result ? current.Nodes[0] : current.Nodes[1];
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (!CheckInput(out int result))
            {
                MessageBox.Show("Incorrect input!", "Error");
                return;
            }
            tree.RemoveNode(result);
            RefreshTree();
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        { }
    }
}