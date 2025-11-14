using UnityEngine;

public class ChaseState : AIState
{
    private float _attackRange;
    
    public ChaseState(AIController controller) : base(controller) 
    {
        _attackRange = controller.attackRange;
    }
    
    public override void OnEnter()
    {
        Debug.Log("🎯 Entrando en estado de Persecución");
        m_agent.speed = m_controller.chaseSpeed;
    }

    public override void UpdateState()
    {
        float distanceToPlayer = Vector3.Distance(m_controller.transform.position, m_playerTransform.position);
        
        // 1. Condición de transición: ¿hemos perdido al jugador?
        if (distanceToPlayer > m_controller.loseSightRadius)
        {
            m_controller.ChangeState(new PatrolState(m_controller));
            return;
        }
        
        // 2. NUEVA CONDICIÓN: ¿estamos en rango de ataque?
        if (distanceToPlayer <= _attackRange)
        {
            m_controller.ChangeState(new AttackState(m_controller));
            return;
        }

        // 3. Lógica del estado: perseguir al jugador
        m_agent.destination = m_playerTransform.position;
    }

    public override void OnExit() 
    {
        Debug.Log("🛑 Saliendo del estado de Persecución");
    }
}