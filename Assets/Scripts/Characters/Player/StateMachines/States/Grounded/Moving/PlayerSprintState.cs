using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSprintState : PlayerMovingState
{
    private PlayerSprintData _sprintData;
    
    private bool _keepSprinting;
    private bool _shouldResetSprintState;

    private float _startTime;
    public PlayerSprintState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        _sprintData = movementData.SprintData;
    }
    
    #region IState Methods

    public override void Enter()
    {
        base.Enter();

        stateMachine.ReusableData.MovementSpeedModifier = _sprintData.SpeedModifier;
        stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StrongForce;
        _shouldResetSprintState = true;
        _startTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (_keepSprinting)
            return;
        
        if(Time.time < _startTime + _sprintData.SprintToRunTime)
            return;
        
        StopSprinting();
    }

    public override void Exit()
    {
        base.Exit();

        if (_shouldResetSprintState)
        {
            _keepSprinting = false;
            stateMachine.ReusableData.ShouldSprint = false;
        }

    }

    #endregion

    #region Main Methods

    public void StopSprinting()
    {
        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        
        stateMachine.ChangeState(stateMachine.RunState);
    }

    #endregion
    
    #region Reusable Mthods
    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Sprint.performed += OnSprintPerformed;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();
        stateMachine.Player.Input.PlayerActions.Sprint.performed -= OnSprintPerformed;
    }

    #endregion

    #region Input Methods
    
    private void OnSprintPerformed(InputAction.CallbackContext obj)
    {
        _keepSprinting = true;
        stateMachine.ReusableData.ShouldWalk = true;
    }
    
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.HardStopState);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        _shouldResetSprintState = false;
        base.OnJumpStarted(context);
    }

    protected override void OnFall()
    {
        _shouldResetSprintState = false;
        
        base.OnFall();
    }

    #endregion

    
}
