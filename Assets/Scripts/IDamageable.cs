public interface IDamageable<T>
{
    void TakeDamage(T damageAmount);
    void Dead();
    void DamageEffect();
    void DeathEffect();
}
