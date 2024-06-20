using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class State 
{
    public abstract void Enter();
    public abstract void Update(float deltaTime);
    public abstract void Exit();

    protected float GetNormalizedTime(Animator AnimatorCompo)
    {
        AnimatorStateInfo currentInfo = AnimatorCompo.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = AnimatorCompo.GetNextAnimatorStateInfo(0);

        if (AnimatorCompo.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!AnimatorCompo.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
