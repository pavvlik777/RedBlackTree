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
            AddNode(ref root, null, data);
        }

        void AddNode(ref m_TreeNode node, m_TreeNode parent, int data)
        {
            if(node == null)
            {
                node = new m_TreeNode(data, false);
                node.parent = parent;
                FixInsertion(ref node);
                return;
            }
            if (data < node.data)
                AddNode(ref node.leftNode, node, data);
            else
                AddNode(ref node.rightNode, node, data);
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

        void FixInsertion(ref m_TreeNode node)
        {
            if (node.parent != null) //наш узел - не корень
            {
                if (!node.parent.isBlack) //родитель текущего - красный
                {
                    m_TreeNode uncle = FindUncle(node);
                    m_TreeNode grandparent = node.parent.parent;
                    if(uncle != null && !uncle.isBlack) //дядя — красный
                    {
                        node.parent.isBlack = true;
                        uncle.isBlack = true;
                        grandparent.isBlack = false;
                        FixInsertion(ref grandparent);
                    }
                    else //дядя — черный
                    {
                        if(ReferenceEquals(node, node.parent.rightNode) && ReferenceEquals(node.parent, grandparent.leftNode))
                        {
                            LeftRotate(ref node.parent);
                            node = node.leftNode;
                        }
                        else if (ReferenceEquals(node, node.parent.leftNode) && ReferenceEquals(node.parent, grandparent.rightNode))
                        {
                            RightRotate(ref node.parent);
                            node = node.rightNode;
                        }
                        grandparent = node.parent.parent;
                        node.parent.isBlack = true;
                        grandparent.isBlack = false;
                        if (ReferenceEquals(node, node.parent.leftNode) && ReferenceEquals(node.parent, grandparent.leftNode))
                        {
                            RightRotate(ref grandparent);
                        }
                        else
                        {
                            LeftRotate(ref grandparent);
                        }
                    }
                }
                else
                    return;
            }
            else
                node.isBlack = true;
        }

        void LeftRotate(ref m_TreeNode node) //удалить лишнюю связь
        {
            m_TreeNode pivot = node.rightNode;
            pivot.parent = node.parent;
            if(node.parent != null)
            {
                if (ReferenceEquals(node.parent.leftNode, node))
                    node.parent.leftNode = pivot;
                else
                    node.parent.rightNode = pivot;
            }

            node.rightNode = pivot.leftNode;
            if (pivot.leftNode != null)
                pivot.leftNode.parent = node;

            node.parent = pivot;
            pivot.leftNode = node;
        }

        void RightRotate(ref m_TreeNode node) //удалить лишнюю связь
        {
            m_TreeNode pivot = node.leftNode;
            pivot.parent = node.parent;
            if (node.parent != null)
            {
                if (ReferenceEquals(node.parent.leftNode, node))
                    node.parent.leftNode = pivot;
                else
                    node.parent.rightNode = pivot;
            }

            node.leftNode = pivot.rightNode;
            if (pivot.rightNode != null)
                pivot.rightNode.parent = node;

            node.parent = pivot;
            pivot.rightNode = node;
        }

        public void RemoveNode(int data)
        {

        }

        public m_TreeNode FindNode(int data)
        {
            return new m_TreeNode();
        }
    }
}
