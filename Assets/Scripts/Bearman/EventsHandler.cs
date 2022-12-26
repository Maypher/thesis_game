using UnityEngine;
using System;

namespace CharacterEvents
{
    public class EventsHandler
    {
        public event Action<bool> WalkingEvent;
        public void InvokeWalkingEvent(bool isWalking) => WalkingEvent?.Invoke(isWalking);

        public event Action<bool> JumpEvent;
        public void InvokeJumpEvent(bool isAirborne) => JumpEvent?.Invoke(isAirborne);

        public event Action<bool> ChargeEvent;
        public void InvokeChargeEvent(bool isCharging) => ChargeEvent?.Invoke(isCharging);

        public event Action AttackEvent;
        public void InvokeAttackEvent() => AttackEvent?.Invoke();

        public event Action ChargedAttackEvent;
        public void InvokeChargedAttackEvent() => ChargedAttackEvent?.Invoke();

        public event Action FinishAttackEvent;
        public void InvokeFinishAttackEvent() => FinishAttackEvent?.Invoke();
    }
}