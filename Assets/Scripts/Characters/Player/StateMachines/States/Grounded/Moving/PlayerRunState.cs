using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunState : PlayerMovingState
{
    private PlayerSprintData _sprintData;
    private float _startTime;
    
    public PlayerRunState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        _sprintData = movementData.SprintData;
    }
    
    #region IState Methods

    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;
        _startTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        
        if(!stateMachine.ReusableData.ShouldWalk)
            return;
        
        if(Time.time < _startTime + _sprintData.RunToWalkTime)
            return;

        StopRunning();
    }

    #endregion

    #region Main Methods

    private void StopRunning()
    {
        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        
        stateMachine.ChangeState(stateMachine.WalkState);
    }

    #endregion
    
    #region Input Methods
    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        base.OnWalkToggleStarted(context);
        stateMachine.ChangeState(stateMachine.WalkState);
    }
    
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.MediumStopState);
    }
    #endregion
}
