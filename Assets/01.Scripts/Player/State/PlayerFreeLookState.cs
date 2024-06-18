using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");

    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float AnimatorDampTime = 0.1f;

    private const float CrossFadeDuration = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.inputReader.TargetEvent += OnTarget;

        stateMachine.AnimatorCompo.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
    }

    public override void Update(float deltaTime)
    {

        if (stateMachine.inputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0));
            return;
        }

        Vector3 movement = CalculateMovement();
        
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        if (stateMachine.inputReader.MovementValue == Vector2.zero)
        {
            stateMachine.AnimatorCompo.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }
        stateMachine.AnimatorCompo.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
        CalculateRotate(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.inputReader.TargetEvent -= OnTarget;
    }

    private void OnTarget()
    {
        if (!stateMachine.TargeterCompo.SelectTarget()) return;
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private void CalculateRotate(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation,
            Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.transform.forward;
        Vector3 right = stateMachine.MainCameraTransform.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.inputReader.MovementValue.y + right * stateMachine.inputReader.MovementValue.x;
    }
}
