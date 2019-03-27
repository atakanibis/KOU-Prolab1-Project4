using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab04
{
    class Menu
    {
        Tree Tree;
        public Menu(Tree tree)
        {
            Tree = tree;
            Console.Clear();
            MainMenu();
        }
        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("-----Main Menu-----");
            Console.WriteLine("1-Category Actions\n2-User Actions\n3-Listing Actions");
            int choice = Int32.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    CategoryMenu();
                    break;
                case 2:
                    UserMenu();
                    break;
                case 3:
                    ListingMenu();
                    break;
                default:
                    MainMenu();
                    break;

            }
        }
        private void CategoryDeletingMenu(string deletename)
        {
            if (Tree.DeleteCategoryByName(deletename)) Console.WriteLine("Category is successfully deleted.");
            else Console.WriteLine(deletename + " is doesnt exist.");
        }
        private void UserDeletingMenu()
        {
            Console.Clear();
            Console.WriteLine("-----Deleting User-----");
            Console.WriteLine("1-Delete all users of category\n2-Delete user from category\n3-Delete user from all categories.");
            int choice = Int32.Parse(Console.ReadLine());
            Console.Clear();
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter category name: ");
                    Category deleteUsers = Tree.FindCategoryByName(Console.ReadLine());
                    if (deleteUsers != null)
                    {
                        bool isAllUsersDeleted = deleteUsers.DeleteAllUsers();
                        if (isAllUsersDeleted) Console.WriteLine("Users successfully deleted.");
                        else Console.WriteLine("This category hasnt any users.");
                        deleteUsers.Users = new MaxHeap();
                    }
                    else Console.WriteLine("This category name doesnt exist.");
                    break;
                case 2:
                    Console.WriteLine("Enter category name: ");
                    Category deleteUserFrom = Tree.FindCategoryByName(Console.ReadLine());
                    if (deleteUserFrom != null)
                    {
                        Console.WriteLine("Enter user name: ");
                        bool deleted = deleteUserFrom.deleteUser(Console.ReadLine());
                        if (deleted) Console.WriteLine("User succesfully deleted.");
                        else Console.WriteLine("This user doesnt exist in this category.");
                    }
                    else Console.WriteLine("Unknown Category Name.");
                    break;
                case 3:
                    Console.WriteLine("Enter user name: ");
                    bool isDeleted = Tree.DeleteUserFromAllCategories(Console.ReadLine());
                    if (isDeleted) Console.WriteLine("User successfully deleted from all tree.");
                    else Console.WriteLine("This user doesnt exist.");
                    break;
                case 0:
                    MainMenu();
                    break;
                default:
                    UserMenu();
                    break;
            }
            Console.WriteLine("Press any key to return main menu.");
            Console.ReadKey();
            MainMenu();
        }
        private void UserMenu()
        {
            Console.Clear();
            Console.WriteLine("-----User Menu-----");
            Console.WriteLine("1-Add User\n2-Delete User\n0-Return To Main Menu");
            int choice = Int32.Parse(Console.ReadLine());
            Console.Clear();
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Category name: ");
                    Category Pushinto = Tree.FindCategoryByName(Console.ReadLine());
                    if (Pushinto == null) Console.WriteLine("This category doesnt exist.");
                    else
                    {
                        Console.WriteLine("Eklemek istediginiz kullanicinin ismini giriniz: ");
                        string addname = Console.ReadLine();
                        if (Tree.FindUserFromAllCategories(addname))
                        {
                            Console.WriteLine("This user already exist. Please try with another id.");
                        }
                        else
                        {
                            User adduser = new User(addname);
                            Pushinto.Users.InsertOrFind(adduser);
                            Console.WriteLine(adduser.ID + " is successfully added.");
                        }
                    }
                    Console.WriteLine("Press any key to return main menu.");
                    Console.ReadKey();
                    MainMenu();
                    break;
                case 2:
                    UserDeletingMenu();
                    string deletename = Console.ReadLine();
                    break;
                case 0:
                    MainMenu();
                    break;
                default:
                    UserMenu();
                    break;
            }
        }
        private void CategoryMenu()
        {
            Console.Clear();
            Console.WriteLine("-----Category Menu-----");
            Console.WriteLine("1-Add Category\n2-Delete Category\n3-Search Category\n0-Return Main Menu");
            int choice = Int32.Parse(Console.ReadLine());
            Console.Clear();
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter the category name: ");
                    Tree.CreateCategoryIfDoesntExist(Console.ReadLine(), true);
                    break;
                case 2:
                    Console.WriteLine("Enter the category name: ");
                    CategoryDeletingMenu(Console.ReadLine());
                    break;
                case 3:
                    Console.WriteLine("Enter the category name: ");
                    string search = Console.ReadLine();
                    Category returncategory = Tree.FindCategoryByName(search);
                    if (returncategory == null) Console.WriteLine("This category doesnt exist.");
                    else
                    {
                        Console.WriteLine(returncategory.Name + " is successfully found.");
                        Console.WriteLine("Name: " + returncategory.Name);
                        Console.WriteLine("Path: " + returncategory.Path);
                        Console.WriteLine("Depth: " + returncategory.Depth);
                        Console.WriteLine("User Count: " + returncategory.Users.Count());
                        Console.WriteLine("Reservation Count: " + returncategory.Users.ReservationCount());
                        Console.WriteLine("Subcategory Count: " + returncategory.SubCategories.Count());
                        Console.WriteLine(returncategory.Name + " Press 0 to add new sub category or press 1 to delete this category.");
                        int deleteoradd = Int32.Parse(Console.ReadLine());
                        if (deleteoradd == 1) CategoryDeletingMenu(search);
                        else if (deleteoradd == 0)
                        {
                            Console.WriteLine("Enter the subcategory name: ");
                            returncategory.addCategory(Console.ReadLine());
                        }
                    }
                    break;
                case 0:
                    MainMenu();
                    break;
                default:
                    CategoryMenu();
                    break;

            }
            Console.WriteLine("Press a key to return main menu.");
            Console.ReadKey();
            MainMenu();
        }
        private void ListingMenu()
        {
            Console.Clear();
            Console.WriteLine("-----Listing Menu-----");
            Console.WriteLine("1-List categories by user id: \n2-List all users of category:\n3-List users by placeid:\n4-List reservations by user id:\n0-Return Main Menu");
            int choice = Int32.Parse(Console.ReadLine());
            Console.Clear();
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter User Name: ");
                    bool founded = Tree.FindUserFromAllCategories(Console.ReadLine());
                    if (!founded) Console.WriteLine("This user doesnt exist.");
                    break;
                case 2:
                    Console.WriteLine("Enter Category Name: ");
                    Category Founded = Tree.FindCategoryByName(Console.ReadLine());
                    if (Founded != null) { if (!Founded.PrintAllUsers()) Console.WriteLine("This category hasn't any users."); }
                    else Console.WriteLine("This category doesnt exist.");
                    break;
                case 3:
                    Console.WriteLine("Enter the place id: ");
                    if (!Tree.PrintFromPlaceName(Console.ReadLine())) Console.WriteLine("There is no any users in this place id."); ;
                    break;
                case 4:
                    Console.WriteLine("Enter user name: ");
                    founded = Tree.FindUserReservationsFromAllCategories(Console.ReadLine());
                    if (!founded) Console.WriteLine("This user doesnt exist.");
                    break;
                case 0:
                    MainMenu();
                    return;
                default:
                    ListingMenu();
                    return;
            }
            Console.WriteLine("Press any key to return main menu.");
            Console.ReadKey();
            MainMenu();
        }
    }
}