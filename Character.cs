// ============================================================================
// ABSTRACT CLASS - Base class for all characters in the game
// Rules: Health stays between 0..MaxHealth; Died event raised once at 0 health
// ============================================================================
public abstract class Character
{
    // Events
    public event Action<Character>? Died;

    // Private fields (set in constructor)
    private string characterName;
    private int maxHealth;
    private float moveSpeed;
    private char sprite;

    // Public properties with encapsulation
    public int Health { get; private set; }
    public bool IsAlive => Health > 0;
    public string CharacterName => characterName;
    public int MaxHealth => maxHealth;
    public float MoveSpeed => moveSpeed;
    protected char Sprite => sprite;

    // Constructor
    protected Character(string name, int maxHealth, float moveSpeed, char sprite)
    {
        characterName = name;
        this.maxHealth = Math.Max(1, maxHealth); // At least 1
        this.moveSpeed = Math.Max(0, moveSpeed);
        this.sprite = sprite;
        Health = this.maxHealth;
    }

    // Abstract method - subclasses must implement what they do each frame
    public abstract void Act();

    // Abstract method - display character state
    public abstract void DisplayStats();

    // Public virtual method - subclasses can override
    public virtual void TakeDamage(int amount)
    {
        if (!IsAlive || amount <= 0) return;

        Health = Math.Max(0, Health - amount);
        Console.WriteLine($"{CharacterName} takes {amount} damage! Health: {Health}/{MaxHealth}");

        if (Health == 0)
        {
            Die();
        }
    }

    // Public method
    public void Heal(int amount)
    {
        if (!IsAlive || amount <= 0) return;

        Health = Math.Min(Health + amount, MaxHealth);
        Console.WriteLine($"{CharacterName} healed for {amount} HP! Current: {Health}/{MaxHealth}");
    }

    // Protected virtual - subclasses can override (e.g., Player shows corpse, Enemy drops loot)
    protected virtual void Die()
    {
        Console.WriteLine($"💀 {CharacterName} has been defeated!");
        RaiseDiedEvent();
    }

    protected void RaiseDiedEvent()
    {
        Died?.Invoke(this);
    }

    public void PrintSprite()
    {
        Console.WriteLine($"[{Sprite}]");
    }
}
