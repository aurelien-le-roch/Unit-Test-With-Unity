using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
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
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(oreNode.PlayerInZone);
        }

        [UnityTest]
        public IEnumerator interact_percent_equal_0_when_player_enter()
        {
            Assert.AreEqual(0, oreNode.InteractPercent);

            player.transform.position += Vector3.right;
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(0, oreNode.InteractPercent);
        }
    }

    public class moving_out_interaclable_zone
    {
        [UnityTest]
        public IEnumerator in_zone_flag_false_after_player_exit()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            var player = GameObject.FindObjectOfType<Player>();
            var oreNode = GameObject.FindObjectOfType<OreNode>();

            player.transform.position += Vector3.right;
            yield return new WaitForSeconds(0.1f);
            Assert.IsTrue(oreNode.PlayerInZone);

            player.transform.position -= Vector3.right;
            yield return new WaitForSeconds(0.1f);

            Assert.IsFalse(oreNode.PlayerInZone);
        }

        [UnityTest]
        public IEnumerator interact_percent_is_equal_to_zero_after_player_exit()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            var player = GameObject.FindObjectOfType<Player>();
            var oreNode = GameObject.FindObjectOfType<OreNode>();

            player.transform.position += Vector3.right;
            yield return new WaitForSeconds(0.1f);

            player.transform.position -= Vector3.right;
            yield return new WaitForSeconds(0.1f);

            Assert.LessOrEqual(0, oreNode.InteractPercent);
        }
    }
}