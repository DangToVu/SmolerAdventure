using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;
    private AudioSource arrowAudioSource;

    private void Start()
    {
        arrowAudioSource = SoundManager.instance.GetComponent<AudioSource>();
    }

    private void Attack()
    {
        cooldownTimer = 0;

        // Check if the player is within range
        if (IsPlayerInRoom())
        {
            // Play the sound if it's not already playing
            if (!arrowAudioSource.isPlaying)
                SoundManager.instance.PlaySound(arrowSound);

            arrows[FindArrow()].transform.position = firePoint.position;
            arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
        }
        else
        {
            // Only stop sound when player leaves the room and the sound is playing
            if (arrowAudioSource.isPlaying)
                arrowAudioSource.Stop();
        }
    }

    private bool IsPlayerInRoom()
    {
        // Find player by tag or reference to the player object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;

        // Check if player is within a certain distance
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance < 20f; // Adjust distance threshold as needed
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}
