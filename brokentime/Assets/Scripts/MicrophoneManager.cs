using UnityEngine;

public class MicrophoneManager : MonoBehaviour
{
    public string keyword = "Broke"; // Algýlanacak kelime
    public string sceneToLoad; // Geçilecek sahne ismi

    public float sensitivity = 0.5f; // Algýlama hassasiyeti
    public float delayBetweenActions = 2f; // Eylemler arasý gecikme süresi
    private float lastActionTime; // Son eylem zamaný

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastActionTime >= delayBetweenActions)
        {
            // Uygulama üzerinde farklý bir eylem gerçekleþtir
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
            // Belirlenen hassasiyetin üzerinde bir ses algýlandýðýnda eylemi gerçekleþtir
            LoadScene();
            lastActionTime = Time.time;
        }
    }

    float GetMicrophoneVolume()
    {
        float micLoudness = 0;
        if (Microphone.IsRecording(null))
        {
            // Mikrofondan ses düzeyini al
            micLoudness = Microphone.GetPosition(null);
        }
        return micLoudness;
    }

    void LoadScene()
    {
        // Belirtilen sahneyi yükle
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }
}
