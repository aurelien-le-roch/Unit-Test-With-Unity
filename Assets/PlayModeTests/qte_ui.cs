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
            var orePrefab = AssetDatabase.LoadAssetAtPath<OreNode>("Assets/Prefabs/OreNode.prefab");
            var oreNode = Object.Instantiate(orePrefab);
            var qteCanvas = oreNode.GetComponentInChildren<UiQTEMiningCanvas>();
            
            Assert.IsFalse(qteCanvas.QtePanel.activeSelf);
            yield return new WaitForFixedUpdate();
            oreNode.QTEMining.SetupQTE(1, 0.5f, 0.2f);
            yield return new WaitForFixedUpdate();
            Assert.IsTrue(qteCanvas.QtePanel.activeSelf);
        }
    }
}