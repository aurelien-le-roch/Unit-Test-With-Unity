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
        public IEnumerator PlayerStartToFocusMe_method_get_call()
        {
            yield return LoadInteractablesTestsScene();
            Player player = GetPlayer();
            OreNode oreNode = GetOreNode();
            
            var interactableWithFocusHandling = Substitute.For<IInteractableWithFocusHandling>();

            oreNode.Interaclable = interactableWithFocusHandling;
            oreNode.HandlePlayerInteractableFocus = interactableWithFocusHandling;
            interactableWithFocusHandling.DidNotReceive().PlayerStartToFocusMe();
            player.transform.position += Vector3.right;
            yield return new WaitForFixedUpdate();
            interactableWithFocusHandling.Received().PlayerStartToFocusMe();
            yield return new WaitForFixedUpdate();
            interactableWithFocusHandling.ClearReceivedCalls();
            player.transform.position -= Vector3.right;
            yield return new WaitForFixedUpdate();
            interactableWithFocusHandling.DidNotReceive().PlayerStartToFocusMe();
        }
        
        [UnityTest]
        public IEnumerator PlayerExitZone_method_get_call()
        {
            yield return LoadInteractablesTestsScene();
            Player player = GetPlayer();
            OreNode oreNode = GetOreNode();
            
            var interactableWithFocusHandling = Substitute.For<IInteractableWithFocusHandling>();

            oreNode.Interaclable = interactableWithFocusHandling;
            oreNode.HandlePlayerInteractableFocus = interactableWithFocusHandling;
            interactableWithFocusHandling.DidNotReceive().PlayerStopToFocusMe();
            player.transform.position += Vector3.right;
            yield return new WaitForFixedUpdate();
            interactableWithFocusHandling.DidNotReceive().PlayerStopToFocusMe();
            player.transform.position -= Vector3.right;
            yield return new WaitForFixedUpdate();
            interactableWithFocusHandling.Received().PlayerStopToFocusMe();
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
}