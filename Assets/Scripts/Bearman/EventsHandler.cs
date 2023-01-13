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

        public event Action ThrowRaccoon;
        public void InvokeThrowRaccoon() => ThrowRaccoon?.Invoke();

        public event Action Shockwave;
        public void InvokeShockwave() => Shockwave?.Invoke();
    }
}