using UnityEngine;

public class LootChestController : MonoBehaviour,IInteractable
{
    private bool _isOpened= false;

    public void Interact()
    {
        if(_isOpened)
        {
            Debug.Log("Haz ABIERTO este cofre.");
            return;
        }

        _isOpened = true;
        Debug.Log("Haz ABIERTO el cofre y encontrado un tesoro de oro en el interior del cofre. Felicidades!");

        //Aqui instanciarías un item añadirias oro al inventario, etc
    }
}