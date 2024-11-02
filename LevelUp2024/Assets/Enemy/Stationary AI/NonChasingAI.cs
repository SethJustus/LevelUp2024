using UnityEngine;

public class NonChasingAI : BaseEnemyAI
{
    //explaining Non-Chasing, because it can be confusing with name
    //If u get within target area, it will come near and attack you.
    //But if you get away from that target area, it wont chase u, unless u get back to target area.

    public float chaseSpeed;
    public float attackRange;
    public Collider2D targetAreaRange; //ai will only chase within target area


    public override void PlayerDetection()
    {
        distanceFromPlayer = Vector2.Distance(transform.position, player.position);

        if (targetAreaRange.bounds.Contains(player.position))
        {
            //chases player
            //move animation
            //move animation
            Vector2 direction = (player.position - transform.position).normalized; //calculates the direction
            transform.position = Vector2.MoveTowards(transform.position, player.position,
                chaseSpeed * Time.deltaTime); // move towards player
        }

        else
        {
            //idle animation
           
        }

        if (distanceFromPlayer <= attackRange)
        {
            //attack animation
  
        }
    }
}

