interface IDamageable
{
    /// <summary>
    ///     Take damage. Use to reduce health
    /// </summary>
    /// <param name="damagePt"> How much damage will be taken</param>
    public void TakeDamage(int damagePt);

    /// <summary>
    /// Function ran to destroy the object
    /// </summary>
    public void Kill();
}
