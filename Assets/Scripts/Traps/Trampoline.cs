using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 20f; // Lực bật lên
    private Animator animator;
    private AudioSource audioSource;
    private bool canBounce = true; // Ngăn chặn bật liên tục

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Lấy AudioSource từ GameObject
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canBounce)
        {
            if (other.CompareTag("Player"))
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null && rb.linearVelocity.y <= 0f) // Chỉ bật nếu Player đang rơi hoặc đứng yên
                {
                    animator.SetTrigger("TrampolineJump"); // Chạy animation bật nhảy
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce); // Bật nhân vật lên

                    if (audioSource != null)
                    {
                        audioSource.Play(); // Phát âm thanh
                    }

                    canBounce = false; // Chặn bật liên tục
                    Invoke(nameof(ResetAnimation), 0.5f); // Chờ animation kết thúc rồi reset
                }
            }
        }
    }

    private void ResetAnimation()
    {
        animator.Play("Trampoline_Idle"); // Đặt lại trạng thái animation về mặc định
        canBounce = true; // Cho phép bật lại
    }
}
