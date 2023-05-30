namespace MovementSystem
{
    public class PlayerMovingState : PlayerGroundedState
    {
        public PlayerMovingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(StateMachine.Player.AnimationData.MovingParameterHash);
            StateMachine.Player.AudioSource.enabled = true;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(StateMachine.Player.AnimationData.MovingParameterHash);
            StateMachine.Player.AudioSource.enabled = false;
        }
    }
}