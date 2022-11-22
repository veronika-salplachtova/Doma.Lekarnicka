using Doma.Lekarnicka.Logic;
using System.Reflection.Metadata.Ecma335;

namespace Doma.Lekarnicka;

enum HomeFirstAidKitItemType
{
    Drug,
    MedicalSupply
}

public class DomaciLekarnickaConsole
{
    private HomeFirstAidKitInventory homeFirstAidKitInventory;

    private Dictionary<string, string> userOptions = new()
    {
        {"1", "View drug list" },
        {"2", "Add new drug"},
        {"3", "Add new medical supplies"},
        {"4", "Remove a drug" },
        {"5", "End" }
    };

    public DomaciLekarnickaConsole()
    {
        homeFirstAidKitInventory = new();
    }

    private bool StartRead()
    {
        try
        {
            homeFirstAidKitInventory.StartRead();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to load data: " + ex.Message);
            return false;
        }
    }

    public void Run()
    {
        if (!StartRead())
            return;

        Console.WriteLine("Home first aid kit");
        ViewOptionsTable();
        string choice = Console.ReadLine();

        while (choice != "5")
        {
            switch (choice)
            {
                case "1":
                    ViewAllItems();
                    break;

                case "2":
                    AddNewItem(HomeFirstAidKitItemType.Drug);
                    break;

                case "3":
                    AddNewItem(HomeFirstAidKitItemType.MedicalSupply);
                    break;

                case "4":
                    RemoveItem();
                    break;

                default:
                    Console.WriteLine("There is no such option");
                    break;
            }
            ViewOptionsTable();
            choice = Console.ReadLine();
        }
    }

    private void ViewOptionsTable()
    {
        Console.WriteLine("");
        Console.WriteLine("Choose one of the following options:");

        foreach (KeyValuePair<string, string> option in userOptions)
        {
            WriteOnConsoleOptionToChoose(option.Key, option.Value);
        }
    }

    private void ViewAllItems()
    {
        try
        {
            if (homeFirstAidKitInventory.HomeFirstAidKitList.Count == 0)
            {
                Console.WriteLine("The list is empty.");
            }
            else
            {
                Console.WriteLine("List of drugs:" + "\nItem name".PadRight(15, ' ') + "Package size" + "Units".PadRight(15, ' ') + "Quantity".PadRight(15, ' ') + "Expiration");
                foreach (HomeFirstAidKitItem item in homeFirstAidKitInventory.HomeFirstAidKitList)
                {
                    if (item is Drug)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }

                Console.WriteLine("\nList of medical supplies:" + "\nItem name".PadRight(15, ' ') + "Quantity".PadRight(15, ' '));
                foreach (HomeFirstAidKitItem item in homeFirstAidKitInventory.HomeFirstAidKitList)
                {
                    if (item is MedicalSupply)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to load data. Error: " + ex.Message);
        }  
    }
    
    private void AddNewItem(HomeFirstAidKitItemType userChoiceItemType)
    {
        try
        {
            string itemName = GetNonEmptyStringFromUser("Write the name of the item.");

            if (itemName == null)
            {
                return;
            }

            if (homeFirstAidKitInventory.DoesExistItemWithName(itemName))
            {
                Console.WriteLine($"The list already contains this item {itemName}. Do you want to add it anyway?\n1 - Yes\n2 - No.");
                string userChoiceAddExistingItem = Console.ReadLine();
                if (userChoiceAddExistingItem != "1")
                {
                    return;
                }
            }

            int? quantity = GetNonEmptyAndCorrectInt("Write the quantity of item.");

            if (quantity == null)
            {
                return;
            }

            switch (userChoiceItemType)
            {
                case HomeFirstAidKitItemType.Drug:
                    DateOnly? itemExpiration = GetNonEmptyAndCorrectDateFromUser("Write the expiration of the item in format D.M.YYYY.");
                    if (itemExpiration == null)
                    {
                        return;
                    }

                    Console.WriteLine("Do you want to enter the package size?\nYes - 1\nNo - Whatever else.");

                    bool userWantsToEnterPackageSize = Console.ReadLine() == "1";
                    int? packageSize;
                    string units;

                    if (userWantsToEnterPackageSize)
                    {
                        packageSize = GetNonEmptyAndCorrectInt("Write the package size.");

                        if (packageSize == null)
                        {
                            return;
                        }

                        units = GetNonEmptyStringFromUser("Write a units of item (pcs, tbl, g, ml)");

                        if (units == null)
                        {
                            return;
                        }

                        homeFirstAidKitInventory.AddItem(new Drug(itemName, packageSize, units, quantity.Value, itemExpiration.Value));
                    }
                    else
                    {
                        packageSize = null;
                        units = null;
                        homeFirstAidKitInventory.AddItem(new Drug(itemName, packageSize, units, quantity.Value, itemExpiration.Value));
                    }
                    break;

                case HomeFirstAidKitItemType.MedicalSupply:
                    homeFirstAidKitInventory.AddItem(new MedicalSupply(itemName, quantity.Value));
                    break;
            };
            Console.WriteLine($"The item {itemName} has been added.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to add item. Error: " + ex.Message);
        }
    }

    private void RemoveItem()
    {
        try
        {
            string itemNameForRemove = GetNonEmptyStringFromUser("Write the name of the drug or the medical supplies.");
            if (itemNameForRemove != null)
            {
                if (homeFirstAidKitInventory.DoesExistItemWithName(itemNameForRemove))
                {
                    homeFirstAidKitInventory.Remove(itemNameForRemove);
                    Console.WriteLine($"{itemNameForRemove} has been removed from the inventory.");
                }
                else
                {
                    Console.WriteLine($"{itemNameForRemove} couldn't be deleted because doesn't exist in the inventory.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to remove item. Error: " + ex.Message);
        }
    }

    private string GetNonEmptyStringFromUser(string textForUser)
    {
        Console.WriteLine(textForUser);
        string stringForVerification = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(stringForVerification))
        {
            Console.WriteLine("The data is not entered correctly. Please, enter again or choose another option(*)");
            stringForVerification = Console.ReadLine();
            if (stringForVerification == "*")
            {
                return null;
            }
        }
        return stringForVerification;
    }

    private DateOnly? GetNonEmptyAndCorrectDateFromUser(string textForUser)
    {
        Console.WriteLine(textForUser);
        string dateFromUser = Console.ReadLine();
        DateOnly date;

        while (!DateOnly.TryParseExact(dateFromUser, "d.M.yyyy", null, System.Globalization.DateTimeStyles.None, out date))
        {
            Console.WriteLine("Invalid date, please retry or choose another option(*)");
            dateFromUser = Console.ReadLine();
            if (dateFromUser == "*")
            {
                return null;
            }
        }
        return date;
    }

    private int? GetNonEmptyAndCorrectInt(string textForUser)
    {
        Console.WriteLine(textForUser);
        string intFromUser = Console.ReadLine();

        int number;

        while (!int.TryParse(intFromUser, out number) || number <= 0)
        {
            Console.WriteLine("Invalid number, please retry or choose another option(*)");
            intFromUser = Console.ReadLine();
            if (intFromUser == "*")
            {
                return null;
            }
        }
        return number;
    }

    private void WriteOnConsoleOptionToChoose(string number, string choice)
    {
        Console.WriteLine(number.PadRight(3, ' ') + choice);
    }

}
