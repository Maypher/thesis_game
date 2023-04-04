using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGrounded : PlayerState
{
    protected PlayerGrounded(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
}
