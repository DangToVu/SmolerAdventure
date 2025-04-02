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
               
                return;
            }
            if (selectLevelUI == null)
            {
                
                return;
            }

            menuUI.SetActive(true);
            selectLevelUI.SetActive(false);
            
        }

        public void StartGame()
        {
            
            SceneManager.LoadScene("Level 1");
        }

        public void ShowSelectLevel()
        {
            if (menuUI == null || selectLevelUI == null)
            {
                return;
            }

            menuUI.SetActive(false);
            selectLevelUI.SetActive(true);
        }

        public void BackToMenu()
        {
            if (menuUI == null || selectLevelUI == null)
            {
                return;
            }

            selectLevelUI.SetActive(false);
            menuUI.SetActive(true);
        }

        public void LoadLevel(string levelName)
        {
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