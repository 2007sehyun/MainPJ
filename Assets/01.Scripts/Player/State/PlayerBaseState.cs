using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    protected void Move(float deltaTime) // Á¤Áö.
    {
        Move(Vector3.zero , deltaTime);
    }
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.charactorController.Move((motion + stateMachine.ForceCompo.Movement) * deltaTime);
    }

    protected void FaceTarget()
    {
        if (stateMachine.TargeterCompo.CurrentTarget == null) return;

        Vector3 lookPos = (stateMachine.TargeterCompo.CurrentTarget.transform.position
            - stateMachine.transform.position);

        lookPos.y = 0;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);

    }
    protected void ReTargeting()
    {
        if (stateMachine.TargeterCompo.CurrentTarget != null)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}
