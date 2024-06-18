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
        stateMachine.WeaponDamage.SetAttack(attack.Damage);
    }
    public override void Update(float deltaTime)
    {
        Move(deltaTime);


        FaceTarget();

        float normalizedTime = GetNormalizedTime();

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

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.AnimatorCompo.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.AnimatorCompo.GetNextAnimatorStateInfo(0);

        if (stateMachine.AnimatorCompo.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!stateMachine.AnimatorCompo.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

}
