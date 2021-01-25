using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.TestTools;

namespace interaclableTest
{
    public class player_moving_into_interactable_with_zone_ore_node
    {
        [UnityTest]
        public IEnumerator PlayerEnterZone_method_get_call()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            Player player = Helpers.GetPlayer();
            OreNode oreNode = Helpers.GetOreNode();
            
            var interactableWithZone = Substitute.For<IInteractableWithZone>();

            oreNode.Interaclable = interactableWithZone;
            oreNode.HandlePlayerInZone = interactableWithZone;
            interactableWithZone.DidNotReceive().PlayerEnterZone();
            player.transform.position += Vector3.right;
            yield return new WaitForFixedUpdate();

            interactableWithZone.Received().PlayerEnterZone();
        }

       
        
//        [UnityTest]
//        public IEnumerator interact_panel_get_enable()
//        {
//            var interactCanvas = GameObject.FindObjectOfType<UiInteractablePercentCanvas>();
//            var interactPanel = interactCanvas.UiInteraclablePercenPanel;
//            
//            Assert.IsFalse(interactPanel.gameObject.activeSelf);
//
//            player.transform.position += Vector3.right;
//            yield return new WaitForFixedUpdate();
//            Assert.IsTrue(interactPanel.gameObject.activeSelf);
//        }
        
        
    }
}