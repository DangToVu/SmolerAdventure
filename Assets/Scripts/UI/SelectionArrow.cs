using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI; // GameObject chứa Main Menu (Start Game, Select Level, Quit)
    [SerializeField] private GameObject selectLevelUI; // GameObject chứa Select Level UI (Level 1, Level 2, ..., Back)
    [SerializeField] private RectTransform[] mainMenuButtons; // Các Text trong Main Menu
    [SerializeField] private RectTransform[] levelSelectButtons; // Các Text trong Select Level UI
    [SerializeField] private AudioClip changeSound;   // Âm thanh khi thay đổi vị trí
    [SerializeField] private AudioClip interactSound; // Âm thanh khi chọn
    [SerializeField] private RectTransform arrowLeft;  // Cầu lửa bên trái
    [SerializeField] private RectTransform arrowRight; // Cầu lửa bên phải
    [SerializeField] private Vector2 arrowSize = new Vector2(200f, 80f); // Kích thước mới cho cầu lửa
    [SerializeField] private string[] levelSceneNames = new string[] { "Level 1", "Level 2", "Level 3", "Level 4", "Level 5" }; // Tên các scene level
    private int currentPosition;
    private bool isInMainMenu = true; // Trạng thái hiện tại: true = Main Menu, false = Select Level UI
    private RectTransform[] currentButtons; // Mảng buttons hiện tại (Main Menu hoặc Select Level)

    private void Awake()
    {
        // Điều chỉnh kích thước của cầu lửa
        if (arrowLeft != null)
            arrowLeft.sizeDelta = arrowSize;
        if (arrowRight != null)
            arrowRight.sizeDelta = arrowSize;
    }

    private void OnEnable()
    {
        // Khởi tạo ở Main Menu
        isInMainMenu = true;
        if (mainMenuUI != null)
            mainMenuUI.SetActive(true);
        else
            Debug.LogError("MainMenuUI is not assigned!");

        if (selectLevelUI != null)
            selectLevelUI.SetActive(false);
        else
            Debug.LogError("SelectLevelUI is not assigned!");

        currentButtons = mainMenuButtons;
        currentPosition = 0; // Khởi tạo vị trí ban đầu là 0 (Start Game)
        ChangePosition(0);   // Đặt vị trí ban đầu
    }

    private void Update()
    {
        // Di chuyển lên khi nhấn W hoặc UpArrow
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);

        // Di chuyển xuống khi nhấn S hoặc DownArrow
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        // Kích hoạt nút khi nhấn Enter
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interact called with currentPosition: " + currentPosition + ", isInMainMenu: " + isInMainMenu);
            Interact();
        }
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (_change != 0 && changeSound != null)
            SoundManager.instance.PlaySound(changeSound);

        if (currentPosition < 0)
            currentPosition = currentButtons.Length - 1;
        else if (currentPosition >= currentButtons.Length)
            currentPosition = 0;

        AssignPosition();
    }

    private void AssignPosition()
    {
        if (currentButtons == null || currentButtons.Length == 0)
        {
            Debug.LogWarning("Current buttons array is empty!");
            return;
        }

        Vector3 buttonPos = currentButtons[currentPosition].position;

        if (arrowLeft != null)
            arrowLeft.position = new Vector3(buttonPos.x - 300f, buttonPos.y, buttonPos.z);

        if (arrowRight != null)
            arrowRight.position = new Vector3(buttonPos.x + 300f, buttonPos.y, buttonPos.z);
    }

    private void Interact()
    {
        if (interactSound != null)
            SoundManager.instance.PlaySound(interactSound);
        else
            Debug.LogWarning("InteractSound is not assigned!");

        if (isInMainMenu)
        {
            // Xử lý trong Main Menu
            switch (currentPosition)
            {
                case 0: // Start Game
                    Debug.Log($"Loading scene: {levelSceneNames[0]}");
                    SceneManager.LoadScene(levelSceneNames[0]); // Mặc định vào Level 1
                    break;
                case 1: // Select Level
                    // Chuyển sang Select Level UI
                    isInMainMenu = false;
                    if (mainMenuUI != null)
                        mainMenuUI.SetActive(false);
                    if (selectLevelUI != null)
                        selectLevelUI.SetActive(true);
                    currentButtons = levelSelectButtons;
                    currentPosition = 0;
                    ChangePosition(0);
                    break;
                case 2: // Quit
                    Debug.Log("Quitting game...");
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                    break;
                default:
                    Debug.LogWarning("Invalid currentPosition in Main Menu: " + currentPosition);
                    break;
            }
        }
        else
        {
            // Xử lý trong Select Level UI
            if (currentPosition < levelSceneNames.Length)
            {
                // Chọn một level (Level 1 đến Level 5)
                Debug.Log($"Loading scene: {levelSceneNames[currentPosition]}");
                SceneManager.LoadScene(levelSceneNames[currentPosition]);
            }
            else if (currentPosition == levelSceneNames.Length)
            {
                // Chọn "Back"
                isInMainMenu = true;
                if (mainMenuUI != null)
                    mainMenuUI.SetActive(true);
                if (selectLevelUI != null)
                    selectLevelUI.SetActive(false);
                currentButtons = mainMenuButtons;
                currentPosition = 0;
                ChangePosition(0);
            }
            else
            {
                Debug.LogWarning("Invalid currentPosition in Select Level UI: " + currentPosition);
            }
        }
    }
}