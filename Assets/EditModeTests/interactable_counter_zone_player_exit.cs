using NSubstitute;
using NUnit.Framework;

namespace interaclableTest
{
    public class interactable_counter_zone_player_exit
    {
        private InteractableCounterZone _interactableCounterZone;
        [SetUp]
        public void Init()
        {
            _interactableCounterZone=new InteractableCounterZone(4);
        }

        [Test]
        public void when_PlayerExitZone_method_get_call_PlayerInZone_flag_is_set_to_false()
        {
            _interactableCounterZone.PlayerExitZone();
            Assert.IsFalse(_interactableCounterZone.PlayerInZone);
        }
        
        [Test]
        public void when_PlayerExitZone_method_get_call_OnPlayerExitZone_event_get_raise()
        {
            var dummySubscriber = Substitute.For<IDummySubscriberForIHandlePlayerInZone>();

            _interactableCounterZone.OnPlayerExitZone += dummySubscriber.HandlePlayerExitZone;
            
            _interactableCounterZone.PlayerExitZone();
            dummySubscriber.Received().HandlePlayerExitZone();
        }
        
        [Test]
        public void when_PlayerExitZone_method_get_call_CurrentCounter_is_set_to_0()
        {
         
            _interactableCounterZone.CurrentCounter+=3;
            _interactableCounterZone.PlayerExitZone();
            Assert.AreEqual(0,_interactableCounterZone.CurrentCounter);
        }
    }
}