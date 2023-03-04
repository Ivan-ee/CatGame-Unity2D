using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
   [SerializeField] private float damage = 20f;
   [SerializeField] private float delayForDestroy = 2f;
 
   private void Start()
   {
      StartCoroutine(DestroyDamageDealerCoroutine());
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.GetComponent<Enemy>())
         col.GetComponent<Enemy>().TakeDamage(damage);
      Destroy(gameObject);
   }

   IEnumerator DestroyDamageDealerCoroutine()
   {
      yield return new WaitForSeconds(delayForDestroy);
      Destroy(gameObject);
   }
}
