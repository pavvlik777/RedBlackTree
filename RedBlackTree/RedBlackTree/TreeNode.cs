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
            m_TreeNode current = root;
            m_TreeNode parent = null;
            m_TreeNode x = null;
            while(current != null)
            {
                parent = current;
                if (data < current.data)
                    current = current.leftNode;
                else
                    current = current.rightNode;
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

        public void RemoveNode(int data)
        {

        }

        public m_TreeNode FindNode(int data)
        {
            return new m_TreeNode();
        }
    }
}