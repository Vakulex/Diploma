using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerMovingState
{
    public PlayerWalkState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovementSpeedModifier = movementData.WalkData.SpeedModifier;
        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.WeakForce;
    }
    #endregion
    
    #region Input Methods
    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        base.OnWalkToggleStarted(context);
        
        stateMachine.ChangeState(stateMachine.RunState);
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.LightStopState);
    }
    #endregion
}
