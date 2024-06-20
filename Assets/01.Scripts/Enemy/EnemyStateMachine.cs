using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator AnimatorCompo { get; private set; }
    [field: SerializeField] public CharacterController EnemyController { get; private set; }
    [field: SerializeField] public ForceReceiver ForeceCompo { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field: SerializeField] public Health HealthCompo { get; private set; }
    [field: SerializeField] public Target TargetCompo { get; private set; }
    [field: SerializeField] public RagDoll Ragdoll { get; private set; }
    [field: SerializeField] public float MovementSpeed{ get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage{ get; private set; }
    [field: SerializeField] public float AttackKnockback { get; private set; }

    public Health Player { get; private set; }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
    }

    private void OnEnable()
    {
        HealthCompo.OnTakeDamage += ChangeImpactState;
        HealthCompo.OnDie += ChangeDeadState;
    }

    private void ChangeDeadState()
    {
        SwitchState(new EnemyDeadState(this));
    }

    private void ChangeImpactState()
    {
        SwitchState(new EnemyImpactState(this));
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
