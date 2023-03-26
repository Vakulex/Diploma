using UnityEngine.InputSystem;

public class PlayerRunState : PlayerMovingState
{
    public PlayerRunState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        
    }
    
    #region IState Methods

    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;
    }

    #endregion

    #region Input Methods
    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        base.OnWalkToggleStarted(context);
        stateMachine.ChangeState(stateMachine.WalkState);
    }
    #endregion
}
