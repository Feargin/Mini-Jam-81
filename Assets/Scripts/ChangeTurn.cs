using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTurn : MonoBehaviour
{
    [SerializeField] private Button _nextTurn;
    
    #region Events
    public static event System.Action<bool> TheNextTurn;
    #endregion
    void Start()
    {
        
    }

    public void NextTurn()
    {
        _nextTurn.interactable = false;
        Spawn.Instance.PlayerControler.GetComponent<PlayerMovement>().enabled = false;
        TheNextTurn?.Invoke(true);
    }

    public void FinishEnemyTurn()
    {
        _nextTurn.interactable = true;
        Spawn.Instance.PlayerControler.GetComponent<PlayerMovement>().enabled = true;
        TheNextTurn?.Invoke(false);
    }
    void Update()
    {
        
    }
}
