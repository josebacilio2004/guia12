using System;

public static class GameEvents{
    public static event Action OnObjectiveActivated;

    public static void TriggerObjectiveActivated(
    ){
                OnObjectiveActivated?.Invoke(); 

    }
}