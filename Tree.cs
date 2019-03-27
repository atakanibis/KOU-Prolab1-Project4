using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab04
{
    class Tree
    {
        readonly List<Category> Categories;
        public Tree(string FileName)
        {
            this.Categories = new List<Category>();
            var lines = File.ReadLines("rezervasyon.txt");
            foreach (var line in lines)
            {
                var args = line.Split(',');
                var categoryNames = args.LastOrDefault();
                this.CreateCategoryIfDoesntExist(categoryNames);
                var currentCategory = FindCategoryByName(categoryNames.Split(':').LastOrDefault());
                var userName = args.FirstOrDefault();
                MaxHeap.Node InsertedOrFounded = currentCategory.Users.InsertOrFind(new User(userName));
                var parserRule = CultureInfo.InvariantCulture.NumberFormat;
                Reservation newReservation = new Reservation(args[1], args[2], float.Parse(args[3], parserRule), float.Parse(args[4], parserRule), args[5]);
                InsertedOrFounded.value.Reservations.Add(newReservation);
                MaxHeap.Sort(InsertedOrFounded);

            }
            Console.WriteLine("Tree successfully created.");
        }
        public void CreateCategoryIfDoesntExist(string Name, bool printIfExist = false)
        {
            bool added = false;
            var categoryNames = Name.Split(':');
            string path = "";
            List<Category> root = Categories;
            foreach (var category in categoryNames)
            {
                path += ":" + category;
                Category PushInto = root.Find(u => u.Name == category);
                if (PushInto == null)
                {
                    var willbepushed = new Category(category, path.Substring(1));
                    PushInto = willbepushed;
                    root.Add(willbepushed);
                    added = true;
                    Console.WriteLine(category + " added to tree successfully.");
                }
                root = PushInto.SubCategories;
            }
            if (!added && printIfExist) Console.WriteLine("This category already exist.");
        }
        public bool DeleteCategoryByName(string Name)
        {
            Queue<List<Category>> q = new Queue<List<Category>>();
            if (Categories != null) q.Enqueue(Categories);
            while (q.Count != 0)
            {
                List<Category> currentCategory = q.Dequeue();
                foreach (var category in currentCategory)
                {
                    if (category.Name == Name)
                    {
                        foreach (var subCategory in category.SubCategories)
                        {
                            currentCategory.Add(subCategory);
                            subCategory.UpdatePathAndDepth(Name);
                        }
                        currentCategory.Remove(category);
                        return true;
                    }
                    else q.Enqueue(category.SubCategories);
                }
            }
            return false;
        }
        public Category FindCategoryByName(string Name)
        {
            return FindCategoryByName(Name, this.Categories);
        }
        private Category FindCategoryByName(string Name, List<Category> Categories)
        {
            if (Categories.Find(u => u.Name == Name) != null) return Categories.Find(u => u.Name == Name);
            else
            {
                foreach (var category in Categories)
                {
                    var willBeReturned = FindCategoryByName(Name, category.SubCategories);
                    if (willBeReturned != null) return willBeReturned;
                }
            }
            return null;
        }
        public bool FindUserFromAllCategories(string name)
        {
            bool founded = false;
            foreach (var category in this.Categories)
            {
                if (category.SearchAndWriteUser(name)) founded = true;
            }
            return founded;
        }
        public bool FindUserReservationsFromAllCategories(string name)
        {
            bool founded = false;
            foreach (var category in this.Categories)
            {
                if (category.SearchAndWriteReservations(name)) founded = true;
            }
            return founded;
        }
        public bool PrintFromPlaceName(string placeName)
        {
            bool printed = false;
            foreach (var category in this.Categories)
            {
                if (category.PrintPlaceName(placeName)) printed = true;
            }
            return printed;
        }
        public bool DeleteUserFromAllCategories(string Name)
        {
            bool isDeleted = false;
            foreach (var category in this.Categories)
            {
                if (category.deleteUser(Name)) isDeleted = true;
            }
            return isDeleted;
        }
    }
}