using UnityEngine;
using UnityEngine.InputSystem;

enum PlayerStates
{
    Idle,
    Move,
    Suck,
    Drop //wyrzucanie rzeczy
}

public class PlayerManager : MonoBehaviour
{
    [Header("Player variables")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float footstepsFrequency = 15f;
    [SerializeField] Vacuum vacuumObject;
    [SerializeField] Room startingRoom;
    [SerializeField] CameraController cameraController;

    private Rigidbody2D playerBody;
    private PlayerInput playerInput;
    private InputAction moveAction, suckAction, dropAction;

    //animator stuff
    private Animator _animator;
    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";


    public Vector2 moveDir = new Vector2(0, 0);

    PlayerStates playerState = PlayerStates.Idle;

    //Footsteps variables
    public TerrainType currentTerrain;
    public bool movementEnabled = true;

    [SerializeField] PlayerFootsteps playerFootsteps;
    float lastFootstepTime;

    private float dropCooldown = 0.3f;
    private float lastDropTime = 0;

    void Start()
    {
        _animator = GetComponent<Animator>();
        playerBody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        suckAction = playerInput.actions["Suck"];
        dropAction = playerInput.actions["Drop"];
        lastFootstepTime = Time.time;
        playerBody.MovePosition(Rooms.GetPosition(startingRoom));
        cameraController.MoveCameraTo(Rooms.GetPosition(startingRoom));
        currentTerrain = Rooms.GetTerrain(startingRoom);
    }

    void Update()
    {
        switch (playerState)
        {
            case PlayerStates.Idle:
                if (suckAction.IsPressed())
                {
                    playerState = PlayerStates.Suck;
                    break;
                }
                if (dropAction.IsPressed() && Time.time - lastDropTime > dropCooldown)
                {
                    lastDropTime = Time.time;
                    playerState = PlayerStates.Drop;
                    break;
                }
                if (moveAction.ReadValue<Vector2>() != Vector2.zero)
                {
                    playerState = PlayerStates.Move;
                }
                break;
            case PlayerStates.Move:
                if (dropAction.IsPressed() && Time.time - lastDropTime > dropCooldown)
                {
                    lastDropTime = Time.time;
                    playerState = PlayerStates.Drop;
                    break;
                }
                if (suckAction.IsPressed())
                {
                    playerState = PlayerStates.Suck;
                    break;
                }
                UpdateMove();
                break;
            case PlayerStates.Suck:
                if (!suckAction.IsPressed())
                {
                    playerState = PlayerStates.Idle;
                    vacuumObject.HideVacuum();
                    break;
                }
                UpdateSuck();
                break;
            case PlayerStates.Drop:
                if (transform.position.y > -10f) return;
                vacuumObject.DropObjects();
                playerState = PlayerStates.Idle;
                break;
            default:
                break;
        }


    }

    private void UpdateMove()
    {
        moveDir = movementEnabled ? moveAction.ReadValue<Vector2>() : Vector2.zero;
        if (moveDir != Vector2.zero)
        {
            Vector2 newPosition = playerBody.position + (moveDir * moveSpeed * Time.fixedDeltaTime);
            playerBody.MovePosition(newPosition);
            if (Time.time - lastFootstepTime > moveSpeed / footstepsFrequency)
            {
                playerFootsteps.PlayFootstepSound(currentTerrain);
                lastFootstepTime = Time.time;
            }
        }
        cameraController.MoveCameraTo(playerBody.position);

        _animator.SetFloat(_horizontal, moveDir.x);
        _animator.SetFloat(_vertical, moveDir.y);
    }

    private void UpdateSuck()
    {
        vacuumObject.SuckObjects();
    }
}
