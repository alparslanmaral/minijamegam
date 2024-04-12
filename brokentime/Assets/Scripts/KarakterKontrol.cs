using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{
    public float hareketHizi = 5f;
    public float rotationSpeed = 360f;
    public GameObject d�nmeObjesi; // Y�r�me y�n�ne do�ru d�nmesini istedi�iniz obje

    private Alteruna.Avatar _avatar;

    private void Start()
    {
        _avatar = GetComponent<Alteruna.Avatar>();

        if (!_avatar.IsMe)
            return;

        // E�er d�nmeObjesi atanmam��sa, karakterin kendisi d�necek
        if (d�nmeObjesi == null)
        {
            d�nmeObjesi = gameObject;
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
            

            // Hareket y�n�ne do�ru d�nme i�lemi
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            d�nmeObjesi.transform.rotation = Quaternion.RotateTowards(d�nmeObjesi.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            
        }

        Vector3 hareket = movementDirection * hareketHizi * Time.deltaTime;
        transform.Translate(hareket, Space.World);
    }

    // Karakterin GameObject'ini ayarlamak i�in public void
    public void SetKarakterObject(GameObject yeniKarakterObject)
    {
        if (yeniKarakterObject != null)
        {
            d�nmeObjesi = yeniKarakterObject;
        }
        else
        {
            Debug.LogWarning("Ge�ersiz karakter objesi!");
        }
    }
}
