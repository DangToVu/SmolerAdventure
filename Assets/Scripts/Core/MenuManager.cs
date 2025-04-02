namespace UI
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class MenuManager : MonoBehaviour
    {
        // Hàm để tải Level 1 khi nhấn Start Game
        public void StartGame()
        {
            SceneManager.LoadScene("Level 1"); // Dùng tên scene, hoặc số thứ tự (ví dụ: 1)
        }

        // Hàm để dừng game khi nhấn Quit
        public void QuitGame()
        {
            // Dừng game trong Editor
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // Thoát game khi chạy ở bản build (ngoài Editor)
            Application.Quit();
#endif

            Debug.Log("Game Quit"); // Để kiểm tra
        }
    }
}