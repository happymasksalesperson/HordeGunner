using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnthillNipperAttackState : NipperAnthillStateBase
{
    public int damage = 10;
    public float attackRadius = 5f;
    public float attackDistance = 5f;
    public float attackTime = 1f;
    public LayerMask layerMask;

    private RaycastHit[] _hitResults = new RaycastHit[10]; 
    
    public override void Enter()
    {
        base.Enter();
        sensor.SetAnimation(NipperAnimationEnum.Attack01);
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackTime);
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        int hitCount = Physics.SphereCastNonAlloc(origin, attackRadius, direction, _hitResults, attackDistance, layerMask);

        for (int i = 0; i < hitCount; i++)
        {
            ITakeDamage takeDamage = _hitResults[i].collider.GetComponent<ITakeDamage>();
            if (takeDamage != null)
            {
                takeDamage.ChangeHealth(-damage);
            }
        }
    }
}