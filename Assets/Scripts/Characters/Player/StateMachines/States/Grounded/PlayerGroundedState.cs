using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerGroundedState : PlayerMovementState
{
    private SlopeData _slopeData;
    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        _slopeData = stateMachine.Player.ColliderUtility.SlopeData;
    }

    #region IState Methods

    public override void Enter()
    {
        base.Enter();

        UpdateShouldSprintState();
        UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Float();
    }

    #endregion

    #region Main methods

    private void Float()
    {
        Vector3 capsuleColliderCenterInWorldSpace =
            stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;
        Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

        if (Physics.Raycast(downwardsRayFromCapsuleCenter,
                out RaycastHit hit, _slopeData.FloatRayDistance,
                stateMachine.Player.LayerData.GroundLayer,
                QueryTriggerInteraction.Ignore))
        {
            float distanceToFloatingPoint =
                stateMachine.Player.ColliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y * 
                stateMachine.Player.transform.localScale.y - hit.distance;

            if (distanceToFloatingPoint == 0f)
                return;

            float amountToLift = distanceToFloatingPoint * _slopeData.StepReachForce - GetPlayerVerticalVelocity().y;

            Vector3 liftForce = new Vector3(0f, amountToLift, 0f);
            
            stateMachine.Player.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
        }
    }
    
    private bool IsThereGroundUnderneath()
    {
        BoxCollider groundCheckCollider =
            stateMachine.Player.ColliderUtility.TriggerColliderData.GroundCheckCollider;
        Vector3 groundColliderCenterInWorldSpace =
            stateMachine.Player.ColliderUtility.TriggerColliderData.GroundCheckCollider.bounds.center;

        Collider[] overlappedGroundColliders = Physics.OverlapBox(groundColliderCenterInWorldSpace, 
            groundCheckCollider.bounds.extents, 
            groundCheckCollider.transform.rotation, 
            stateMachine.Player.LayerData.GroundLayer, 
            QueryTriggerInteraction.Ignore);

        return overlappedGroundColliders.Length > 0; 
    }

    #endregion
    
    #region Reusable Methods
    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Dash.started += OnDashStarted;
        stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;
    }


    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Dash.canceled -= OnDashStarted;
        stateMachine.Player.Input.PlayerActions.Jump.started -= OnJumpStarted;
    }
    
    protected virtual void OnMove()
    {
        if (stateMachine.ReusableData.ShouldSprint)
        {
            stateMachine.ChangeState(stateMachine.SprintState);
            return;
        }
        
        if (stateMachine.ReusableData.ShouldWalk)
        {
            stateMachine.ChangeState(stateMachine.WalkState);
            return;
        }
        
        stateMachine.ChangeState(stateMachine.RunState);
    }

    protected override void OnContactWithGroundExited(Collider collider)
    {
        base.OnContactWithGroundExited(collider);
        if(IsThereGroundUnderneath())
            return;

        Vector3 capsuleColliderCenterInWorldSpace =
            stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;

        Ray downwardsRayFromCapsuleBottom = new Ray(capsuleColliderCenterInWorldSpace - stateMachine.Player.ColliderUtility.CapsuleColliderData.ColliderVerticalExtents,
            Vector3.down);
        
        if(!Physics.Raycast(downwardsRayFromCapsuleBottom, out _,
               movementData.GroundToFallRayDistance, 
               stateMachine.Player.LayerData.GroundLayer, 
               QueryTriggerInteraction.Ignore))
            OnFall();
        
    }


    protected virtual void OnFall()
    {
        stateMachine.ChangeState(stateMachine.FallState);
    }

    private void UpdateShouldSprintState()
    {
        if (!stateMachine.ReusableData.ShouldSprint)
            return;

        if (stateMachine.ReusableData.MovementInput != Vector2.zero)
            return;

        stateMachine.ReusableData.ShouldSprint = false;
    }
    #endregion

    #region Input Methods
    protected virtual void OnDashStarted(InputAction.CallbackContext obj)
    {
        stateMachine.ChangeState(stateMachine.DashState);
    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.JumpState);
    }
    #endregion
}
