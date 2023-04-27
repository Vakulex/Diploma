using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRollState : PlayerLandState
{
    private PlayerRollData _rollData;
    public PlayerRollState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        _rollData = movementData.RollData;
    }

    #region IState methods

    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementSpeedModifier = _rollData.SpeedModifier;
        stateMachine.ReusableData.ShouldSprint = false;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        if(stateMachine.ReusableData.MovementInput != Vector2.zero) 
            return;
        RotateTowardsTargetRotation();
    }

    public override void OnAnimationTransitionEvent()
    {
        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.MediumStopState);
            return;
        }
        
        OnMove();
    }

    #endregion

    #region Input Methods
    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        
    }
    #endregion
}
