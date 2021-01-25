using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace interaclableTest
{
    public class player_call_the_right_interactable_methodes_when_moving_in_out_of_trigger
    {
        [UnityTest]
        public IEnumerator PlayerEnterZone_method_get_call()
        {
            yield return LoadInteractablesTestsScene();
            Player player = GetPlayer();
            OreNode oreNode = GetOreNode();
            
            var interactableWithZone = Substitute.For<IInteractableWithZone>();

            oreNode.Interaclable = interactableWithZone;
            oreNode.HandlePlayerInZone = interactableWithZone;
            interactableWithZone.DidNotReceive().PlayerEnterZone();
            player.transform.position += Vector3.right;
            yield return new WaitForFixedUpdate();
            interactableWithZone.Received().PlayerEnterZone();
            yield return new WaitForFixedUpdate();
            interactableWithZone.ClearReceivedCalls();
            player.transform.position -= Vector3.right;
            yield return new WaitForFixedUpdate();
            interactableWithZone.DidNotReceive().PlayerEnterZone();
        }
        
        [UnityTest]
        public IEnumerator PlayerExitZone_method_get_call()
        {
            yield return LoadInteractablesTestsScene();
            Player player = GetPlayer();
            OreNode oreNode = GetOreNode();
            
            var interactableWithZone = Substitute.For<IInteractableWithZone>();

            oreNode.Interaclable = interactableWithZone;
            oreNode.HandlePlayerInZone = interactableWithZone;
            interactableWithZone.DidNotReceive().PlayerExitZone();
            player.transform.position += Vector3.right;
            yield return new WaitForFixedUpdate();
            interactableWithZone.DidNotReceive().PlayerExitZone();
            player.transform.position -= Vector3.right;
            yield return new WaitForFixedUpdate();
            interactableWithZone.Received().PlayerExitZone();
        }
        
        [UnityTest]
        public IEnumerator handleInteractable_tick_method_get_call()
        {
            yield return LoadInteractablesTestsScene();
            Player player = GetPlayer();

            var playerHandleInteractable = Substitute.For<IPlayerHandleInteractable>();
            player.HandleInteractable = playerHandleInteractable;
            yield return null;
            player.HandleInteractable.Received().Tick(Time.deltaTime);
            yield return null;
            player.HandleInteractable.Received().Tick(Time.deltaTime);
        }
        
        private  IEnumerator LoadInteractablesTestsScene()
        {
            var operation = SceneManager.LoadSceneAsync("InteraclablesTestsScene");
            while (operation.isDone == false)
                yield return null;
        }

        private  Player GetPlayer()
        {
            return GameObject.FindObjectOfType<Player>();
        }

        private  OreNode GetOreNode()
        {
            return GameObject.FindObjectOfType<OreNode>();
        }
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
        
//    [UnityTest]
//    public IEnumerator interact_panel_fillAmount_increase_with_InteractPercent()
//    {
//    var interactCanvas = GameObject.FindObjectOfType<UiInteractablePercentCanvas>();
//    var interactPanel = interactCanvas.UiInteraclablePercenPanel;
//            
//            
//    yield return new WaitForSeconds(0.1f);
//
//    Assert.AreEqual(_oreNodeInteractable.InteractPercent,interactPanel.FillAmount);
//    }
    
}