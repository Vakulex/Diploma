using UnityEngine.InputSystem;

public class PlayerHardLandState : PlayerLandState
{
    public PlayerHardLandState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;
        base.Enter();
        
        stateMachine.Player.Input.PlayerActions.Movement.Disable();

        ResetVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        
        stateMachine.Player.Input.PlayerActions.Movement.Enable();
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    public override void OnAnimationExitEvent()
    {
        stateMachine.Player.Input.PlayerActions.Movement.Enable();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        if(!IsMovingHorizontally())
            return;
        
        ResetVelocity();
    }

    #endregion

    #region Reusable Methods

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();
        stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;
    }

    protected override void OnMove()
    {
        if(stateMachine.ReusableData.ShouldWalk)
            return;
        stateMachine.ChangeState(stateMachine.RunState);
    }
    
    #endregion

    #region Input Methods
    
    private void OnMovementStarted(InputAction.CallbackContext context)
    {
        OnMove();
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        
    }

    #endregion
}
