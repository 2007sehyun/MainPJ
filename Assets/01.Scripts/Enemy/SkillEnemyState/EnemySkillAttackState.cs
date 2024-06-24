using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillAttackState : EnemySkillBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");

    private const float TransitionDuration = 0.1f;

    public EnemySkillAttackState(EnemySkillStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

        stateMachine.WeaponDamage.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);

        stateMachine.AnimatorCompo.CrossFadeInFixedTime(AttackHash, TransitionDuration);
    }

    public override void Update(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.AnimatorCompo) >= 1)
        {
            stateMachine.SwitchState(new EnemySkillChaseState(stateMachine));
        }
        FacePlayer();
    }
    public override void Exit()
    {

    }
}
