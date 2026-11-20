
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float Movementspeed;
    [SerializeField] private Vector2 clamp;

    private Rigidbody rb;
    private GameManager gm;

    private Vector2 move;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
    }

    private void FixedUpdate()
    {
        if (gm != null && !gm.IsGameRunning)
            return;

        MovementHandler();
    }

    public void Move(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    private void MovementHandler()
    {
        Vector3 currentPosition = rb.position;

        Vector3 moveDirection = new Vector3(move.x, 0, move.y);

        Vector3 newPosition = currentPosition +
            moveDirection * Movementspeed * Time.fixedDeltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -clamp.x, clamp.x);
        newPosition.z = Mathf.Clamp(newPosition.z, -clamp.y, clamp.y);

        rb.MovePosition(newPosition);
    }
}
