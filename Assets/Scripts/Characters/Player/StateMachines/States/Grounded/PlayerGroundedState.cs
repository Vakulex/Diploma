using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerMovementState
{
    private SlopeData _slopeData;
    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        _slopeData = stateMachine.Player.ColliderUtility.SlopeData;
    }

    #region IState Methods

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

    #endregion
    
    #region Reusable Methods
    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();
        stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
    }
    
    protected virtual void OnMove()
    {
        if (stateMachine.ReusableData.ShouldWalk)
        {
            stateMachine.ChangeState(stateMachine.WalkState);
            return;
        }
        
        stateMachine.ChangeState(stateMachine.RunState);
    }
    #endregion

    #region Input Methods
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    #endregion
}
