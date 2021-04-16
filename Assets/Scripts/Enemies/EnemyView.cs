using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleTank.Bullets;
using System;

namespace BattleTank.Enemies
{
    public class EnemyView : MonoBehaviour, IDamagable
    {
        public EnemyController enemyController;

        private EnemyState currentState;

        public Transform Fire_Transform;
        public LayerMask rayMask;

        [HideInInspector]public ParticleSystem ExplosionParticles;
        private float Current_Health;
         [HideInInspector]public bool m_Dead;
        public Slider health_slider;
        public Image fill_Image;
        public Color Full_HealthColor=Color.green;
        public Color Zero_HealthColor=Color.red;
        public GameObject Explosion_Prefab;
        public Material tankColor;
        public bool Fired;

       // [SerializeField]
        public IdleState idleState;
       // [SerializeField]
        public EnemyPatrolling patrollingState;
       // [SerializeField]
        public AttackState attackState;


        private void Update()
        {
           
        }

        public void initialize(EnemyController enemyController)
        {
            this.enemyController = enemyController;
           
        }

         private void Start()
        {
            if(enemyController.EnemyModel.Health==0)
            {
               enemyController.EnemyModel.Health=100;
            }
            ExplosionParticles=Instantiate(Explosion_Prefab).GetComponent<ParticleSystem>();
            ExplosionParticles.gameObject.SetActive(false);
            m_Dead=false;
            enemyController.SetHealthUI();
            setColor();
        }
        
        //Setting colors of enemy tank
        private void setColor()
        {
            MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
            for(int i=0;i<renderers.Length;i++)
            {
                renderers[i].material.color=Color.cyan;
            }
        }


        //Destroy enemy on collision with bullet.
        public void TakeDamage(BulletType bullettype, int damage)
        {
            Debug.Log("Taking damage " + damage);
            enemyController.ApplyDamage(damage);
            EnemyService.Instance.onDamageEvent();
        }


        //Adding explosion force to the tank when hit by a bullet
        public void AddExplosionForce(float ExplosionForce,Vector3 position,float ExplosionRadius)
        {
             gameObject.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce,position,ExplosionRadius);
             Debug.Log("Explosion force added");
        }



       //Changing states of the enemy tank to one of the states by using state machines
        public void ChangeState(EnemyState newState)
        {
            Debug.Log("Enemy is changing state");
            if (currentState != null)
            {
                currentState.OnExitState();
            }

            currentState = newState;

            currentState.OnStateEnter();
        }




        public Vector3 position()
        {
            return transform.position;
        } 


        //Collision with enviroment
        public void ChangeDirection()
        {
            {
               transform.Rotate(new Vector3(0f,UnityEngine.Random.Range(70f, 130f), 0f));
            }
        }  
        
        //Destroying enemy tank view
        public void enemyDestroyView(EnemyView enemyView)
        {
            Destroy(enemyView.gameObject);
            enemyView = null;
        }

    }
}
