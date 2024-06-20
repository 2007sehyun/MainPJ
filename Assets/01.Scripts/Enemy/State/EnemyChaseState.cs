using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float DurationTime = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.AnimatorCompo.CrossFadeInFixedTime(LocomotionHash, DurationTime);
    }


    public override void Update(float deltaTime)
    {
        MoveToPlayer(deltaTime);

        FacePlayer();

        if (!IsinChaseRnage() && stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        else if (IsInAttackRange() && stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.SwitchState(new EnemyAttackState(stateMachine));
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
