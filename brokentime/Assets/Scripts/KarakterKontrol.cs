using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{
    public float hareketHizi = 5f;
    public float rotationSpeed = 360f;
    public GameObject dönmeObjesi; // Yürüme yönüne doðru dönmesini istediðiniz obje

    private Alteruna.Avatar _avatar;

    private void Start()
    {
        _avatar = GetComponent<Alteruna.Avatar>();

        if (!_avatar.IsMe)
            return;

        // Eðer dönmeObjesi atanmamýþsa, karakterin kendisi dönecek
        if (dönmeObjesi == null)
        {
            dönmeObjesi = gameObject;
        }
    }

    void Update()
    {
        if (!_avatar.IsMe)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0f, vertical);

        if (movementDirection != Vector3.zero)
        {
            

            // Hareket yönüne doðru dönme iþlemi
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            dönmeObjesi.transform.rotation = Quaternion.RotateTowards(dönmeObjesi.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            
        }

        Vector3 hareket = movementDirection * hareketHizi * Time.deltaTime;
        transform.Translate(hareket, Space.World);
    }

    // Karakterin GameObject'ini ayarlamak için public void
    public void SetKarakterObject(GameObject yeniKarakterObject)
    {
        if (yeniKarakterObject != null)
        {
            dönmeObjesi = yeniKarakterObject;
        }
        else
        {
            Debug.LogWarning("Geçersiz karakter objesi!");
        }
    }
}
