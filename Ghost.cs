// ============================================================================
// SEALED CLASS - Ghost (Enemy subclass)
// A monster that fades; attacks pass through while faded
// ============================================================================
public sealed class Ghost : Enemy
{
    private float fadeSpeed = 0.4f;  // Fades per second
    private float alpha = 1f;

    public Ghost(string name, int health, float moveSpeed)
        : base(name, EnemyType.Bat, health, moveSpeed, loot: 0)  // No loot
    {
        alpha = 1f;
    }

    public override void Act()
    {
        if (!IsAlive) return;

        // Chase movement (reuses base behavior)
        base.Act();

        // Fade in and out with PingPong effect
        alpha += (float)Math.Sin(DateTime.Now.Millisecond / 1000f * fadeSpeed) * 0.5f;
        alpha = Math.Clamp(alpha, 0.15f, 1.0f);
    }

    // Override - when faded, attacks pass through
    public override void TakeDamage(int amount)
    {
        if (alpha < 0.5f)
        {
            Console.WriteLine($"💫 The attack passes straight through the {CharacterName}!");
            return;
        }

        base.TakeDamage(amount);
    }

    public override void DisplayStats()
    {
        Console.WriteLine($"\n[GHOST: {CharacterName}]");
        Console.WriteLine($"  HP: {Health}/{MaxHealth}");
        Console.WriteLine($"  Fade Level: {(alpha * 100):F0}% visible");
        Console.WriteLine($"  (No loot when defeated)");
    }
}
