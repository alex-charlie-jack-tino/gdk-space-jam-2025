using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipSelectControl : MonoBehaviour
{

    private InputSystem_Actions _actions;

    private InputAction moveP1;
    private InputAction moveP2;
    private InputAction selectP1;
    private InputAction selectP2;

    public GameObject p1Sticker;
    public int[] P1locations;
    public GameObject p2Sticker;
    public int[] P2locations;

    private RectTransform p1RT;
    private RectTransform p2RT;

    private int p1hovering = 0;
    private int p2hovering = 0;
    private bool p1CanInput = true;
    private bool p2CanInput = true;

    public GameObject[] prefabs;

    private void Awake()
    {
        _actions = new();
        _actions.Enable();
        _actions.Player.Enable();

        p1RT = p1Sticker.GetComponent<RectTransform>();
        p2RT = p2Sticker.GetComponent<RectTransform>();

        p1RT.anchoredPosition = new Vector2(P1locations[0], p1RT.anchoredPosition.y);
        p2RT.anchoredPosition = new Vector2(P2locations[0], p2RT.anchoredPosition.y);
    }

    private void OnEnable()
    {
        moveP1 = _actions.Player.MoveP1;
        selectP1 = _actions.Player.AttackP1;
        moveP2 = _actions.Player.MoveP2;
        selectP2 = _actions.Player.AttackP2;

        moveP1.performed += movep1;
        moveP2.performed += movep2;
        selectP1.performed += selectp1;
        selectP2.performed += selectp2;
    }

    private void OnDisable()
    {
        moveP1.performed -= movep1;
        moveP2.performed -= movep2;
        selectP1.performed -= selectp1;
        selectP2.performed -= selectp2;


        _actions.Player.Disable();
        _actions.Disable();
    }

    private void movep1(InputAction.CallbackContext ctx)
    {
        if (p1CanInput)
        {
            Vector2 context = ctx.ReadValue<Vector2>();
            if (context.x == 0)
                return; // Don't read W or S inputs

            if (context.x > 0)
            {
                if (p1hovering == 3)
                    p1hovering = 0;
                else
                    p1hovering++;
            }
            else if (context.x < 0)
            {
                if (p1hovering == 0)
                    p1hovering = 3;
                else
                    p1hovering--;
            }
            p1RT.anchoredPosition = new Vector2(P1locations[p1hovering], p1RT.anchoredPosition.y);
        }
    }

    private void movep2(InputAction.CallbackContext ctx)
    {
        if (p2CanInput)
        {
            Vector2 context = ctx.ReadValue<Vector2>();
            if (context.x == 0)
                return; // Don't read W or S inputs

            if (context.x > 0)
            {
                if (p2hovering == 3)
                    p2hovering = 0;
                else
                    p2hovering++;
            }
            else if (context.x < 0)
            {
                if (p2hovering == 0)
                    p2hovering = 3;
                else
                    p2hovering--;
            }
            p2RT.anchoredPosition = new Vector2(P2locations[p2hovering], p2RT.anchoredPosition.y);
        }
    }

    private void selectp1(InputAction.CallbackContext ctx)
    {
        if (p1CanInput)
        {
            StaticShipSelectHolder.p1Prefab = prefabs[p1hovering];
            Image p1img = p1Sticker.GetComponent<Image>();
            p1img.color = Color.cadetBlue;
            p1CanInput = false;
            confirm();
        }
    }

    private void selectp2(InputAction.CallbackContext ctx)
    {
        if (p2CanInput)
        {
            StaticShipSelectHolder.p2Prefab = prefabs[p2hovering];
            Image p2img = p2Sticker.GetComponent<Image>();
            p2img.color = Color.darkRed;
            p2CanInput = false;
            confirm();
        }
    }

    private void confirm()
    {
        if (!p2CanInput && !p1CanInput)
        {
            StartCoroutine(LoadRoutine());
        }
    }

    

    private IEnumerator LoadRoutine()
    {
        AsyncOperation sceneLoadOp = SceneManager.LoadSceneAsync("JackTestScene", LoadSceneMode.Single);

        while (!sceneLoadOp.isDone)
        {
            print(sceneLoadOp.progress);
            yield return null;
        }
    }
}
