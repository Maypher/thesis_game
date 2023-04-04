using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerGrounded
{
    public PlayerMove(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
}
