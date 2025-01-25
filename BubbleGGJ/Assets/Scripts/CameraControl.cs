using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;

    [SerializeField] private float moveSpeed;

    private CharacterController controller;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

#if ENABLE_INPUT_SYSTEM
    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    private void Update()
    {
        Move();
    }
#endif

    private void Move()
    {
        Vector3 correctedVector = Quaternion.Euler(0, -45, 0) * new Vector3(move.x, 0, move.y);
        controller.Move(correctedVector * (moveSpeed * Time.deltaTime));
    }
}

