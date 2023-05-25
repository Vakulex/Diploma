using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerAttackingState : PlayerGroundedState
    {
        private float _cooldownToIdle = 2f;
        private int _attackCounter = 0;
        
        public bool IsAttacking;
        
        public PlayerAttackingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.ReusableData.MovementSpeedModifier = 0f;
            StateMachine.Player.Rigidbody.velocity = Vector3.zero;
            StateMachine.ReusableData.IsAttacking = IsAttacking;
            base.Enter();

            StartAnimation(StateMachine.Player.AnimationData.AttackParameterHash);
            
            Attack();
        }

        private void Attack()
        {
            StateMachine.ReusableData.IsAttacking = true;
            _attackCounter++;
            if (_attackCounter > 2)
                _attackCounter = 1;
            
            StartAnimation(StateMachine.Player.AnimationData.AttackingCounterParameterHash, _attackCounter);
            StateMachine.Player.StartCoroutine(ReturnToIdle());
            if(!StateMachine.ReusableData.IsAttacking)
                StateMachine.ChangeState(StateMachine.IdlingState);
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
        
        public override void Update()
        {
            base.Update();

            if (StateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                return;
            }

            OnMove();
        }

        IEnumerator ReturnToIdle()
        {
            yield return new WaitForSeconds(_cooldownToIdle);
            StateMachine.ReusableData.IsAttacking = false;
        }
    }
}
