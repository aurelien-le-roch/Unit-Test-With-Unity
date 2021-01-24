using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace interaclableTest
{
    public class moving_out_interaclable_zone
    {
        [UnityTest]
        public IEnumerator in_zone_flag_false_after_player_exit()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            var player = GameObject.FindObjectOfType<Player>();
            var oreNode = GameObject.FindObjectOfType<OreNode>().OreNodeInteractable;

            player.transform.position += Vector3.right;
            yield return new WaitForFixedUpdate();
            Assert.IsTrue(oreNode.PlayerInZone);

            player.transform.position -= Vector3.right;
            yield return new WaitForFixedUpdate();

            Assert.IsFalse(oreNode.PlayerInZone);
        }

        [UnityTest]
        public IEnumerator interact_percent_is_equal_to_zero_after_player_exit()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            var player = GameObject.FindObjectOfType<Player>();
            var oreNode = GameObject.FindObjectOfType<OreNode>().OreNodeInteractable;

            player.transform.position += Vector3.right;
            yield return new WaitForFixedUpdate();

            player.transform.position -= Vector3.right;
            yield return new WaitForFixedUpdate();

            Assert.LessOrEqual(0, oreNode.InteractPercent);
        }
    }
}