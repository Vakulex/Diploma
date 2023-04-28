using UnityEngine;

public class PlayerLightLandState : PlayerLandState
{
    public PlayerLightLandState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0f;
        base.Enter();


        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StationaryForce;
        
        ResetVelocity();
    }

    public override void Update()
    {
        base.Update();
        
        if(stateMachine.ReusableData.MovementInput == Vector2.zero)
            return;
        OnMove();
    }

    public override void OnAnimationTransitionEvent()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
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
