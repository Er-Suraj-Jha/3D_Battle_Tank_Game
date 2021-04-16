using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleTank.Enemies;

public interface IDamagable
{
     void TakeDamage(BulletType bullettype, int damage);  
     void AddExplosionForce(float ExplosionForce,Vector3 position,float ExplosionRadius);
     Vector3 position();
}
