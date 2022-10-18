using Doma.Lekarnicka.Logic;

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
        {"4", "End"}
    };
 

    public void WriteOnConsoleOptionToChoose(string number, string choice)
    {
        Console.WriteLine(number.PadRight(3,' ') + choice);
        Action myAction = ViewOptionsTable;
    }


    public void ViewOptionsTable()
    {
        Console.WriteLine("Choose one of the following options:");

        foreach (KeyValuePair<string,string> option in userOptions)
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
    }

    public void Run()
    {
        Console.WriteLine("List of medication");
        ViewOptionsTable();
        string choice = Console.ReadLine();

        while (choice != "4")
        { 
            if (userOptions.ContainsKey(choice))             
            {
                if (choice == "1")
                {
                    ViewAllDrugs(drugInventory);
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Write the name of the drug");
                    drugInventory.AddDrug(new(Console.ReadLine()));

                }       
                else if (choice == "3")
                {
                    Console.WriteLine("Write the name of the drug");
                    string drugToRemove = Console.ReadLine();
                    drugInventory.Remove(drugToRemove);
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
