using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace ResourceDefinitionTest
{
    public class resource_definition 
    {
        [Test]
        public void AddToInventories_method_call_is_calling_ResourceInventory_Add_method()
        {
            var resourceDefinition = AssetDatabase.LoadAssetAtPath<ResourceDefinition>("Assets/Scripts/ScriptableObject/unit_test_resource1.asset");

            var subHaveInventories = Substitute.For<IHaveInventories>();
            
            
            resourceDefinition.AddToInventories(subHaveInventories);
            
            subHaveInventories.ResourceInventory.Received().Add(resourceDefinition,resourceDefinition.AmountToAddInTheResourceInventory);
            
            
        }
    }
}

