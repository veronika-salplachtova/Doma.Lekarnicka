namespace Doma.Lekarnicka.Logic.Tests
{
    public class DrugTests
    {
        [Fact]
        public void DrugIsCreatedWithProperName()
        {
            Drug drug = new("Paralen");
            Assert.Equal("Paralen", drug.Name);
        }

        [Fact]
        public void DrugIsAddToDrugList()
        {
            DrugInventory drugs = new();
            Drug drug = new("Paralen");
            drugs.AddDrug(drug);
            Assert.Contains(drug, drugs.DrugList);
        }


     }
}