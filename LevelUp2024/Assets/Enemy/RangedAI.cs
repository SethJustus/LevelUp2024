using UnityEngine;

public class RangedAI : BaseEnemyAI
{ 
    public float chaseSpeed;
    public float attackRange;

    public override void PlayerDetection()
    {
        distanceFromPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > attackRange)
        {
            //follow player
            //move animation
            Vector2 direction = (player.position - transform.position).normalized; //calculates the direction
            transform.position = Vector2.MoveTowards(transform.position, player.position,
                chaseSpeed * Time.deltaTime); // move towards player
            ;
        }

        if (distanceFromPlayer <= attackRange)
        {
            //attack animation
        }

    }
}


