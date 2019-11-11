using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Seems to be a problem with Zebra
/// </summary>
namespace MyList
{
    class Program
    {
        static void Main(string[] args)
        {
            MyList<string> weapons = new MyList<string>();
            weapons.Add("Cat");
            weapons.Add("Dog");
            weapons.Add("Zebra");
            weapons.Add("Lion");
            weapons.Add("Lima");
            weapons.Add("Gato");
            weapons.Add("Callum Adams");
            weapons.Add("Big T");
            weapons.MovePointer(5);
            weapons.DisplayList();
            weapons.MovePointer(7);
            Console.ReadLine();
        }

    }

    /// <summary>
    /// A list that has a 'Generic Type Perameter', denoted by <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class MyList<T>
    {
        ///<summary>The entry point of the Node (a refrence to the first node)</summary>
        public Node<T> Head;
        /// <summary>
        /// The current node/pointer (allows traversal of the list)
        /// </summary>
        public Node<T> current;

        //A pointer along the list which can be moved at any time
        public Node<T> pointer;
        
        ///<summary>The length of the list (the number of nodes in the list)</summary>
        int listLength = 0;

        /// <summary>
        /// Add an item to the end of the list
        /// </summary>
        /// <param name="input"></param>
        public void Add(T input)
        {
            //Instantiate a new node and give it a new input
            Node<T> Node = new Node<T>(input);
            GiveNodePosition(Node);
            listLength += 1;
        }

        ///// <summary>
        ///// Move the pointer from it's current position to it's new position
        ///// </summary>
        ///// <param name="newPos"></param>
        //public void MovePointer(int newPos)
        //{
        //    Console.WriteLine(ReturnNodePosition(pointer.Data));

        //    int pointerPosition = ReturnNodePosition(pointer.Data);

        //    int nodesToMove = newPos - pointerPosition;

        //    for (int i = 0; i < nodesToMove; i++)
        //    {
        //        pointer = pointer.NextNode;
        //    }
        //    Console.WriteLine();
        //    Console.WriteLine(ReturnNodePosition(pointer.Data));
        //    Console.WriteLine(ReturnData(ReturnNodePosition(pointer.Data)));
        //    Console.WriteLine();
        //}

        /// <summary>
        /// Return the data held within a node at a given position
        /// </summary>
        /// <param name="pos"></param>
        public T ReturnData(int pos)
        {
            Node<T> tempPointer = Head;

            for (int i = 0; i < pos-1; i++)
            {
                tempPointer = tempPointer.NextNode;
            }

            return tempPointer.Data;
        }

        public void DisplayList()
        {
            //Creates a temporary pointer
            Node<T> TempPointer;
            //Sets pointer position equal to the Head position
            TempPointer = Head;

            //For the number of nodes in the list (the length of the list)
            for (int i = 0; i < listLength; i++)
            {
                //Print current data in the node
                Console.WriteLine(TempPointer.Data);
                //Move the TempPointer to the next TempPointer
                TempPointer = TempPointer.NextNode;
            }

        }

        /// <summary>
        /// Assign the node a position within the list
        /// </summary>
        /// <param name="Node"></param>
        public void GiveNodePosition(Node<T> Node)
        {
            //If the list is empty, the Head is equal to the first node
            if (this.Head == null)
            {
                //The Head of the list is equal to the first node
                this.Head = Node;
            }
            else
            {
                //The next node of the current node is equal to the new node
                current.NextNode = Node;

                //The previous node (in relation to the new node) is equal to the current node
                Node.PreviousNode = current;
            }

            //The current is now equal to the new node (pointer moved to end)
            current = Node;
        }

        /// <summary>
        /// Assign the node a position within the list, while traversing through the list
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="TempPointer"></param>
        public void GiveNodePosition(Node<T> Node, Node<T> TempPointer)
        {
            //The node's previous node is equal to the tempPointer
            Node.PreviousNode = TempPointer;
            //The node's next node is equal to the tempPointer's next node
            Node.NextNode = TempPointer.NextNode;
            //The tempPointer's next node is equal to the node (the main subject)
            TempPointer.NextNode = Node;
            //The Node's next node's previous node is now equal to the node
            Node.NextNode.PreviousNode = Node;
            //The tempPointer's previous node is equal to the tempPointe/Head
            TempPointer.PreviousNode = Node.PreviousNode;
            //The tempPointer now sits on the node
            TempPointer = Node;
        }

        /// <summary>
        /// Add an item to a given position in the string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pos"></param>
        public void Add(T input, int pos)
        {
            //Instantiate a new node and give it an input value
            Node<T> Node = new Node<T>(input);
            //A temporary pointer that points to nodes while transending the list
            Node<T> TempPointer;
            //The temporary pointer is equal to the Head (Put the pointer to the start of the list)
            TempPointer = Head;

            //If the position of insertion is equal to 1
            if (pos == 1)
            {
                //The node's next node is the tempPointer / Head
                Node.NextNode = TempPointer;
                //Because the node is now at the top, it has no previous nodem hence null
                Node.PreviousNode = null;
                //The tempPointer's previous node now looks at the node
                TempPointer.PreviousNode = Node;
                //The new node is now the head of the list/at the top
                Head = Node;
            }
            //If the position of insertion is equal to 2
            else if (pos == 2)
            {
                ///Change the head's next node to point at the new node, and then the change the pointer's 
                ///from the (old) second node and point them to the new node (which is now the new second node)
                Head.NextNode.PreviousNode = Node;
                Node.PreviousNode = Head;
                Node.NextNode = Head.NextNode;
                Head.NextNode = Node;
            }
            else if (pos > 2)
            {
                //For the position-2, traverse along the list and then add a node (putting it in the desired position, hence -1)
                for (int i = 0; i < pos - 2; i++)
                {
                    TempPointer = TempPointer.NextNode;
                }

                GiveNodePosition(Node, TempPointer);
            }

            listLength += 1;


        }

        //Returns the position that the node is in the list
        public int ReturnNodePosition(T input)
        {
            //Create a pointer that starts at the top of the list
            Node<T> TempPointer = Head;
                        
            int position = 0;
            
            //While the current node is not equal to the node we are looking for
            while (!EqualityComparer<T>.Default.Equals(TempPointer.Data, input))
            {
                //Move the pointer to it's own next pointer
                TempPointer = TempPointer.NextNode;
                //Increment the position value by one
                position += 1;
            }

            //Add one onto the position value becuase the position value above stops one short of our input
            position += 1;


            return position;
        }

        //Moves a node from an old poisition to a new position when the two positions are known
        /// <param name="oldPos"></param>
        /// <param name="newPos"></param>
        public void MoveNode(int oldPos, int newPos)
        {
            Node<T> OldTempPosPointer = Head;
            Node<T> NewTempPosPointer = Head;

            //Moves the TempOldPosPointer to the node on the old position
            for (int i = 0; i < oldPos - 1; i++)
            {
                OldTempPosPointer = OldTempPosPointer.NextNode;
            }

            //Moves the TempNewPosPointer to the node on the new position
            for (int i = 0; i < newPos - 1; i++)
            {
                NewTempPosPointer = NewTempPosPointer.NextNode;
            }

            if (newPos == 1 && oldPos != listLength)
            {
                //Isolates Node
                OldTempPosPointer.PreviousNode.NextNode = OldTempPosPointer.NextNode;
                OldTempPosPointer.NextNode.PreviousNode = OldTempPosPointer.PreviousNode;

                OldTempPosPointer.PreviousNode = null;
                OldTempPosPointer.NextNode = NewTempPosPointer;

                NewTempPosPointer.PreviousNode = OldTempPosPointer;

                Head = OldTempPosPointer;
            }
            else if (newPos == listLength && oldPos != 1)
            {
                //Isolates node
                OldTempPosPointer.PreviousNode.NextNode = OldTempPosPointer.NextNode;
                OldTempPosPointer.NextNode.PreviousNode = OldTempPosPointer.PreviousNode;

                OldTempPosPointer.PreviousNode = NewTempPosPointer;
                OldTempPosPointer.NextNode = null;

                NewTempPosPointer.NextNode = OldTempPosPointer;
            }
            else if (oldPos == 1 && newPos != listLength)
            {
                NewTempPosPointer = NewTempPosPointer.NextNode;
                Head = OldTempPosPointer.NextNode;
                OldTempPosPointer.NextNode = NewTempPosPointer;
                Head.PreviousNode = null;
                

                NewTempPosPointer.PreviousNode.NextNode = OldTempPosPointer;
                OldTempPosPointer.PreviousNode = NewTempPosPointer.PreviousNode;
                NewTempPosPointer.PreviousNode = OldTempPosPointer;
            }
            else if (oldPos == listLength && newPos != 1)
            {
                OldTempPosPointer.PreviousNode.NextNode = null;

                OldTempPosPointer.NextNode = NewTempPosPointer;
                OldTempPosPointer.PreviousNode = NewTempPosPointer.PreviousNode;

                NewTempPosPointer.PreviousNode.NextNode = OldTempPosPointer;
                NewTempPosPointer.PreviousNode = OldTempPosPointer;
            }
            else if (oldPos == listLength && newPos == 1)
            {
                OldTempPosPointer.PreviousNode.NextNode = null;
                OldTempPosPointer.PreviousNode = null;
                Head = OldTempPosPointer;
                OldTempPosPointer.NextNode = NewTempPosPointer;
            }
            else if (newPos == listLength && oldPos == 1)
            {
                //NewTempPosPointer = NewTempPosPointer.NextNode;
                Head = OldTempPosPointer.NextNode;
                OldTempPosPointer.NextNode.PreviousNode = null;

                NewTempPosPointer.NextNode = OldTempPosPointer;
                OldTempPosPointer.PreviousNode = NewTempPosPointer;
                NewTempPosPointer.PreviousNode = OldTempPosPointer;
                OldTempPosPointer.NextNode = NewTempPosPointer;
            }
            else
            {
                //Isolate Node
                OldTempPosPointer.PreviousNode.NextNode = OldTempPosPointer.NextNode;
                OldTempPosPointer.NextNode.PreviousNode = OldTempPosPointer.PreviousNode;


                OldTempPosPointer.PreviousNode = NewTempPosPointer;
                OldTempPosPointer.NextNode = NewTempPosPointer.NextNode;
                                
                NewTempPosPointer.NextNode.PreviousNode = OldTempPosPointer;
                NewTempPosPointer.NextNode = OldTempPosPointer;
            }

        }

        //Moves a node from an old poisition to a new position when the node name and new position is known
        /// <param name="Name"></param>
        /// <param name="newPos"></param>
        public void MoveNode(T Name, int newPos)
        {
            int oldPos = ReturnNodePosition(Name);
            MoveNode(oldPos, newPos);
        }

        //Moves a node at a given position to replace the node in another position, given by it's name
        /// <param name="Name"></param>
        /// <param name="newPos"></param>
        public void MoveNode(int pos, T Name)
        {
            int newPos = ReturnNodePosition(Name);
            MoveNode(pos, newPos);
        }

        /// <summary>
        /// Remove an item from the list that equals <typeparamref name="input"/>
        /// </summary>
        /// <param name="input"></param>
        public void Remove(T input)
        {
            //Instantiate a new node and set it equal to the head
            //This will essentially act as a pointer, that traverses the list
            Node<T> TempPointer = Head;

            //While TempPointer.Data is not equal to the input
            while (!EqualityComparer<T>.Default.Equals(TempPointer.Data, input))
            {
                TempPointer = TempPointer.NextNode;
            }

            //If the node to remove is the same as the head
            if (TempPointer == Head)
            {
                //Remove the head and all it's pointers
                //Make the next node in the list the new head and assign new pointers
                Head = Head.NextNode;
                TempPointer.NextNode.PreviousNode = null;
                TempPointer.NextNode = null;
            }
            //If the node to remove is the last one in the list
            else if (TempPointer == current)
            {
                //Remove the last node in the list and assign new pointers
                current = TempPointer.PreviousNode;
                TempPointer.PreviousNode.NextNode = null;
                TempPointer.PreviousNode = null;
            }
            else
            {
                //Sets the tempPointer's previous node's next node, to the node after the main node
                TempPointer.PreviousNode.NextNode = TempPointer.NextNode;
                //Sets the tempPointer's next node's previous node, to the node before the main node
                TempPointer.NextNode.PreviousNode = TempPointer.PreviousNode;
            }

            //Essentialy 'Deletes' the object
            TempPointer = null;


            //Decreases the list length by one (1)
            listLength -= 1;
        }

        /// <summary>
        /// Remve an item from the list at the given position <typeparamref name="pos"/>
        /// </summary>
        /// <param name="pos"></param>
        public void Remove(int pos)
        {
            //Instantiate a new node and set it equal to the head
            //This will essentially act as a pointer, that traverses the list
            Node<T> TempPointer = Head;

            //For position of the node that wants to be removed - 1
            for (int i = 0; i < pos - 1; i++)
            {
                TempPointer = TempPointer.NextNode;
            }

            //If the node to remove is the same as the head
            if (TempPointer == Head)
            {
                //Remove the head and all it's pointers
                //Make the next node in the list the new head and assign new pointers
                Head = Head.NextNode;
                TempPointer.NextNode.PreviousNode = null;
                TempPointer.NextNode = null;
            }
            //If the node to be deleted is equal to the last node in the lsit
            else if (TempPointer == current)
            {
                //Remove the last node in the list
                //Make the previous node in the list the new current and assign new pointers
                current = TempPointer.PreviousNode;
                TempPointer.PreviousNode.NextNode = null;
                TempPointer.PreviousNode = null;
            }
            else
            {
                //Sets the tempPointer's previous node's next node, to the node after the main node
                TempPointer.PreviousNode.NextNode = TempPointer.NextNode;
                //Sets the tempPointer's next node's previous node, to the node before the main node
                TempPointer.NextNode.PreviousNode = TempPointer.PreviousNode;
                //'Deletes' the object
            }
            TempPointer = null;

            ////Sets the tempPointer's previous node's next node, to the node after the main node
            //TempPointer.PreviousNode.NextNode = TempPointer.NextNode;
            ////Sets the tempPointer's next node's previous node, to the node before the main node
            //TempPointer.NextNode.PreviousNode = TempPointer.PreviousNode;

            listLength -= 1;
        }

        //Replace a node with new data, given the new input and the position of the node to be replaced
        /// <param name="input"></param>
        /// <param name="pos"></param>
        public void ReplaceNode(T input, int pos)
        {
            Node<T> TempPointer = Head;

            for (int i = 0; i < pos-1; i++)
            {
                TempPointer = TempPointer.NextNode;
            }

            TempPointer.Data = input;

        }

        //Replace a node with new data, given the new input and the data held within the node to be replaced
        /// <param name="input"></param>
        /// <param name="nodeToBeReplaced"></param>
        public void ReplaceNode(T input, T nodeToBeReplaced)
        {
            Node<T> TempPointer = Head;
            for (int i = 0; i < ReturnNodePosition(nodeToBeReplaced)-1; i++)
            {
                TempPointer = TempPointer.NextNode;
            }
            TempPointer.Data = input;
        }

        /// <summary>
        /// Search the list to see it it contains the given <typeparamref name="input"/> 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Contains(T input)
        {
            bool contains = false;

            Node<T> TempPointer = Head;

            //For the length of the list
            for (int i = 0; i < listLength; i++)
            {
                //If tempPointer's data is not equal to the input
                if (!EqualityComparer<T>.Default.Equals(TempPointer.Data, input))
                {
                    //Move the tempPointer to the next node
                    TempPointer = TempPointer.NextNode;
                }
                else //The only other alternative
                {
                    //The list must contain the input
                    contains = true;
                }
            }

            return contains;
        }

        /// <summary>
        /// Removes all items from the list
        /// </summary>
        public void Clear()
        {
            //Set the first and last pointer of the list equal to null
            Head = null;
            current = null;
            //Set the list length equal to zero
            listLength = 0;
            //Since the list is still essentially there, but with no pointers, collect and remove the abandoned list
            GC.Collect();
        }

    }

    class Node<T>
    {
        //The data held within the node
        public T Data;
        //The Next Node infront of this node
        public Node<T> NextNode;
        //The Previous Node behind this node
        public Node<T> PreviousNode;

        /// <summary>
        /// Constructor to set a node's data equal to the input
        /// </summary>
        /// <param name="input"></param>
        public Node(T input)
        {
            //Node's data equal to input
            this.Data = input;
        }

    }



}
