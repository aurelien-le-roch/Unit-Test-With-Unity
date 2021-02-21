using System.Collections;
using System.Collections.Generic;
using AimCraftMiniGame;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace aim_minigame
{
    public class aim_state_machine : MonoBehaviour
    {
        [UnityTest]
        public IEnumerator start_in_idle_state()
        {
            yield return LoadAimMiniGameTestsScene();
            var aimStateMachine = GetAimStateMachine();
            Assert.AreEqual(typeof(AimIdle),aimStateMachine.CurrentStateType );
        }
         
        
        [TestCase(1,ExpectedResult = null)]
        [TestCase(12,ExpectedResult = null)]
      
        [UnityTest]
        public IEnumerator switch_from_idle_to_begin_state_when_TryToBeginMiniGame_is_call_with_positive_amount(int maxScore)
        {
            yield return LoadAimMiniGameTestsScene();
            var aimStateMachine = GetAimStateMachine();
            aimStateMachine.TryToBeginMiniGame(maxScore);
            yield return null;
            Assert.AreEqual(typeof(AimBegin),aimStateMachine.CurrentStateType);
        }
        [TestCase(1,ExpectedResult = null)]
        [TestCase(12,ExpectedResult = null)]
      
        [UnityTest]
        public IEnumerator switch_from_idle_to_begin_state_when_MaxScore_is_greater_positive_amount(int maxScore)
        {
            yield return LoadAimMiniGameTestsScene();
            var aimStateMachine = GetAimStateMachine();
            var subScore = Substitute.For<IAimScore>();
            aimStateMachine.Score = subScore;
            subScore.MaxScore.Returns(maxScore);
            yield return null;
            Assert.AreEqual(typeof(AimBegin),aimStateMachine.CurrentStateType);
        }
        
        [TestCase(-10,ExpectedResult = null)]
        [TestCase(-1,ExpectedResult = null)]
        [TestCase(0,ExpectedResult = null)]
        [UnityTest]
        public IEnumerator stay_to_idle_state_when_MaxScore_is_equal_or_less_than_0_amount(int maxScore)
        {
            yield return LoadAimMiniGameTestsScene();
            var aimStateMachine = GetAimStateMachine();
            var subScore = Substitute.For<IAimScore>();
            aimStateMachine.Score = subScore;
            subScore.MaxScore.Returns(maxScore);
            yield return null;
            Assert.AreEqual(typeof(AimIdle),aimStateMachine.CurrentStateType);
        }
        
        [TestCase(-10,ExpectedResult = null)]
        [TestCase(-1,ExpectedResult = null)]
        [TestCase(0,ExpectedResult = null)]
        [UnityTest]
        public IEnumerator stay_to_idle_state_when_TryToBeginMiniGame_is_call_with_equal_or_less_than_0_amount(int maxScore)
        {
            yield return LoadAimMiniGameTestsScene();
            var aimStateMachine = GetAimStateMachine();
            aimStateMachine.TryToBeginMiniGame(maxScore);
            yield return null;
            Assert.AreEqual(typeof(AimIdle),aimStateMachine.CurrentStateType);
        }

        
        [UnityTest]
        public IEnumerator switch_from_begin_to_play_after_BeginTime_seconds()
        {
            yield return LoadAimMiniGameTestsScene();
            var aimStateMachine = GetAimStateMachine();
            aimStateMachine.TryToBeginMiniGame(5);
            yield return new WaitForSeconds(aimStateMachine.BeginTime*0.9f);
            Assert.AreEqual(typeof(AimBegin),aimStateMachine.CurrentStateType);
            yield return new WaitForSeconds(aimStateMachine.BeginTime*0.1f);
            yield return null;
            Assert.AreEqual(typeof(AimPlay),aimStateMachine.CurrentStateType);
        }
        
        [TestCase(-10,ExpectedResult = null)]
        [TestCase(-1,ExpectedResult = null)]
        [TestCase(0,ExpectedResult = null)]
        [UnityTest]
        public IEnumerator switch_from_play_to_end_when_life_is_equal_or_less_than_0(int life)
        {
            yield return LoadAimMiniGameTestsScene();
            var aimStateMachine = GetAimStateMachine();
            var subLife = Substitute.For<IAimLifePoint>();
            aimStateMachine.LifePoint = subLife;
            aimStateMachine.TryToBeginMiniGame(5);
            subLife.CurrentLifePoint.Returns(life);
            yield return new WaitForSeconds(aimStateMachine.BeginTime);

            yield return null;
            Assert.AreEqual(typeof(AimEnd),aimStateMachine.CurrentStateType);
        }
        
        [TestCase(10,ExpectedResult = null)]
        [TestCase(1,ExpectedResult = null)]
        [UnityTest]
        public IEnumerator stay_in_play_when_life_is_greater_than_0(int life)
        {
            yield return LoadAimMiniGameTestsScene();
            var aimStateMachine = GetAimStateMachine();
            var subLife = Substitute.For<IAimLifePoint>();
            aimStateMachine.LifePoint = subLife;
            aimStateMachine.TryToBeginMiniGame(5);
            subLife.CurrentLifePoint.Returns(life);

            yield return new WaitForSeconds(aimStateMachine.BeginTime);

            yield return null;

            Assert.AreEqual(typeof(AimPlay),aimStateMachine.CurrentStateType);
        }


        [TestCase(15,5,ExpectedResult = null)]
        [TestCase(10,8,ExpectedResult = null)]
        [TestCase(1,1,ExpectedResult = null)]
        [UnityTest]
        public IEnumerator switch_from_play_to_end_when_score_is_greater_or_equal_to_MaxScore(int currentScore,int maxScore)
        {
            yield return LoadAimMiniGameTestsScene();
            var aimStateMachine = GetAimStateMachine();
            var subScore = Substitute.For<IAimScore>();
            aimStateMachine.TryToBeginMiniGame(maxScore);
            //idle
            yield return null;
            //begin
            aimStateMachine.Score = subScore;
            subScore.CurrentScore.Returns(currentScore);
            subScore.MaxScore.Returns(maxScore);
            yield return new WaitUntil(()=>aimStateMachine.CurrentStateType ==typeof( AimPlay));
            //play
            

            yield return null;
            Assert.AreEqual(typeof(AimEnd),aimStateMachine.CurrentStateType);
        }

        [TestCase(-5,5,ExpectedResult = null)]
        [TestCase(7,8,ExpectedResult = null)]
        [TestCase(0,1,ExpectedResult = null)]
        [UnityTest]
        public IEnumerator stay_in_play_when_CurrentScore_is_less_than_MaxScore(int currentScore,int maxScore)
        {
            yield return LoadAimMiniGameTestsScene();
            var aimStateMachine = GetAimStateMachine();
            var subScore = Substitute.For<IAimScore>();
            aimStateMachine.TryToBeginMiniGame(maxScore);
            //idle
            yield return null;
            //begin
            aimStateMachine.Score = subScore;
            subScore.CurrentScore.Returns(currentScore);
            subScore.MaxScore.Returns(maxScore);
            yield return new WaitForSeconds(aimStateMachine.BeginTime+0.1f);
            //play
            

            yield return null;
            Assert.AreEqual(typeof(AimPlay),aimStateMachine.CurrentStateType);
        }
        
        private AimStateMachine GetAimStateMachine()
        {
            return FindObjectOfType<AimStateMachine>();
        }
        private  IEnumerator LoadAimMiniGameTestsScene()
        {
            var operation = SceneManager.LoadSceneAsync("AimMiniGame");
            while (operation.isDone == false)
                yield return null;
        }
    }
}

