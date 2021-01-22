using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace interaclable
{
    public class moving_into_interactable_zone
    {
        [UnityTest]
        public IEnumerator in_zone_flag_true_when_player_enter()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            var player = GameObject.FindObjectOfType<Player>();
            var oreNode = GameObject.FindObjectOfType<OreNode>();

            Assert.IsFalse(oreNode.PlayerInZone);

            player.transform.position += Vector3.right;
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(oreNode.PlayerInZone);
        }

        [UnityTest]
        public IEnumerator interact_percent_equal_0_when_player_enter()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            var player = GameObject.FindObjectOfType<Player>();
            var oreNode = GameObject.FindObjectOfType<OreNode>();

            Assert.AreEqual(0, oreNode.InteractPercent);

            player.transform.position += Vector3.right;
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(0, oreNode.InteractPercent);
        }
    }

    public class interact_hold
    {
        [UnityTest]
        public IEnumerator if_percent_is_at_1_it_stay_at_1()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            var player = GameObject.FindObjectOfType<Player>();
            var oreNode = GameObject.FindObjectOfType<OreNode>();

            var testPlayerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = testPlayerInput;

            testPlayerInput.InteractHold.Returns(true);
            player.transform.position += Vector3.right;

            yield return new WaitForSeconds(0.1f);

            oreNode.InteractPercent = 0.9f;
            var holdTime = Time.time + 0.1f;
            while (Time.time < holdTime)
            {
                oreNode.InteractHold(player.gameObject);

                yield return null;
            }

            Assert.GreaterOrEqual(oreNode.InteractPercent, 1);
        }

        [UnityTest]
        public IEnumerator if_percent_is_not_at_1_it_increase_overtime()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            var player = GameObject.FindObjectOfType<Player>();
            var oreNode = GameObject.FindObjectOfType<OreNode>();


            player.transform.position += Vector3.right;
            var startInteractPercent = oreNode.InteractPercent;
            yield return new WaitForSeconds(0.1f);

            var holdTime = Time.time + 0.2f;
            while (Time.time < holdTime)
            {
                oreNode.InteractHold(player.gameObject);
                yield return null;
            }

            Assert.Greater(oreNode.InteractPercent, startInteractPercent);
        }


        [UnityTest]
        public IEnumerator if_percent_increase_to_1_AlreadyHit100Percent_flag_is_set_to_true()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            var player = GameObject.FindObjectOfType<Player>();
            var oreNode = GameObject.FindObjectOfType<OreNode>();


            player.transform.position += Vector3.right;

            yield return new WaitForSeconds(0.1f);

            Assert.IsFalse(oreNode.AlreadyHit100Percent);
            oreNode.InteractPercent = 0.9f;
            var holdTime = Time.time + 0.2f;
            while (Time.time < holdTime)
            {
                oreNode.InteractHold(player.gameObject);
                yield return null;
            }

            Assert.IsTrue(oreNode.AlreadyHit100Percent);
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

    public class player_in_zone_but_dont_interact
    {
        [UnityTest]
        public IEnumerator if_percent_is_not_at_100_it_get_reduce_overtime_to_0()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            var player = GameObject.FindObjectOfType<Player>();
            var oreNode = GameObject.FindObjectOfType<OreNode>();


            player.transform.position += Vector3.right;
            yield return new WaitForSeconds(0.1f);
            oreNode.InteractPercent = 0.2f;
            player.transform.position -= Vector3.right;
            yield return new WaitForSeconds(0.1f);

            Assert.Less(oreNode.InteractPercent, 0.2f);
        }

        [UnityTest]
        public IEnumerator if_percent_is_at_100_it_dont_get_reduce_overtime()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            var player = GameObject.FindObjectOfType<Player>();
            var oreNode = GameObject.FindObjectOfType<OreNode>();


            player.transform.position += Vector3.right;
            yield return new WaitForSeconds(0.1f);
            oreNode.InteractPercent = 1;
            oreNode.InteractHold(player.gameObject);
            yield return new WaitForSeconds(0.1f);
            player.transform.position -= Vector3.right;
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(100, oreNode.InteractPercent);
        }
    }

    public static class Helpers
    {
        public static IEnumerator LoadInteractablesTestsScene()
        {
            var operation = SceneManager.LoadSceneAsync("InteraclablesTestsScene");
            while (operation.isDone == false)
                yield return null;
        }
    }
}