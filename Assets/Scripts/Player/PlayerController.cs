using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    // Movement variables
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.8f;
    [SerializeField]
    private float rotationSpeed = 5f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private PlayerInput playerInput;
    private Transform cameraTransform;

    // Player input actions
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction useAction;

    // QUIZ TESTING
    [SerializeField]
    private QuizManager quizManger;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        if (playerInput == null) {
            Debug.LogError("PlayerInput is missing!");
            return;
        }

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        useAction = playerInput.actions["Use"];
        cameraTransform = Camera.main.transform;
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable() {
        useAction.performed += _ => UseTool();
    }

    private void OnDisable() {
        useAction.performed -= _ => UseTool();
    }

    private void UseTool()
    {
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
        float rayLength = 500f;

        // actual Ray
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            // our Ray intersected a collider
            //Debug.Log(hit.transform.name);
        }
    }

    void Update()
    {
        // Check if player is grounded 
        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotate towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Quiz testing
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "quiz")
        {
            Debug.Log("Quiz");
        }
    }
}