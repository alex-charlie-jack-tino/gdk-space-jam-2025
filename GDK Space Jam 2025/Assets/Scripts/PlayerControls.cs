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
            move = InputSystem.actions.FindAction("MoveP1");
            shoot = InputSystem.actions.FindAction("AttackP1");
            this.gameObject.layer = 7;
            PlayerMovement pMovement = GetComponent<PlayerMovement>();
            pMovement.setControls(move, shoot);
        }
        else if (playerNum == 2)
        {
            move = InputSystem.actions.FindAction("MoveP2");
            shoot = InputSystem.actions.FindAction("AttackP2");
            this.gameObject.layer = 8;
            PlayerMovement pMovement = GetComponent<PlayerMovement>();
            pMovement.setControls(move, shoot);
        }
    }
}
