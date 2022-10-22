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


    public void Run()
    {
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
        Console.WriteLine("List of drugs:" + "\nItem name".PadRight(15,' ') + "Expiration");
        foreach (HomeFirstAidKitItem item in homeFirstAidKitInventory.HomeFirstAidKitList)
        {
            if (item is Drug drug)
            {
                Console.WriteLine(item.ToString());
            }
        }

        Console.WriteLine("\nList of medical supplies: \nItem name");
        foreach (HomeFirstAidKitItem item in homeFirstAidKitInventory.HomeFirstAidKitList)
        {
            if (item is MedicalSupply medicalSupplies)
            {
                Console.WriteLine(item.ToString());
            }
        }

        if (homeFirstAidKitInventory.HomeFirstAidKitList.Count == 0)
        {
            Console.WriteLine("The list is empty.");
        }
    }

    private void AddNewItem(HomeFirstAidKitItemType userChoiceItemType)
    {
        string itemName = GetNonEmptyStringFromUser("Write the name of the item.");
        bool addItemToList = true;

        if (itemName != null)
        {
            if (homeFirstAidKitInventory.DoesExistItemWithName(itemName))
            {
                Console.WriteLine($"The list already contains this item {itemName}. Do you want to add it anyway?\n1 - Yes\n2 - No.");
                string userChoiceAddExistingItem = Console.ReadLine();
                if (userChoiceAddExistingItem != "1")
                {
                    addItemToList = false;
                }
            }

            if (addItemToList)
            {
                if (userChoiceItemType == HomeFirstAidKitItemType.Drug)
                {
                    DateOnly? itemExpiration = GetNonEmptyAndCorrectDateFromUser("Write the expiration of the item in format D.M.YYYY or D/M/YYYY.");
                    if (itemExpiration != null)
                    {
                        homeFirstAidKitInventory.AddItem(new Drug(itemName, itemExpiration.Value));
                    }
                }
                else if (userChoiceItemType == HomeFirstAidKitItemType.MedicalSupply)
                {
                    homeFirstAidKitInventory.AddItem(new MedicalSupply(itemName));
                }
            }
        }
    }


    private void RemoveItem()
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

        while (!DateOnly.TryParseExact(dateFromUser, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out date))
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

    private void WriteOnConsoleOptionToChoose(string number, string choice)
    {
        Console.WriteLine(number.PadRight(3, ' ') + choice);
    }
}
