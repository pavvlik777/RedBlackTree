using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTree
{
    public class m_TreeNode
    {
        public int data;
        public bool isBlack;

        public m_TreeNode parent;

        public m_TreeNode leftNode;
        public m_TreeNode rightNode;

        #region Constructors
        public m_TreeNode()
        {
            data = 0;
            isBlack = true;
            leftNode = null;
            rightNode = null;

            parent = null;
        }

        public m_TreeNode(int data)
        {
            this.data = data;
            isBlack = true;
            leftNode = null;
            rightNode = null;

            parent = null;
        }

        public m_TreeNode(int data, bool isBlack)
        {
            this.data = data;
            this.isBlack = isBlack;
            leftNode = null;
            rightNode = null;

            parent = null;
        }
        #endregion
    }

    public class RedBlackTree
    {
        private m_TreeNode root;
        public m_TreeNode Root { get => root; }

        bool IsNodeBlack(m_TreeNode node) => node == null || node.isBlack;
        void SetNodeColor(ref m_TreeNode node, bool isBlack)
        {
            if (node == null)
                return;
            node.isBlack = isBlack;
        }

        public RedBlackTree()
        {
            root = null;
        }

        public RedBlackTree(int data)
        {
            root = null;
            AddNode(data);
        }

        public void AddNode(int data)
        {
            m_TreeNode current = root;
            m_TreeNode parent = null;
            m_TreeNode x = null;
            while(current != null)
            {
                parent = current;
                current = data < current.data ? current.leftNode : current.rightNode;
            }

            x = new m_TreeNode(data, false);
            x.parent = parent;

            if (parent != null)
                if (data < parent.data)
                    parent.leftNode = x;
                else
                    parent.rightNode = x;
            else
                root = x;

            fixNode = x;
            FixInsertion();
        }

        m_TreeNode FindUncle(m_TreeNode node)
        {
            if (node.parent != null)
            {
                if (node.parent.parent != null)
                    if (ReferenceEquals(node.parent, node.parent.parent.leftNode))
                        return node.parent.parent.rightNode;
                    else
                        return node.parent.parent.leftNode;
                else
                    return null;
            }
            else
                return null;
        }

        private m_TreeNode fixNode;
        private m_TreeNode rotateNode;
        void FixInsertion()
        {
            m_TreeNode node = fixNode;
            while(node != root && !node.parent.isBlack)
            {
                if(node.parent == node.parent.parent.leftNode)
                {
                    m_TreeNode uncle = FindUncle(node);
                    if(uncle == null)
                    {
                        if (node == node.parent.rightNode)
                        {
                            node = node.parent;
                            rotateNode = node;
                            LeftRotate();
                        }
                        node.parent.isBlack = true;
                        node.parent.parent.isBlack = false;
                        rotateNode = node.parent.parent;
                        RightRotate();
                    }
                    else if(!uncle.isBlack)
                    {
                        node.parent.isBlack = true;
                        uncle.isBlack = true;
                        node.parent.parent.isBlack = false;
                        node = node.parent.parent;
                    }
                    else
                    {
                        if(node == node.parent.rightNode)
                        {
                            node = node.parent;
                            rotateNode = node;
                            LeftRotate();
                        }
                        node.parent.isBlack = true;
                        node.parent.parent.isBlack = false;
                        rotateNode = node.parent.parent;
                        RightRotate();
                    }
                }
                else
                {
                    m_TreeNode uncle = FindUncle(node);
                    if (uncle == null)
                    {
                        if (node == node.parent.leftNode)
                        {
                            node = node.parent;
                            rotateNode = node;
                            RightRotate();
                        }
                        node.parent.isBlack = true;
                        node.parent.parent.isBlack = false;
                        rotateNode = node.parent.parent;
                        LeftRotate();
                    }
                    else if (!uncle.isBlack)
                    {
                        node.parent.isBlack = true;
                        uncle.isBlack = true;
                        node.parent.parent.isBlack = false;
                        node = node.parent.parent;
                    }
                    else
                    {
                        if (node == node.parent.leftNode)
                        {
                            node = node.parent;
                            rotateNode = node;
                            RightRotate();
                        }
                        node.parent.isBlack = true;
                        node.parent.parent.isBlack = false;
                        rotateNode = node.parent.parent;
                        LeftRotate();
                    }
                }
            }
            root.isBlack = true;
        }

        void LeftRotate()
        {
            m_TreeNode node = rotateNode;
            m_TreeNode pivot = node.rightNode;

            node.rightNode = pivot.leftNode;
            if (pivot.leftNode != null) pivot.leftNode.parent = node;

            if (pivot != null) pivot.parent = node.parent;
            if (node.parent != null)
            {
                if (node.parent.leftNode == node)
                    node.parent.leftNode = pivot;
                else
                    node.parent.rightNode = pivot;
            }
            else
            {
                root = pivot;
            }

            pivot.leftNode = node;
            if (node != null) node.parent = pivot;
        }

        void RightRotate()
        {
            m_TreeNode node = rotateNode;
            m_TreeNode pivot = node.leftNode;

            node.leftNode = pivot.rightNode;
            if (pivot.rightNode != null) pivot.rightNode.parent = node;

            if (pivot != null) pivot.parent = node.parent;
            if (node.parent != null)
            {
                if (node.parent.leftNode == node)
                    node.parent.leftNode = pivot;
                else
                    node.parent.rightNode = pivot;
            }
            else
            {
                root = pivot;
            }

            pivot.rightNode = node;
            if (node != null) node.parent = pivot;
        }

        public void RemoveNode(int data)//пофиксить удаление
        {
            m_TreeNode node = FindNode(data);
            m_TreeNode x = null;
            m_TreeNode y = null;

            if (node == null) return;

            if(node.leftNode == null || node.rightNode == null)
            {
                y = node;
            }
            else
            {
                y = node.rightNode;
                while (y.leftNode != null) y = y.leftNode;
            }

            if (y.leftNode != null)
                x = y.leftNode;
            else
                x = y.rightNode;

            x.parent = y.parent;
            if (y.parent != null)
            {
                if (y == y.parent.leftNode)
                    y.parent.leftNode = x;
                else
                    y.parent.rightNode = x;
            }
            else
                root = x;

            if (y != node) node.data = y.data;

            if(!y.isBlack)
            {
                fixNode = y;
                FixDelete();
            }
        }

        void FixDelete()
        {
            m_TreeNode node = fixNode;
            while (node != root && node.isBlack)
            {
                if(node == node.parent.leftNode)
                {
                    m_TreeNode sibling = node.parent.rightNode;
                    if(!IsNodeBlack(sibling))
                    {
                        SetNodeColor(ref sibling, true);
                        SetNodeColor(ref node.parent, false);
                        rotateNode = node.parent;
                        LeftRotate();
                        sibling = node.parent.rightNode;
                    }
                    if(IsNodeBlack(sibling.rightNode) && IsNodeBlack(sibling.leftNode))
                    {
                        SetNodeColor(ref sibling, false);
                        node = node.parent;
                    }
                    else
                    {
                        if(IsNodeBlack(sibling.rightNode))
                        {
                            SetNodeColor(ref sibling.leftNode, true);
                            SetNodeColor(ref sibling, false);
                            rotateNode = sibling;
                            RightRotate();
                            sibling = node.parent.rightNode;
                        }
                        SetNodeColor(ref sibling, node.parent.isBlack);
                        SetNodeColor(ref node.parent, true);
                        SetNodeColor(ref sibling.rightNode, true);
                        rotateNode = node.parent;
                        LeftRotate();
                        node = root;
                    }
                }
                else
                {
                    m_TreeNode sibling = node.parent.leftNode;
                    if (!IsNodeBlack(sibling))
                    {
                        SetNodeColor(ref sibling, true);
                        SetNodeColor(ref node.parent, false);
                        rotateNode = node.parent;
                        RightRotate();
                        sibling = node.parent.leftNode;
                    }
                    if (IsNodeBlack(sibling.rightNode) && IsNodeBlack(sibling.leftNode))
                    {
                        SetNodeColor(ref sibling, false);
                        node = node.parent;
                    }
                    else
                    {
                        if (IsNodeBlack(sibling.leftNode))
                        {
                            SetNodeColor(ref sibling.rightNode, true);
                            SetNodeColor(ref sibling, false);
                            rotateNode = sibling;
                            LeftRotate();
                            sibling = node.parent.leftNode;
                        }
                        SetNodeColor(ref sibling, node.parent.isBlack);
                        SetNodeColor(ref node.parent, true);
                        SetNodeColor(ref sibling.leftNode, true);
                        rotateNode = node.parent;
                        RightRotate();
                        node = root;
                    }
                }
            }
            node.isBlack = true;
        }

        public m_TreeNode FindNode(int data)
        {
            m_TreeNode current = root;
            while(current != null)
            {
                if (current.data == data)
                    return current;
                else
                    current = data < current.data ? current.leftNode : current.rightNode;
            }
            return null;
        }
    }
}