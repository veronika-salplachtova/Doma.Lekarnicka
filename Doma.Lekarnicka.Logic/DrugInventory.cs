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

    public void Remove(string drugName)
    {
        Drug drugItem = drugList.Find(d => d.Name.Equals(drugName, StringComparison.OrdinalIgnoreCase));
        Remove(drugItem);              
    }

    public void Remove(Drug drugItem)
    {
        drugList.Remove(drugItem);
    }

}
