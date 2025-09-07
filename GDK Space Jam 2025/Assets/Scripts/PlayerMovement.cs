using System.Collections;
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
    public float cooldownTime;

    [SerializeField] float maxSpeed = 4f;

    public PlayerControls playerControls;
    
    // Actions
    InputAction move;
    InputAction shoot;

    InputSystem_Actions inputActions;

    Vector2 moveInput;
    Vector3 movement;
    Vector3 deltaVelocity;
    Vector3 deltaRotation;
    float currentSpeed;
    Vector3 rotation;
    bool onCooldown = false;
    Rigidbody rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Awake()
    {

        
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        move.performed -= Move;
        move.canceled -= Move;
        shoot.performed -= Shoot;
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
        rigidbody.AddRelativeForce(movement);
        rigidbody.AddRelativeTorque(rotation);
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        
        //read input
        moveInput = ctx.ReadValue<Vector2>();
        //Forward or Backward
        var moveSpeed = moveInput.y > 0 ? forwardSpeed : backSpeed;

        // Calulate force to add to ship
        movement = new Vector3(0, moveInput.y * moveSpeed, 0);

        //add new rotation to existing rotation
        rotation = new Vector3(0, 0, moveInput.x * turnSpeed);
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        if (!onCooldown)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.layer = 9 - playerControls.playerNum;

            if (cooldownTime != 0)
                StartCoroutine("bulletCooldown");
        }
    }

    IEnumerator bulletCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }

    public void setControls(InputAction newMove, InputAction newShoot)
    {
        move = newMove;
        shoot = newShoot;

        move.performed += Move;
        move.canceled += Move;
        shoot.performed += Shoot;

        if (weaponType == 4)
        {
            shoot.canceled += Shoot;
        }
    }
}
