using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 direction;
    private float speed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer actualColor;
    private SpriteRenderer actualShape;
    private SpriteRenderer newColor;
    private SpriteRenderer newShape;
    public LayerMask layerMask;
    private float distanciaRaycast = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        actualColor = GetComponent<SpriteRenderer>();
        actualShape = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        direction = new Vector2(x, y);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = direction.normalized * speed;

        if (direction != Vector2.zero)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distanciaRaycast, layerMask);

            if (hit.collider != null)
            {
                Debug.DrawRay(transform.position, direction * distanciaRaycast, Color.red);

                string info = "Detectado: " + hit.collider.gameObject.name +
                              ", Posición: " + hit.collider.transform.position +
                              ", Tag: " + hit.collider.tag;

                if (hit.collider.CompareTag("Color"))
                {
                    SpriteRenderer sr = hit.collider.GetComponent<SpriteRenderer>();
                    info += ", Color: " + sr.color;
                }
                else if (hit.collider.CompareTag("Shape"))
                {
                    SpriteRenderer sr = hit.collider.GetComponent<SpriteRenderer>();
                    info += ", Sprite: " + sr.sprite.name;
                }

                Debug.Log(info);
            }
            else
            {
                Debug.DrawRay(transform.position, direction * distanciaRaycast, Color.green);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Color")
        {
            newColor = other.GetComponent<SpriteRenderer>();
            actualColor.color = newColor.color;
        }

        if (other.tag == "Shape")
        {
            newShape = other.GetComponent<SpriteRenderer>();
            actualShape.sprite = newShape.sprite;
        }
    }
}
