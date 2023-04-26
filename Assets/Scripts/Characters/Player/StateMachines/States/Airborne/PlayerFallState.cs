using UnityEngine;

public class PlayerFallState : PlayerAirborneState
{
    private PlayerFallData _fallData;
    public PlayerFallState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        _fallData = airborneData.FallData;
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

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
