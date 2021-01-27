using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace interaclableTest
{
    public class interactable_counter_zone_interact_down
    {
        private InteractableCounterZone _interactableCounterZone;
        private GameObject _emptyGameObject;
        [SetUp]
        public void Init()
        {
            _interactableCounterZone=new InteractableCounterZone(4);
            _emptyGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        }
        
        [Test]
        public void when_InteractDown_CurrentCounter_increase()
        {
            var currentCounterBeforeInteractDown = _interactableCounterZone.CurrentCounter;
            _interactableCounterZone.InteractDown(_emptyGameObject);
            Assert.AreEqual(currentCounterBeforeInteractDown+1,_interactableCounterZone.CurrentCounter);
        }
        
        [Test]
        public void when_InteractDown_OnCounterChange_event_get_raise()
        {
            var dummySubscriber = Substitute.For<IDummySubscriverForInteractableCounter>();
            _interactableCounterZone.OnCounterChange += dummySubscriber.HandleCounterChange;
            _interactableCounterZone.InteractDown(_emptyGameObject);
            dummySubscriber.Received().HandleCounterChange(_interactableCounterZone.MaxCounter,_interactableCounterZone.CurrentCounter);
        }
        
        [Test]
        public void when_InteractDown_CurrentCounter_increase_if_less_than_MaxCounter()
        {
            _interactableCounterZone.CurrentCounter = _interactableCounterZone.MaxCounter - 1;
            var currentCounterBeforeInteractDown = _interactableCounterZone.CurrentCounter;
            _interactableCounterZone.InteractDown(_emptyGameObject);
            Assert.Greater(_interactableCounterZone.CurrentCounter,currentCounterBeforeInteractDown);
            _interactableCounterZone.InteractDown(_emptyGameObject);
            _interactableCounterZone.InteractDown(_emptyGameObject);
            Assert.AreEqual(_interactableCounterZone.MaxCounter,_interactableCounterZone.CurrentCounter);
        }
        
        [Test]
        public void when_InteractDown_OnMaxCounterHit_event_get_raise_when_CurrentCounter_equal_MaxCounter()
        {
            var dummySubscriber = Substitute.For<IDummySubscriverForInteractableCounter>();
            _interactableCounterZone.OnMaxCounterHit += dummySubscriber.HandleMaxCounterHit;
            _interactableCounterZone.OnCounterChange += dummySubscriber.HandleCounterChange;
            _interactableCounterZone.CurrentCounter = _interactableCounterZone.MaxCounter - 2;
            _interactableCounterZone.InteractDown(_emptyGameObject);
            dummySubscriber.DidNotReceive().HandleMaxCounterHit();
            _interactableCounterZone.InteractDown(_emptyGameObject);
            dummySubscriber.Received().HandleMaxCounterHit();
            dummySubscriber.ClearReceivedCalls();
            _interactableCounterZone.InteractDown(_emptyGameObject);
            dummySubscriber.DidNotReceive().HandleCounterChange(_interactableCounterZone.MaxCounter,_interactableCounterZone.CurrentCounter);
            dummySubscriber.DidNotReceive().HandleMaxCounterHit();
        }
    }
}