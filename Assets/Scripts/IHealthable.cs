public interface IHealthable<T>
{
    int health { get; set; }
    int maxHealth { get; set; }

    void Regen(T regenAmount);
}