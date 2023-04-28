using UnityEngine;

public class PlayerFallState : PlayerAirborneState
{
    private PlayerFallData _fallData;
    private Vector3 _playerPositionOnEnter;
    public PlayerFallState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        _fallData = airborneData.FallData;
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        _playerPositionOnEnter = stateMachine.Player.transform.position;
        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        ResetVerticalVelocity();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        LimitVerticalVelocity();
    }
    #endregion

    #region Reusable Methods
    protected override void ResetSprintState()
    {
        
    }

    protected override void OnContactWithGround(Collider collider)
    {
        float fallDistance = Mathf.Abs(_playerPositionOnEnter.y - stateMachine.Player.transform.position.y);

        if (fallDistance < _fallData.MinimalDistanceToBeConsideredHardFall)
        {
            stateMachine.ChangeState(stateMachine.LightLandState);
            return;
        }

        if (stateMachine.ReusableData.ShouldWalk && 
            !stateMachine.ReusableData.ShouldSprint ||
            stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.HardLandState);
            return;
        }
        
        stateMachine.ChangeState(stateMachine.RollState);
    }

    #endregion

    #region Main Methods
    private void LimitVerticalVelocity()
    {
        Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();
        if(playerVerticalVelocity.y >= -_fallData.FallSpeedLimit)
            return;

        Vector3 limitedVelocity =
            new Vector3(0f, -_fallData.FallSpeedLimit - playerVerticalVelocity.y, 0f);
        
        stateMachine.Player.Rigidbody.AddForce(limitedVelocity, ForceMode.VelocityChange);
    }
    #endregion
}
