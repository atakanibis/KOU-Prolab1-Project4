using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab04
{
    class MaxHeap
    {
        public Node mainNode;
        public class Node
        {
            public User value { get; set; }
            public Node left { get; set; }
            public Node right { get; set; }
            public Node parent { get; set; }
            public Node(User user) { value = user; }
        }
        public Node InsertOrFind(User user)
        {
            if (mainNode == null)
            {
                mainNode = new Node(user);
                mainNode.parent = mainNode;
                return mainNode;
            }
            else return this.InsertOrFind(mainNode, user);
        }
        private Node InsertOrFind(Node temp, User user)
        {
            var founded = this.Find(user.ID);
            if (founded != null) return founded;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(temp);
            while (q.Count != 0)
            {
                Node temprory = q.Dequeue();
                if (temprory.left == null)
                {
                    temprory.left = new Node(user);
                    temprory.left.parent = temprory;
                    return temprory.left;
                }
                else q.Enqueue(temprory.left);
                if (temprory.right == null)
                {
                    temprory.right = new Node(user);
                    temprory.right.parent = temprory;
                    return temprory.right;
                }
                else q.Enqueue(temprory.right);
            }
            return null;
        }
        public int Count()
        {
            int count = 0;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(mainNode);
            while (q.Count != 0)
            {
                Node temprory = q.Dequeue();
                if (temprory == null) continue;
                if (temprory.left != null)
                {
                    q.Enqueue(temprory.left);
                }
                if (temprory.right != null)
                {
                    q.Enqueue(temprory.right);
                }
                count++;
            }
            return count;
        }
        public int ReservationCount()
        {
            int count = 0;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(mainNode);
            while (q.Count != 0)
            {
                Node temprory = q.Dequeue();
                if (temprory == null) continue;
                if (temprory.left != null)
                {
                    q.Enqueue(temprory.left);
                }
                if (temprory.right != null)
                {
                    q.Enqueue(temprory.right);
                }
                count += temprory.value.Reservations.Count();
            }
            return count;
        }
        public Node Find(User user)
        {
            return Find(user.ID);
        }
        public Node Find(string name)
        {
            Queue<Node> q = new Queue<Node>();
            if (mainNode != null) q.Enqueue(mainNode);
            while (q.Count != 0)
            {
                Node temprory = q.Dequeue();
                if (temprory.left != null)
                {
                    q.Enqueue(temprory.left);
                }
                if (temprory.right != null)
                {
                    q.Enqueue(temprory.right);
                }
                if (temprory.value.ID == name) return temprory;
            }
            return null;
        }
        public static void Sort(Node node)
        {
            while (node.value.Reservations.Count() > node.parent.value.Reservations.Count())
            {
                User temp = node.value;
                node.value = node.parent.value;
                node.parent.value = temp;
                node = node.parent;
            }
        }
        private void SortFromMiddle(Node node)
        {
            while ((node.left != null && node.left.value.Reservations.Count() > node.value.Reservations.Count()) || (node.right != null && node.right.value.Reservations.Count() > node.value.Reservations.Count()))
            {
                if (node.left.value.Reservations.Count() > node.value.Reservations.Count())
                {
                    User temp = node.value;
                    node.value = node.left.value;
                    node.left.value = temp;
                    node = node.left;
                }
                else if (node.right.value.Reservations.Count() > node.value.Reservations.Count())
                {
                    User temp = node.value;
                    node.value = node.right.value;
                    node.right.value = temp;
                    node = node.right;
                }
            }
        }
        private Node FindLast()
        {
            Queue<Node> q = new Queue<Node>();
            if (mainNode != null) q.Enqueue(mainNode);
            bool lastRow = false;
            while (q.Count != 0)
            {
                Node temprory = q.Dequeue();
                if (temprory.left != null)
                {
                    q.Enqueue(temprory.left);
                }
                if (temprory.right != null)
                {
                    q.Enqueue(temprory.right);
                }
                if (temprory.left == null && temprory.right == null) lastRow = true;
                if (lastRow && q.Count == 0) return temprory;
            }
            return null;
        }
        public bool deleteUser(string userName)
        {
            Node willBeRemoved = this.Find(userName);
            if (willBeRemoved != null)
            {
                if (willBeRemoved == willBeRemoved.parent)
                {
                    mainNode = null;
                    return true;
                }
                Node last = FindLast();
                if (last != null)
                {
                    User temp = willBeRemoved.value;
                    willBeRemoved.value = last.value;
                    last.value = temp;
                    if (last.parent.right != null) last.parent.right = null;
                    else last.parent.left = null;
                    MaxHeap.Sort(willBeRemoved);
                    this.SortFromMiddle(willBeRemoved);
                    return true;
                }
            }
            return false;
        }
        public bool printUsers(string CategoryName)
        {
            bool anyPrinted = false;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(mainNode);
            while (q.Count != 0)
            {
                Node temprory = q.Dequeue();
                if (temprory == null) continue;
                if (temprory.left != null)
                {
                    q.Enqueue(temprory.left);
                }
                if (temprory.right != null)
                {
                    q.Enqueue(temprory.right);
                }
                anyPrinted = true;
                Console.WriteLine("User Name: {0} Reservation Count: {1} Category: {2}", temprory.value.ID, temprory.value.Reservations.Count(), CategoryName); //Place: {1} Time: {2} Longitude: {3} Latitude: {4}
            }
            return anyPrinted;
        }
        public bool printUser(string name, string categoryName)
        {
            bool founded = true;
            Node willBeWritten = this.Find(name);
            if (willBeWritten != null)
                Console.WriteLine("{1}: {0}", willBeWritten.value.Reservations.Count(), categoryName);
            else founded = false;
            return founded;
        }
        public bool printReservations(string name)
        {
            bool founded = true;
            Node willBeWritten = this.Find(name);
            if (willBeWritten != null) willBeWritten.value.printReservations();
            else founded = false;
            return founded;
        }
        public bool printNameIfPlaceNameExist(string placeName, string categoryName)
        {
            bool printed = false;
            Queue<Node> q = new Queue<Node>();
            if (mainNode != null) q.Enqueue(mainNode);
            while (q.Count != 0)
            {
                Node temprory = q.Dequeue();
                if (temprory.left != null)
                {
                    q.Enqueue(temprory.left);
                }
                if (temprory.right != null)
                {
                    q.Enqueue(temprory.right);
                }
                if (temprory.value.printFromPlace(placeName, categoryName)) printed = true;
            }
            return printed;
        }
    }
}
