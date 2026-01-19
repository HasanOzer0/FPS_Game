using UnityEngine;

public class SmartJump : MonoBehaviour
{
    [Header("Hedef Ayarlar")]
    [Tooltip("Karakter kaç metre yükselsin? (Örn: 1.5 = Ýnsan boyu kadar)")]
    public float targetHeight = 2.0f;

    [Tooltip("Peþ peþe zýplama süresi")]
    public float jumpCooldown = 1.0f;

    [Header("Zemin Kontrolü")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 1.1f; // Karakterin boyuna göre artýrabilirsin

    private Rigidbody rb;
    private bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Zýplayýnca saða sola devrilmesin
        if (rb != null) rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        // Space'e bastýk mý? Süre doldu mu? Yerde miyiz?
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            PerformJump();
        }
    }

    void PerformJump()
    {
        // --- FORMÜL: V = Kök(2 * h * g) ---
        // Bu formül, yer çekimi ne olursa olsun hedef yüksekliðe (h) ulaþmak için
        // gereken hýzý (v) hesaplar.

        float gravity = Physics.gravity.y; // Mevcut yer çekimi (Örn: -30)

        // Formülün Unitycesi:
        float jumpVelocity = Mathf.Sqrt(2 * targetHeight * Mathf.Abs(gravity));

        // Mevcut dikey hýzý sýfýrla (Tutarlý zýplama için)
        Vector3 velocity = rb.velocity;
        velocity.y = 0;
        rb.velocity = velocity;

        // Hesaplanan hýzý direkt uygula (Force yerine Velocity deðiþimi daha kesindir)
        rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);

        // Bekleme süresi
        canJump = false;
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    void ResetJump()
    {
        canJump = true;
    }

    // Basit yer kontrolü (Raycast)
  
}