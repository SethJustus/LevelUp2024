using System;
using UnityEngine;

public class ChasingAI : BaseEnemyAI
{
    public float chaseSpeed;
    public float attackRange;
    private bool attacking = false;
    public Animator anim;

    public override void PlayerDetection()
    {
        distanceFromPlayer = Vector2.Distance(transform.position, player.position);
        if(attacking){
            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.95){
                // attack hitbox
                if(distanceFromPlayer < 20){
                    
                }
            }
            // stop attack
            if(anim.GetCurrentAnimatorStateInfo(0).length <= anim.GetCurrentAnimatorStateInfo(0).normalizedTime){
                attacking = false;
                 anim.SetBool("attacking", false);
            }
            return;
        }

        
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > attackRange)
        {
            //follow player
            //move animation
            Vector2 direction = (player.position - transform.position).normalized; //calculates the direction

            // animate the player
            // code for all cardinal movement
            // if(Math.Abs(direction[0]) > Math.Abs(direction[1])){
            //     anim.SetFloat("hmov", Math.Abs(direction[0]) / direction[0]);
            //     anim.SetFloat("vmov", 0);
            // } else {
            //     anim.SetFloat("vmov", Math.Abs(direction[1]) / direction[1]);
            //     anim.SetFloat("hmov", 0);
            // }
            if(direction[0] != 0)
            {
                Vector2 flip = this.transform.localScale;
                flip.x = Math.Abs(flip.x) * Math.Abs(direction[0]) / direction[0];
                this.transform.localScale = flip;
            }
            if(direction[0] > 0){
                anim.SetFloat("vmov", 1);
            } else {
                anim.SetFloat("vmov", -1);
            }

            transform.position = Vector2.MoveTowards(transform.position, player.position,
                chaseSpeed * Time.deltaTime); // move towards player
            
        }

        if (distanceFromPlayer <= attackRange)
        {
            //attack animation
            anim.SetBool("attacking", true);
            attacking = true;
        }
    }
}
