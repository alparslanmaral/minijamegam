using UnityEngine;

public class Follower : MonoBehaviour
{
    public float speed = 5f; // Takip h�z�n� ayarlamak i�in
    public float minDistance = 2f; // Takip�i ile oyuncu aras�ndaki minimum mesafe
    public float startDistance = 1f; // Takibe ba�lamadan �nceki mesafe
    public float avoidanceRadius = 0.5f; // �� i�e ge�meyi �nlemek i�in ka��nma yar��ap�

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
            // Oyuncuya do�ru y�ntem hesaplay�n
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // Hareket et
            transform.position += direction * speed * Time.deltaTime;

            // Mesafeyi kontrol et
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < minDistance)
            {
                // E�er mesafe minimumdan k���kse, takip�iyi oyuncunun pozisyonuna y�nlendirin
                transform.position = player.transform.position - direction * minDistance;
            }

            // �� i�e ge�meyi �nle
            AvoidOverlap();
        }
        // Takip ba�lamadan �nce
        else if (player != null && waitForStart)
        {
            // Oyuncu ile belirli mesafe aral��� kontrol edin
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= startDistance)
            {
                // Ba�lamaya haz�r oldu�unda takibi ba�lat
                isFollowing = true;
                waitForStart = false;

                // Player'� Follower'�n alt nesnesi yap
                transform.SetParent(player.transform);
            }
        }
    }

    void FindPlayer()
    {
        // Player'� bul ve player de�i�kenini ayarla
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void AvoidOverlap()
    {
        // Bu takip�inin etraf�nda ba�ka bir takip�i var m� kontrol et
        Collider[] colliders = Physics.OverlapSphere(transform.position, avoidanceRadius);
        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject && col.CompareTag("Follower"))
            {
                // Ba�ka bir takip�i bulundu, biraz geriye kayd�r
                Vector3 avoidDirection = (transform.position - col.transform.position).normalized;
                transform.position += avoidDirection * speed * Time.deltaTime;
            }
        }
    }
}
