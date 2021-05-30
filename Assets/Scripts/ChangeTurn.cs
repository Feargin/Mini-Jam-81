using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeTurn : Singleton<ChangeTurn>
{
	[SerializeField] private int [] _numTurn;
	[SerializeField] private int [] _countEnemy;
	[SerializeField] private int [] _indexEnemy;

	[SerializeField] private Button _nextTurn;
	[SerializeField] private TMP_Text _countTurnText;
	private float _timer;
    public int CountTurn = 0;

    #region Events
	public static event System.Action<bool> TheNextTurn;
    
    #endregion
	protected void OnEnable() => Spawn.OnGameStart += OnGameStart;

	protected void OnDisable() => Spawn.OnGameStart -= OnGameStart;

	private void OnGameStart()
	{
		
	}

	private IEnumerator Timer()
	{
		while (true)
		{
			yield return new WaitForSeconds(5f);
			FinishEnemyTurn();
			yield break;
		}
	}

    public void NextTurn()
    {
        _nextTurn.interactable = false;
        Spawn.Instance.PlayerControler.GetComponent<PlayerMovement>().enabled = false;
        TheNextTurn?.Invoke(true);
        StartCoroutine(Timer());
        
    }

    public void FinishEnemyTurn()
    {
	    StopCoroutine(Timer());
	    _nextTurn.interactable = true;
	    Spawn.Instance.PlayerControler.GetComponent<PlayerMovement>().enabled = true;
	    TheNextTurn?.Invoke(false);
	    CountTurn += 1;
	    _countTurnText.text = "" + CountTurn;
	    foreach (var v in _numTurn)
	    {
		    if (v == CountTurn)
		    {
			    for (int i = 0; i < _indexEnemy.Length; i++)
			    {
				    Spawn.Instance.Creator(_indexEnemy[i], _countEnemy[i]);
			    }
		    }
	    }
    }

    void Update()
    {
        
    }
}
