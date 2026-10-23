// ============================================================================
// SIMPLE CLASS - Pickup (represents an item in the world)
// In a real game, this would be a MonoBehaviour component
// ============================================================================
public class Pickup
{
    public Item Item { get; private set; }
    public string Location { get; private set; }

    public Pickup(Item item, string location)
    {
        Item = item;
        Location = location;
    }

    public void Display()
    {
        Console.WriteLine($"  {Item.Name} at {Location}");
    }
}
