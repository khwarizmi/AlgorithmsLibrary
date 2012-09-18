using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary.Trees
{
    class RangeTree<TValue>
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
                set { _value = value; }
            }

            #endregion

        }
        #endregion

        #region DataMembers
        int _itemsCount;
        TreeNode<TValue> root;
        Comparison<TValue> cmp;
        #endregion

        public RangeTree(Comparison<TValue> nodeComparer)
        {
            cmp = nodeComparer;
        }

        private TreeNode<TValue> getNode(TValue item)
        {
            TreeNode<TValue> x = root;

            while (x != null)
            {
                int comparison = cmp(item, x.Value);
                if (comparison == 0)
                    return x;
                else if (comparison < 0)
                    x = x.LeftNode;
                else
                    x = x.RightNode;
            }

            return null;
        }
        private TreeNode<TValue> getSpliterNode(TValue r1, TValue r2)
        {
            TreeNode<TValue> x = root;
            while (x != null)
            {
                if (cmp.Invoke(r1, x.Value) <= 0 && cmp.Invoke(r2, x.Value) >= 0)
                {
                    return x;
                }
                else if (cmp.Invoke(r1, x.Value) > 0)
                    x = x.RightNode;
                else
                    x = x.LeftNode;
            }

            return null;
        }
        private List<TValue> fetchSubTree(TreeNode<TValue> node)
        {
            if (node == null)
                return null;

            //Fetch Left and Right Childs
            List<TValue> leftChilds = fetchSubTree(node.LeftNode);
            TValue value = node.Value;
            List<TValue> rightChilds = fetchSubTree(node.RightNode);

            //Merge both Lists if not Null
            if (leftChilds == null)
                leftChilds = new List<TValue>();
            leftChilds.Add(value);
            if (rightChilds != null)
                leftChilds.AddRange(rightChilds);

            return leftChilds;
        }

        public int count()
        {
            return _itemsCount;
        }
        public bool contains(TValue item)
        {
            return getNode(item) != null;
        }

        public void insert(TValue item)
        {
            if (root == null)
            {
                _itemsCount = 1;
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
            //Add Items Count
            _itemsCount++;
        }
        private void Fix_Insert(TreeNode<TValue> z)
        {
            while (z.Parent != null && z.Parent.NodeColor == Color.Red && z.Parent.Parent != null)
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
                                parentParent.LeftNode = parent;
                            else
                                parentParent.RightNode = parent;
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
                                parentParent.LeftNode = parent;
                            else
                                parentParent.RightNode = parent;
                        }
                    }
                }
            }

            //Always Set Color of Root node to Black
            root.NodeColor = Color.Black;
        }

        public List<TValue> range(TValue r1, TValue r2)
        {
            List<TValue> returnRange = new List<TValue> ();
            TreeNode<TValue> splitNode = getSpliterNode(r1, r2);

            if (splitNode == null)
                return returnRange;

            TreeNode<TValue> splitRight = splitNode.RightNode;
            while (splitRight != null)
            {
                if (cmp.Invoke(r2, splitRight.Value) >= 0)
                {
                    //Fetch all left-subtree Nodes
                    List<TValue> temp = fetchSubTree(splitRight.LeftNode);
                    if(temp != null)
                        returnRange.AddRange(temp);
                    returnRange.Add(splitRight.Value);
                    //Recurse on Right node
                    splitRight = splitRight.RightNode;
                }
                else
                {
                    splitRight = splitRight.LeftNode;
                }
            }

            returnRange.Add(splitNode.Value);

            TreeNode<TValue> splitLeft = splitNode.LeftNode;
            while (splitLeft != null)
            {
                if (cmp.Invoke(r1, splitLeft.Value) <= 0)
                {
                    //Fetch all left-subtree Nodes
                    List<TValue> temp = fetchSubTree(splitLeft.RightNode);
                    if(temp != null)
                        returnRange.AddRange(temp);
                    returnRange.Add(splitLeft.Value);
                    //Recurse on Right node
                    splitLeft = splitLeft.LeftNode;
                }
                else
                {
                    splitLeft = splitLeft.RightNode;
                }
            }

            returnRange.Reverse();
            return returnRange;
        }

        private TreeNode<TValue> RotateLeft(TreeNode<TValue> x)
        {
            //Console.WriteLine(x.Value + " " + x.RightNode.Value + " " + (x.LeftNode != null? x.LeftNode.Value.ToString() : ""));
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

            if (y.LeftNode != null && cmp.Invoke(y.LeftNode.Value, y.Value) > 0)
                throw new Exception("Invalid Right Rotation at node " + y.Value);

            return y;
        }
        private TreeNode<TValue> RotateRight(TreeNode<TValue> x)
        {
            //Console.WriteLine(x.Value + " " + x.LeftNode.Value + " " + (x.RightNode != null ? x.RightNode.Value.ToString() : ""));
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

            if (y.LeftNode != null && cmp.Invoke(y.LeftNode.Value, y.Value) > 0)
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

        private void TestTreeConditions()
        {
            //1- Root is Black
            if (root != null && root.NodeColor != Color.Black)
                throw new Exception("Rule Number One : Root Node tree isn't black");
            
            //2- Every Red node has two Black childs
            TestRedProperty(root, 0);

        }
        private void TestRedProperty(TreeNode<TValue> x, int level)
        {
            if (x == null)
                return;
            
            if(x.NodeColor == Color.Red && !((x.LeftNode == null || x.LeftNode.NodeColor == Color.Black) && (x.RightNode == null || x.RightNode.NodeColor == Color.Black)))
                throw new Exception("Rule Number Two : Red Node = "+ x.Value +" at level Number = " + level + " with one or two red child nodes");
            
            TestRedProperty(x.LeftNode, level + 1);
            TestRedProperty(x.RightNode, level + 1);
        }

        #region TestFunctions
        public static void Test_Range()
        {
            Random random = new Random ();
            int tries = random.Next(100);
            for (int i = 0; i < tries; i++)
            {
                RangeTree<int> range = new RangeTree<int>((int x, int y) => { return x - y; });
                int items = random.Next(300);
                List<int> values = new List<int>();
                for (int j = 0; j < items; j++)
                {
                    int t = random.Next(500000);
                    range.insert(t);
                    values.Add(t);
                }
                int r1 = random.Next(5000);
                int r2 = r1 + random.Next(100000);
                List<int> ret = range.range(r1, r2);

                if(range.count() != items)
                    throw new Exception("Expected Count of Tree is : " + items + " While recieved Tree Count is : " + range.count());

                int expectedCount = 0;
                for (int j = 0; j < values.Count; j++)
                    if (r1 <= values[j] && r2 >= values[j])
                        expectedCount++;

                for (int p = 0; p < ret.Count; p++)
                    if (ret[p] < r1 || ret[p] > r2)
                        throw new Exception("Wrong Range Added");

                if (expectedCount != ret.Count)
                {
                    for (int k = 0; k < ret.Count; k++)
                        Console.Write(" " + ret[i]);

                    throw new Exception("Expected Count is : " + expectedCount + " While recieved is : " + ret.Count);
                }
            }
        }
        public static void Test_Insert()
        {
            RangeTree<int> rbt = new RangeTree<int>((int x, int y) => { return x - y; });
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
