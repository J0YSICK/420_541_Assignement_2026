using UnityEngine;
using System.Collections;


// A coin that adds points to the score when the player touches it.
// Needs a collider with Is Trigger ticked on the same GameObject.
public class SpeedBoostPickup : MonoBehaviour
{

    [Header("Boost")]
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float duration = 5f;
    [SerializeField] private float respawnTime = 20f;

    [Header("Sounds")]
    [SerializeField] private AudioClip pickupSound;

    private Renderer[] renderers;
    private Collider col;


    // Called automatically when something enters this trigger
    private void OnTriggerEnter(Collider other)
    {
        // Only react to the player, ignore everything else
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            StartCoroutine(Boost(other.GetComponentInParent<PlayerMovement>()));
            StartCoroutine(Respawn());

            // Remove the coin from the scene: it's collected once and gone
            Destroy(gameObject);
        }
    }

    private IEnumerator Boost(PlayerMovement player){
        player.MultiplySpeed(speedMultiplier);
        yield return new WaitForSeconds(duration);
    }

    private IEnumerator Respawn(){
        SetVisible(false);
        yield return new WaitForSeconds(respawnTime);
        SetVisible(true);
    }

    private void SetVisible(bool visible){
        foreach(Renderer r in renderers){
            r.enabled = visible;
        }
        col.enabled = visible;
    }
}

