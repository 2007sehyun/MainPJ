using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float DurationTime = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

        stateMachine.AnimatorCompo.CrossFadeInFixedTime(LocomotionHash, DurationTime);
    }
    public override void Update(float deltaTime)
    {
        Move(deltaTime);

        if (IsinChaseRnage())
        {
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
            return; 
        }

        FacePlayer();
        stateMachine.AnimatorCompo.SetFloat(SpeedHash, 0 , AnimatorDampTime, deltaTime);

    }

    public override void Exit()
    {

    }

}
