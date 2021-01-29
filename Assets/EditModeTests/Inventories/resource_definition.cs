using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace InventoryTest
{
    public class resource_definition 
    {
        [Test]
        public void AddToInventories_method_call_is_calling_ResourceInventory_Add_method()
        {
            var resourceDefinition = Helpers.GetResourceDefinition1();

            var subHaveInventories = Substitute.For<IHaveInventories>();
            
            
            resourceDefinition.AddToInventories(subHaveInventories);
            
            subHaveInventories.ResourceInventory.Received().Add(resourceDefinition,resourceDefinition.AmountToAddInTheResourceInventory);
            
            
        }
    }
}

