using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillDeadState : EnemySkillBaseState
{
    public EnemySkillDeadState(EnemySkillStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ChangeToRagdoll(true);
        stateMachine.WeaponDamage.gameObject.SetActive(false);
        GameMananegr.Instance.target.Remove(stateMachine.TargetCompo);
        GameObject.Destroy(stateMachine.TargetCompo);
    }

    public override void Exit()
    {

    }

    public override void Update(float deltaTime)
    {

    }
}
