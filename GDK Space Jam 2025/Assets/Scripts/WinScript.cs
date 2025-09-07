using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
    public TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (StaticShipSelectHolder.winPlayer == 1)
        {
            text.text = "Player 1 wins!";
        }
        else if (StaticShipSelectHolder.winPlayer == 2) {
            text.text = "Player 2 wins!";
        }
    }
}
