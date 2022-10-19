using Doma.Lekarnicka.Logic;
using System.Reflection.Metadata.Ecma335;

namespace Doma.Lekarnicka;

public class DomaciLekarnickaConsole
{
    private DrugInventory drugInventory;

    public DomaciLekarnickaConsole()
    {
        drugInventory = new();
    }

    Dictionary<string, string> userOptions = new()
    {
        {"1", "View drug list" },
        {"2", "Add new drug"},
        {"3", "Remove a drug" },
        {"4", "End" }
    };


    public void WriteOnConsoleOptionToChoose(string number, string choice)
    {
        Console.WriteLine(number.PadRight(3, ' ') + choice);
        Action myAction = ViewOptionsTable;
    }


    public void ViewOptionsTable()
    {
        Console.WriteLine("");
        Console.WriteLine("Choose one of the following options:");

        foreach (KeyValuePair<string, string> option in userOptions)
        {
            WriteOnConsoleOptionToChoose(option.Key, option.Value);
        }

    }

    public void ViewAllDrugs(DrugInventory drugs)
    {
        foreach (Drug drug in drugs.DrugList)
        {
            Console.WriteLine(drug.Name);
        }
        if (drugs.DrugList.Count == 0)
        {
            Console.WriteLine("The drug list is empty.");
        }
    }

    public string GetNonEmptyStringFromUser()
    {
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


    public void Run()
    {
        Console.WriteLine("List of medication");
        ViewOptionsTable();
        string choice = Console.ReadLine();

        while (choice != "4")
        {
            if (choice == "1")
            {
                ViewAllDrugs(drugInventory);
            }
            else if (choice == "2")
            {
                Console.WriteLine("Write the name of the drug");
                string drugName = GetNonEmptyStringFromUser();
                if (drugName != null)
                {
                    drugInventory.AddDrug(new(drugName));
                    Console.WriteLine("The drug was added.");
                }
            }
            else if (choice == "3")
            {
                Console.WriteLine("Write the name of the drug");
                string drugNameForRemove = GetNonEmptyStringFromUser();
                if (drugNameForRemove != null)
                {
                    drugInventory.Remove(drugNameForRemove);
                }
            }
            else
            {
                Console.WriteLine("There is no such option");
            }

            ViewOptionsTable();
            choice = Console.ReadLine();
        }
    }
}
