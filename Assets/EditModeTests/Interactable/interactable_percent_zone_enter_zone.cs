using NSubstitute;
using NUnit.Framework;

namespace interaclableTest
{
    public class interactable_percent_zone_enter_zone
    {
        private InteractablePercentFocusHandling _interactablePercentFocusHandling;
        [SetUp]
        public void Init()
        {
            _interactablePercentFocusHandling=new InteractablePercentFocusHandling();
        }
        
        [Test]
        public void when_PlayerEnterZone_method_get_call_PlayerInZone_flag_is_set_to_true()
        {
            Assert.IsFalse(_interactablePercentFocusHandling.IHavePlayerFocus);
            
            _interactablePercentFocusHandling.PlayerStartToFocusMe();
            Assert.IsTrue(_interactablePercentFocusHandling.IHavePlayerFocus);
        }
        
        [Test]
        public void when_PlayerEnterZone_method_get_call_OnPlayerEnterZone_event_get_raise()
        {
            var dummySubscriber = Substitute.For<IDummySubscriberForIHandlePlayerInZone>();

            _interactablePercentFocusHandling.OnPlayerFocusMe += dummySubscriber.HandlePlayerEnterFocusHandling;
            
            _interactablePercentFocusHandling.PlayerStartToFocusMe();
            dummySubscriber.Received().HandlePlayerEnterFocusHandling();
        }
    }
}