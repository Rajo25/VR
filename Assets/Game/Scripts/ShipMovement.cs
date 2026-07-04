using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 4f;

    [Header("Limits")]
    [SerializeField] private float maxHorizontal = 4f;
    [SerializeField] private float maxVertical = 3f;

    private Vector3 startPosition;

    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool moveDown;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero;

        if (moveLeft)
            movement += Vector3.left;

        if (moveRight)
            movement += Vector3.right;

        if (moveUp)
            movement += Vector3.up;

        if (moveDown)
            movement += Vector3.down;

        transform.localPosition += movement.normalized * moveSpeed * Time.deltaTime;

        Vector3 pos = transform.localPosition;

        pos.x = Mathf.Clamp(pos.x,
            startPosition.x - maxHorizontal,
            startPosition.x + maxHorizontal);

        pos.y = Mathf.Clamp(pos.y,
            startPosition.y - maxVertical,
            startPosition.y + maxVertical);

        transform.localPosition = pos;
    }

    // LEFT
    public void StartMoveLeft() => moveLeft = true;
    public void StopMoveLeft() => moveLeft = false;

    // RIGHT
    public void StartMoveRight() => moveRight = true;
    public void StopMoveRight() => moveRight = false;

    // UP
    public void StartMoveUp() => moveUp = true;
    public void StopMoveUp() => moveUp = false;

    // DOWN
    public void StartMoveDown() => moveDown = true;
    public void StopMoveDown() => moveDown = false;
}