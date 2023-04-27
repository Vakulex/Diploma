using UnityEngine.InputSystem;

public class PlayerLandState : PlayerGroundedState  
{
    public PlayerLandState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region Input Methods

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        
    }

    #endregion
}
