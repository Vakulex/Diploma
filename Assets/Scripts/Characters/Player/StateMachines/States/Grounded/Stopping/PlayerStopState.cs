using UnityEngine.InputSystem;

public class PlayerStopState : PlayerGroundedState
{
    public PlayerStopState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods
    
    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementSpeedModifier = 0f;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!IsMovingHorizontally())
            return;
        
        DecelerateHorizontally();
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
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
    
    #endregion
    
    #region Input Methods

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        
    }

    private void OnMovementStarted(InputAction.CallbackContext context)
    {
        OnMove();
    }
    #endregion
}
