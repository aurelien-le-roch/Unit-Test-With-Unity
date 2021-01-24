using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace QteMiningTest
{
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
        public void when_use_get_call_twice_OnQTEEnd_event_get_raise_with_correct_result(float totalTime,float mediumTime,float perfectTime,float timeBetweenUSe,QteResult expectedResult)
        {
            var gameobject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            var emptyMonoBehaviour = gameobject.AddComponent<EmptyMonoBehaviourForTest>();
            var qteMining = new QTEMining();
            var dummySubscriber = Substitute.For<IDummySubscriberForQteResult>();

            qteMining.OnQTEEnd += dummySubscriber.HandleQteResult;
            
            qteMining.SetupQTE(totalTime,mediumTime,perfectTime);
            qteMining.Use(emptyMonoBehaviour,0);
            dummySubscriber.DidNotReceive().HandleQteResult(expectedResult);
            qteMining.Use(emptyMonoBehaviour,timeBetweenUSe);
            
            dummySubscriber.Received().HandleQteResult(expectedResult);
        }

        [Test]
        public void when_job_over_method_get_call_event_on_job_over_get_raise()
        {
            var qteMining = new QTEMining();
            var dummySubscriber = Substitute.For<IDummySubscriberForQteResult>();

            qteMining.OnJobOver += dummySubscriber.HandleJobOver;
            qteMining.JobIsOver();
            dummySubscriber.Received().HandleJobOver();
        }
        [TestCase(QteResult.Perfect)]
        [TestCase(QteResult.Medium)]
        [TestCase(QteResult.Fail)]
        public void when_reset_method_get_call_event_on_qte_reset_get_raise(QteResult result)
        {
            var qteMining = new QTEMining();
            var dummySubscriber = Substitute.For<IDummySubscriberForQteResult>();

            qteMining.OnQTEReset += dummySubscriber.HandleQteReset;
            qteMining.Reset(result);
            dummySubscriber.Received().HandleQteReset(result);
        }
    }
    public class EmptyMonoBehaviourForTest : MonoBehaviour
    {
        
    }
    public interface IDummySubscriberForQteResult
    {
        void HandleQteResult(QteResult result);
        void HandleJobOver();
        void HandleQteReset(QteResult result);
    }
}



