using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BattleTank.Enemies;
using System;


namespace BattleTank.Tanks
{
    public class TankView : MonoBehaviour,IDamagable
    {
        public TankController tankController;
        private float movement;
        private float Turn;
        [HideInInspector]public ParticleSystem ExplosionParticles;
        private float Current_Health;
        [HideInInspector]public bool m_Dead;
        public Slider health_slider;
        public Image fill_Image;
        public Color Full_HealthColor=Color.green;
        public Color Zero_HealthColor=Color.red;
        public GameObject Explosion_Prefab;

        public Rigidbody Shell;
        public Transform Fire_Transform;
        public Slider Aim_Slider;
        public float Min_Launch_Force=15f;
        public float Max_Launch_Force=30f;
        public float Max_Charge_Time=0.75f;
        private string Fire_Button;
        public float Current_Launch_Force;
        public float Charge_Speed;
        public bool Fired;
        //public Lobby_Controller lobby_Controller;
     


         public void initialize(TankController tankController)
        {
            this.tankController = tankController;
            
        }


        private void Start()
        {
            ExplosionParticles=Instantiate(Explosion_Prefab).GetComponent<ParticleSystem>();
            ExplosionParticles.gameObject.SetActive(false);
            m_Dead=false;
            Debug.Log("This tank view is of " + tankController.TankModel.TankType);
            tankController.SetHealthUI();
            Current_Launch_Force=Min_Launch_Force;
            Aim_Slider.value=Min_Launch_Force;
            Fire_Button="Fire";
            Charge_Speed=(Max_Launch_Force-Min_Launch_Force)/Max_Charge_Time;
            Set_Tank_Color();
        }

            
        //Setting color of the tank
        private void Set_Tank_Color()
        {
            if(Lobby_Controller.Red)
            {
                 MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
                 for(int i=0;i<renderers.Length;i++)
                {
                  renderers[i].material.color=Color.red;
                }
            }
            else if(Lobby_Controller.Green)
            {
                 MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
                 for(int i=0;i<renderers.Length;i++)
                {
                  renderers[i].material.color=Color.green;
                }
            }
             else if(Lobby_Controller.Orange)
            {
                 MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
                 for(int i=0;i<renderers.Length;i++)
                {
                  renderers[i].material.color=Color.yellow;
                }
            }
            else
            {
                 MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
                 for(int i=0;i<renderers.Length;i++)
                {
                  renderers[i].material.color=Color.blue;
                }
            }
        }


         private void Update()
         {
            Aim_Slider.value=Min_Launch_Force;
            if(Current_Launch_Force >= Max_Launch_Force && !Fired)
            {
                Debug.Log("Inside tank view");
                Current_Launch_Force=Max_Launch_Force;
                tankController.tankFire();
            }
            else if(Input.GetButtonDown(Fire_Button))
            {
                Fired=false;
                Current_Launch_Force=Min_Launch_Force;
            }
            else if(Input.GetButton(Fire_Button) && !Fired)
            {
                Current_Launch_Force+=Charge_Speed*Time.deltaTime;
                Aim_Slider.value=Current_Launch_Force;
            }
            else if(Input.GetButtonUp(Fire_Button) && !Fired)
            {
                tankController.tankFire();
            }

            Movement();
        }

        private void Movement()
        {
            movement=Input.GetAxis("Vertical1");
            Turn=Input.GetAxis("Horizontal1");
        }

         private void FixedUpdate()
        {

             if (movement != 0)
            {
                tankController.Move(movement);
            }   
             if (Turn != 0)
                tankController.Turn(Turn);
        }
        
        //Collision with enemy tank.
        private void OnCollisionEnter(Collision collision)
        {
            //IDamagable damagable=collider.GetComponent<IDamagable>();
            if (collision.gameObject.GetComponent<EnemyView>()!=null)
            {
                Debug.Log("Collided with enemy");
                tankController.OnDeath();

            }
            
        }

        //Taking damage by player.
        public void TakeDamage(BulletType bullettype, int damage)
        {
            Debug.Log("Player tank is taking damage");
            tankController.ApplyDamage(damage);
        }
        

        //Adding explosion force to the tank when hit by a bullet
        public void AddExplosionForce(float ExplosionForce,Vector3 position,float ExplosionRadius)
        {
             gameObject.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce,position,ExplosionRadius);
             Debug.Log("Explosion force added");
        }

        public Vector3 position()
        {
            return transform.position;
        } 


        //Destroying TankView
        public void DestroyView(TankView tankView)
        {
            Destroy(tankView.gameObject);
            tankView = null;
        }

        
   }
}


