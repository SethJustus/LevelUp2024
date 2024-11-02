using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportingAI : BaseEnemyAI
{
    public float attackRange;
    public float teleportingRange; //distance from player
    public float teleportingDelay; //stay before teleporting
    public float attackDelay; //delay before attacking the player
    private bool isAttacking = false;

    private void Start()
    {
        StartCoroutine(TeleportAndAttack());
    }
    public override void PlayerDetection()
    {
        distanceFromPlayer = Vector2.Distance(transform.position, player.position);
    }
    private IEnumerator TeleportAndAttack()
    {
        while (true)
        {
            PlayerDetection();

            if (distanceFromPlayer <= teleportingRange)
            {
                Teleport();
                yield return new WaitForSeconds(teleportingDelay);

                if (distanceFromPlayer <= attackRange)
                {
                    Attack();
                    yield return new WaitForSeconds(attackDelay);
                }
            
               }

            yield return null;

        }
    }


    private void Teleport()
    {
        Vector2 randomPosition = player.position + (Vector3) (Vector2)Random.insideUnitCircle * teleportingRange;
         randomPosition.y = transform.position.y;

        transform.position = randomPosition; // teleporting to random position
    }

    private void Attack()
    {
        if(!isAttacking)
        {
            isAttacking = true;

            Vector2 direction = (player.position - transform.position).normalized; 

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));  //rotates the AI to face player

            //attack animation
        }
    }
}