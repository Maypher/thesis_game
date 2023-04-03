interface IDamageable
{
    /// <summary>
    /// Function for entity to take damage
    /// </summary>
    /// <param name="attackDetails"></param>
    /// <returns>A bool that indicates if this was a killing hit</returns>
    public bool TakeDamage(AttackDetails attackDetails);

    /// <summary>
    /// Function ran to destroy the object
    /// </summary>
    public void Kill();
}
