using UnityEngine;

public class Follower : MonoBehaviour
{
    public float speed = 5f; // Takip h�z�n� ayarlamak i�in
    public float minDistance = 2f; // Takip�i ile oyuncu aras�ndaki minimum mesafe
    public float startDistance = 1f; // Takibe ba�lamadan �nceki mesafe

    private GameObject player;
    private bool isFollowing = false;
    private bool waitForStart = true;

    void Start()
    {
        // Oyuncuyu "Player" etiketiyle bulun
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
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
            }
        }
    }
}
