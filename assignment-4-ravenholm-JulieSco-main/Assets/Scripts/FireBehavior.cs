using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehavior : MonoBehaviour
{

    [SerializeField] private float damagePerSecond = 10f;
    [SerializeField] private float tickRate = 0.5f;
    private float timeSinceLastTick;



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeSinceLastTick += Time.deltaTime;

            if (timeSinceLastTick >= tickRate)
            {
                var playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    float damageAmount = damagePerSecond * tickRate;
                    playerHealth.TakeDamage(damageAmount);
                }

                timeSinceLastTick = 0f;
            }
        }
    }
}
