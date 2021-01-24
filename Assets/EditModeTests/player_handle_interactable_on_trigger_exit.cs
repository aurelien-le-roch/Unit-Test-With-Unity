using NSubstitute;
using NUnit.Framework;

namespace PlayerHandleInteraclableTest
{
    public class player_handle_interactable_on_trigger_exit
    {
        [Test]
        public void when_current_interaclable_exit_it_get_pop_from_CurrentInteraclables()
        {
            var player = Substitute.For<IPlayer>();
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclable1 = Substitute.For<IInteraclable>();
            var interaclable2 = Substitute.For<IInteraclable>();
            var interaclable3 = Substitute.For<IInteraclable>();
            
            playerHandleInteractable.OnTriggerEnter2D(interaclable1);
            playerHandleInteractable.OnTriggerEnter2D(interaclable2);
            playerHandleInteractable.OnTriggerEnter2D(interaclable3);
            
            Assert.AreEqual(interaclable3,playerHandleInteractable.CurrentInteractables.Peek());
            playerHandleInteractable.OnTriggerExit2D(interaclable3);
            Assert.AreEqual(interaclable2,playerHandleInteractable.CurrentInteractables.Peek());
        }
        
        [Test]
        public void when_not_current_interaclable_exit_the_current_interactable_stay_at_the_top_of_CurrentInteraclables()
        {
            var player = Substitute.For<IPlayer>();
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclable1 = Substitute.For<IInteraclable>();
            var interaclable2 = Substitute.For<IInteraclable>();
            var interaclable3 = Substitute.For<IInteraclable>();
            
            playerHandleInteractable.OnTriggerEnter2D(interaclable1);
            playerHandleInteractable.OnTriggerEnter2D(interaclable2);
            playerHandleInteractable.OnTriggerEnter2D(interaclable3);
            
            Assert.AreEqual(interaclable3,playerHandleInteractable.CurrentInteractables.Peek());
            playerHandleInteractable.OnTriggerExit2D(interaclable2);
            Assert.AreEqual(interaclable3,playerHandleInteractable.CurrentInteractables.Peek());
        }
        
        [Test]
        public void when_not_current_interaclable_exit_it_get_remove_from_CurrentInteraclables()
        {
            var player = Substitute.For<IPlayer>();
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclable1 = Substitute.For<IInteraclable>();
            var interaclable2 = Substitute.For<IInteraclable>();
            var interaclable3 = Substitute.For<IInteraclable>();
            
            playerHandleInteractable.OnTriggerEnter2D(interaclable1);
            playerHandleInteractable.OnTriggerEnter2D(interaclable2);
            playerHandleInteractable.OnTriggerEnter2D(interaclable3);
            
            Assert.IsTrue(playerHandleInteractable.CurrentInteractables.Contains(interaclable2));
            playerHandleInteractable.OnTriggerExit2D(interaclable2);
            Assert.IsFalse(playerHandleInteractable.CurrentInteractables.Contains(interaclable2));
        }
        
        [Test]
        public void when_not_current_interaclableWithZone_exit_PlayerExitZone_dont_get_call_on_it()
        {
            var player = Substitute.For<IPlayer>();
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclable1 = Substitute.For<IInteraclable>();
            var interaclable2 = Substitute.For<IInteractableWithZone>();
            var interaclable3 = Substitute.For<IInteraclable>();
            
            playerHandleInteractable.OnTriggerEnter2D(interaclable1);
            playerHandleInteractable.OnTriggerEnter2D(interaclable2);
            playerHandleInteractable.OnTriggerEnter2D(interaclable3);
            
            interaclable2.ClearReceivedCalls();
            playerHandleInteractable.OnTriggerExit2D(interaclable2);
            interaclable2.DidNotReceive().PlayerExitZone();
        }
        
        [Test]
        public void when_current_interaclableWithZone_exit_PlayerExitZone_get_call_on_it()
        {
            var player = Substitute.For<IPlayer>();
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclable1 = Substitute.For<IInteraclable>();
            var interaclable2 = Substitute.For<IInteraclable>();
            var interaclable3 = Substitute.For<IInteractableWithZone>();
            
            playerHandleInteractable.OnTriggerEnter2D(interaclable1);
            playerHandleInteractable.OnTriggerEnter2D(interaclable2);
            playerHandleInteractable.OnTriggerEnter2D(interaclable3);
            
            interaclable3.ClearReceivedCalls();
            playerHandleInteractable.OnTriggerExit2D(interaclable3);
            interaclable3.Received().PlayerExitZone();
        }
        
        [Test]
        public void when_current_interaclableWithZone_exit_PlayerenterZone_get_call_on_new_currentI_interactableWithZone()
        {
            var player = Substitute.For<IPlayer>();
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclable1 = Substitute.For<IInteraclable>();
            var interaclable2 = Substitute.For<IInteractableWithZone>();
            var interaclable3 = Substitute.For<IInteraclable>();
            
            playerHandleInteractable.OnTriggerEnter2D(interaclable1);
            playerHandleInteractable.OnTriggerEnter2D(interaclable2);
            playerHandleInteractable.OnTriggerEnter2D(interaclable3);
            
            interaclable2.ClearReceivedCalls();
            playerHandleInteractable.OnTriggerExit2D(interaclable3);
            interaclable2.Received().PlayerEnterZone();
        }
        
    }
}