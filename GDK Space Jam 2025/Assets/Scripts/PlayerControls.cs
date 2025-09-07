using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public int playerNum;

    public InputAction move;
    public InputAction shoot;

    public void setUp()
    {
        if (playerNum == 1)
        {
            move = InputSystem.actions.FindAction("Move");
            shoot = InputSystem.actions.FindAction("Attack");
            this.gameObject.layer = 7;
            PlayerMovement pMovement = GetComponent<PlayerMovement>();
            pMovement.setControls(move, shoot);
        }
        else if (playerNum == 2)
        {
            Debug.Log("oh no this should not be happening");
        }
    }
}
