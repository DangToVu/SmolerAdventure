using UnityEngine;
using System.Collections;

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
        if (other.CompareTag("Player")) 
        {
            anim.SetTrigger("Win");
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }

            
            StartCoroutine(QuitGameAfterDelay(5f));
        }
    }

    IEnumerator QuitGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
       
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
