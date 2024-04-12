using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform takipEdilecekObj;
    public float takipHizi = 5f;
    public float zOffset = -3f; // Karakterden uzaklýk

    private Vector3 initialRotation;

    void Start()
    {
        // Baþlangýçtaki x, y, z rotasyonlarý kaydet
        initialRotation = transform.rotation.eulerAngles;
    }

    void LateUpdate()
    {
        if (takipEdilecekObj != null)
        {
            // Kameranýn pozisyonunu sadece x ve z koordinatlarýna doðru güncelle
            Vector3 targetPosition = new Vector3(takipEdilecekObj.position.x, transform.position.y, takipEdilecekObj.position.z + zOffset);
            transform.position = Vector3.Lerp(transform.position, targetPosition, takipHizi * Time.deltaTime);

            // Kameranýn rotasyonunu baþlangýçtaki x, y, z rotasyonlarýna göre sabit tut
            transform.rotation = Quaternion.Euler(initialRotation);
        }
    }
}
