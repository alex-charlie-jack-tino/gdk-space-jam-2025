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
    Vector2 movement;
    float currentSpeed;
    Vector3 rotation;

    private CharacterController controller;
    private UnityEngine.InputSystem.Keyboard keyboard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyboard = UnityEngine.InputSystem.Keyboard.current;
        controller = gameObject.AddComponent<CharacterController>();
        if (keyboard == null)
            return;
        forwardSpeed = 5;
        backSpeed = 5;
        turnSpeed = 5;

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
        
        if (keyboard.wKey.isPressed)
        {
            Vector3 forward = new Vector3(forwardSpeed, 0, 0);
            controller.Move(forward);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(movement * currentSpeed);
        transform.Rotate(rotation);
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        
        movement = new Vector2(0, moveInput.y);
        if (movement.x > 0)
        {
            currentSpeed = forwardSpeed;
        }
        else
        {
            currentSpeed = backSpeed;
        }

        rotation = new Vector3(0, 0, moveInput.x * turnSpeed);
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
