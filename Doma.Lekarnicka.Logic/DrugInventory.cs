namespace Doma.Lekarnicka.Logic;

public class DrugInventory
{
    private List<Drug> drugList;

    public DrugInventory()
    {
        drugList = new List<Drug>();
    }

    public void AddDrug(Drug drugItem)
    {
        drugList.Add(drugItem);
    }

    public void Remove(Drug drugItem)
    {
        drugList.Remove(drugItem);
    }

    public IReadOnlyList<Drug> DrugList {get {return drugList;} }
}
