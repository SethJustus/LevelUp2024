using UnityEngine;

public class CowardAI : BaseEnemyAI
{
    public float speed;
    public float safeDistance; //distance to feel no need to run

    private bool isFleeing = false;

    public override void PlayerDetection()
    {
        distanceFromPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceFromPlayer <= lineOfSight)
        {
            isFleeing = true;
            //move animation
            Vector2 direction = (transform.position - player.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }

        if (isFleeing && distanceFromPlayer >= safeDistance)
        {
            //idle animation
            isFleeing = false;
        }
    }
}