// ============================================================================
// Main Program - Demonstrates OOP Concepts
// ============================================================================
class Program
{
    static bool gameOver = false;
    static Player? player;

    static void Win()
    {
        gameOver = true;
        player?.DisableInput();
        Console.WriteLine("\n🏆 YOU WIN! Every monster in the lair is defeated.");
    }

    static void Lose()
    {
        gameOver = true;
        player?.DisableInput();
        Console.WriteLine("\n💀 YOU LOSE! The dungeon claims another adventurer.");
    }

    static void Check(Character c)
    {
        if (EnemiesDefeatedCount() == 3)
        {
            Win();
        }
    }

    static int EnemiesDefeatedCount()
    {
        return Enemy.EnemiesDefeated;
    }

    // Set everything up for a new game (also used by Replay)
    static void ResetGame()
    {
        gameOver = false;
        player = new Player("Adventurer");
        player.Died += c => Lose();
    }

    static void Main()
    {
        Console.WriteLine("╔════════════════════════════════════════════╗");
        Console.WriteLine("║   DUNGEON ESCAPE - OOP Demo                ║");
        Console.WriteLine("║   Demonstrating: Abstraction, Inheritance, ║");
        Console.WriteLine("║   Polymorphism, Composition, Encapsulation ║");
        Console.WriteLine("╚════════════════════════════════════════════╝\n");

        // ======== Create Player ========
        ResetGame();
        Player hero = player!;
        Console.WriteLine("Player created!");
        hero.PrintSprite();
        hero.DisplayStats();

        // ======== Create Items (ItemType enum) ========
        Item coin = new Item(ItemType.Coin, "Gold Coin", "Worth 1 coin");
        Item potion = new Item(ItemType.Potion, "Health Potion", "Restores 2 HP");
        Item key = new Item(ItemType.Key, "Rusty Key", "Opens the door");

        // Collect some items
        hero.Collect(coin);
        hero.Collect(potion);
        hero.Collect(potion);
        
        Console.WriteLine($"\nInventory: {hero.Inventory}");

        // ======== Create Room ========
        Room duntonRoom = new Room("Goblin's Lair", "Dark and spooky");

        // ======== Create Enemies (Polymorphism - different EnemyTypes) ========
        Enemy slime = new Enemy("Slime", EnemyType.Slime, health: 2, moveSpeed: 1.2f, loot: 50);
        Enemy bat = new Enemy("Bat", EnemyType.Bat, health: 1, moveSpeed: 2.2f, loot: 75);
        Ghost ghost = new Ghost("Phantom", health: 2, moveSpeed: 0.8f);

        slime.Died += Check;
        bat.Died += Check;
        ghost.Died += Check;

        duntonRoom.AddEnemy(slime);
        duntonRoom.AddEnemy(bat);
        duntonRoom.AddEnemy(ghost);
        duntonRoom.AddItem(new Item(ItemType.Coin, "Lost Coin", "Shiny!"));
        duntonRoom.AddItem(key);

        // ======== Display Room ========
        duntonRoom.DisplayRoom();

        // ======== Combat Demonstration ========
        Console.WriteLine("\n\n════════════════════════════════");
        Console.WriteLine("COMBAT DEMONSTRATION");
        Console.WriteLine("════════════════════════════════");

        // Hero attacks slime (polymorphism - Attack method)
        hero.Attack(slime);
        Console.WriteLine();
        slime.Act();

        hero.Heal(2);

        // Check if alive
        Console.WriteLine($"\nSlime alive: {slime.IsAlive}");
        Console.WriteLine($"Hero alive: {hero.IsAlive}");

        // Defeat the slime with another attack
        Console.WriteLine("\n--- Final attack! ---");
        hero.Attack(slime);
        hero.Attack(slime);

        if (!slime.IsAlive)
        {
            Console.WriteLine($"Hero can now collect the {slime.LootReward} coin loot!");
        }

        // Try drinking a potion
        Console.WriteLine("\n--- Using a potion ---");
        hero.DrinkPotion();

        // Try to ghost (attacks pass through)
        Console.WriteLine("\n--- Attacking the Ghost ---");
        hero.Attack(ghost);
        hero.Attack(ghost);

        // ======== Final Stats ========
        Console.WriteLine("\n\n════════════════════════════════");
        Console.WriteLine("FINAL STATE");
        Console.WriteLine("════════════════════════════════");
        hero.DisplayStats();
        slime.DisplayStats();
        ghost.DisplayStats();

        Console.WriteLine("\n✓ Demo Complete! This example demonstrates:");
        Console.WriteLine("  ★ Abstraction (Character class with Act() abstract method)");
        Console.WriteLine("  ★ Inheritance (Player, Enemy, Ghost inherit from Character)");
        Console.WriteLine("  ★ Polymorphism (Different Act() & TakeDamage implementations)");
        Console.WriteLine("  ★ Composition (Room owns Enemies/Items; Player owns Inventory)");
        Console.WriteLine("  ★ Encapsulation (Private fields, public get-only properties)");
        Console.WriteLine("  ★ Sealed Classes (Player, Ghost cannot be subclassed)");
        Console.WriteLine("  ★ Enums (ItemType, EnemyType for type-safe collections)");
    }
}
