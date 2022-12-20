interface IAttack
{

    /// <summary>
    /// Used to attack an enemy. Best used with IDamagable
    /// </summary>
    void Attack();

    /// <summary>
    /// Function ran after finishing Attack(). Mainly for animation handling
    /// </summary>
    void FinishAttack();
}
