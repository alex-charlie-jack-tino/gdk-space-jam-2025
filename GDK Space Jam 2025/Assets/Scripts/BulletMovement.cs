using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    // Bullet Stats
    public float bulletSpeed;
    public int bulletType;
    public int bounces;

    Rigidbody rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddRelativeForce(new Vector2(0,1) * bulletSpeed);
    }

    void FixedUpdate()
    {
        //transform.Translate(new Vector3(0, 1, 0));
        if (transform.position.z > Bounds.ArenaZRadius || transform.position.z < -Bounds.ArenaZRadius)
        {
            Vector3 newLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.SetPositionAndRotation(newLocation, transform.rotation);
        }
        if (transform.position.x > Bounds.ArenaXRadius || transform.position.x < -Bounds.ArenaXRadius)
        {
            Vector3 newLocation = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
            transform.SetPositionAndRotation(newLocation, transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.layer == 6)
       {
           if (bounces == 0)
                Destroy(this.gameObject);
           else
                bounces--;
       }
    }
}
