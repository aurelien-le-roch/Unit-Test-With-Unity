using NSubstitute;
using NUnit.Framework;

namespace interaclableTest
{
    public class interactable_percent_zone_exit_zone
    {
        private InteractablePercentFocusHandling _interactablePercentFocusHandling;

        [SetUp]
        public void Init()
        {
            _interactablePercentFocusHandling = new InteractablePercentFocusHandling();
        }
        [Test]
        public void when_PlayerExitZone_method_get_call_PlayerInZone_flag_is_set_to_false()
        {
            _interactablePercentFocusHandling.PlayerStopToFocusMe();
            Assert.IsFalse(_interactablePercentFocusHandling.IHavePlayerFocus);
        }
        
        [Test]
        public void when_PlayerExitZone_method_get_call_OnPlayerExitZone_event_get_raise()
        {
            var dummySubscriber = Substitute.For<IDummySubscriberForIHandlePlayerInZone>();

            _interactablePercentFocusHandling.OnPlayerStopFocusMe += dummySubscriber.HandlePlayerExitFocusHandling;
            
            _interactablePercentFocusHandling.PlayerStopToFocusMe();
            dummySubscriber.Received().HandlePlayerExitFocusHandling();
        }
        
        [TestCase(0, 0)]
        [TestCase(0.2f, 0)]
        [TestCase(0.9f,0)]
        [TestCase(1f, 0)]
        [TestCase(1.1f,0)]
        public void when_PlayerExitZone_method_get_call_InteractPercent_is_set_to_0(float percentBeforeExit,float result)
        {
            _interactablePercentFocusHandling.InteractPercent = percentBeforeExit;
            _interactablePercentFocusHandling.PlayerStopToFocusMe();
            Assert.AreEqual(result,_interactablePercentFocusHandling.InteractPercent);
        }
    }
}