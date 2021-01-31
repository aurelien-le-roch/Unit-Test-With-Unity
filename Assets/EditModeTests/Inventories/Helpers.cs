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
        
        public static RecipeDefinition GetRecipeDefinition1ResourceResult()
        {
            return AssetDatabase.LoadAssetAtPath<RecipeDefinition>("Assets/EditModeTests/Inventories/unit_test_recipe1_result_is_resource.asset");
        }
        
        public static RecipeDefinition GetRecipeDefinition2RecipeResult()
        {
            return AssetDatabase.LoadAssetAtPath<RecipeDefinition>("Assets/EditModeTests/Inventories/unit_test_recipe2_result_is_recipe.asset");
        }
        
        public static RecipeDefinition GetRecipeDefinition3WrongResult()
        {
            return AssetDatabase.LoadAssetAtPath<RecipeDefinition>("Assets/EditModeTests/Inventories/unit_test_recipe3_result_is_not_setup_correctly.asset");
        }
    }
}