using NSubstitute;
using NUnit.Framework;

namespace InventoryTest
{
    public class resource_definition 
    {
        [Test]
        public void AddToInventories_method_call_is_calling_ResourceInventory_Add_method()
        {
            var resourceDefinition = Helpers.GetResourceDefinition1();

            var subHaveInventories = Substitute.For<IHaveInventories>();
            
            
            resourceDefinition.AddToInventory(subHaveInventories,1);
            
            subHaveInventories.ResourceInventory.Received().Add(resourceDefinition,1);
        }
    }
}

