using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleTank.Tanks;

namespace BattleTank.Bullets
{
    public class BulletView : MonoBehaviour
    {
        public ParticleSystem ExplosionParticles;
        public float ExplosionForce=1200f;
        public float MaxLifeTime=2f;
        public float ExplosionRadius=8f;
        
        public BulletController bulletController;


         //Linking view and controller.
        public void Initialize(BulletController controller, string Layer)
        {
            this.bulletController = controller;
            this.gameObject.layer = LayerMask.NameToLayer(Layer);
        }


        private void Start()
        {
            Destroy(gameObject,MaxLifeTime);
            Debug.Log("This is from Bullet View");
        }


        //Fun to set position of shells
        public void SetPos(Transform Fire_Transform,float Current_Launch_Force)
        {
            transform.position = Fire_Transform.position;
            transform.rotation = Fire_Transform.rotation;
            Rigidbody rb=GetComponent<Rigidbody>();
            rb.velocity=Current_Launch_Force*Fire_Transform.forward;
        }
        


        //Jo rule hamne padha hai uske hisaab se tankcontroller ko hi ham call kar sakte hn direct tank view ko nahi
        private void OnTriggerEnter(Collider collider)
        {
            if(collider.GetComponent<IdleState>()==null  && collider.GetComponent<EnemyPatrolling>()==null && collider.GetComponent<AttackState>()==null)
            {
            
            Collider[] colliders=Physics.OverlapSphere(transform.position,ExplosionRadius);
            for(int i=0;i<colliders.Length;i++)
            {
                IDamagable damagable=colliders[i].GetComponent<IDamagable>();
                if(damagable==null)
                continue;
                damagable.AddExplosionForce(ExplosionForce,transform.position,ExplosionRadius);
                Debug.Log("From bullet view collision detection");
                float damage=bulletController.CalculateDamage(damagable.position());
                damagable.TakeDamage(bulletController.GetBulletModel().BulletType,(int)damage);
            }

            ExplosionParticles.transform.parent=null;
            ExplosionParticles.Play();
            Destroy(ExplosionParticles.gameObject,ExplosionParticles.main.duration);
            Destroy(gameObject);
            BulletService.Instance.DestroyBullet(bulletController);

            }

        }  
 

        //Destroy BulletView.
        public void DestroyBulletView(BulletView bulletviewDes)
        {
            Destroy(bulletviewDes.gameObject, 2f);
            bulletviewDes = null;
        }

    }
}

