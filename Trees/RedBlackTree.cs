using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary.Trees
{
    class RedBlackTree<TValue>
    {
        #region TreeNode
        enum Color {Red, Black};
        class TreeNode<TValue>
        {
            TValue _value;
            TreeNode<TValue> _left, _right, _parent;
            Color _color;
            public TreeNode(){ }

            public TreeNode(TValue Value, TreeNode<TValue> Left, TreeNode<TValue> Right, Color NodeColor)
            {
                _value = Value;
                _left = Left;
                _right = Right;
                _parent = null;
                _color = NodeColor;
            }

            public TreeNode(TValue Value, TreeNode<TValue> Left, TreeNode<TValue> Right,TreeNode<TValue> Parent, Color NodeColor)
            {
                _value = Value;
                _left = Left; 
                _right = Right;
                _parent = Parent;
                _color = NodeColor;
            }

            public TreeNode<TValue> Copy()
            {
                return new TreeNode<TValue>(_value, _left, _right, _color);
            }

            #region DataAccessMembers
            public TreeNode<TValue> Parent
            {
                get { return _parent; }
                set { _parent = value;}
            }

            public TreeNode<TValue> RightNode
            {
                get { return _right; }
                set { _right = value; }
            }

            public TreeNode<TValue> LeftNode
            {
                get { return _left; }
                set { _left = value; }
            }

            public Color NodeColor
            {
                get { return _color; }
                set {
                        if (value != Color.Black && value != Color.Red)
                            throw new Exception("Invalid Node Color Type");
                        _color = value; 
                    }
            }

            public TValue Value
            {
                get { return _value; }
            }

            #endregion

        }
        #endregion

        #region DataMembers
        TreeNode<TValue> root;
        Comparison<TValue> cmp;
        #endregion

        public RedBlackTree(Comparison<TValue> nodeComparer)
        {
            cmp = nodeComparer;
        }

        private TreeNode<TValue> treeSuccessor(TreeNode<TValue> node)
        {
            if(node == null)
                throw new Exception("no successor for null node");

            while (node != null)
            {
                if (node.LeftNode == null && node.RightNode == null)
                {
                    return node;
                }
                else if (node.LeftNode == null && node.RightNode != null)
                {
                    node = node.RightNode;
                }
                else if (node.LeftNode != null)
                {
                    node = node.LeftNode;
                }
            }

            return null;
        }

        private TreeNode<TValue> getNode(TValue item)
        {
            TreeNode<TValue> x = root;

            while (x != null)
            {
                int comparison = cmp(x.Value, item);
                if (comparison == 0)
                    return x;
                else if (comparison < 0)
                    x = x.LeftNode;
                else
                    x = x.RightNode;
            }

            return null;
        }

        public bool contains(TValue item)
        {
            return getNode(item) != null;
        }

        public void insert(TValue item)
        {
            if (root == null)
            {
                root = new TreeNode<TValue>(item, null, null, null, Color.Black);
                return;
            }

            TreeNode<TValue> x = root;
            TreeNode<TValue> xParent = root;
            
            //Decide Node Parent
            while (x != null)
            {
                if (cmp.Invoke(item, x.Value) < 0)
                {
                    xParent = x;
                    x = x.LeftNode;
                }
                else
                {
                    xParent = x;
                    x = x.RightNode;
                }
            }

            //Add Node
            x = new TreeNode<TValue>(item, null, null, xParent, Color.Red);
            if (cmp.Invoke(item, xParent.Value) < 0)
            {
                xParent.LeftNode = x;
            }
            else
            {
                xParent.RightNode = x;
            }

            Fix_Insert(x);
        }
        private void Fix_Insert(TreeNode<TValue> z)
        {
            while (z.NodeColor == Color.Red && z.Parent != null && z.Parent.Parent != null)
            {
                if (z.Parent.Parent.LeftNode == z.Parent)
                {
                    TreeNode<TValue> x = z.Parent;
                    TreeNode<TValue> y = (z.Parent.Parent.RightNode != null ? z.Parent.Parent.RightNode : new TreeNode<TValue>(x.Value, null, null, x.Parent, Color.Black));
                    
                    //Case 1: ParentParent is Black and Both Parent = Uncle is Red
                    if (x.Parent.NodeColor == Color.Black && (x.NodeColor == y.NodeColor && x.NodeColor == Color.Red))
                    {
                        x.Parent.NodeColor = Color.Red;
                        x.NodeColor = Color.Black;
                        y.NodeColor = Color.Black;
                        z = x.Parent;
                    }
                    else
                    {
                        //Case 2,3: ParentParent is Black and Uncle is Black and Parent is Red
                        if (x.Parent.NodeColor == Color.Black && y.NodeColor == Color.Black && x.NodeColor == Color.Red)
                        {
                            //Case 2:
                            if (x.RightNode == z)
                            {
                                x = RotateLeft(x);
                                y.Parent.LeftNode = x;
                            }

                            //Case 3:
                            x.NodeColor = Color.Black;
                            x.Parent.NodeColor = Color.Red;

                            TreeNode<TValue> parent = x.Parent;
                            TreeNode<TValue> parentParent = parent.Parent;
                            bool leftNode = (parentParent != null && parentParent.LeftNode == parent) ? true : false;

                            parent = RotateRight(parent);
                            z = parent;

                            //Case we are the root
                            if (parentParent == null)
                            {
                                root = parent;
                                break;
                            }
                            else
                            {
                                if (leftNode)
                                {
                                    parentParent.LeftNode = parent;
                                }
                                else
                                {
                                    parentParent.RightNode = parent;
                                }
                            }
                        }
                    }
                }
                else
                {
                    TreeNode<TValue> x = z.Parent;
                    TreeNode<TValue> y = (z.Parent.Parent.LeftNode != null ? z.Parent.Parent.LeftNode : new TreeNode<TValue>(x.Value, null, null, x.Parent, Color.Black));

                    //Case 1: ParentParent is Black and Both Left and Right Node are Red
                    if (x.Parent.NodeColor == Color.Black && x.NodeColor == y.NodeColor && x.NodeColor == Color.Red)
                    {
                        x.Parent.NodeColor = Color.Red;
                        x.NodeColor = Color.Black;
                        y.NodeColor = Color.Black;
                        z = x.Parent;
                    }
                    else
                    {
                        //Case 2,3: ParentParent is Black and Uncle is Black and Parent is Red
                        if (x.Parent.NodeColor == Color.Black && x.NodeColor == Color.Red && y.NodeColor == Color.Black)
                        {
                            //Case 2:
                            if (x.LeftNode == z)
                            {
                                x = RotateRight(x);
                                y.Parent.RightNode = x;
                            }

                            //Case 3:
                            x.NodeColor = Color.Black;
                            x.Parent.NodeColor = Color.Red;

                            TreeNode<TValue> parent = x.Parent;
                            TreeNode<TValue> parentParent = parent.Parent;
                            bool leftNode = (parentParent != null && parentParent.LeftNode == parent) ? true : false;

                            parent = RotateLeft(parent);
                            z = parent;

                            //Case we are the root
                            if (parentParent == null)
                            {
                                root = parent;
                                break;
                            }
                            else
                            {
                                if (leftNode)
                                {
                                    parentParent.LeftNode = parent;
                                }
                                else
                                {
                                    parentParent.RightNode = parent;
                                }
                            }
                        }
                    }
                }
            }

            //Always Set Color of Root node to Black
            root.NodeColor = Color.Black;
        }

        public void delete(TValue item)
        {
            TreeNode<TValue> node = getNode(item);

            if (node == null)
                throw new Exception("Unfound Element to Delete");


        }
        private void Fix_Delete(TreeNode<TValue> z)
        {

        }

        private TreeNode<TValue> RotateLeft(TreeNode<TValue> x)
        {
            Console.WriteLine(x.Value + " " + x.RightNode.Value + " " + (x.LeftNode != null? x.LeftNode.Value.ToString() : ""));
            if (x.NodeColor == Color.Black)
            {
                throw new Exception("Invalid ! Left Rotation on a Black Node.");
            }

            TreeNode<TValue> parentNode = x.Parent;
            TreeNode<TValue> y = x.RightNode;
            y.Parent = x.Parent;

            if (y == null)
            {
                throw new Exception("Invalid ! Left Rotation on a Node with Null Right Child.");
            }

            //Set X-left Node to Y-Right Node, Set X to Be Y-Left Node
            x.RightNode = y.LeftNode; 
            y.LeftNode = x;
            //Set Nodes Parents
            x.Parent = y;
            if(x.RightNode != null)
                x.RightNode.Parent = x;
           
            if (y.RightNode != null && cmp.Invoke(y.RightNode.Value, y.Value) < 0)
                throw new Exception("Invalid Left Rotation at node " + y.Value);

            if (y.LeftNode != null && cmp.Invoke(y.LeftNode.Value, y.Value) >= 0)
                throw new Exception("Invalid Right Rotation at node " + y.Value);

            return y;
        }
        private TreeNode<TValue> RotateRight(TreeNode<TValue> x)
        {
            Console.WriteLine(x.Value + " " + x.LeftNode.Value + " " + (x.RightNode != null ? x.RightNode.Value.ToString() : ""));

            if (x.NodeColor == Color.Black)
            {
                throw new Exception("Invalid ! Left Rotation on a Black Node.");
            }

            TreeNode<TValue> parentNode = x.Parent;
            TreeNode<TValue> y = x.LeftNode;
            y.Parent = x.Parent;

            if (y == null)
            {
                throw new Exception("Invalid ! Left Rotation on a Node with Null Right Child.");
            }

            //Set X-left Node to Y-Right Node, Set X to Be Y-Left Node
            x.LeftNode = y.RightNode;
            y.RightNode = x;
            x.Parent = y;
            if(x.LeftNode != null)
                x.LeftNode.Parent = x;

            if (y.RightNode != null && cmp.Invoke(y.RightNode.Value, y.Value) < 0)
                throw new Exception("Invalid Left Rotation at node " + y.Value);

            if (y.LeftNode != null && cmp.Invoke(y.LeftNode.Value, y.Value) >= 0)
                throw new Exception("Invalid Right Rotation at node " + y.Value);

            return y;
        }

        public void printTree()
        {
            print(root);
        }

        private void print(TreeNode<TValue> node)
        {
            if (node == null)
                return;

            if (node.Parent != null && node.Parent.LeftNode == node)
                Console.Write(" Left child: ");
            if (node.Parent != null && node.Parent.RightNode == node)
                Console.Write(" Right Child: ");
            
            Console.WriteLine("Value : " + node.Value + " Color: " + node.NodeColor + " Parent : " + (node.Parent != null ? node.Parent.Value.ToString() : ""));
            print(node.LeftNode);
            print(node.RightNode);
        }

        #region TestFunctions
        public static void Test_Insert()
        {
            RedBlackTree<int> rbt = new RedBlackTree<int>((int x,int y) => { return x - y; });
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};// 4, 5, 6, 7, 8, 9, 10 };
            //Array.Reverse(arr);

            for (int i = 0; i < arr.Length; i++)
                rbt.insert(arr[i]);

            rbt.printTree();
        }
        public void Test_RotateLeft(TValue[] param)
        {
            TreeNode<TValue> myRoot = new TreeNode<TValue>();
            myRoot.RightNode = new TreeNode<TValue>(param[0], null, new TreeNode<TValue>(), myRoot, Color.Red);
            myRoot.LeftNode = new TreeNode<TValue>(param[1], null, null, myRoot, Color.Red);
            TreeNode<TValue> newRight = myRoot.RightNode;
            newRight.RightNode = new TreeNode<TValue>(param[2], null, null, newRight, Color.Red);

            print(myRoot);
            Console.WriteLine("After");
            TreeNode<TValue> New = RotateLeft(myRoot);
            print(New);
        }
        #endregion

    }
}
