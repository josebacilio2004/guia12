using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestiona los distintos estados y pantallas de la UI.
/// Implementa patrón Singleton y el patrón State.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject inGameHudPanel;
    [SerializeField] private GameObject victoryPanel;

    private Dictionary<string, GameObject> panels;

    private UIState _currentState;

    public MainMenuState MainMenuState { get; private set; }
    public InGameState InGameState { get; private set; }
    public PauseMenuState PauseMenuState { get; private set; }
    public VictoryState VictoryState { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Diccionario de panels
        panels = new Dictionary<string, GameObject>
        {
            { "MainMenu", mainMenuPanel },
            { "Pause", pauseMenuPanel },
            { "HUD", inGameHudPanel },
            { "Victory", victoryPanel }
        };

        // Estados
        MainMenuState = new MainMenuState(this);
        InGameState = new InGameState(this);
        PauseMenuState = new PauseMenuState(this);
        VictoryState = new VictoryState(this);
    }

    private void Start()
    {
        // Estado inicial
        ChangeState(MainMenuState);
    }

    private void Update()
    {
        // Escape → Pausa / Reanudar
        if (UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (_currentState == InGameState)
                ChangeState(PauseMenuState);
            else if (_currentState == PauseMenuState)
                ChangeState(InGameState);
        }
    }

    public void ChangeState(UIState newState)
    {
        if (_currentState == newState) return;

        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    /// <summary>Activa un panel y desactiva los demás.</summary>
    public void ShowPanel(string key)
    {
        foreach (var p in panels.Values)
            p.SetActive(false);

        if (panels.ContainsKey(key))
            panels[key].SetActive(true);
        else
            Debug.LogWarning($"Panel '{key}' no está registrado.");
    }

    // --- Métodos UI Buttons ---

    public void OnPlayButtonClicked() => ChangeState(InGameState);
    public void OnResumeButtonClicked() => ChangeState(InGameState);
    public void OnExitButtonClicked() => Application.Quit();

}
