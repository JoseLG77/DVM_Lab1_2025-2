using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5;
    private Rigidbody2D rb;
    private Vector2 movement; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * speed;
    }
}
