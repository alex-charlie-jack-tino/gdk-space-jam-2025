using UnityEngine;

public class TestSpawnScript : MonoBehaviour
{
    public GameObject ship;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject newShip = Instantiate(ship, this.transform);
        PlayerControls shipScript = newShip.GetComponent<PlayerControls>();
        shipScript.playerNum = 1;
        shipScript.setUp();
    }
}
