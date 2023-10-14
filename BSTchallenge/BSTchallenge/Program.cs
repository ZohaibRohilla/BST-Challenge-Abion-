using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        BinaryTree tree = new BinaryTree();
        while (true)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Insert data");
            Console.WriteLine("2. View tree");
            Console.WriteLine("3. Search tree");
            Console.WriteLine("4. Delete node");
            Console.WriteLine("5. Clear tree");
            Console.WriteLine("6. Pre order traversal");
            Console.WriteLine("7. In order traversal");
            Console.WriteLine("8. Post order traversal");
            Console.WriteLine("9. Exit");

            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter the name:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter the integer:");
                    int value = Convert.ToInt32(Console.ReadLine());
                    tree.Insert(name, value);
                    break;
                case 2:
                    tree.DisplayTree();
                    break;
                case 3:
                    Console.WriteLine("Enter the name to search:");
                    string searchName = Console.ReadLine();
                    tree.Search(searchName);
                    break;
                case 4:
                    Console.WriteLine("Enter the name to delete:");
                    string deleteName = Console.ReadLine();
                    tree.Delete(deleteName);
                    break;
                case 5:
                    tree.Clear();
                    break;
                case 6:
                    tree.PreOrder();
                    break;
                case 7:
                    tree.InOrder();
                    break;
                case 8:
                    tree.PostOrder();
                    break;
                case 9:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}

class Node
{
    public string Name { get; set; }
    public int Value { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(string name, int value)
    {
        Name = name;
        Value = value;
        Left = null;
        Right = null;
    }
}

class BinaryTree
{
    public Node Root { get; set; }

    public BinaryTree()
    {
        Root = null;
    }

    public void Insert(string name, int value)
    {
        if (Root == null)
        {
            Root = new Node(name, value);
        }
        else
        {
            InsertRecursive(Root, name, value);
        }
    }

    private Node InsertRecursive(Node current, string name, int value)
    {
        if (value < current.Value)
        {
            if (current.Left == null)
            {
                current.Left = new Node(name, value);
            }
            else
            {
                InsertRecursive(current.Left, name, value);
            }
        }
        else if (value >= current.Value)
        {
            if (current.Right == null)
            {
                current.Right = new Node(name, value);
            }
            else
            {
                InsertRecursive(current.Right, name, value);
            }
        }

        return current;
    }

    public void DisplayTree()
    {
        DisplayTreeRecursive(Root);
    }

    private void DisplayTreeRecursive(Node current)
    {
        if (current == null)
        {
            return;
        }

        Console.WriteLine($"Name: {current.Name}, Value: {current.Value}");
        DisplayTreeRecursive(current.Left);
        DisplayTreeRecursive(current.Right);
    }

    public void Search(string name)
    {
        Node result = SearchRecursive(Root, name);
        if (result == null)
        {
            Console.WriteLine($"No node found with name: {name}");
        }
        else
        {
            Console.WriteLine($"Found node: Name: {result.Name}, Value: {result.Value}");
        }
    }

    private Node SearchRecursive(Node current, string name)
    {
        if (current == null)
        {
            return null;
        }

        if (name == current.Name)
        {
            return current;
        }

        Node result = SearchRecursive(current.Left, name);
        if (result != null)
        {
            return result;
        }

        return SearchRecursive(current.Right, name);
    }

    public void Delete(string name)
    {
        Root = DeleteRecursive(Root, name);
    }

    private Node DeleteRecursive(Node current, string name)
    {
        if (current == null)
        {
            return null;
        }

        if (name == current.Name)
        {
            // Node to delete found

            if (current.Left == null && current.Right == null)
            {
                return null;
            }

            if (current.Right == null)
            {
                return current.Left;
            }

            if (current.Left == null)
            {
                return current.Right;
            }

            // Node has two children, find in-order successor
            Node inOrderSuccessor = GetMinValueNode(current.Right);
            current.Value = inOrderSuccessor.Value;
            current.Name = inOrderSuccessor.Name;

            // Delete in-order successor node
            current.Right = DeleteRecursive(current.Right, inOrderSuccessor.Name);
            return current;
        }

        if (name.CompareTo(current.Name) < 0)
        {
            current.Left = DeleteRecursive(current.Left, name);
        }
        else
        {
            current.Right = DeleteRecursive(current.Right, name);
        }

        return current;
    }

    private Node GetMinValueNode(Node node)
    {
        Node current = node;
        while (current.Left != null)
        {
            current = current.Left;
        }

        return current;
    }

    public void Clear()
    {
        Root = null;
    }

    public void PreOrder()
    {
        PreOrderRecursive(Root);
    }

    private void PreOrderRecursive(Node current)
    {
        if (current == null)
        {
            return;
        }

        Console.WriteLine($"Name: {current.Name}, Value: {current.Value}");
        PreOrderRecursive(current.Left);
        PreOrderRecursive(current.Right);
    }

    public void InOrder()
    {
        InOrderRecursive(Root);
    }

    private void InOrderRecursive(Node current)
    {
        if (current == null)
        {
            return;
        }

        InOrderRecursive(current.Left);
        Console.WriteLine($"Name: {current.Name}, Value: {current.Value}");
        InOrderRecursive(current.Right);
    }

    public void PostOrder()
    {
        PostOrderRecursive(Root);
    }

    private void PostOrderRecursive(Node current)
    {
        if (current == null)
        {
            return;
        }

        PostOrderRecursive(current.Left);
        PostOrderRecursive(current.Right);
        Console.WriteLine($"Name: {current.Name}, Value: {current.Value}");
    }
}
