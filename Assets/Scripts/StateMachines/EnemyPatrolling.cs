using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleTank.Enemies;
using BattleTank.Tanks;

public class EnemyPatrolling : EnemyState
{
    private float speed = 5f;
    private Transform playerPos;
    private TankView tankView;

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        enemyView.transform.LookAt(playerPos.position);
       Debug.Log("Enemy has entered patroling state");

    }

    public override void OnExitState()
    {
        base.OnExitState();
        
        Debug.Log("Enemy has exit patroling state");
    }

    private void Update()
    {
       //  enemyView.transform.LookAt(playerPos.position);
         enemyView.transform.Translate(0, 0, speed * Time.deltaTime);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TankView>() != null)
        {
            GameObject player = other.gameObject;
            Transform goal = player.transform;
            this.playerPos = goal;
            enemyView.ChangeState(enemyView.patrollingState);
        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<TankView>() != null)
        {
            enemyView.ChangeState(enemyView.idleState);
        }
    }

}