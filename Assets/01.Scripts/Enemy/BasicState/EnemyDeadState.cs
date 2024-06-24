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
