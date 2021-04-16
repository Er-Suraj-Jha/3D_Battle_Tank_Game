using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleTank.Tanks;

namespace BattleTank.Enemies
{
    
    //[RequireComponent(typeof(EnemyView))]
    public class EnemyState : MonoBehaviour
    {
        public EnemyView enemyView;


        private void Awake()
        {
          Debug.Log("Inside enemy state ");
        }


        public virtual void OnStateEnter()
        {
            this.enabled = true;
            
        }

        public virtual void OnExitState()
        {
            this.enabled = false;
        }

        public void Tick()
        {

        }

    }

}