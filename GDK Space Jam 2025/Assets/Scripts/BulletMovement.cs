using UnityEngine;
using UnityEngine.Rendering;

public class BulletMovement : MonoBehaviour
{
    // Bullet Stats
    public float bulletSpeed;
    public int bulletType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 1, 0));
    }
}
