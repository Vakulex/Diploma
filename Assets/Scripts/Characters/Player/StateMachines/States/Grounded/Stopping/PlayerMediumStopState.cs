public class PlayerMediumStopState : PlayerStopState
{
    public PlayerMediumStopState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    
    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.MediumDecelerationForce;
        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.MediumForce;
    }
    #endregion
}
