

public static class Enums
{
    /// <summary>
    /// Move Direction.
    /// </summary>
    public enum MoveDirection
    {
        Up = -2,
        Down = 2,
        Left = -1,
        Right = 1,
    }
    
    /// <summary>
    /// Game State.
    /// </summary>
    public enum StateGame
    {
        None = 0,
        Start = 1,
        Pause = 2,
        Playing = 3,
        Waiting = 4,
        GameOver = 5,
    }
}
