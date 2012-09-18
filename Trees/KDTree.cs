using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary.Trees
{
    class KDTree<TX, TY>
    {
        int itemCount;
        Comparison<TX> cmpX;
        Comparison<TY> cmpY;
        TreeNode<TX,TY> root = null;
        #region TreeNode
        class TreeNode<TX, TY>
        {
            TX _x;
            TY _y;
            TreeNode<TX, TY> _left, _right, _parent;
            public TreeNode(){ }

            public TreeNode(TX x, TY y, TreeNode<TX, TY> Left, TreeNode<TX, TY> Right)
            {
                _x = x;
                _y = y;
                _left = Left;
                _right = Right;
                _parent = null;
            }

            public TreeNode(TX x, TY y,TreeNode<TX, TY> Left, TreeNode<TX, TY> Right, TreeNode<TX, TY> Parent)
            {
                _x = x;
                _y = y;
                _left = Left; 
                _right = Right;
                _parent = Parent;
            }

            #region DataAccessMembers
            public TreeNode<TX, TY> Parent
            {
                get { return _parent; }
                set { _parent = value;}
            }

            public TreeNode<TX, TY> RightNode
            {
                get { return _right; }
                set { _right = value; }
            }

            public TreeNode<TX, TY> LeftNode
            {
                get { return _left; }
                set { _left = value; }
            }

            public TX X
            {
                get { return _x; }
                set { _x = value; }
            }

            public TY Y
            {
                get { return _y; }
                set { _y = value; }
            }

            #endregion

        }
        #endregion

        public KDTree(Comparison<TX> xComparison, Comparison<TY> yComparison)
        {
            cmpX = xComparison;
            cmpY = yComparison;
        }

        public void insert(TX x, TY y)
        {
            if (root == null)
            {
                itemCount = 1;
                root = new TreeNode<TX, TY>(x, y, null, null);
            }
            else
            {
                bool leftChild = false;
                int level = 0;
                TreeNode<TX, TY> node = root;
                TreeNode<TX, TY> parentNode = root;

                while (node != null)
                {
                    parentNode = node;
                    leftChild = (cmpY(y, node.Y) < 0 && level % 2 == 0) || (cmpX(x, node.X) < 0 && level % 2 == 1);

                    if (leftChild)
                        node = node.LeftNode;
                    else
                        node = node.RightNode;

                    level++;
                }

                TreeNode<TX, TY> newNode = new TreeNode<TX, TY>(x, y, null, null, parentNode);

                if (leftChild)
                    parentNode.LeftNode = newNode;
                else
                    parentNode.RightNode = newNode;
            }

        }

        public List<TreeNode<TX, TY>> getX()
        {
            return visit(root, 0, false);
        }

        public List<TreeNode<TX, TY>> getY()
        {
            return visit(root, 0, true);
        }

        public void search(TreeNode<TX, TY> item, ref TX minX, ref TY minY, ref TX maxX, ref TY maxY)
        {
            int level = 0;
            TreeNode<TX, TY> node = root;

            while (node != null)
            {
                //Found Current Element then Return
                if (cmpX(node.X, item.X) == 0 && cmpY(node.Y, item.Y) == 0)
                    return;

                bool leftChild = (cmpY(item.Y, node.Y) < 0 && level % 2 == 0) || (cmpX(item.X, node.X) < 0 && level % 2 == 1);

                if (level % 2 == 1)
                {
                    if (cmpX(item.X, node.X) < 0 && cmpX(maxX, node.X) >= 0)
                        maxX = node.X;
                    else if (cmpX(item.X, node.X) >= 0 && cmpX(minX, node.X) <= 0)
                        minX = node.X;
                }
                else if (level % 2 == 0)
                {
                    if (cmpY(item.Y, node.Y) < 0 && cmpY(maxY, node.Y) >= 0)
                        maxY = node.Y;
                    else if (cmpY(item.Y, node.Y) >= 0 && cmpY(minY, node.Y) <= 0)
                        minY = node.Y;
                }

                if (leftChild)
                    node = node.LeftNode;
                else
                    node = node.RightNode;

                level++;
            }
        }

        private List<TreeNode<TX, TY>> visit(TreeNode<TX, TY> node, int level, bool even)
        {
            if (node == null)
                return null;

            List<TreeNode<TX, TY>> L = visit(node.LeftNode, level + 1, even);
            List<TreeNode<TX, TY>> R = visit(node.RightNode, level + 1, even);

            if (L == null)
                L = new List<TreeNode<TX, TY>>();

            if (level % 2 == 0 && even || level % 2 == 1 && !even)
                L.Add(node);

            if (R != null)
                L.AddRange(R);

            return L;
        }
    }
}
