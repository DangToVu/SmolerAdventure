using UnityEngine;

public class SpikeBallController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = Vector2.up;  // Ban đầu di chuyển theo chiều dọc
    }

    void FixedUpdate()
    {
        // Chỉ cho phép di chuyển theo trục Y
        rb.linearVelocity = new Vector2(0f, moveDirection.y * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Đảo ngược hướng Y khi va chạm
        moveDirection.y *= -1f;
    }
}
