using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerIndex _playerIndex;
    public static int P1CollisionLayer => 7;
    public static int P2CollisionLayer => 8;

    // Player Stats
    public float forwardSpeed;
    public float backSpeed;
    public float turnSpeed;
    public int weaponType;
    public GameObject bulletP1, bulletP2;
    public int dashType;
    public bool holdToFire;
    public float cooldownTime;

    [SerializeField] private float _maxSpeed = 4f;

    public PlayerControls playerControls;
    
    // Actions
    private InputAction _move;
    private InputAction _shoot;

    Vector2 moveInput;
    Vector3 movement;
    Vector3 deltaVelocity;
    Vector3 deltaRotation;
    float currentSpeed;
    Vector3 rotation;
    bool onCooldown = false;
    Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        _move.performed -= Move;
        _move.canceled -= Move;
        _shoot.performed -= Shoot;
    }

    public void InitControls(InputAction newMove, InputAction newShoot, PlayerIndex index)
    {
        _playerIndex = index;

        _move = newMove;
        _shoot = newShoot;

        _move.performed += Move;
        _move.canceled += Move;
        _shoot.performed += Shoot;

        print($"Initialized. move={_move}, shoot={_shoot}");

        //if (weaponType == 4)
        //{
        //    _shoot.canceled += Shoot;
        //}
    }

    void FixedUpdate()
    {
        //screen wrap
        if (transform.position.z > Bounds.ArenaZRadius || transform.position.z < -Bounds.ArenaZRadius)
        {
            Vector3 newLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z*-1 + (transform.position.z < 0 ? -1 : 1));
            transform.SetPositionAndRotation(newLocation, transform.rotation);
        }
        if (transform.position.x > Bounds.ArenaXRadius || transform.position.x < -Bounds.ArenaXRadius)
        {
            Vector3 newLocation = new Vector3(transform.position.x * -1 + (transform.position.x < 0 ? -1 : 1), transform.position.y, transform.position.z);
            transform.SetPositionAndRotation(newLocation, transform.rotation);
        }
        rigidBody.AddRelativeForce(movement);
        rigidBody.AddRelativeTorque(rotation);
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        //read input
        moveInput = ctx.ReadValue<Vector2>();


        print($"move: {moveInput}");

        //Forward or Backward
        var moveSpeed = moveInput.y > 0 ? forwardSpeed : backSpeed;

        // Calculate force to add to ship
        movement = new Vector3(0, moveInput.y * moveSpeed, 0);

        //add new rotation to existing rotation
        rotation = new Vector3(0, 0, moveInput.x * turnSpeed);
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        print("shoot");

        if (!onCooldown)
        {
            GameObject newBullet = Instantiate(_playerIndex == PlayerIndex.A ? bulletP1 : bulletP2, transform.position, transform.rotation);

            if (cooldownTime != 0)
                StartCoroutine(BulletCooldown());
        }
    }

    IEnumerator BulletCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }
}
