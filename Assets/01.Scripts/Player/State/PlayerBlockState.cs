using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerBaseState
{
    private readonly int BlockHash = Animator.StringToHash("Block");

    private const float durationTime = 0.1f;
    public PlayerBlockState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.HealthCompo.SetIsBlock(true);
        stateMachine.AnimatorCompo.CrossFadeInFixedTime(BlockHash, durationTime);
    }

    public override void Update(float deltaTime)
    {
        Move(deltaTime);

        if(!stateMachine.inputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            return;
        }

        if(stateMachine.TargeterCompo.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.HealthCompo.SetIsBlock(false);
    }


}
