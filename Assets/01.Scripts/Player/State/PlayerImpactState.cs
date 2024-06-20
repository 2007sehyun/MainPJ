using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float ImpactdurationTime = 0.1f;

    private float duration = 1f;
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.AnimatorCompo.CrossFadeInFixedTime(ImpactHash, ImpactdurationTime);
    }
    public override void Update(float deltaTime)
    {
        Move(deltaTime);
        duration -= deltaTime;
        if(duration <=0 )
        {
            ReTargeting();
        }
    }

    public override void Exit()
    {
    }
}
