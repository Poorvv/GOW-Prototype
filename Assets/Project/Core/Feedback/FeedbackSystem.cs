using UnityEngine;

public class FeedbackSystem : MonoBehaviour
{

    [SerializeField] private GameObject hitVFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSFX;

    public void PlayHitFeedback(Vector3 position, Vector3 direction)
    {
        if(hitVFX != null)
        {
            GameObject vfx = Instantiate(hitVFX, position, Quaternion.LookRotation(direction));
            Destroy(vfx, 10f);
        }
        if(audioSource != null && hitSFX != null)
        {
            audioSource.PlayOneShot(hitSFX);
        }
        
    }
}
