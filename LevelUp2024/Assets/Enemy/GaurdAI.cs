using UnityEngine;
public class GaurdAI : BaseEnemyAI
{
    public float attackRange;

    public override void PlayerDetection()
    {
        distanceFromPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceFromPlayer <= attackRange)
        {
            //attack animation
        }
    }

}