using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTypeAtkBehaviour : AtkBehaviour
{

    public override void callAtkMoition(GameObject target = null, Transform posAtkStart = null)
    {

        if (target == null && posAtkStart != null)
        {
            return;
        }

        Vector3 vecProjectile = posAtkStart?.position ?? transform.position;

        if (atkEffectPrefab != null)
        {
            GameObject objProjectille = GameObject.Instantiate<GameObject>(atkEffectPrefab, vecProjectile, Quaternion.identity);

            objProjectille.transform.forward = transform.forward;

            CollisionProjectileAtk projectile = objProjectille.GetComponent<CollisionProjectileAtk>();

            if (projectile != null)
            {
                projectile.projectileParents = this.gameObject;
                projectile.target = target;
                projectile.attackBehaviour = this;
            }
        }
        nowAtkCoolTime = 0.0f;
    }

}
