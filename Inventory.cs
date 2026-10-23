// ============================================================================
// SEALED CLASS - Inventory (plain C# class, not MonoBehaviour)
// What the knight carries and rules for using items
// ============================================================================
public sealed class Inventory
{
    public int Coins { get; private set; } = 0;
    public int Potions { get; private set; } = 0;
    public bool HasKey { get; private set; } = false;

    // Add an item to inventory
    public void Add(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Coin:
                Coins++;
                break;
            case ItemType.Potion:
                Potions++;
                break;
            case ItemType.Key:
                HasKey = true;
                break;
        }
    }

    // Try to use a potion (returns success)
    public bool TryUsePotion()
    {
        if (Potions <= 0) return false;

        Potions--;
        return true;
    }

    // Display inventory contents
    public override string ToString()
    {
        return $"Coins: {Coins} | Potions: {Potions} | Key: {(HasKey ? "yes" : "no")}";
    }
}
