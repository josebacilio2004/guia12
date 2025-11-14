# 🎮 Estudio de IA - Práctica 12
**Nombre del Estudio:** Nexus Game Studios

## 👥 Miembros y Roles

| Integrante | Rol | Responsabilidades |
|------------|-----|-------------------|
| Bacilio De La Cruz José Anthony | Arquitecto de IA | Implementación del State Pattern y sistemas de estados |
| Escobar Bendezu Aldrin Edwin | Integrador de Sistemas | Configuración NavMesh e integración de componentes |
| Gutierrez Taipe Luis Alberto | Diseñador de Comportamiento | Balance de parámetros y gameplay |
| Urrutia Uceda Julihno | Líder de Calidad y Documentación | Testing, documentación y gestión del repositorio |

## 🎯 Descripción del Hito

Implementamos un sistema completo de IA utilizando el Patrón State, donde los enemigos patrullan waypoints, persiguen al jugador al detectarlo, atacan cuando están cerca y pueden ser aturdidos con un rifle especial. El sistema incluye diferentes tipos de daño (Stun, Physical, Fire) y transiciones fluidas entre estados.

## 💭 Reflexión del Estudio

### Sinergia y Fricción

**Mayor beneficio:** La división de roles nos permitió trabajar en paralelo y especializarnos en diferentes aspectos del sistema. José pudo enfocarse en la arquitectura mientras Aldrin integraba los componentes.

**Mayor desafío:** Coordinar las interfaces entre los diferentes sistemas (Rifle → IDamageable → AIController). Lo resolvimos mediante reuniones diarias de sincronización y pruebas constantes de integración.

### El Alma de la Máquina

**Parámetro más impactante:** La diferencia entre `detectionRadius` y `loseSightRadius` fue crucial para crear un comportamiento creíble. Un radio de detección de 10 y pérdida de vista de 15 genera esa sensación de "persistencia" donde el enemigo no se rinde inmediatamente, haciendo que se sienta más inteligente y determinado.

## 🚀 Características Implementadas

- ✅ Sistema de estados completo (Patrol, Chase, Attack, Stun)
- ✅ Rifle con diferentes tipos de daño
- ✅ NavMesh con navegación inteligente
- ✅ Sistema de salud y muerte de enemigos
- ✅ Comportamiento creíble y ajustable

## 🎮 Controles

- **Click Izquierdo**: Disparar rifle
- **Tecla 1**: Daño Stun (Aturdimiento)
- **Tecla 2**: Daño Physical (Normal)
- **Tecla 3**: Daño Fire (Extra)
