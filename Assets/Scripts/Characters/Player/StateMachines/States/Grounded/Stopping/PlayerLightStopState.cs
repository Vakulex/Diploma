public class PlayerLightStopState : PlayerStopState
{
    public PlayerLightStopState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.LightDecelerationForce;
        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.WeakForce;
    }
    #endregion
}
