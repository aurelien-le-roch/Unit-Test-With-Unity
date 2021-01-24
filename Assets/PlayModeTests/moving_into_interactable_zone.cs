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
        private OreNodeInteractable _oreNodeInteractable;
        
        [UnitySetUp]
        public IEnumerator init()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            player = Helpers.GetPlayer();
            _oreNodeInteractable = Helpers.GetOreNode().OreNodeInteractable;
            
        }
        [UnityTest]
        public IEnumerator in_zone_flag_true_when_player_enter()
        {
            Assert.IsFalse(_oreNodeInteractable.PlayerInZone);

            player.transform.position += Vector3.right;
            yield return new WaitForFixedUpdate();

            Assert.IsTrue(_oreNodeInteractable.PlayerInZone);
        }

        [UnityTest]
        public IEnumerator interact_percent_equal_0_when_player_enter()
        {
            Assert.AreEqual(0, _oreNodeInteractable.InteractPercent);

            player.transform.position += Vector3.right;
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(0, _oreNodeInteractable.InteractPercent);
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