using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab04
{
    class Category
    {
        public string Name { get; set; }
        public List<Category> SubCategories { get; set; }
        public MaxHeap Users { get; set; }
        public string Path;
        public int Depth;
        public Category(string Name, string Path)
        {
            this.Name = Name;
            this.SubCategories = new List<Category>();
            this.Users = new MaxHeap();
            this.Path = Path;
            this.Depth = Path.Split(':').Length;
        }
        public bool deleteUser(string UserName)
        {
            bool userRemoved = false;
            if (Users.deleteUser(UserName)) userRemoved = true;
            foreach (var subCategory in SubCategories) if (subCategory.deleteUser(UserName)) userRemoved = true;
            return userRemoved;
        }
        public void UpdatePathAndDepth(string removeFromPath)
        {
            var paths = this.Path.Split(':').ToList();
            foreach (var path in paths)
            {
                if (path == removeFromPath)
                {
                    paths.Remove(path);
                    this.Path = string.Join(":", paths);
                    this.Depth = Path.Split(':').Length;
                    break;
                }
            }
            foreach (var subCategory in SubCategories) subCategory.UpdatePathAndDepth(removeFromPath);
        }
        public bool DeleteAllUsers()
        {
            bool anyDeleted = false;
            if (Users.mainNode != null)
            {
                Users = new MaxHeap();
                anyDeleted = true;
            }
            foreach (var subCategory in SubCategories) if (subCategory.DeleteAllUsers()) anyDeleted = true;
            return anyDeleted;
        }
        public bool SearchAndWriteUser(string name)
        {
            bool founded = false;
            if (Users.printUser(name, this.Name)) founded = true;
            foreach (var subCategory in SubCategories) if (subCategory.SearchAndWriteUser(name)) founded = true;
            return founded;
        }
        public bool SearchAndWriteReservations(string name)
        {
            bool founded = false;
            if (Users.printReservations(name)) founded = true;
            foreach (var subCategory in SubCategories) if (subCategory.SearchAndWriteReservations(name)) founded = true;
            return founded;
        }
        public bool PrintPlaceName(string name)
        {
            bool founded = false;
            if (Users.printNameIfPlaceNameExist(name, Name)) founded = true;
            foreach (var subCategory in SubCategories) if (subCategory.PrintPlaceName(name)) founded = true;
            return founded;
        }
        public bool PrintAllUsers()
        {
            bool anyPrinted = false;
            if (Users.printUsers(this.Name)) anyPrinted = true;
            foreach (var subCategory in SubCategories) if (subCategory.PrintAllUsers()) anyPrinted = true;
            return anyPrinted;
        }
        public void addCategory(string name)
        {
            Category PushInto = this.SubCategories.Find(u => u.Name == name);
            if (PushInto != null) Console.WriteLine("This category already exist.");
            else
            {
                var willbepushed = new Category(name, this.Path + ":" + name);
                this.SubCategories.Add(willbepushed);
                Console.WriteLine(willbepushed.Name + " added to tree successfully.");
            }
        }
    }
}