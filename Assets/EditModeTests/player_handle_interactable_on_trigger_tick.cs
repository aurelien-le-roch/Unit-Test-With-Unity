using NSubstitute;
using NUnit.Framework;

namespace PlayerHandleInteraclableTest
{
    //note can be better with [Setup] method
    public class player_handle_interactable_on_trigger_tick
    {
        private float baseDeltatime=0.01f;
        [Test]
        public void on_PlayerInput_InteractDown_the_InteractDown_method_get_call_on_the_current_interactable()
        {
            var player = Substitute.For<IPlayer>();
            var playerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = playerInput;
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclable1 = Substitute.For<IInteraclable>();
            var interaclable2 = Substitute.For<IInteraclable>();
            var interaclable3 = Substitute.For<IInteraclable>();
            
            playerHandleInteractable.OnTriggerEnter2D(interaclable1);
            playerHandleInteractable.OnTriggerEnter2D(interaclable2);
            playerHandleInteractable.OnTriggerEnter2D(interaclable3);

            playerHandleInteractable.Tick(0.01f);
            interaclable3.DidNotReceive().InteractDown(player.gameObject);
            playerInput.InteractDown.Returns(true);
            playerHandleInteractable.Tick(0.01f);
            interaclable3.Received().InteractDown(player.gameObject);
        }
        
        [Test]
        public void on_PlayerInput_InteractDown_the_InteractDown_method_dont_get_call_on_not_the_current_interactable()
        {
            var player = Substitute.For<IPlayer>();
            var playerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = playerInput;
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclable1 = Substitute.For<IInteraclable>();
            var interaclable2 = Substitute.For<IInteraclable>();
            var interaclable3 = Substitute.For<IInteraclable>();
            
            playerHandleInteractable.OnTriggerEnter2D(interaclable1);
            playerHandleInteractable.OnTriggerEnter2D(interaclable2);
            playerHandleInteractable.OnTriggerEnter2D(interaclable3);

            playerHandleInteractable.Tick(baseDeltatime);
            interaclable1.DidNotReceive().InteractDown(player.gameObject);
            interaclable2.DidNotReceive().InteractDown(player.gameObject);
            playerInput.InteractDown.Returns(true);
            playerHandleInteractable.Tick(baseDeltatime);
            interaclable1.DidNotReceive().InteractDown(player.gameObject);
            interaclable2.DidNotReceive().InteractDown(player.gameObject);
        }
        
        [Test]
        public void on_PlayerInput_InteractHold_the_InteractHold_method_get_call_on_the_current_interactable()
        {
            var player = Substitute.For<IPlayer>();
            var playerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = playerInput;
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclable1 = Substitute.For<IInteraclable>();
            var interaclable2 = Substitute.For<IInteraclable>();
            var interaclable3 = Substitute.For<IInteraclable>();
            
            playerHandleInteractable.OnTriggerEnter2D(interaclable1);
            playerHandleInteractable.OnTriggerEnter2D(interaclable2);
            playerHandleInteractable.OnTriggerEnter2D(interaclable3);

            playerHandleInteractable.Tick(baseDeltatime);
            interaclable3.DidNotReceive().InteractHold(player.gameObject,baseDeltatime);
            playerInput.InteractHold.Returns(true);
            playerHandleInteractable.Tick(baseDeltatime);
            interaclable3.Received().InteractHold(player.gameObject,baseDeltatime);
        }
        
        [Test]
        public void on_PlayerInput_InteractHold_the_InteractHold_method_dont_get_call_on_not_the_current_interactable()
        {
            var player = Substitute.For<IPlayer>();
            var playerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = playerInput;
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclable1 = Substitute.For<IInteraclable>();
            var interaclable2 = Substitute.For<IInteraclable>();
            var interaclable3 = Substitute.For<IInteraclable>();
            
            playerHandleInteractable.OnTriggerEnter2D(interaclable1);
            playerHandleInteractable.OnTriggerEnter2D(interaclable2);
            playerHandleInteractable.OnTriggerEnter2D(interaclable3);

            playerHandleInteractable.Tick(baseDeltatime);
            interaclable1.DidNotReceive().InteractHold(player.gameObject,baseDeltatime);
            interaclable2.DidNotReceive().InteractHold(player.gameObject,baseDeltatime);
            playerInput.InteractHold.Returns(true);
            playerHandleInteractable.Tick(baseDeltatime);
            interaclable1.DidNotReceive().InteractHold(player.gameObject,baseDeltatime);
            interaclable2.DidNotReceive().InteractHold(player.gameObject,baseDeltatime);
        }

