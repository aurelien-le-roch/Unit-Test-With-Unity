using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace interaclableTest
{
    public class ui_interactable_percent 
    {
        [UnityTest]
        public IEnumerator panel_get_enable_when_interactor_enter_interactable_zone()
        {
            var player = Substitute.For<IPlayer>();
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var oreNode = GetOreNode();
            var uiInteractablePercentCanvas = oreNode.GetComponentInChildren<UiInteractablePercentCanvas>();
          
            yield return null;
            Assert.IsFalse(uiInteractablePercentCanvas.UiInteraclablePercenPanel.gameObject.activeSelf);
            playerHandleInteractable.OnTriggerEnter2D(oreNode.OreNodeInteractable);
            yield return null;
            Assert.IsTrue(uiInteractablePercentCanvas.UiInteraclablePercenPanel.gameObject.activeSelf);
        }
        
        [UnityTest]
        public IEnumerator panel_dont_get_disable_when_interactor_exit_interactable_zone()
        {
            var player = Substitute.For<IPlayer>();
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var oreNode = GetOreNode();
            var uiInteractablePercentCanvas = oreNode.GetComponentInChildren<UiInteractablePercentCanvas>();
          
            yield return null;
            playerHandleInteractable.OnTriggerEnter2D(oreNode.OreNodeInteractable);
            yield return null;
            playerHandleInteractable.OnTriggerExit2D(oreNode.OreNodeInteractable);
            yield return null;
            Assert.IsTrue(uiInteractablePercentCanvas.UiInteraclablePercenPanel.gameObject.activeSelf);
        }
        private OreNode GetOreNode()
        {
            var orePrefab = AssetDatabase.LoadAssetAtPath<OreNode>("Assets/Prefabs/OreNode.prefab");
            return Object.Instantiate(orePrefab);
        }
    }
}

//  zombi test (dont work anymore) can be upgrade to test fillamount on panel
//        

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

