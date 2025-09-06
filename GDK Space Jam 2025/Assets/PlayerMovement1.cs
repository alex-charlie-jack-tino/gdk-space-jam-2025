using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public int forwardSpeed;
    public int backSpeed;
    public int turnSpeed;
    public int weaponType;
    public int dashType;
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

    // Update is called once per frame
    void Update()
    {
        if (keyboard.wKey.isPressed)
        {
            
            Vector3 forward = new Vector3(forwardSpeed, 0, 0);
            controller.Move(forward);
        }
    }


}
