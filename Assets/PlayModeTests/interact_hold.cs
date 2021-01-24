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
        private OreNodeInteractable _oreNodeInteractable;
        
        [UnitySetUp]
        public IEnumerator init()
        {
            yield return Helpers.LoadInteractablesTestsScene();
            player = Helpers.GetPlayer();
            _oreNodeInteractable = Helpers.GetOreNode().OreNodeInteractable;
            var testPlayerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = testPlayerInput;
            player.transform.position += Vector3.right;

            yield return new WaitForFixedUpdate();
            testPlayerInput.InteractHold.Returns(true);
        }
        
        [UnityTest]
        public IEnumerator if_percent_is_at_1_it_stay_at_1()
        {
            _oreNodeInteractable.InteractPercent = 0.9f;
            yield return new WaitForSeconds(0.1f);
            Assert.GreaterOrEqual(_oreNodeInteractable.InteractPercent, 1);
        }

        [UnityTest]
        public IEnumerator if_percent_is_not_at_1_it_increase_overtime()
        {
            var startInteractPercent = _oreNodeInteractable.InteractPercent;
            yield return new WaitForSeconds(0.1f);
            Assert.Greater(_oreNodeInteractable.InteractPercent, startInteractPercent);
        }


        [UnityTest]
        public IEnumerator if_percent_increase_to_1_AlreadyHit100Percent_flag_is_set_to_true()
        {
            Assert.IsFalse(_oreNodeInteractable.AlreadyHit100Percent);
            _oreNodeInteractable.InteractPercent = 0.9f;
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(_oreNodeInteractable.AlreadyHit100Percent);
        }
        
        [UnityTest]
        public IEnumerator interact_panel_fillAmount_increase_with_InteractPercent()
        {
            var interactCanvas = GameObject.FindObjectOfType<UiInteractablePercentCanvas>();
            var interactPanel = interactCanvas.UiInteraclablePercenPanel;
            
            
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(_oreNodeInteractable.InteractPercent,interactPanel.FillAmount);
        }
    }
}