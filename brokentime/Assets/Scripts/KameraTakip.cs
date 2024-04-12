using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform takipEdilecekObj;
    public float takipHizi = 5f;
    public float zOffset = -3f; // Karakterden uzakl�k

    private Vector3 initialRotation;

    void Start()
    {
        // Ba�lang��taki x, y, z rotasyonlar� kaydet
        initialRotation = transform.rotation.eulerAngles;
    }

    void LateUpdate()
    {
        if (takipEdilecekObj != null)
        {
            // Kameran�n pozisyonunu sadece x ve z koordinatlar�na do�ru g�ncelle
            Vector3 targetPosition = new Vector3(takipEdilecekObj.position.x, transform.position.y, takipEdilecekObj.position.z + zOffset);
            transform.position = Vector3.Lerp(transform.position, targetPosition, takipHizi * Time.deltaTime);

            // Kameran�n rotasyonunu ba�lang��taki x, y, z rotasyonlar�na g�re sabit tut
            transform.rotation = Quaternion.Euler(initialRotation);
        }
    }
}
