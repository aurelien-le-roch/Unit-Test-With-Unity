using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using QteMiningTest;
using UnityEngine;

namespace interaclableTest
{
    public class interactable_percent_zone_dont_interact
    {
        private InteractablePercentZone _interactablePercentZone;
        private GameObject _emptyGameObject;
        [SetUp]
        public void Init()
        {
            _interactablePercentZone=new InteractablePercentZone();
            _emptyGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        }
        
        [Test]
        public void when_DontInteract_InteractPercent_dont_get_reduce_if_AlreadyHit100Percent_flag_is_true()
        {
            _interactablePercentZone.InteractPercent = 0;
            _interactablePercentZone.InteractHold(_emptyGameObject,10f);
            _interactablePercentZone.InteractHold(_emptyGameObject,0.01f);

            var interactPercentBeforeDontInteract = _interactablePercentZone.InteractPercent;
            _interactablePercentZone.DontInteract(0.2f);
            Assert.AreEqual(interactPercentBeforeDontInteract,_interactablePercentZone.InteractPercent);
        }
        
        [Test]
        public void when_DontInteract_InteractPercent_get_reduce_if_AlreadyHit100Percent_flag_is_false()
        {
            _interactablePercentZone.InteractPercent = 0.4f;
            _interactablePercentZone.InteractHold(_emptyGameObject,0.1f);
            _interactablePercentZone.InteractHold(_emptyGameObject,0.01f);
            Assert.IsFalse(_interactablePercentZone.AlreadyHit100Percent);
            
            var interactPercentBeforeDontInteract = _interactablePercentZone.InteractPercent;
            _interactablePercentZone.DontInteract(0.2f);
            Assert.Less(_interactablePercentZone.InteractPercent,interactPercentBeforeDontInteract);
        }
        
        [Test]
        public void when_DontInteract_InteractPercent_get_reduce_t0_not_less()
        {
            _interactablePercentZone.InteractPercent = 0.1f;
            
            _interactablePercentZone.DontInteract(5f);
            _interactablePercentZone.DontInteract(0.01f);
            Assert.AreEqual(0,_interactablePercentZone.InteractPercent);
        }
    }

    public interface IDummySubscriberForIHandlePlayerInZone
    {
        void HandlePlayerEnterZone();
        void HandlePlayerExitZone();
    }

    public interface IDummySubscriverForInteractablePercent
    {
        void HandleInteractableHit100Percent();
    }
    
    public interface IDummySubscriverForInteractableCounter
    {
        void HandleCounterChange(int max,int current);
        void HandleMaxCounterHit();
    }
}