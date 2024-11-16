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
    [SerializeField] Vacuum vacuumObject;
    [SerializeField] GameObject startingScene;
    [SerializeField] CameraController cameraController;

    private Rigidbody2D playerBody;
    private PlayerInput playerInput;
    private InputAction moveAction, suckAction, dropAction;

    private Vector2 moveDir = new Vector2(0, 0);

    PlayerStates playerState = PlayerStates.Idle;

    //Footsteps variables
    public TerrainType currentTerrain = TerrainType.Stone;
    public bool movementEnabled = true;

    [SerializeField] PlayerFootsteps playerFootsteps;
    float lastFootstepTime;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        suckAction = playerInput.actions["Suck"];
        dropAction = playerInput.actions["Drop"];
        lastFootstepTime = Time.time;
        playerBody.MovePosition(startingScene.transform.position);
        cameraController.MoveCameraTo(startingScene.transform.position);
    }

    void Update()
    {
        switch (playerState)
        {
            case PlayerStates.Idle:
                if (suckAction.IsPressed())
                {
                    playerState = PlayerStates.Suck;
                    vacuumObject.gameObject.SetActive(true);
                    break;
                }
                if (dropAction.IsPressed())
                {
                    playerState = PlayerStates.Drop;
                    break;
                }
                if (moveAction.ReadValue<Vector2>() != Vector2.zero)
                {
                    playerState = PlayerStates.Move;
                }
                break;
            case PlayerStates.Move:
                if (dropAction.IsPressed())
                {
                    playerState = PlayerStates.Drop;
                    break;
                }
                if (suckAction.IsPressed())
                {
                    playerState = PlayerStates.Suck;
                    vacuumObject.gameObject.SetActive(true);
                    break;
                }
                UpdateMove();
                break;
            case PlayerStates.Suck:
                if (!suckAction.IsPressed())
                {
                    playerState = PlayerStates.Idle;
                    vacuumObject.gameObject.SetActive(false);
                    break;
                }
                UpdateSuck();
                break;
            case PlayerStates.Drop:
                vacuumObject.gameObject.SetActive(true);
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
            if (Time.time - lastFootstepTime > moveSpeed / 15f)
            {
                playerFootsteps.PlayFootstepSound(currentTerrain);
                lastFootstepTime = Time.time;
            }
        }
        cameraController.MoveCameraTo(playerBody.position);
    }

    private void UpdateSuck()
    {
        //idk czy trzeba tu cos dawać tbh, wszystko chyba w vaccum.cs
    }
}
