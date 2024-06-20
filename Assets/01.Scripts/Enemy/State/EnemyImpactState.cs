using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float ImpactdurationTime = 0.1f;

    private float duration = 1f;
    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.AnimatorCompo.CrossFadeInFixedTime(ImpactHash , ImpactdurationTime);
    }
    public override void Update(float deltaTime)
    {
        Debug.Log(stateMachine.Agent.isOnNavMesh);
        Move(deltaTime);
        duration -= deltaTime;
        if (duration <=0f)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }
    public override void Exit()
    {

    }
}
