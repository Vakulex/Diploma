using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerMovingState
{
    private PlayerWalkData _walkData;
    public PlayerWalkState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        _walkData = movementData.WalkData;
    }

    #region IState Methods
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = _walkData.SpeedModifier;

        stateMachine.ReusableData.BackwardsCameraRecenteringData = _walkData.BackwardsCameraRecenteringData;
        
        base.Enter();
        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.WeakForce;
    }

    public override void Exit()
    {
        base.Exit();
        
        SetBaseCameraRecentringData();
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
        base.OnMovementCanceled(context);
    }
    #endregion
}
