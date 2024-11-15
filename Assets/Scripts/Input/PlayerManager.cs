using UnityEngine;
using UnityEngine.InputSystem;

enum PlayerStates
{
    Idle,
    Move,
    Suck
}

public class PlayerManager : MonoBehaviour
{
    [Header("Player variables")]
    [SerializeField] float moveSpeed = 5f;

    private Rigidbody2D rb;

    private PlayerInput playerInput;
    private InputAction moveAction;

    private Vector2 moveDir = new Vector2(0, 0);

    PlayerStates playerState = PlayerStates.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

    void Update()
    {
        switch (playerState)
        {
            case PlayerStates.Idle:
                if (moveAction.ReadValue<Vector2>() != Vector2.zero)
                {
                    playerState = PlayerStates.Move;
                }
                break;
            case PlayerStates.Move:
                UpdateMove();
                break;
            default:
                break;
        }
    }

    private void UpdateMove()
    {
        moveDir = moveAction.ReadValue<Vector2>();
        if (moveDir != Vector2.zero)
        {
            Vector2 newPosition = rb.position + (moveDir * moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }
    }
}
