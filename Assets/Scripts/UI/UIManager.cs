using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Nếu pause screen đã active thì unpause và ngược lại
            PauseGame(!pauseScreen.activeInHierarchy);
        }
    }

    #region Game Over
    // Kích hoạt màn hình game over
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    // Khởi động lại level
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quay về Main Menu
    public void MainMenu()
    {
        // Đặt lại timeScale về 1 để đảm bảo thời gian chạy bình thường ở scene mới
        Time.timeScale = 1;
        SceneManager.LoadScene(0); // 0 là index của scene Menu trong Build Settings
    }

    // Thoát game hoặc quay về Menu nếu đang pause
    public void Quit()
    {
        // Kiểm tra nếu đang ở trạng thái pause
        if (pauseScreen.activeInHierarchy)
        {
            // Nếu đang pause, quay về Main Menu thay vì thoát game
            MainMenu();
        }
        else
        {
            // Nếu không pause, thoát game bình thường
            Application.Quit(); // Thoát game (chỉ hoạt động ở bản build)

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Thoát chế độ Play trong Editor
#endif
        }

        Debug.Log("Quit button pressed");
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        // Nếu status == true thì pause, nếu status == false thì unpause
        pauseScreen.SetActive(status);

        // Khi pause thì đặt timeScale về 0 (thời gian dừng)
        // Khi unpause thì đặt lại về 1 (thời gian chạy bình thường)
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion
}