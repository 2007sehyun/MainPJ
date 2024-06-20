using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerAttackState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;

    private Attack attack;
    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.AnimatorCompo.CrossFadeInFixedTime(attack.animationName, attack.TransitionDuration);
        stateMachine.WeaponDamage.SetAttack(attack.Damage , attack.Knockback);
    }
    public override void Update(float deltaTime)
    {
        Move(deltaTime);


        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.AnimatorCompo);

        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }
            if (stateMachine.inputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if(stateMachine.TargeterCompo.CurrentTarget!= null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
        previousFrameTime = normalizedTime;
    }


    public override void Exit()
    {
    }


    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) return;

        if (normalizedTime < attack.ComboAttackTime) return;

        stateMachine.SwitchState(
            new PlayerAttackState(
                stateMachine, attack.ComboStateIndex)
            );
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;

        stateMachine.ForceCompo.AddForce(stateMachine.transform.forward * attack.Force);

        alreadyAppliedForce = true; 
    }
}
