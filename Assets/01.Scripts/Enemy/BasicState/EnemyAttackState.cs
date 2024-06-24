using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");

    private const float TransitionDuration = 0.1f;

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

        stateMachine.WeaponDamage.SetAttack(stateMachine.AttackDamage ,stateMachine.AttackKnockback);

        stateMachine.AnimatorCompo.CrossFadeInFixedTime(AttackHash, TransitionDuration);
    }

    public override void Update(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.AnimatorCompo) >= 1)
        {
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
        }
        FacePlayer();
    }
    public override void Exit()
    {

    }

}
