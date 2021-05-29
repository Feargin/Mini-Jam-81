using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeTurn : Singleton<ChangeTurn>
{
	[SerializeField] private Button _nextTurn;
	[SerializeField] private TMP_Text _countTurnText;
    public int CountTurn = 0;
    
    #region Events
	public static event System.Action<bool> TheNextTurn;
    
    #endregion
	protected void OnEnable()
	{
		Spawn.OnGameStart += OnGameStart;
	}
	
	protected void OnDisable()
	{
		Spawn.OnGameStart -= OnGameStart;
	}
	
	private void OnGameStart()
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
        CountTurn += 1;
        _countTurnText.text = "" + CountTurn;
    }
    void Update()
    {
        
    }
}
