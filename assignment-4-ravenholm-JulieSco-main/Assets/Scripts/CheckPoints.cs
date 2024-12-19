using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [SerializeField] private AudioClip activationSound;
    private bool isActivated = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && activationSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        Debug.Log($"<color=yellow>Checkpoint created at: {transform.position}</color>");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActivated) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log($"<color=green>Player entered checkpoint trigger at: {transform.position}</color>");
            var playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                ActivateCheckpoint(playerHealth);
            }
        }
        else
        {
            Debug.LogError("PlayerHealth component not found on player!");
        }
    }

    private void ActivateCheckpoint(PlayerHealth playerHealth)
    {
        isActivated = true;
        playerHealth.SetCheckpoint(transform.position);
        Debug.Log($"<color=green>Checkpoint activated! Position: {transform.position}</color>");

    }
}
