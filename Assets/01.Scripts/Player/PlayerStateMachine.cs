using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader inputReader { get; private set; }
    [field: SerializeField] public CharacterController charactorController { get; private set; }
    [field: SerializeField] public Animator AnimatorCompo { get; private set; }
    [field: SerializeField] public Targeter TargeterCompo { get; private set; }
    [field: SerializeField] public ForceReceiver ForceCompo { get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field: SerializeField] public Health HealthCompo{ get; private set; }
    [field: SerializeField] public RagDoll Ragdoll { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingMoveSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    public Transform MainCameraTransform  { get; private set; }
    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }



    private void OnEnable()
    {
        HealthCompo.OnTakeDamage += ChangeImpactState;
        HealthCompo.OnDie += ChangeDieState;
    }

    private void ChangeDieState()
    {
        SwitchState(new PlayerDeadState(this));
    }

    private void ChangeImpactState()
    {
        SwitchState(new PlayerImpactState(this));
    }

    private void OnDisable()
    {
        HealthCompo.OnTakeDamage -= ChangeImpactState;
        HealthCompo.OnDie -= ChangeDieState;
    }
}