        [Test]
        public void when_player_dont_interact_the_dont_interact_method_get_call_on_the_current_interactable()
        {
            var player = Substitute.For<IPlayer>();
            var playerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = playerInput;
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclable1 = Substitute.For<IInteraclable>();
            var interaclable2 = Substitute.For<IInteraclable>();
            var interaclable3 = Substitute.For<IInteraclable>();
            
            playerHandleInteractable.OnTriggerEnter2D(interaclable1);
            playerHandleInteractable.OnTriggerEnter2D(interaclable2);
            playerHandleInteractable.OnTriggerEnter2D(interaclable3);
            
            playerInput.InteractDown.Returns(false);
            playerInput.InteractHold.Returns(false);
            playerHandleInteractable.Tick(baseDeltatime);
            interaclable3.Received().DontInteract(baseDeltatime);
        }
        
        [Test]
        public void when_player_dont_interact_the_dont_interact_method_dont_get_call_on_the_not_current_interactable()
        {
            var player = Substitute.For<IPlayer>();
            var playerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = playerInput;
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclable1 = Substitute.For<IInteraclable>();
            var interaclable2 = Substitute.For<IInteraclable>();
            var interaclable3 = Substitute.For<IInteraclable>();
            
            playerHandleInteractable.OnTriggerEnter2D(interaclable1);
            playerHandleInteractable.OnTriggerEnter2D(interaclable2);
            playerHandleInteractable.OnTriggerEnter2D(interaclable3);
            
            playerInput.InteractDown.Returns(false);
            playerInput.InteractHold.Returns(false);
            playerHandleInteractable.Tick(baseDeltatime);
            interaclable1.DidNotReceive().DontInteract(baseDeltatime);
            interaclable2.DidNotReceive().DontInteract(baseDeltatime);
        }

        [Test]
        public void if_elfe_if_else_order_call()
        {
            var player = Substitute.For<IPlayer>();
            var playerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = playerInput;
            var playerHandleInteractable = new PlayerHandleInteractable(player);
            var interaclable1 = Substitute.For<IInteraclable>();
            
            playerHandleInteractable.OnTriggerEnter2D(interaclable1);
            playerInput.InteractDown.Returns(true);
            playerInput.InteractHold.Returns(true);
            
            playerHandleInteractable.Tick(baseDeltatime);
            
            interaclable1.Received().InteractDown(player.gameObject);
            interaclable1.DidNotReceive().InteractHold(player.gameObject,baseDeltatime);
            interaclable1.DidNotReceive().DontInteract(baseDeltatime);
            
            interaclable1.ClearReceivedCalls();
            playerInput.InteractDown.Returns(false);
            playerInput.InteractHold.Returns(true);
            
            playerHandleInteractable.Tick(baseDeltatime);
            interaclable1.DidNotReceive().InteractDown(player.gameObject);
            interaclable1.Received().InteractHold(player.gameObject,baseDeltatime);
            interaclable1.DidNotReceive().DontInteract(baseDeltatime);
            
            interaclable1.ClearReceivedCalls();
            playerInput.InteractDown.Returns(false);
            playerInput.InteractHold.Returns(false);
            playerHandleInteractable.Tick(baseDeltatime);
            
            interaclable1.DidNotReceive().InteractDown(player.gameObject);
            interaclable1.DidNotReceive().InteractHold(player.gameObject,baseDeltatime);
            interaclable1.Received().DontInteract(baseDeltatime);
        }
    }
}