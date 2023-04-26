using UnityEngine;

public class PlayerJumpState : PlayerAirborneState
{
    private PlayerJumpData _jumpData;
    private bool _canStartFalling;
    private bool _shouldKeepRotating;
    public PlayerJumpState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        _jumpData = airborneData.JumpData;
        stateMachine.ReusableData.RotationData = _jumpData.RotationData;
    }

    #region IState Methods
    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementSpeedModifier = 0f;
        stateMachine.ReusableData.MovementDecelerationForce = _jumpData.DecelerationForce;

        _shouldKeepRotating = stateMachine.ReusableData.MovementInput != Vector2.zero;

        Jump();
    }

    public override void Exit()
    {
        base.Exit();
        
        SetBaseRotationData();

        _canStartFalling = false;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        if(_shouldKeepRotating)
            RotateTowardsTargetRotation();
        
        if(IsMovingUp()) 
            DecelerateVertically();
    }

    public override void Update()
    {
        base.Update();
        
        if (!_canStartFalling && IsMovingUp(0f))
            _canStartFalling = true;
        
        if(!_canStartFalling || GetPlayerVerticalVelocity().y > 0)
            return;
            
        if(GetPlayerVerticalVelocity().y > 0)
            return;
        
        stateMachine.ChangeState(stateMachine.FallState);
    }

    #endregion

    #region Reusable Methods

    protected override void ResetSprintState()
    {
        
    }

    #endregion

    #region Main Methods
    private void Jump()
    {
        Vector3 jumpForce = stateMachine.ReusableData.CurrentJumpForce;

        Vector3 jumpDirection = stateMachine.Player.transform.forward;
        if (_shouldKeepRotating)
        {
            jumpDirection = GetTargetRotation(stateMachine.ReusableData.CurrentTargetRotation.y);
        }

        jumpForce.x *= jumpDirection.x;
        jumpForce.z *= jumpDirection.z;

        Vector3 capsuleColliderCenterInWorldSpace =
            stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;

        Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);
        if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit,
                _jumpData.JumpToGroundRayDistance,
                stateMachine.Player.LayerData.GroundLayer,
                QueryTriggerInteraction.Ignore))
        {
            float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

            if (IsMovingUp())
            {
                float forceModifier = _jumpData.JumpForceModifierOnSlopeUpwards.Evaluate(groundAngle);
                jumpForce.x *= jumpDirection.x;
                jumpForce.z *= jumpDirection.z;
            }

            if (IsMovingDown())
            {
                float forceModifier = _jumpData.JumpForceModifierOnSlopeDownwards.Evaluate(groundAngle);
                jumpForce.x *= jumpDirection.y;
            }
        }
        ResetVelocity();
        
        stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
    }
    #endregion
}
