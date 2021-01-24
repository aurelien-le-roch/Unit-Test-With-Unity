using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.TestTools;

namespace interaclableTest
{
    public class moving_into_interactable_zone
    {
        private Player player;
        private OreNode oreNode;
        
        [UnitySetUp]
        public IEnumerator init()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            player = Helpers.GetPlayer();
            oreNode = Helpers.GetOreNode();
            
        }
        [UnityTest]
        public IEnumerator in_zone_flag_true_when_player_enter()
        {
            Assert.IsFalse(oreNode.PlayerInZone);

            player.transform.position += Vector3.right;
            yield return new WaitForFixedUpdate();

            Assert.IsTrue(oreNode.PlayerInZone);
        }

        [UnityTest]
        public IEnumerator interact_percent_equal_0_when_player_enter()
        {
            Assert.AreEqual(0, oreNode.InteractPercent);

            player.transform.position += Vector3.right;
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(0, oreNode.InteractPercent);
        }
        
        [UnityTest]
        public IEnumerator interact_panel_get_enable()
        {
            var interactCanvas = GameObject.FindObjectOfType<UiInteractablePercentCanvas>();
            var interactPanel = interactCanvas.UiInteraclablePercenPanel;
            
            Assert.IsFalse(interactPanel.gameObject.activeSelf);

            player.transform.position += Vector3.right;
            yield return new WaitForFixedUpdate();
            Assert.IsTrue(interactPanel.gameObject.activeSelf);
        }
        
        
    }
}