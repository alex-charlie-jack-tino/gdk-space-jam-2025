using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerIndex
{
    A,
    B
}

public class PlayerControls : MonoBehaviour
{
    private InputSystem_Actions _actions;

    private void Awake()
    {
        _actions = new();
        _actions.Player.Enable();
    }

    public void Initialize(PlayerIndex playerIndex)
    {
        InputAction move;
        InputAction shoot;

        PlayerMovement pMovement = GetComponent<PlayerMovement>();

        switch (playerIndex)
        {
            case PlayerIndex.A:
                move = _actions.Player.MoveP1;
                shoot = _actions.Player.AttackP1;
                gameObject.layer = 7;
                break;
            case PlayerIndex.B:
                move = _actions.Player.MoveP2;
                shoot = _actions.Player.AttackP2;
                gameObject.layer = 8;
                break;
            default:
                Debug.LogError("No such player index, only use A or B", this);
                return;
        }

        pMovement.InitControls(move, shoot, playerIndex);
    }

    private void OnDisable()
    {
        _actions.Player.Disable();
        _actions.Disable();
    }
}
