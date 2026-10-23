// ============================================================================
// SIMPLE CLASS - Item (used in Inventory and as Pickup in the world)
// ============================================================================
public class Item
{
    public ItemType ItemType { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Item(ItemType type, string name, string description)
    {
        ItemType = type;
        Name = name;
        Description = description;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"  • {Name}: {Description}");
    }
}
