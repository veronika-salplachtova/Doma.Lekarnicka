namespace Doma.Lekarnicka.Logic.Tests
{
    public class HomeFirstAidKitInventoryTests
    {
        [Fact]
        public void DrugIsAddToDrugList()
        {
            HomeFirstAidKitInventory listOfItem = new();
            listOfItem.StartRead();
            Drug drug = new("ParalenAdd", 100, "tbl", 2, new(2001, 1, 1));
            listOfItem.AddItem(drug);
            Assert.Contains(drug, listOfItem.HomeFirstAidKitList);
            listOfItem.Remove("ParalenAdd");
        }

        [Fact]
        public void RemovingDrugByExistingNameInInventory_RemovesDrugWithTheSameName()
        {
            HomeFirstAidKitInventory listOfItem = new();
			listOfItem.StartRead();
			Drug drug = new("TestParalen", 100, "tbl", 2, new(2001, 1, 1));
            listOfItem.AddItem(drug);
            listOfItem.Remove("TestParalen");
            Assert.DoesNotContain(drug, listOfItem.HomeFirstAidKitList);
        }

        [Fact]
        public void DrugNameWithLowerCase_IsFoundInTheInventoryWhenContainsSameNameWithUpperCase()
        {   HomeFirstAidKitInventory listOfItem = new();
			listOfItem.StartRead();
			Drug drug = new("ParalenAdd", 100, "tbl", 2, new(2001, 1, 1));
            listOfItem.AddItem(drug);
            Assert.True(listOfItem.DoesExistItemWithName("paralenAdd"));
			listOfItem.Remove("ParalenAdd");
		}

        [Fact]
        public void NonExistingDrugNameInInventory_ReturnsFalseForExistsCheck()
        {
            HomeFirstAidKitInventory listOfItem = new();
			listOfItem.StartRead();
			Drug drug = new("ParalenAdd", 100, "tbl", 2, new(2001, 1, 1));
            listOfItem.AddItem(drug);
            Assert.False(listOfItem.DoesExistItemWithName("Paral"));
			listOfItem.Remove("ParalenAdd");
		}
    }
}