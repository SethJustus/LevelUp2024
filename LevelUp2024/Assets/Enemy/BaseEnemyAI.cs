using UnityEngine;


public abstract class BaseEnemyAI : MonoBehaviour
{
   
    public Transform player;
    public float health;
    public float lineOfSight; // from how far the AI will detect the player
    protected float distanceFromPlayer; //calculates the distance b/w player and AI

    protected Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        PlayerDetection();
    }

    public abstract void PlayerDetection();

}


