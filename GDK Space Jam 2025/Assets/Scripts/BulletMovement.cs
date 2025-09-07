using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private const int k_wallsLayer = 6;

    // Bullet Stats
    public float bulletSpeed;
    public int bulletType;
    public int remainingBounces;

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
            transform.position = newLocation;
        }
        if (transform.position.x > Bounds.ArenaXRadius || transform.position.x < -Bounds.ArenaXRadius)
        {
            Vector3 newLocation = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
            transform.position = newLocation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == k_wallsLayer)
        //{
        //    //if (gameObject.layer == collision.gameObject.layer && collision.gameObject.health)
        //    //    collision.gameObject.health--;
        //}
        if (collision.gameObject.tag == ("Player"))
                collision.gameObject.SendMessage("TakeDamage");
                Destroy(gameObject);
        if (collision.gameObject.layer == k_wallsLayer)
        {
            remainingBounces--;

            if (remainingBounces == 0)
                Destroy(gameObject);
        }
    }
}
