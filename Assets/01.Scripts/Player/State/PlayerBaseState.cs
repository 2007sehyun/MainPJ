using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}
