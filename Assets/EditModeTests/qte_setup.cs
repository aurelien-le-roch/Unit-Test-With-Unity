using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace QteMiningTest
{
    public class qte_setup
    {
        [Test]
        public void valid_TotalTime_value_after_setup()
        {
            var qteMining = new QTEMining();
            
            qteMining.SetupQTE(1,0.5f,0.2f);
            
            Assert.AreEqual(1,qteMining.TotalTime);
        }

        [TestCase(10,8,1,9)]
        [TestCase(10,6,2,8)]
        [TestCase(10,4,3,7)]
        public void valid_MediumRange_after_setup(float totalTime,float mediumTime,float minRangeResult,float maxRangeResult)
        {
            var qteMining = new QTEMining();
            
            qteMining.SetupQTE(totalTime,mediumTime,2f);
            Assert.AreEqual(minRangeResult,qteMining.MediumRange.Min);
            Assert.AreEqual(maxRangeResult,qteMining.MediumRange.Max);
        }

        [TestCase(10,8,1,9)]
        [TestCase(10,6,2,8)]
        [TestCase(10,4,3,7)]
        public void valid_PerfectRange_after_setup(float totalTime,float perfectTime,float minRangeResult,float maxRangeResult)
        {
            var qteMining = new QTEMining();
            
            qteMining.SetupQTE(totalTime,9,perfectTime);
            Assert.AreEqual(minRangeResult,qteMining.PerfectRange.Min);
            Assert.AreEqual(maxRangeResult,qteMining.PerfectRange.Max);
        }
        
        [Test]
        public void set_IsSetup_flag_to_true_after_setup()
        {
            var qteMining = new QTEMining();
            Assert.IsFalse(qteMining.IsSetup);
            qteMining.SetupQTE(10,8,2);
            Assert.IsTrue(qteMining.IsSetup);
        }
    }

    public class qte_use
    {
        [Test]
        public void when_use_get_call_twice_IsRunning_flag_is_false()
        {
            var gameobject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            var emptyMonoBehaviour = gameobject.AddComponent<EmptyMonoBehaviourForTest>();
            var qteMining = new QTEMining();
            
            Assert.False(qteMining.IsRunning);
            qteMining.Use(emptyMonoBehaviour,0);
            Assert.True(qteMining.IsRunning); 
            qteMining.Use(emptyMonoBehaviour,0.1f);
            Assert.False(qteMining.IsRunning);
        }
        
        [Test]
        public void when_use_get_call_once_IsRunning_flag_is_true()
        {
            var gameobject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            var emptyMonoBehaviour = gameobject.AddComponent<EmptyMonoBehaviourForTest>();
            var qteMining = new QTEMining();
            
            Assert.False(qteMining.IsRunning);
            qteMining.Use(emptyMonoBehaviour,2f);
            Assert.True(qteMining.IsRunning);
        }
        
        [TestCase(10,8,2,3,QteResult.Medium)]
        [TestCase(10,8,2,5,QteResult.Perfect)]
        [TestCase(10,8,2,6,QteResult.Perfect)]
        [TestCase(10,8,2,12,QteResult.Fail)]
        [TestCase(10,8,2,0.2f,QteResult.Fail)]
        public void when_use_get_call_twice_OnQTEEnd_event_get_call_with_correct_result(float totalTime,float mediumTime,float perfectTime,float timeBetweenUSe,QteResult expectedResult)
        {
            var gameobject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            var emptyMonoBehaviour = gameobject.AddComponent<EmptyMonoBehaviourForTest>();
            var qteMining = new QTEMining();
            var dummySubscriber = Substitute.For<IDummySubscriberForQteResult>();

            qteMining.OnQTEEnd += dummySubscriber.HandleQteResult;
            
            qteMining.SetupQTE(totalTime,mediumTime,perfectTime);
            qteMining.Use(emptyMonoBehaviour,0);
            qteMining.Use(emptyMonoBehaviour,timeBetweenUSe);
            
            dummySubscriber.Received().HandleQteResult(expectedResult);
        }
        
    }

    public class EmptyMonoBehaviourForTest : MonoBehaviour
    {
        
    }
    public interface IDummySubscriberForQteResult
    {
        void HandleQteResult(QteResult result);
    }
}
