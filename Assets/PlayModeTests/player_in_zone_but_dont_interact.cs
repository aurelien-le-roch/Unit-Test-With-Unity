using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace interaclableTest
{
    public class player_in_zone_but_dont_interact
    {
        private Player player;
        private OreNode oreNode;
        private IPlayerInput testPlayerInput;
        
        [UnitySetUp]
        public IEnumerator init()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            player = Helpers.GetPlayer();
            oreNode = Helpers.GetOreNode();
            testPlayerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = testPlayerInput;
            player.transform.position += Vector3.right;

            yield return new WaitForSeconds(0.1f);
            
        }
        
        [UnityTest]
        public IEnumerator if_percent_is_not_at_1_it_get_reduce_overtime_to_0()
        {
            
            oreNode.InteractPercent = 0.2f;
            testPlayerInput.InteractHold.Returns(false);
            yield return new WaitForSeconds(0.1f);

            Assert.Less(oreNode.InteractPercent, 0.2f);
            Assert.Greater(oreNode.InteractPercent,-0.01f);
        }

        [UnityTest]
        public IEnumerator if_percent_is_at_1_it_dont_get_reduce_overtime()
        {
            
            oreNode.InteractPercent = 0.9f;
            testPlayerInput.InteractHold.Returns(true);
            yield return new WaitForSeconds(0.1f);
            testPlayerInput.InteractHold.Returns(false);
            yield return new WaitForSeconds(0.1f);

            Assert.GreaterOrEqual(oreNode.InteractPercent ,1);
        }
    }
}