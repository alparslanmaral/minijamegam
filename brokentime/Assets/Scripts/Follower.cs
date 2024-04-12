using UnityEngine;

public class Follower : MonoBehaviour
{
    public float speed = 5f; // Takip hýzýný ayarlamak için
    public float minDistance = 2f; // Takipçi ile oyuncu arasýndaki minimum mesafe
    public float startDistance = 1f; // Takibe baþlamadan önceki mesafe

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
            }
        }
    }
}
