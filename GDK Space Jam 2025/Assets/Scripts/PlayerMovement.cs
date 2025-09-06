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
    [SerializeField] float maxSpeed = 4f;
    // Actions
    InputAction move;
    InputAction shoot;

    InputSystem_Actions inputActions;

    Vector2 moveInput;
    Vector3 movement;
    Vector3 deltaVelocity;
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
        //screen wrap
        if (transform.position.z > 15 || transform.position.z < -15)
        {
            Vector3 newLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z*-1);
            transform.SetPositionAndRotation(newLocation, transform.rotation);
        }
        if (transform.position.x > 25 || transform.position.x < -25)
        {
            Vector3 newLocation = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
            transform.SetPositionAndRotation(newLocation, transform.rotation);
        }
        transform.Rotate(rotation);
        movement += deltaVelocity;
        float clampedMovement = Mathf.Clamp(movement.magnitude, 0, maxSpeed);
        movement = movement.normalized * clampedMovement;
        transform.Translate(movement, Space.World);
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        //read input
        moveInput = ctx.ReadValue<Vector2>();
        //Forward or Backward
        var moveSpeed = moveInput.y > 0 ? forwardSpeed : backSpeed;
        //calculate new velocity vector to existing velocity
        deltaVelocity = transform.up*moveInput.y*moveSpeed;
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
        //add new rotation to existing rotation
        rotation += new Vector3(moveInput.x * turnSpeed, 0, 0);
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
