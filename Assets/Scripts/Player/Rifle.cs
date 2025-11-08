using UnityEngine;
using UnityEngine.InputSystem;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Settings")]
    [SerializeField] private float shootRange = 20f;
    [SerializeField] private LayerMask enemyLayerMask;
    
    [Header("Visual Feedback")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float lineDisplayTime = 0.1f;

    void Start()
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }
        
        Debug.Log("🎯 Rifle inicializado - Listo para disparar con Input System");
        
        // Verificar que el Input System esté disponible
        if (Mouse.current == null)
        {
            Debug.LogWarning("⚠️ Mouse no detectado en Input System");
        }
        else
        {
            Debug.Log("✅ Input System Mouse detectado correctamente");
        }
    }
    
    void Update()
    {
        // SOLO Input System - NO usar Input.GetMouseButtonDown
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("🔫 BOTÓN IZQUIERDO PRESIONADO - DISPARANDO!");
            Shoot();
        }
        
        // Opcional: también disparar con Space bar para testing
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("🔫 ESPACIO PRESIONADO - DISPARANDO!");
            Shoot();
        }
    }
    
    private void Shoot()
    {
        RaycastHit hit;
        Vector3 startPosition = transform.position;
        Vector3 direction = transform.forward;
        
        Debug.Log($"🎯 RAYCAST desde: {startPosition} dirección: {direction}");

        if (Physics.Raycast(startPosition, direction, out hit, shootRange, enemyLayerMask))
        {
            Debug.Log($"✅ GOLPEÓ: {hit.collider.gameObject.name}");
            Debug.Log($"   📍 Posición impacto: {hit.point}");
            Debug.Log($"   🏷️ Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");
            
            Debug.DrawRay(startPosition, direction * hit.distance, Color.red, 5f);
            ShowShotLine(startPosition, hit.point);
            
            // Verificar IInteractable
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log($"🎯 LLAMANDO Interact() en: {hit.collider.gameObject.name}");
                interactable.Interact();
                Debug.Log("💫 ¡ENEMIGO ATURDIDO!");
            }
            else
            {
                Debug.LogWarning($"❌ NO tiene IInteractable: {hit.collider.gameObject.name}");
                
                // Debug: mostrar todos los componentes
                Component[] allComponents = hit.collider.GetComponents<Component>();
                Debug.Log($"📋 Componentes en {hit.collider.gameObject.name}:");
                foreach (Component comp in allComponents)
                {
                    Debug.Log($"   - {comp.GetType().Name}");
                }
            }
        }
        else
        {
            Debug.Log($"❌ RAYCAST FALLÓ - No golpeó ningún enemigo");
            Debug.Log($"   📏 Rango máximo: {shootRange}");
            Debug.Log($"   🎯 Layer Mask: {enemyLayerMask.value}");
            
            Debug.DrawRay(startPosition, direction * shootRange, Color.yellow, 5f);
            ShowShotLine(startPosition, startPosition + direction * shootRange);
        }
    }
    
    private void ShowShotLine(Vector3 start, Vector3 end)
    {
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
            lineRenderer.enabled = true;
            Invoke(nameof(HideShotLine), lineDisplayTime);
        }
    }
    
    private void HideShotLine()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }
    
    // Método para debug visual en el Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * shootRange);
    }
}