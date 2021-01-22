using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace interaclableTest
{
    public class interact_hold
    {
        private Player player;
        private OreNode oreNode;
        
        [UnitySetUp]
        public IEnumerator init()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            player = Helpers.GetPlayer();
            oreNode = Helpers.GetOreNode();
            var testPlayerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = testPlayerInput;
            player.transform.position += Vector3.right;

            yield return new WaitForFixedUpdate();
            testPlayerInput.InteractHold.Returns(true);
        }
        
        [UnityTest]
        public IEnumerator if_percent_is_at_1_it_stay_at_1()
        {
            oreNode.InteractPercent = 0.9f;
            yield return new WaitForSeconds(0.1f);
            Assert.GreaterOrEqual(oreNode.InteractPercent, 1);
        }

        [UnityTest]
        public IEnumerator if_percent_is_not_at_1_it_increase_overtime()
        {
            var startInteractPercent = oreNode.InteractPercent;
            yield return new WaitForSeconds(0.1f);
            Assert.Greater(oreNode.InteractPercent, startInteractPercent);
        }


        [UnityTest]
        public IEnumerator if_percent_increase_to_1_AlreadyHit100Percent_flag_is_set_to_true()
        {
            Assert.IsFalse(oreNode.AlreadyHit100Percent);
            oreNode.InteractPercent = 0.9f;
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(oreNode.AlreadyHit100Percent);
        }
        
        [UnityTest]
        public IEnumerator interact_panel_fillAmount_increase_with_InteractPercent()
        {
            var interactCanvas = GameObject.FindObjectOfType<UiInteractableCanvas>();
            var interactPanel = interactCanvas.UiInteraclablePanel;
            
            
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(oreNode.InteractPercent,interactPanel.FillAmount);
        }
    }
}