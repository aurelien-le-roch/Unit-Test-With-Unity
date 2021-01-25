using NSubstitute;
using NUnit.Framework;

namespace interaclableTest
{
    public class interactable_percent_zone_enter_zone
    {
        private InteractablePercentZone _interactablePercentZone;
        [SetUp]
        public void Init()
        {
            _interactablePercentZone=new InteractablePercentZone();
        }
        
        [Test]
        public void when_PlayerEnterZone_method_get_call_PlayerInZone_flag_is_set_to_true()
        {
            Assert.IsFalse(_interactablePercentZone.PlayerInZone);
            
            _interactablePercentZone.PlayerEnterZone();
            Assert.IsTrue(_interactablePercentZone.PlayerInZone);
        }
        
        [Test]
        public void when_PlayerEnterZone_method_get_call_OnPlayerEnterZone_event_get_raise()
        {
            var dummySubscriber = Substitute.For<IDummySubscriberForIHandlePlayerInZone>();

            _interactablePercentZone.OnPlayerEnterZone += dummySubscriber.HandlePlayerEnterZone;
            
            _interactablePercentZone.PlayerEnterZone();
            dummySubscriber.Received().HandlePlayerEnterZone();
        }
    }
}