using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashState : PlayerGroundedState
{
    private PlayerDashData _dashData;
    private float _startTime;
    private int _consecutiveDashesUsed;
    
    public PlayerDashState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        _dashData = movementData.DashData;
    }

    #region IState Methods

    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementSpeedModifier = _dashData.SpeedModifier;
        AddForceOnTransitionFromStationaryState();
        
        UpdateConsecutiveDashes();
        
        _startTime = Time.time;
    }
    
    public override void OnAnimationTransitionEvent()
    {
        base.OnAnimationTransitionEvent();

        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        stateMachine.ChangeState(stateMachine.SprintState);
    }
    #endregion
    
    #region Main Methods
    private void AddForceOnTransitionFromStationaryState()
    {
        if(stateMachine.ReusableData.MovementInput != Vector2.zero)
            return;

        Vector3 characterRotationDirection = stateMachine.Player.transform.forward;

        characterRotationDirection.y = 0f;

        stateMachine.Player.Rigidbody.velocity = characterRotationDirection * GetMovementSpeed();
    }

    private void UpdateConsecutiveDashes()
    {
        if (!isConsectutive())
            _consecutiveDashesUsed = 0;

        ++_consecutiveDashesUsed;

        if (_consecutiveDashesUsed == _dashData.ConsecutiveDashesLimitAmount)
        {
            _consecutiveDashesUsed = 0;
            stateMachine.Player.Input.DisableActionFor(stateMachine.Player.Input.PlayerActions.Dash,
                _dashData.DashLimitReachedCooldown);
        }
    }

    private bool isConsectutive()
    {
        return Time.time < _startTime + _dashData.TimeToBeConsideredConsecutive;
    }
    #endregion
    
    #region Input Methods
    protected override void OnDashStarted(InputAction.CallbackContext obj)
    {
        
    }
    #endregion
}
