namespace Doma.Lekarnicka.Logic.Tests
{
    public class HomeFirstAidKitInventoryTests
    {
        [Fact]
        public void DrugIsAddToDrugList()
        {
            HomeFirstAidKitInventory listOfItem = new();
            Drug drug = new("Paralen", new(2001, 1, 1));
            listOfItem.AddItem(drug);
            Assert.Contains(drug, listOfItem.HomeFirstAidKitList);
        }

        [Fact]
        public void RemovingDrugByExistingNameInInventory_RemovesDrugWithTheSameName()
        {
            HomeFirstAidKitInventory inventory = new();
            Drug drug = new("Paralen", new(2001, 1, 1));
            inventory.AddItem(drug);
            inventory.Remove("Paralen");
            Assert.DoesNotContain(drug, inventory.HomeFirstAidKitList);
        }

        [Fact]
        public void DrugNameWithLowerCase_IsFoundInTheInventoryWhenContainsSameNameWithUpperCase()
        {   HomeFirstAidKitInventory listOfItem = new();
            Drug drug = new("Paralen", new(2001, 1, 1));
            listOfItem.AddItem(drug);
            Assert.True(listOfItem.DoesExistItemWithName("paralen"));
        }

        [Fact]
        public void NonExistingDrugNameInInventory_ReturnsFalseForExistsCheck()
        {
            HomeFirstAidKitInventory inventory = new();
            Drug drug = new("Paralen", new(2001, 1, 1));
            inventory.AddItem(drug);
            Assert.False(inventory.DoesExistItemWithName("Paral"));
        }
    }
}