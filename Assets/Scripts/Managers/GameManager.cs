using System.Collections;
using UnityEngine;

/// <summary>
/// Gestiona los estados del juego: jugar, ganar, perder.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Playing, Victory, Loss }
    private GameState _currentState;

    [Header("Gameplay Settings")]
    [SerializeField] private int _objectivesToWin = 3;

    public GameLogic Logic { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Logic = new GameLogic(_objectivesToWin);
    }

    private void OnEnable()
    {
        GameEvents.OnObjectiveActivated += HandleObjectiveActivated;
    }

    private void OnDisable()
    {
        GameEvents.OnObjectiveActivated -= HandleObjectiveActivated;
    }

    private void Start()
    {
        ChangeState(GameState.Playing);
    }

    private void HandleObjectiveActivated()
    {
        if (_currentState != GameState.Playing) return;

        Logic.CompleteObjective();

        Debug.Log($"Objetivo Completado. Progreso: {Logic.ObjectivesCompleted}/{Logic.ObjectivesToWin}");

        if (Logic.IsVictoryConditionMet)
            ChangeState(GameState.Victory);
    }

    public void ChangeState(GameState newState)
    {
        if (_currentState == newState) return;

        _currentState = newState;
        Debug.Log($"Nuevo estado del juego: {_currentState}");

        switch (_currentState)
        {
            case GameState.Playing:
                break;

            case GameState.Victory:
                StartCoroutine(VictorySequence());
                break;

            case GameState.Loss:
                break;
        }
    }

    private IEnumerator VictorySequence()
    {
        Debug.Log("Secuencia de victoria iniciada...");

        // Desactivar control del jugador
        var player = FindFirstObjectByType<FirstPersonController>();
        if (player != null)
            player.enabled = false;

        yield return new WaitForSeconds(1f);

        // Mostrar UI de victoria
        if (UIManager.Instance != null)
            UIManager.Instance.ChangeState(UIManager.Instance.VictoryState);

        yield return new WaitForSeconds(3f);

        // Regresar al menú principal
        UIManager.Instance.ChangeState(UIManager.Instance.MainMenuState);
    }
}
