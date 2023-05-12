using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Jilibili
{
    public abstract class JilibiliState : StateMachine.State<Jilibili>
    {
        protected Jilibili jilibili;

        public JilibiliState(Jilibili entity, StateMachine<Jilibili> stateMachine) : base(entity, stateMachine)
        {
            jilibili = entity;
        }
    }
}