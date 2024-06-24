using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillJumpState : EnemySkillBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");

    private const float DurationTime = 0.1f;


    public EnemySkillJumpState(EnemySkillStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        FacePlayer();
        stateMachine.WeaponHandle.IsAttack = true;
        stateMachine.AnimatorCompo.CrossFadeInFixedTime(JumpHash, DurationTime);
    }

    public override void Update(float deltaTime)
    {

        if (!stateMachine.WeaponHandle.IsAttack)
        {
            stateMachine.SwitchState(new EnemySkillIdleState(stateMachine));
        }
        MoveToPlayer(deltaTime);
    }

    public override void Exit()
    {
        stateMachine.WeaponDamage.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }

    private void MoveToPlayer(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh && stateMachine.Agent.enabled == true)
        {
            stateMachine.Agent.destination = stateMachine.Player.transform.position;

            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.JumpSpeed, deltaTime);

            stateMachine.Agent.velocity = stateMachine.EnemyController.velocity;
        }
    }
}
