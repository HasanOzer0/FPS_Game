using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    private bool isDead = false;

    // YÖNTEM 1: Mermi deðerse (Fiziksel Mermi)
    private void OnCollisionEnter(Collision collision)
    {
        // Mermi çarptý mý diye kontrol etmeye gerek yok, 
        // ne çarparsa çarpsýn hasar alsýn (Garanti yöntem)
        TakeDamage(25f);
    }

    // YÖNTEM 2: Iþýn deðerse (Raycast - Bazý silahlar böyledir)
    public void TakeDamage(float amount)
    {
        if (isDead) return;

        // GameManager'daki güçlendirme çarpanýný alýyoruz!
        // Eðer marketten güçlendirme aldýysa mermi 2 kat vurur.
        float finalDamage = amount;
        if (GameManager.Instance != null)
        {
            finalDamage = amount * GameManager.Instance.damageMultiplier;
        }

        health -= finalDamage;
        transform.localScale *= 0.95f;

        if (health <= 0) Die();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        // GameManager varsa para ekle
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddGold(50);
        }

        // Varsa patlama efekti (Particle) ekleyebilirsin ama þart deðil
        Destroy(gameObject); // Objeyi yok et
    }
}
