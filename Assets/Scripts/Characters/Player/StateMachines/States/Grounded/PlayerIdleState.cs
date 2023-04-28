using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    private PlayerIdleData _idleData;
    public PlayerIdleState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        _idleData = movementData.IdleData;
    }

    #region IState Methods
    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        stateMachine.ReusableData.BackwardsCameraRecenteringData = _idleData.BackwardsCameraRecenteringData;
        
        base.Enter();

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StationaryForce;
        ResetVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            return;

        OnMove();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        if(!IsMovingHorizontally())
            return;
        
        ResetVelocity();
    }

    #endregion
}
