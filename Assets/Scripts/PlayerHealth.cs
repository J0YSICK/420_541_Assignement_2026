using UnityEngine;

public class PlayerHeallth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    private int health;
    private Rigidbody rb;
    private Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Trap")){
            health--;

            if(health <=0){
                Restart();
            }
        }
    }

    private void Restart(){
        transform.position = startPos;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10f){
            Restart();
        }
    }
}
