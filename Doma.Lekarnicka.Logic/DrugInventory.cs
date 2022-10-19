namespace Doma.Lekarnicka.Logic;

public class DrugInventory
{
    private List<Drug> drugList;

    public DrugInventory()
    {
        drugList = new ();

    }

    public IReadOnlyList<Drug> DrugList { get { return drugList; } }

    public void AddDrug(Drug drugItem)
    {
        drugList.Add(drugItem);
    }

     public void Remove (string drugToRemove)
     {
        Drug drugItem = drugList.Find(d => d.Name.Equals(drugToRemove,StringComparison.InvariantCultureIgnoreCase));
        if (drugItem != null)
        {
            drugList.Remove(drugItem);
            Console.WriteLine("The drug has been deleted.");
        }
        else
        {
            Console.WriteLine("The drug was not found in list of medication.");
        }
    }
     
}
