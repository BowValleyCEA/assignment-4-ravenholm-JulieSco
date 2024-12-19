using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float respawnDelay = 4f;
    [SerializeField] private AudioClip deathSound;
    private float currentHealth;
    private Vector3 respawnPoint;
    private AudioSource audioSource;
    private bool isDead = false;
    private CharacterController characterController;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && deathSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); //find funny death sound
        }

        respawnPoint = transform.position;
        Debug.Log($"Initial spawn point set to: {respawnPoint}");
        characterController = GetComponent<CharacterController>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        currentHealth = Mathf.Min(currentHealth, maxHealth);
        Debug.Log($"Player Health: {currentHealth}");

        if (currentHealth <= 0f)
        {
            Debug.Log("Health reached 0, calling Die()");
            Die();
        }
        
        
    }

    private void Die()
    {
        if (isDead) return;

        Debug.Log("Die() method called");

        isDead = true;

        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        DisablePlayer();
        
        StartCoroutine(RespawnSequence());

    }

    private void DisablePlayer()
    {
        if (characterController != null)
        {
            characterController.enabled = false;
        }
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    private void EnablePlayer()
    {
        if (characterController != null)
        {
            characterController.enabled = true;
        }
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }

        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
        }

    }

    private IEnumerator RespawnSequence()
    {
        Debug.Log("Starting respawn");
        yield return new WaitForSeconds(respawnDelay);

        Debug.Log($"Current position before respawn: {transform.position}");
        if (characterController != null)
        {
            characterController.enabled = false;
        }
        transform.position = respawnPoint;
        if (characterController != null)
        {
            characterController.enabled = true;
        }
        Debug.Log($"<color=blue>Respawned at: {transform.position}</color>");

        currentHealth = maxHealth;
        EnablePlayer();
        isDead = false;
        Debug.Log("Player respawned");
    }

    public void SetCheckpoint(Vector3 position)
    {
        respawnPoint = position;
        Debug.Log($"<color=green>Checkpoint activated! Position: {transform.position}</color>");

    }

}
