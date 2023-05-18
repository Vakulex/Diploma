using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerAttackingState : PlayerGroundedState
    {
        private int _attackCounter = 0;
        
        public PlayerAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.ReusableData.MovementSpeedModifier = 0f;
            
            base.Enter();

            StartAnimation(StateMachine.Player.AnimationData.AttackParameterHash);
            
            Attack();
        }

        private void Attack()
        {
            _attackCounter++;
            if (_attackCounter > 2)
                _attackCounter = 1;
            
            StartAnimation(StateMachine.Player.AnimationData.AttackingCounterParameterHash, _attackCounter);
            
        }

        protected override void OnAttackStarted(InputAction.CallbackContext obj)
        {
            Attack();
        }

        public override void Exit()
        {
            base.Exit();
            _attackCounter = 0;
            
            StopAnimation(StateMachine.Player.AnimationData.AttackParameterHash);
            StateMachine.Player.Animator.SetInteger(StateMachine.Player.AnimationData.AttackingCounterParameterHash, 0);
        }
    }
}
