// ============================================================================
// DERIVED CLASS - Player (sealed Character)
// The knight: touch controls, attack, potions, inventory
// ============================================================================
public sealed class Player : Character
{
    // Owns: Inventory and Countdown timers
    public Inventory Inventory { get; private set; }
    private Countdown invulnerabilityTimer;
    private Countdown attackTimer;

    // Attack properties
    public int attackDamage = 1;

    // Potion properties
    private int potionHealing = 2;

    public Player(string name)
        : base(name, maxHealth: 5, moveSpeed: 3f, sprite: '@')
    {
        Inventory = new Inventory();
        invulnerabilityTimer = new Countdown(1.0f);  // 1 second invulnerability
        attackTimer = new Countdown(0.4f);           // 0.4 second attack cooldown
    }

    // Abstract method implementation
    public override void Act()
    {
        // Tick timers each frame
        invulnerabilityTimer.Tick(Countdown.FrameTime);
        attackTimer.Tick(Countdown.FrameTime);
    }

    // Override to handle invulnerability
    public override void TakeDamage(int amount)
    {
        if (invulnerabilityTimer.IsRunning)
        {
            Console.WriteLine($"{CharacterName} is invulnerable!");
            return;
        }

        base.TakeDamage(amount);
        if (IsAlive)
        {
            invulnerabilityTimer.Restart();
        }
    }

    // Override die - don't call base.Die() (knight turns red, stays visible)
    protected override void Die()
    {
        Console.WriteLine($"☠️  {CharacterName} has fallen! Game Over.");
        RaiseDiedEvent();
    }

    // Collect an item in the world
    public void Collect(Item item)
    {
        Inventory.Add(item.ItemType);
        Console.WriteLine($"{CharacterName} collected: {item.Name}");
    }

    // Attack an enemy if cooldown allows
    public void Attack(Enemy target)
    {
        if (!IsAlive) return;
        if (attackTimer.IsRunning)
        {
            Console.WriteLine($"{CharacterName} is not ready to attack yet!");
            return;
        }

        Console.WriteLine($"{CharacterName} attacks {target.CharacterName}!");
        target.TakeDamage(attackDamage);
        attackTimer.Restart();
    }

    // Drink a potion
    public void DrinkPotion()
    {
        if (!IsAlive)
        {
            Console.WriteLine("Can't use potions while defeated!");
            return;
        }

        if (Health == MaxHealth)
        {
            Console.WriteLine($"{CharacterName} is already at full health!");
            return;
        }

        if (Inventory.TryUsePotion())
        {
            Heal(potionHealing);
            Console.WriteLine($"Potions remaining: {Inventory.Potions}");
        }
        else
        {
            Console.WriteLine("No potions left!");
        }
    }

    public override void DisplayStats()
    {
        Console.WriteLine($"\n╔════════════════════════╗");
        Console.WriteLine($"║  PLAYER STATS          ║");
        Console.WriteLine($"║  Name: {CharacterName,-15}║");
        Console.WriteLine($"║  HP: {Health,3}/{MaxHealth,-3}         ║");
        Console.WriteLine($"║  Invulnerable: {invulnerabilityTimer.IsRunning.ToString(),-3}    ║");
        Console.WriteLine($"║  Attack Ready: {(!attackTimer.IsRunning).ToString(),-3}      ║");
        Console.WriteLine($"║  Inventory:    ║");
        Console.WriteLine($"║    {Inventory}║");
        Console.WriteLine($"╚════════════════════════╝");
    }
}
