using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] public float damage;

    [Header("Firetrap Timer")]
    [SerializeField] public float activationDelay;
    [SerializeField] public float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    [Header("Firetrap SFX")]
    [SerializeField] private AudioClip firetrapSound;

    private bool triggered;
    private bool active;
    private Health playerHealth;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (playerHealth != null && active) 
        {
            playerHealth.TakeDamage(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            playerHealth = collision.GetComponent<Health>();
            if (!triggered) 
            {
                StartCoroutine(ActivateFiretrap());
            }
            if (active) 
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = null;
        }
    }
    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRenderer.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
