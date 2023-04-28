using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashState : PlayerGroundedState
{
    private PlayerDashData _dashData;
    private float _startTime;
    private int _consecutiveDashesUsed;
    private bool _shouldKeepRotating;
    
    public PlayerDashState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        _dashData = movementData.DashData;
    }

    #region IState Methods

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = _dashData.SpeedModifier;
        base.Enter();

        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StrongForce;

        stateMachine.ReusableData.RotationData = _dashData.RotationData;
        
        Dash();

        _shouldKeepRotating = stateMachine.ReusableData.MovementInput != Vector2.zero;
        
        UpdateConsecutiveDashes();
        
        _startTime = Time.time;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!_shouldKeepRotating)
            return;
        
        RotateTowardsTargetRotation();
    }

    public override void OnAnimationTransitionEvent()
    {
        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.HardStopState);
            return;
        }
        stateMachine.ChangeState(stateMachine.SprintState);
    }

    public override void Exit()
    {
        base.Exit();
        
        SetBaseRotationData();
    }

    #endregion
    
    #region Main Methods
    private void Dash()
    {
        Vector3 dashDirection = stateMachine.Player.transform.forward;

        dashDirection.y = 0f;

        UpdateTargetRotation(dashDirection, false);

        if (stateMachine.ReusableData.MovementInput != Vector2.zero)
        {
            UpdateTargetRotation(GetMovementInputDirection());
            dashDirection = GetTargetRotation(stateMachine.ReusableData.CurrentTargetRotation.y);
        }
        
        stateMachine.Player.Rigidbody.velocity = dashDirection * GetMovementSpeed();
    }

    private void UpdateConsecutiveDashes()
    {
        if (!IsConsecutive())
            _consecutiveDashesUsed = 0;

        ++_consecutiveDashesUsed;

        if (_consecutiveDashesUsed == _dashData.ConsecutiveDashesLimitAmount)
        {
            _consecutiveDashesUsed = 0;
            stateMachine.Player.Input.DisableActionFor(stateMachine.Player.Input.PlayerActions.Dash,
                _dashData.DashLimitReachedCooldown);
        }
    }

    private bool IsConsecutive()
    {
        return Time.time < _startTime + _dashData.TimeToBeConsideredConsecutive;
    }
    #endregion

    #region Reusable Methods
    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Movement.performed += OnMovementPerformed;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        
        stateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformed;
    }

    #endregion
    
    #region Input Methods
    protected override void OnDashStarted(InputAction.CallbackContext context)
    {
        
    }
    
    
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        _shouldKeepRotating = true;
    }
    #endregion
}
