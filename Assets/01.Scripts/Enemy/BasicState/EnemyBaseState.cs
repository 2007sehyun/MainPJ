using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;   
    }
   
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero , deltaTime);
    }

    protected void Move(Vector3 vector, float deltaTime)
    {
          stateMachine.EnemyController.Move((vector + stateMachine.ForeceCompo.Movement) * deltaTime);
    }

    protected bool IsinChaseRnage()
    {
        if (stateMachine.Player.IsDead) return false;

        float playerDisstanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDisstanceSqr < stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;    
    }

    protected void FacePlayer()
    {
        if (stateMachine.Player == null) return;

        Vector3 lookPos = stateMachine.Player.transform.position - stateMachine.transform.position;
        lookPos.y = 0;
        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

}
