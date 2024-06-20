using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ChangeToRagdoll(true);
        stateMachine.WeaponDamage.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.TargetCompo);
    }

    public override void Exit()
    {

    }

    public override void Update(float deltaTime)
    {

    }
}
