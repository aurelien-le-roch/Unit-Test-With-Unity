using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace LootableTest
{
    public class lootable_by_player
    {
        [UnityTest]
        public IEnumerator when_player_enter_lootable_AddToInventories_method_is_call()
        {
            yield return LoadInteractablesTestsScene();
            var playerWithSubIHaveInventories = GetPlayer();

            var lootablesInScene = GameObject.FindObjectsOfType<Lootable>();

            foreach (var lootable in lootablesInScene)
            {
                var sub = Substitute.For<ICanBeAddedToInventories>();
                lootable.CanBeAddedToInventories = sub;
            }

            foreach (var lootable in lootablesInScene)
            {
                lootable.transform.position = playerWithSubIHaveInventories.transform.position;
            }

            yield return new WaitForFixedUpdate();

            foreach (var lootable in lootablesInScene)
            {
                lootable.CanBeAddedToInventories.Received().AddToInventories(playerWithSubIHaveInventories);
            }
        }
        
        [UnityTest]
        public IEnumerator lootable_are_destroy_after_looted()
        {
            yield return LoadInteractablesTestsScene();
            var playerWithSubIHaveInventories = GetPlayer();

            var lootablesInScene = GameObject.FindObjectsOfType<Lootable>();

            var lootablesGameobjects = new List<GameObject>();
            
        
            foreach (var lootable in lootablesInScene)
            {
                lootablesGameobjects.Add(lootable.gameObject);
                lootable.transform.position = playerWithSubIHaveInventories.transform.position;
            }

            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();

            foreach (var lootable in lootablesGameobjects)
            {
                Assert.IsTrue(lootable==null);
            }
        }

        [UnityTest]
        public IEnumerator
            when_player_enter_lootable_that_are_not_setup_correctly_in_editor_AddToInventories_method_is_not_call()
        {
            yield return LoadInteractablesTestsScene();
            var playerWithSubIHaveInventories = GetPlayer();

            var prefab = AssetDatabase.LoadAssetAtPath<Lootable>
                ("Assets/PlayModeTests/lootableNotSetupCorrectlyInEditorForTest.prefab");
            var lootableNotSetupCorrectlyInEditor =
                GameObject.Instantiate(prefab, Vector3.one * 100, Quaternion.identity);

            var sub = Substitute.For<ICanBeAddedToInventories>();
            lootableNotSetupCorrectlyInEditor.CanBeAddedToInventories = sub;

            lootableNotSetupCorrectlyInEditor.transform.position = playerWithSubIHaveInventories.transform.position;

            yield return new WaitForFixedUpdate();
            
            lootableNotSetupCorrectlyInEditor.CanBeAddedToInventories.DidNotReceive()
                .AddToInventories(playerWithSubIHaveInventories);
        }


        private IEnumerator LoadInteractablesTestsScene()
        {
            var operation = SceneManager.LoadSceneAsync("LootablesTestsScene");
            while (operation.isDone == false)
                yield return null;
        }

        private Player GetPlayer()
        {
            var player = GameObject.FindObjectOfType<Player>();

            var IhaveInventories = Substitute.For<IHaveInventories>();

            player.RecipeInventory.Returns(IhaveInventories.RecipeInventory);
            player.ResourceInventory.Returns(IhaveInventories.ResourceInventory);
            return player;
        }
    }
}