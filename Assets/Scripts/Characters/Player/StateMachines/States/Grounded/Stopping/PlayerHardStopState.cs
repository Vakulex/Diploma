public class PlayerHardStopState : PlayerStopState
{
    public PlayerHardStopState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    
    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.HardDecelerationForce;
    }
    #endregion

    #region Reusable Methods
    
    protected override void OnMove()
    {
        if(stateMachine.ReusableData.ShouldWalk)
            return;
        
        stateMachine.ChangeState(stateMachine.RunState);
    }

    #endregion
}
