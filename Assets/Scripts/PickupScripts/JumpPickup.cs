using UnityEngine;
using System.Collections;


// A coin that adds points to the score when the player touches it.
// Needs a collider with Is Trigger ticked on the same GameObject.
public class JumpPickup : MonoBehaviour
{

    [Header("Boost")]
    [SerializeField] private float duration = 30f;
    [SerializeField] private float respawnTime = 30f;

    [Header("Sounds")]
    [SerializeField] private AudioClip pickupSound;

    private Renderer[] renderers;
    private Collider col;

    private void Start(){
        renderers = GetComponentsInChildren<Renderer>();
        col = GetComponent<Collider>();
    }


    // Called automatically when something enters this trigger
    private void OnTriggerEnter(Collider other)
    {
        // Only react to the player, ignore everything else
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            StartCoroutine(Boost(other.GetComponentInParent<PlayerMovement>()));
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Boost(PlayerMovement player){
        player.SetDoubleJump(true);
        yield return new WaitForSeconds(duration);
        player.SetDoubleJump(false);
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

