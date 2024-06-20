using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetRightHash = Animator.StringToHash("TargetingRight");

    private const float CrossFadeDuration = 0.1f;

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.inputReader.CancleEvent += OnCancel;

        stateMachine.AnimatorCompo.CrossFadeInFixedTime(TargetingBlendTreeHash,CrossFadeDuration);
    }



    public override void Update(float deltaTime)
    {
        if(stateMachine.inputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine , 0));
            return;
        }
        if(stateMachine.inputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockState(stateMachine));
            return;
        }
        if (stateMachine.TargeterCompo.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.TargetingMoveSpeed, deltaTime);

        UpdateAnimator(deltaTime);

        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.inputReader.CancleEvent -= OnCancel;
    }

    private void OnCancel()
    {
        stateMachine.TargeterCompo.Cancel();

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.inputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.inputReader.MovementValue.y;

        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.inputReader.MovementValue.y == 0)
        {
            stateMachine.AnimatorCompo.SetFloat(TargetForwardHash, 0, 0.1f , deltaTime);
        }
        else
        {
            float value = stateMachine.inputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.AnimatorCompo.SetFloat(TargetForwardHash, 1, 0.1f, deltaTime);
        }

        if (stateMachine.inputReader.MovementValue.x == 0)
        {
            stateMachine.AnimatorCompo.SetFloat(TargetRightHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.inputReader.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.AnimatorCompo.SetFloat(TargetRightHash, value, 0.1f, deltaTime);
        }
    }
}
