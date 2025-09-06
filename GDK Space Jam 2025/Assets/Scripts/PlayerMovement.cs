using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    // Player Stats
    public float forwardSpeed;
    public float backSpeed;
    public float turnSpeed;
    public int weaponType;
    public GameObject bullet;
    public int dashType;
    public bool holdToFire;

    // Actions
    InputAction move;
    InputAction shoot;

    InputSystem_Actions inputActions;

    Vector2 moveInput;
    Vector3 movement;
    float currentSpeed;
    Vector3 rotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Awake()
    {
        move = InputSystem.actions.FindAction("Move");
        shoot = InputSystem.actions.FindAction("Attack");
    }

    private void OnEnable()
    {
        move.performed += Move;
        move.canceled += Move;
        shoot.performed += Shoot;
        if (weaponType == 4)
        {
            shoot.canceled += Shoot;
        }
    }

    private void OnDisable()
    {
        move.performed -= Move;
        move.canceled -= Move;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y > 14 || transform.position.y < -14)
        {
            Vector3 newLocation = new Vector3(transform.position.x, transform.position.y * -1, transform.position.z);
            transform.SetPositionAndRotation(newLocation, transform.rotation);
        }
        if (transform.position.x > 29 || transform.position.x < -29)
        {
            Vector3 newLocation = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
            transform.SetPositionAndRotation(newLocation, transform.rotation);
        }
        transform.Rotate(rotation);
        transform.Translate(movement, Space.World);
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        var moveSpeed = moveInput.y > 0 ? forwardSpeed : backSpeed;
        movement += transform.up*moveInput.y*moveSpeed;
        /*
        if (movement.x > 0)
        {
            currentSpeed = forwardSpeed;
        }
        else
        {
            currentSpeed = backSpeed;
        }
        */
        rotation += new Vector3(0, 0, moveInput.x * turnSpeed);
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
