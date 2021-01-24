using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace PlayerHandleInteraclableTest
{
    public class player_handle_interactable_on_trigger_enter
    {
        [Test]
        public void when_enter_CurrentInteraclables_Count_increase_by_one()
        {
            var player = Substitute.For<IPlayer>();
            var interaclable = Substitute.For<IInteraclable>();
            var playerHandleInteractable = new PlayerHandleInteractable(player);

            var countBeforeEnter = playerHandleInteractable.CurrentInteractables.Count;
            for (int i = 0; i < 4; i++)
            {
                countBeforeEnter = playerHandleInteractable.CurrentInteractables.Count;
                playerHandleInteractable.OnTriggerEnter2D(interaclable);
                Assert.AreEqual(countBeforeEnter + 1, playerHandleInteractable.CurrentInteractables.Count);
            }
        }

        [Test]
        public void when_enter_CurrentInteraclables_peek_return_the_interaclable_that_enter()
        {
            var player = Substitute.For<IPlayer>();
            var playerHandleInteractable = new PlayerHandleInteractable(player);

            for (int i = 0; i < 4; i++)
            {
                var interaclable = Substitute.For<IInteraclable>();
                playerHandleInteractable.OnTriggerEnter2D(interaclable);
                Assert.AreEqual(interaclable, playerHandleInteractable.CurrentInteractables.Peek());
            }
        }

        [Test]
        public void when_enter_with_a_IInteractableWithZone_PlayerEnterZone_get_call_on_it()
        {
            var player = Substitute.For<IPlayer>();
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclableWithZone = Substitute.For<IInteractableWithZone>();

            playerHandleInteractable.OnTriggerEnter2D(interaclableWithZone);

            interaclableWithZone.Received().PlayerEnterZone();
        }
        
        [Test]
        public void when_enter_with_a_IInteractable_PlayerExitZone_get_call_on_the_IInteractableWithZone_present_before_enter()
        {
            var player = Substitute.For<IPlayer>();
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclableWithZone = Substitute.For<IInteractableWithZone>();

            playerHandleInteractable.OnTriggerEnter2D(interaclableWithZone);

            var secondInteractable = Substitute.For<IInteraclable>();
            
            playerHandleInteractable.OnTriggerEnter2D(secondInteractable);
            interaclableWithZone.Received().PlayerExitZone();
        }
    }
}