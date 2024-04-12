using UnityEngine;

public class MicrophoneManager : MonoBehaviour
{
    public string keyword = "Broke"; // Alg�lanacak kelime
    public string sceneToLoad; // Ge�ilecek sahne ismi

    public float sensitivity = 0.5f; // Alg�lama hassasiyeti
    public float delayBetweenActions = 2f; // Eylemler aras� gecikme s�resi
    private float lastActionTime; // Son eylem zaman�

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastActionTime >= delayBetweenActions)
        {
            // Uygulama �zerinde farkl� bir eylem ger�ekle�tir
            LoadScene();
            lastActionTime = Time.time;
        }

        // Mikrofondan gelen sesi kontrol et
        CheckMicrophoneInput();
    }

    void CheckMicrophoneInput()
    {
        float vol = GetMicrophoneVolume();
        if (vol > sensitivity && Time.time - lastActionTime >= delayBetweenActions)
        {
            // Belirlenen hassasiyetin �zerinde bir ses alg�land���nda eylemi ger�ekle�tir
            LoadScene();
            lastActionTime = Time.time;
        }
    }

    float GetMicrophoneVolume()
    {
        float micLoudness = 0;
        if (Microphone.IsRecording(null))
        {
            // Mikrofondan ses d�zeyini al
            micLoudness = Microphone.GetPosition(null);
        }
        return micLoudness;
    }

    void LoadScene()
    {
        // Belirtilen sahneyi y�kle
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }
}
