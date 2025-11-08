using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("AI Settings")]
    public Transform[] waypoints;
    public float patrolSpeed = 5f;
    public float chaseSpeed = 5f;
    public float detectionRadius = 10f;
    public float loseSightRadius = 15f;

    [Header("Stun Settings")]
    [SerializeField] public float stunDuration = 3f; // Variable expuesta para el diseñador

    private AIState _currentState;
        
    private void Awake()
    {
        ChangeState(new PatrolState(this));
    }    
 
    void Update()
    {
        _currentState?.UpdateState();
    }

    public void ChangeState(AIState newState)
    {
        _currentState?.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }

    // Nuevo método para aturdir
    public void Stun()
    {
        if (_currentState is StunState) return; // Ya está aturdido
        
        ChangeState(new StunState(this, stunDuration));
    }

    // Para acceder desde otras clases si es necesario
    public bool IsStunned()
    {
        return _currentState is StunState;
    }
}