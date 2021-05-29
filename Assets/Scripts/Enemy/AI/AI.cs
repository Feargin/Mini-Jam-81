using UnityEngine;

public class AI : MonoBehaviour
{
	public bool InitialState = false;
	public AI NextState;
	
	private void OnEnable() => ChangeTurn.TheNextTurn += TurnBegin;
	private void OnDisable() => ChangeTurn.TheNextTurn -= TurnBegin;
	
	private void TurnBegin(bool npc_turn)
	{
		if(npc_turn && InitialState)
		{
			BeginState();
		}
	}
	
	public virtual void BeginState()
	{
		
	}
	
	public virtual void ExitState()
	{
		if(NextState != null)
		{
			NextState.BeginState();
		}
	}
}
