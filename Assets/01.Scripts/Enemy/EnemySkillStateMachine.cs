using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySkillStateMachine : StateMachine
{
    [field: SerializeField] public Animator AnimatorCompo { get; private set; }
    [field: SerializeField] public CharacterController EnemyController { get; private set; }
    [field: SerializeField] public ForceReceiver ForeceCompo { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field: SerializeField] public WeaponHandler WeaponHandle{ get; private set; }
    [field: SerializeField] public Health HealthCompo { get; private set; }
    [field: SerializeField] public Target TargetCompo { get; private set; }
    [field: SerializeField] public RagDoll Ragdoll { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float JumpSpeed { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float SkillCoolTime { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public int SkillkDamage { get; private set; }
    [field: SerializeField] public float AttackKnockback { get; private set; }

    public Health Player { get; private set; }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        SwitchState(new EnemySkillIdleState(this));
    }

    private void OnEnable()
    {
        HealthCompo.OnTakeDamage += ChangeImpactState;
        HealthCompo.OnDie += ChangeDeadState;
    }

    private void ChangeDeadState()
    {
        SwitchState(new EnemySkillDeadState(this));
    }

    private void ChangeImpactState()
    {
        SwitchState(new EnemySkillImpactState(this));
    }

    private void OnDisable()
    {
        HealthCompo.OnTakeDamage -= ChangeImpactState;
        HealthCompo.OnDie -= ChangeDeadState;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }

}

