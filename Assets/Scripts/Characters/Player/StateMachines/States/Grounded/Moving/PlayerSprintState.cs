using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSprintState : PlayerMovingState
{
    private PlayerSprintData _sprintData;
    
    private bool _keepSprinting;

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
        _keepSprinting = false;
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
    }
    
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.HardStopState);
    }
    
    #endregion

    
}
