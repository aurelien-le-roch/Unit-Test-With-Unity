using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using QteMiningTest;
using UnityEngine;

namespace interaclableTest
{
    public class interactable_percent_zone_dont_interact
    {
        private InteractablePercentFocusHandling _interactablePercentFocusHandling;
        private GameObject _emptyGameObject;
        [SetUp]
        public void Init()
        {
            _interactablePercentFocusHandling=new InteractablePercentFocusHandling();
            _emptyGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        }
        
        [Test]
        public void when_DontInteract_InteractPercent_dont_get_reduce_if_AlreadyHit100Percent_flag_is_true()
        {
            _interactablePercentFocusHandling.InteractPercent = 0;
            _interactablePercentFocusHandling.InteractHold(_emptyGameObject,10f);
            _interactablePercentFocusHandling.InteractHold(_emptyGameObject,0.01f);

            var interactPercentBeforeDontInteract = _interactablePercentFocusHandling.InteractPercent;
            _interactablePercentFocusHandling.DontInteract(0.2f);
            Assert.AreEqual(interactPercentBeforeDontInteract,_interactablePercentFocusHandling.InteractPercent);
        }
        
        [Test]
        public void when_DontInteract_InteractPercent_get_reduce_if_AlreadyHit100Percent_flag_is_false()
        {
            _interactablePercentFocusHandling.InteractPercent = 0.4f;
            _interactablePercentFocusHandling.InteractHold(_emptyGameObject,0.1f);
            _interactablePercentFocusHandling.InteractHold(_emptyGameObject,0.01f);
            Assert.IsFalse(_interactablePercentFocusHandling.AlreadyHit100Percent);
            
            var interactPercentBeforeDontInteract = _interactablePercentFocusHandling.InteractPercent;
            _interactablePercentFocusHandling.DontInteract(0.2f);
            Assert.Less(_interactablePercentFocusHandling.InteractPercent,interactPercentBeforeDontInteract);
        }
        
        [Test]
        public void when_DontInteract_InteractPercent_get_reduce_t0_not_less()
        {
            _interactablePercentFocusHandling.InteractPercent = 0.1f;
            
            _interactablePercentFocusHandling.DontInteract(5f);
            _interactablePercentFocusHandling.DontInteract(0.01f);
            Assert.AreEqual(0,_interactablePercentFocusHandling.InteractPercent);
        }
    }
}