using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;       // L’AudioSource del player
    public AudioClip footstepClip;        

    [Header("Step Settings")]
    public float stepInterval = 0.35f;    //intervallo tra i passi
    public float pitchRandomness = 0.1f;  //suono casuale

    private TopDownMover2D mover;
    private float stepTimer;

    void Awake()
    {
        mover = GetComponent<TopDownMover2D>();
    }

    // void Update()
    // {
    //     Vector2 velocity = mover.GetVelocity();
    //     if (velocity.magnitude > 0.1f)
    //     {
    //         stepTimer -= Time.deltaTime;

    //         if (stepTimer <= 0f)
    //         {
    //             PlayFootstep();
    //             stepTimer = stepInterval;
    //         }
    //     }
    //     else
    //     {
    //         // Player fermo → reset del timer
    //         stepTimer = 0f;
    //     }
    // }

    // void PlayFootstep()
    // {
    //     audioSource.pitch = 1f + Random.Range(-pitchRandomness, pitchRandomness);
    //     audioSource.PlayOneShot(footstepClip);
    // }
}
