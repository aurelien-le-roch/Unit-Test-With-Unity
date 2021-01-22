using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace interaclableTest
{
    public static class Helpers
    {
        public static IEnumerator LoadInteractablesTestsScene()
        {
            var operation = SceneManager.LoadSceneAsync("InteraclablesTestsScene");
            while (operation.isDone == false)
                yield return null;
        }

        public static Player GetPlayer()
        {
            return GameObject.FindObjectOfType<Player>();
        }

        public static OreNode GetOreNode()
        {
            return GameObject.FindObjectOfType<OreNode>();
        }
    }
}