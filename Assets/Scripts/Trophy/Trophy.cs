using UnityEngine;

public class Trophy : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // When player touch the trophy
        {
            anim.SetTrigger("Win"); 
            if (audioSource != null && !audioSource.isPlaying) 
            {
                audioSource.Play(); 
            }
        }
    }
}
