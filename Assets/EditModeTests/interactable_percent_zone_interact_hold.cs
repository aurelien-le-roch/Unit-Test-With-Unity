using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace interaclableTest
{
    public class interactable_percent_zone_interact_hold
    {
        private InteractablePercentZone _interactablePercentZone;
        private GameObject _emptyGameObject;

        [SetUp]
        public void Init()
        {
            _interactablePercentZone = new InteractablePercentZone();
            _emptyGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        }
        [TestCase(0,true)]
        [TestCase(0.2f,true)]
        [TestCase(0.9f,true)]
        [TestCase(1f,false)]
        [TestCase(1.1f,false)]
        public void when_InteractHold_InteractPercent_increase_only_if_it_less_than_1(float startPercent,bool expectedToIncrease)
        {
            _interactablePercentZone.InteractPercent = startPercent;
            _interactablePercentZone.InteractHold(_emptyGameObject,0.01f);

            var interactPercentIncrease = startPercent < _interactablePercentZone.InteractPercent;
            Assert.True(interactPercentIncrease==expectedToIncrease);
        }

        [Test]
        public void when_InteractHold_and_InteractPercent_hit_1_AlreadyHit100Percent_flag_is_set_to_true()
        {
            _interactablePercentZone.InteractPercent = 0;
            _interactablePercentZone.InteractHold(_emptyGameObject,0.01f);
            Assert.IsFalse(_interactablePercentZone.AlreadyHit100Percent);
            _interactablePercentZone.InteractHold(_emptyGameObject,10f);
            _interactablePercentZone.InteractHold(_emptyGameObject,0.01f);
            Assert.IsTrue(_interactablePercentZone.AlreadyHit100Percent);
        }
        
        [Test]
        public void when_InteractHold_and_InteractPercent_hit_1_OnInteractableHit100Percent_event_raise()
        {
            var dummySubscriber = Substitute.For<IDummySubscriverForInteractablePercent>();

            _interactablePercentZone.OnInteractableHit100Percent += dummySubscriber.HandleInteractableHit100Percent;
            
            _interactablePercentZone.InteractPercent = 0;
            _interactablePercentZone.InteractHold(_emptyGameObject,0.01f);
            dummySubscriber.DidNotReceive().HandleInteractableHit100Percent();
            _interactablePercentZone.InteractHold(_emptyGameObject,10f);
            _interactablePercentZone.InteractHold(_emptyGameObject,0.01f);
            dummySubscriber.Received().HandleInteractableHit100Percent();
        }
        
    }
}