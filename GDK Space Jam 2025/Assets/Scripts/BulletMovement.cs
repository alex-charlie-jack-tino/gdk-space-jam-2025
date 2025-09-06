using UnityEngine;
using UnityEngine.Rendering;

public class BulletMovement : MonoBehaviour
{
    // Bullet Stats
    public float bulletSpeed;
    public int bulletType;
    public int bounces;

    Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddRelativeForce(new Vector2(0,1) * bulletSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(new Vector3(0, 0.5f, 0));
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
