using UnityEditor;

namespace InventoryTest
{
    public static class Helpers
    {
        public static ResourceDefinition GetResourceDefinition1()
        {
            return AssetDatabase.LoadAssetAtPath<ResourceDefinition>("Assets/EditModeTests/Inventories/unit_test_resource1.asset");
        }
        public static ResourceDefinition GetResourceDefinition2()
        {
            return AssetDatabase.LoadAssetAtPath<ResourceDefinition>("Assets/EditModeTests/Inventories/unit_test_resource2.asset");
        }
        public static ResourceDefinition GetResourceDefinition3()
        {
            return AssetDatabase.LoadAssetAtPath<ResourceDefinition>("Assets/EditModeTests/Inventories/unit_test_resource3.asset");
        }
        
        public static RecipeDefinition GetRecipeDefinition1()
        {
            return AssetDatabase.LoadAssetAtPath<RecipeDefinition>("Assets/EditModeTests/Inventories/unit_test_recipe1.asset");
        }
        
        public static RecipeDefinition GetRecipeDefinition2()
        {
            return AssetDatabase.LoadAssetAtPath<RecipeDefinition>("Assets/EditModeTests/Inventories/unit_test_recipe2.asset");
        }
    }
}