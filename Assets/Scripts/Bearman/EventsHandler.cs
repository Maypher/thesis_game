using UnityEngine;
using System;

namespace CharacterEvents
{
    public class EventsHandler
    {
        public event Action AttackEvent;
        public void InvokeAttackEvent() => AttackEvent?.Invoke();

        public event Action ChargedAttackEvent;
        public void InvokeChargedAttackEvent() => ChargedAttackEvent?.Invoke();

        public event Action FinishAttackEvent;
        public void InvokeFinishAttackEvent() => FinishAttackEvent?.Invoke();
    }
}