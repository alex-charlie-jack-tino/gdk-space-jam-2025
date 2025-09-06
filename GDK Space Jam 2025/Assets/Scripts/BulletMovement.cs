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
        if (transform.position.y > 14 || transform.position.y < -14)
        {
            Vector3 newLocation = new Vector3(transform.position.x, transform.position.y * -1, transform.position.z);
            transform.SetPositionAndRotation(newLocation, transform.rotation);
        }
        if (transform.position.x > 29 || transform.position.x < -29)
        {
            Vector3 newLocation = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
            transform.SetPositionAndRotation(newLocation, transform.rotation);
        }
    }
}
