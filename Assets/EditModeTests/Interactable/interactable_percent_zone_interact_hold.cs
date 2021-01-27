using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace interaclableTest
{
    public class interactable_percent_zone_interact_hold
    {
        private InteractablePercentFocusHandling _interactablePercentFocusHandling;
        private GameObject _emptyGameObject;

        [SetUp]
        public void Init()
        {
            _interactablePercentFocusHandling = new InteractablePercentFocusHandling();
            _emptyGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        }
        [TestCase(0,true)]
        [TestCase(0.2f,true)]
        [TestCase(0.9f,true)]
        [TestCase(1f,false)]
        [TestCase(1.1f,false)]
        public void when_InteractHold_InteractPercent_increase_only_if_it_less_than_1(float startPercent,bool expectedToIncrease)
        {
            _interactablePercentFocusHandling.InteractPercent = startPercent;
            _interactablePercentFocusHandling.InteractHold(_emptyGameObject,0.01f);

            var interactPercentIncrease = startPercent < _interactablePercentFocusHandling.InteractPercent;
            Assert.True(interactPercentIncrease==expectedToIncrease);
        }

        [Test]
        public void when_InteractHold_and_InteractPercent_hit_1_AlreadyHit100Percent_flag_is_set_to_true()
        {
            _interactablePercentFocusHandling.InteractPercent = 0;
            _interactablePercentFocusHandling.InteractHold(_emptyGameObject,0.01f);
            Assert.IsFalse(_interactablePercentFocusHandling.AlreadyHit100Percent);
            _interactablePercentFocusHandling.InteractHold(_emptyGameObject,10f);
            _interactablePercentFocusHandling.InteractHold(_emptyGameObject,0.01f);
            Assert.IsTrue(_interactablePercentFocusHandling.AlreadyHit100Percent);
        }
        
        [Test]
        public void when_InteractHold_and_InteractPercent_hit_1_OnInteractableHit100Percent_event_raise()
        {
            var dummySubscriber = Substitute.For<IDummySubscriverForInteractablePercent>();

            _interactablePercentFocusHandling.OnInteractableHit100Percent += dummySubscriber.HandleInteractableHit100Percent;
            
            _interactablePercentFocusHandling.InteractPercent = 0;
            _interactablePercentFocusHandling.InteractHold(_emptyGameObject,0.01f);
            dummySubscriber.DidNotReceive().HandleInteractableHit100Percent();
            _interactablePercentFocusHandling.InteractHold(_emptyGameObject,10f);
            _interactablePercentFocusHandling.InteractHold(_emptyGameObject,0.01f);
            dummySubscriber.Received().HandleInteractableHit100Percent();
        }
        
    }
}