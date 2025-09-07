using UnityEngine;

public class TestSpawnScript : MonoBehaviour
{
    public GameObject ship;

    void Start()
    {
        GameObject newShip = Instantiate(ship, transform);
        PlayerControls shipScript = newShip.GetComponent<PlayerControls>();
        shipScript.Initialize(PlayerIndex.A);
    }
}
