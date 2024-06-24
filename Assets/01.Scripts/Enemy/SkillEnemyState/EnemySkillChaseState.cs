using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillChaseState : EnemySkillBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float DurationTime = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    float delaytime = 0;

    public EnemySkillChaseState(EnemySkillStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
        stateMachine.AnimatorCompo.CrossFadeInFixedTime(LocomotionHash, DurationTime);
    }


    public override void Update(float deltaTime)
    {
        delaytime += deltaTime;
        if (stateMachine.SkillCoolTime <= delaytime)
        {
            delaytime = 0;
            stateMachine.SwitchState(new EnemySkillJumpState(stateMachine));
        }

        MoveToPlayer(deltaTime);

        FacePlayer();

        if (!IsinChaseRnage() && stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.SwitchState(new EnemySkillIdleState(stateMachine));
            return;
        }
        else if (IsInAttackRange() && stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.SwitchState(new EnemySkillAttackState(stateMachine));
        }

        stateMachine.AnimatorCompo.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
    }
    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }

    private void MoveToPlayer(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = stateMachine.Player.transform.position;

            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);

            stateMachine.Agent.velocity = stateMachine.EnemyController.velocity;
        }
    }

    private bool IsInAttackRange()
    {
        if (stateMachine.Player.IsDead) return false;

        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
    }
}
