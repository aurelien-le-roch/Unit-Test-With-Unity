using NSubstitute;
using NUnit.Framework;

namespace interaclableTest
{
    public class interactable_counter_zone_player_enter
    {
        private InteractableCounterZone _interactableCounterZone;

        [SetUp]
        public void Init()
        {
            _interactableCounterZone=new InteractableCounterZone(4);
        }
        
        [Test]
        public void when_PlayerEnterZone_method_get_call_PlayerInZone_flag_is_set_to_true()
        {
            Assert.IsFalse(_interactableCounterZone.PlayerInZone);
            
            _interactableCounterZone.PlayerEnterZone();
            Assert.IsTrue(_interactableCounterZone.PlayerInZone);
        }
        
        [Test]
        public void when_PlayerEnterZone_method_get_call_OnPlayerEnterZone_event_get_raise()
        {
            var dummySubscriber = Substitute.For<IDummySubscriberForIHandlePlayerInZone>();

            _interactableCounterZone.OnPlayerEnterZone += dummySubscriber.HandlePlayerEnterZone;
            
            _interactableCounterZone.PlayerEnterZone();
            dummySubscriber.Received().HandlePlayerEnterZone();
        }
    }
}