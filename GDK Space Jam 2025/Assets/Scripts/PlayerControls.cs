using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public int playerNum;

    private InputSystem_Actions _actions;

    private void Awake()
    {
        _actions = new();
    }

    public void SetUp()
    {
        InputAction move;
        InputAction shoot;

        if (playerNum == 1)
        {
            move = _actions.Player.MoveP1;
            shoot = _actions.Player.AttackP1;
            gameObject.layer = 7;
            PlayerMovement pMovement = GetComponent<PlayerMovement>();
            pMovement.InitControls(move, shoot);
        }
        else if (playerNum == 2)
        {
            move = _actions.Player.MoveP2;
            shoot = _actions.Player.AttackP2;
            gameObject.layer = 8;
            PlayerMovement pMovement = GetComponent<PlayerMovement>();
            pMovement.InitControls(move, shoot);
        }
    }
}
