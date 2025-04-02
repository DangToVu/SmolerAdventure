using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        // Lấy AudioSource components
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        // Giữ object này khi chuyển scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // Hủy object trùng lặp
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        // Gán giá trị âm lượng ban đầu
        ChangeMusicVolume(0);
        ChangeSoundVolume(0);

        // Phát nhạc nền ngay khi khởi tạo (nếu muốn)
        // PlayMusic(musicSource.clip); // Bỏ comment nếu bạn muốn nhạc phát ngay từ đầu
    }

    // Hàm phát âm thanh (sound)
    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    // Hàm mới: Phát nhạc (music) từ AudioSource
    public void PlayMusic(AudioClip _music)
    {
        if (_music == null)
        {
            Debug.LogWarning("Music clip is null! Please assign a music clip to play.");
            return;
        }

        musicSource.clip = _music; // Gán clip nhạc vào musicSource
        musicSource.Play(); // Phát nhạc
        musicSource.loop = true; // Đặt nhạc lặp lại (nếu bạn muốn nhạc nền lặp vô hạn)
    }

    // Hàm dừng nhạc (nếu cần)
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Điều chỉnh âm lượng âm thanh (sound)
    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "soundVolume", _change, soundSource);
    }

    // Điều chỉnh âm lượng nhạc (music)
    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", _change, musicSource);
    }

    // Hàm hỗ trợ thay đổi âm lượng
    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        // Lấy giá trị âm lượng hiện tại và thay đổi
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        // Kiểm tra giá trị tối đa và tối thiểu
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        // Gán giá trị cuối cùng
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        // Lưu giá trị vào PlayerPrefs
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}