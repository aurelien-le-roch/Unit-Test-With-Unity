using System.Collections;
using System.Collections.Generic;
using interaclableTest;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace QteMiningTest
{
    public class qte_ui
    {
        [UnityTest]
        public IEnumerator qte_panel_get_enable_after_qte_setup()
        {
            var oreNode = GetOreNode();
            var qteCanvas = oreNode.GetComponentInChildren<UiQTEMiningCanvas>();
            
            Assert.IsFalse(qteCanvas.QtePanel.activeSelf);
            yield return new WaitForFixedUpdate();
            oreNode.HaveQteMining.QTEMining.SetupQTE(1, 0.5f, 0.2f);
            yield return new WaitForFixedUpdate();
            Assert.IsTrue(qteCanvas.QtePanel.activeSelf);
        }
       
        [UnityTest]
        [TestCase(10,8,0.8f,1,1,ExpectedResult = null)]
        [TestCase(5,3,0.6f,1,1,ExpectedResult = null)]
        [TestCase(4,2,0.5f,1,1,ExpectedResult = null)]
        public IEnumerator qte_panel_medium_bar_get_valid_local_scale_after_qte_setup
            (float totalTime,float mediumTime,float scaleX,float scaleY,float scaleZ)
        {
            
            var oreNode = GetOreNode();
            var qteCanvas = oreNode.GetComponentInChildren<UiQTEMiningCanvas>();
            
            yield return new WaitForFixedUpdate();
            oreNode.HaveQteMining.QTEMining.SetupQTE(totalTime, mediumTime, 0.2f);
            yield return new WaitForFixedUpdate();
            var expectedLocalScale = new Vector3(scaleX,scaleY,scaleZ);
            
            Assert.AreEqual(expectedLocalScale,qteCanvas._mediumBarLocalScale);
        }
        
        [UnityTest]
        [TestCase(10,8,0.8f,1,1,ExpectedResult = null)]
        [TestCase(5,3,0.6f,1,1,ExpectedResult = null)]
        [TestCase(4,2,0.5f,1,1,ExpectedResult = null)]
        public IEnumerator qte_panel_perfect_bar_get_valid_local_scale_after_qte_setup
            (float totalTime,float perfectTime,float scaleX,float scaleY,float scaleZ)
        {
            var oreNode = GetOreNode();
            var qteCanvas = oreNode.GetComponentInChildren<UiQTEMiningCanvas>();
            
            yield return new WaitForFixedUpdate();
            oreNode.HaveQteMining.QTEMining.SetupQTE(totalTime, totalTime*0.9f, perfectTime);
            yield return new WaitForFixedUpdate();
            var expectedLocalScale = new Vector3(scaleX,scaleY,scaleZ);
            
            Assert.AreEqual(expectedLocalScale,qteCanvas._perfectBarLocalScale);
        }

        private OreNode GetOreNode()
        {
            var orePrefab = AssetDatabase.LoadAssetAtPath<OreNode>("Assets/Prefabs/OreNode.prefab");
            return Object.Instantiate(orePrefab);
        }
    }
}