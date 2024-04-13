using UnityEngine;

public class Follower : MonoBehaviour
{
    public float speed = 5f; // Takip hýzýný ayarlamak için
    public float minDistance = 2f; // Takipçi ile oyuncu arasýndaki minimum mesafe
    public float startDistance = 1f; // Takibe baþlamadan önceki mesafe
    public float avoidanceRadius = 0.5f; // Ýç içe geçmeyi önlemek için kaçýnma yarýçapý

    private GameObject player;
    private bool isFollowing = false;
    private bool waitForStart = true;

    void Update()
    {
        // Oyuncuyu bul
        FindPlayer();

        // Oyuncu varsa ve takip ediliyorsa
        if (player != null && isFollowing)
        {
            // Oyuncuya doðru yöntem hesaplayýn
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // Hareket et
            transform.position += direction * speed * Time.deltaTime;

            // Mesafeyi kontrol et
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < minDistance)
            {
                // Eðer mesafe minimumdan küçükse, takipçiyi oyuncunun pozisyonuna yönlendirin
                transform.position = player.transform.position - direction * minDistance;
            }

            // Ýç içe geçmeyi önle
            AvoidOverlap();
        }
        // Takip baþlamadan önce
        else if (player != null && waitForStart)
        {
            // Oyuncu ile belirli mesafe aralýðý kontrol edin
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= startDistance)
            {
                // Baþlamaya hazýr olduðunda takibi baþlat
                isFollowing = true;
                waitForStart = false;

                // Player'ý Follower'ýn alt nesnesi yap
                transform.SetParent(player.transform);
            }
        }
    }

    void FindPlayer()
    {
        // Player'ý bul ve player deðiþkenini ayarla
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void AvoidOverlap()
    {
        // Bu takipçinin etrafýnda baþka bir takipçi var mý kontrol et
        Collider[] colliders = Physics.OverlapSphere(transform.position, avoidanceRadius);
        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject && col.CompareTag("Follower"))
            {
                // Baþka bir takipçi bulundu, biraz geriye kaydýr
                Vector3 avoidDirection = (transform.position - col.transform.position).normalized;
                transform.position += avoidDirection * speed * Time.deltaTime;
            }
        }
    }
}
