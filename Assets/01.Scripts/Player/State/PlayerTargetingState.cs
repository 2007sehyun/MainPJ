using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.inputReader.CancleEvent += OnCancle;

        stateMachine.AnimatorCompo.Play(TargetingBlendTreeHash);
    }

   

    public override void Update(float deltaTime)
    {
        Debug.Log(stateMachine.TargeterCompo.CurrentTarget.name);   
    }

    public override void Exit()
    {
        stateMachine.inputReader.CancleEvent -= OnCancle;
    }

    private void OnCancle()
    {
        stateMachine.TargeterCompo.Cancle();

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
}
