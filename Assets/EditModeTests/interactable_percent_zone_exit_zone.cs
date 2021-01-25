using NSubstitute;
using NUnit.Framework;

namespace interaclableTest
{
    public class interactable_percent_zone_exit_zone
    {
        private InteractablePercentZone _interactablePercentZone;

        [SetUp]
        public void Init()
        {
            _interactablePercentZone = new InteractablePercentZone();
        }
        [Test]
        public void when_PlayerExitZone_method_get_call_PlayerInZone_flag_is_set_to_false()
        {
            _interactablePercentZone.PlayerExitZone();
            Assert.IsFalse(_interactablePercentZone.PlayerInZone);
        }
        
        [Test]
        public void when_PlayerExitZone_method_get_call_OnPlayerExitZone_event_get_raise()
        {
            var dummySubscriber = Substitute.For<IDummySubscriberForIHandlePlayerInZone>();

            _interactablePercentZone.OnPlayerExitZone += dummySubscriber.HandlePlayerExitZone;
            
            _interactablePercentZone.PlayerExitZone();
            dummySubscriber.Received().HandlePlayerExitZone();
        }
        
        [TestCase(0, 0)]
        [TestCase(0.2f, 0)]
        [TestCase(0.9f,0)]
        [TestCase(1f, 0)]
        [TestCase(1.1f,0)]
        public void when_PlayerExitZone_method_get_call_InteractPercent_is_set_to_0(float percentBeforeExit,float result)
        {
            _interactablePercentZone.InteractPercent = percentBeforeExit;
            _interactablePercentZone.PlayerExitZone();
            Assert.AreEqual(result,_interactablePercentZone.InteractPercent);
        }
    }
}