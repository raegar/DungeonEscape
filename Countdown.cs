// ============================================================================
// COUNTDOWN - A reusable timer (plain C# class, not MonoBehaviour)
// Used by Player and Enemy for various cooldowns
// ============================================================================
public sealed class Countdown
{
    // Time between frames at roughly 60 FPS, shared by everything that ticks a timer
    public const float FrameTime = 0.016f;

    public float Duration { get; private set; }
    public float Remaining { get; private set; }
    public bool IsRunning => Remaining > 0;
    public float Progress => Duration > 0 ? 1f - (Remaining / Duration) : 1f;

    public Countdown(float duration)
    {
        Duration = Math.Max(0, duration);
        Remaining = 0;
    }

    public void Restart()
    {
        Remaining = Duration;
    }

    public void Stop()
    {
        Remaining = 0;
    }

    public bool Tick(float deltaTime)
    {
        if (!IsRunning) return false;

        Remaining -= deltaTime;
        if (Remaining <= 0)
        {
            Remaining = 0;
            return true; // Finished this frame
        }
        return false;
    }
}
