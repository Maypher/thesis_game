interface IDamageable
{
    /// <summary>
    ///     Take damage. Use to reduce health. Returns bool that indicates if the hit killed the entity
    /// </summary>
    /// <param name="damagePt"> How much damage will be taken</param>
    public bool TakeDamage(int damagePt);

    /// <summary>
    /// Function ran to destroy the object
    /// </summary>
    public void Kill();
}
