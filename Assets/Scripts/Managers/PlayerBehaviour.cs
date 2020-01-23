using OneDevApp;
using UnityEngine;
using static Enums;

public class PlayerBehaviour : Singleton<PlayerBehaviour>
{
    // ======================== Variables ======================== //

    #region Variables

    /// <summary>
    /// Snake Head
    /// </summary>
    [SerializeField] private TailingObject SnakeHead;
    /// <summary>
    /// Current move direction
    /// </summary>
    private MoveDirection _currentDirection;
    /// <summary>
    /// Last move direction
    /// </summary>
    private MoveDirection _lastDirection;
    /// <summary>
    /// Game Speed
    /// </summary>
    [SerializeField] private float _gameSpeed;
    /// <summary>
    /// Snake length
    /// </summary>
    int snakeLength = 1;
    /// <summary>
    /// Time till next move
    /// </summary>
    private float _timeUntilNextMove;

    #endregion

    // ======================== Functional ====================== //

    #region Functional 

    /// <summary>
    ///     Moves the snake up by one field.
    /// </summary>
    public void MoveUp()
    {
        Vector3 position = GetPosition();
        MoveTo(new Vector3(position.x, 0.5f, position.z + 1f));
    }

    /// <summary>
    ///     Moves the snake down by one field.
    /// </summary>
    public void MoveDown()
    {
        Vector3 position = GetPosition();
        MoveTo(new Vector3(position.x, 0.5f, position.z - 1f));
    }

    /// <summary>
    ///     Moves the snake left by one field.
    /// </summary>
    public void MoveLeft()
    {
        Vector3 position = GetPosition();
        MoveTo(new Vector3(position.x - 1f, 0.5f, position.z));
    }

    /// <summary>
    ///     Moves the snake right by one field.
    /// </summary>
    public void MoveRight()
    {
        Vector3 position = GetPosition();
        MoveTo(new Vector3(position.x + 1f, 0.5f, position.z));
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameReady()) return;

        if (GameManager.Instance.IsGameEnd())
        {
            return;
        } 

        MoveDirection wantedDirection = _currentDirection;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            wantedDirection = MoveDirection.Left;
        }
        else
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            wantedDirection = MoveDirection.Right;
        }
        else
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            wantedDirection = MoveDirection.Up;
        }
        else
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            wantedDirection = MoveDirection.Down;
        }

        if (snakeLength > 1)
        {
            // If adding a move direction from another is 0 the move
            // directions are opposite to each other.
            // We are checking this since we want to avoid the player to
            // randomly loose by pressing the opposite direction key during
            // a long game.
            int directionCheck = (int)_lastDirection + (int)wantedDirection;
            if (directionCheck != 0)
            {
                _currentDirection = wantedDirection;
            }
            else
            {
                _currentDirection = _lastDirection;
            }
        }
        else
        {
            _currentDirection = wantedDirection;
        }

        _timeUntilNextMove -= Time.deltaTime;

        if (_timeUntilNextMove > 0)
            return;

        _timeUntilNextMove = _gameSpeed;

        Move(_currentDirection);

        _lastDirection = _currentDirection;
    }

    #endregion

    // ======================= Helper ======================== //

    #region Helper
        
    /// <summary>
    ///     Player Movement.
    /// </summary>
    public void Move(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Left:
                MoveLeft();
                break;
            case MoveDirection.Up:
                MoveUp();
                break;
            case MoveDirection.Right:
                MoveRight();
                break;
            case MoveDirection.Down:
                MoveDown();
                break;
        }
    }

    /// <summary>
    ///     Moves the snake to the given position.
    /// </summary>
    public void MoveTo(Vector3 position)
    {
        SnakeHead.MoveTo(position);
    }

    /// <summary>
    ///     Adds a new tail to this snake.
    /// </summary>
    public void AddTail()
    {
        snakeLength++;
        TailingObject tail = SnakeHead.AddTail();
        tail.transform.SetParent(transform);
    }

    /// <summary>
    ///     Returns the current (head) posiiton of this snake.
    /// </summary>
    public Vector3 GetPosition()
    {
        return SnakeHead.transform.position;
    }

    #endregion
}
