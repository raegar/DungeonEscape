// ============================================================================
// SIMPLE CLASS - Room (Composition - contains Enemies and Items)
// ============================================================================
public class Room
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    private List<Enemy> enemies = new List<Enemy>();
    private List<Item> items = new List<Item>();

    public Room(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void DisplayRoom()
    {
        Console.WriteLine($"\n╔═══════════════════════════════╗");
        Console.WriteLine($"║  ROOM: {Name,-21}║");
        Console.WriteLine($"║  {Description,-27}║");
        Console.WriteLine($"╚═══════════════════════════════╝");

        Console.WriteLine($"\nEnemies in this room:");
        if (enemies.Count == 0)
        {
            Console.WriteLine("  (none)");
        }
        else
        {
            foreach (var enemy in enemies)
            {
                Console.Write("  ");
                enemy.PrintSprite();
                enemy.DisplayStats();
            }
        }

        Console.WriteLine($"\nItems in this room:");
        if (items.Count == 0)
        {
            Console.WriteLine("  (none)");
        }
        else
        {
            foreach (var item in items)
            {
                item.DisplayInfo();
            }
        }
    }

    public List<Enemy> GetEnemies()
    {
        return enemies;
    }

    public List<Item> GetItems()
    {
        return items;
    }
}
