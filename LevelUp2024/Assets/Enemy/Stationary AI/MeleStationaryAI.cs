using UnityEngine;

public class MeleStationayAI : MonoBehaviour
{
    // this is for mele weapon. If its bow and arrow, we can change the line of sight

    Rigidbody2D rb;
    public float lineOfSight = 2f; //detectiona radius to know if player is close to it
    bool playerInRange = false;
    public Transform player;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector2.Distance(transform.position, player.position);
        if(playerDistance < lineOfSight )
        {
            Debug.Log("PLAYER IN RANGE");
            playerInRange = true;
            //attack animation
        }

        else
        {
            Debug.Log("PLAYER NOT IN RANGE");
            playerInRange = false;
        }
    }
}
