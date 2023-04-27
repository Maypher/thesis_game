interface IDamageable
{
    /// <summary>
    /// Function for entity to take damage
    /// </summary>
    /// <param name="attackDetails"></param>
    public void TakeDamage(AttackDetails attackDetails);

    /// <summary>
    /// Function ran to destroy the object
    /// </summary>
    public void Kill();
}
