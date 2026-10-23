// ============================================================================
// DERIVED CLASS - Enemy (inherits from Character)
// Monsters that move by type, deal contact damage, may drop loot
// ============================================================================
public class Enemy : Character
{
    // Shared count of every enemy defeated so far
    public static int EnemiesDefeated = 0;

    // Enemy behavior
    public EnemyType Type { get; private set; }
    private Countdown wanderTimer;
    private Countdown hitTimer;
    private float currentDirection = 0f;

    // Combat
    private int touchDamage = 1;

    // Loot
    public int LootReward { get; private set; }

    public Enemy(string name, EnemyType type, int health, float moveSpeed, int loot)
        : base(name, health, moveSpeed, sprite: 'E')
    {
        Type = type;
        LootReward = loot;
        wanderTimer = new Countdown(1.5f);  // Pick new direction every 1.5s for slimes
        hitTimer = new Countdown(1.0f);    // Wait 1s between hits
    }

    // Abstract method - behaves based on type
    public override void Act()
    {
        if (!IsAlive) return;

        wanderTimer.Tick(0.016f);
        hitTimer.Tick(0.016f);

        // Movement based on type
        switch (Type)
        {
            case EnemyType.Slime:
                BehaveSlime();
                break;
            case EnemyType.Bat:
                BehaveBat();
                break;
            case EnemyType.Spider:
                BehaveSpider();
                break;
        }
    }

    private void BehaveSlime()
    {
        // Wander randomly
        if (!wanderTimer.IsRunning)
        {
            currentDirection = (float)(new Random().NextDouble() * Math.PI * 2);
            wanderTimer.Restart();
        }
    }

    private void BehaveBat()
    {
        // Always chase (would need Player reference in real code)
    }

    private void BehaveSpider()
    {
        // Guard until player in sight (would check distance in real code)
    }

    // Override die - drops loot
    protected override void Die()
    {
        Console.WriteLine($"💀 {CharacterName} has been defeated!");
        Console.WriteLine($"Dropped {LootReward} coins!");
        EnemiesDefeated++;
        base.Die();
    }

    public override void DisplayStats()
    {
        Console.WriteLine($"\n[ENEMY: {CharacterName}]");
        Console.WriteLine($"  Type: {Type}");
        Console.WriteLine($"  HP: {Health}/{MaxHealth}");
        Console.WriteLine($"  Touch Damage: {touchDamage} | Hit Cooldown: {hitTimer.Remaining:F1}s");
        Console.WriteLine($"  Loot: {LootReward} coins");
    }
}
