namespace UI
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject menuUI; // Gán GameObject "Menu" trong Inspector
        [SerializeField] private GameObject selectLevelUI; // Gán GameObject "SelectLevelUI" trong Inspector

        private void Start()
        {
            if (menuUI == null)
            {
                Debug.LogError("MenuUI is not assigned in the Inspector! Please assign it.");
                return;
            }
            if (selectLevelUI == null)
            {
                Debug.LogError("SelectLevelUI is not assigned in the Inspector! Please assign it.");
                return;
            }

            menuUI.SetActive(true);
            selectLevelUI.SetActive(false);
            Debug.Log("MenuManager Start: MenuUI is active, SelectLevelUI is inactive.");
        }

        public void StartGame()
        {
            Debug.Log("StartGame called. Loading Level 1...");
            SceneManager.LoadScene("Level 1");
        }

        public void ShowSelectLevel()
        {
            Debug.Log("ShowSelectLevel called.");
            if (menuUI == null || selectLevelUI == null)
            {
                Debug.LogError("Cannot show SelectLevelUI: MenuUI or SelectLevelUI is not assigned!");
                return;
            }

            menuUI.SetActive(false);
            selectLevelUI.SetActive(true);
            Debug.Log("MenuUI hidden, SelectLevelUI shown.");
        }

        public void BackToMenu()
        {
            Debug.Log("BackToMenu called.");
            if (menuUI == null || selectLevelUI == null)
            {
                Debug.LogError("Cannot return to Menu: MenuUI or SelectLevelUI is not assigned!");
                return;
            }

            selectLevelUI.SetActive(false);
            menuUI.SetActive(true);
            Debug.Log("SelectLevelUI hidden, MenuUI shown.");
        }

        public void LoadLevel(string levelName)
        {
            Debug.Log($"LoadLevel called with level: {levelName}");
            SceneManager.LoadScene(levelName);
        }

        public void QuitGame()
        {
            Debug.Log("QuitGame called.");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}