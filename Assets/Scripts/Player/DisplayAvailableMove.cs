using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PF_AStar))]
public class DisplayAvailableMove : MonoBehaviour
{
	[SerializeField] private Color _passableColor = Color.yellow;
	[SerializeField] private LayerMask _playerMask;
	[SerializeField] private float _displayUpdate = 0.3f;
	private bool _mouseOver = false;
	private PF_AStar _pathfinding;
	private Movement _movement;
	private List<Node> _passablePath;
	private bool _checkMouseExit = true;
	private List<Node> _entities;
	
	private void Awake()
	{
		_mouseOver = false;
		_pathfinding = GetComponent<PF_AStar>();
		_movement = GetComponent<Movement>();
	}
	
	public void Enable(bool checkMouseExit = false)
	{
		_checkMouseExit = checkMouseExit;
		CalculatePath();
		ShowPassable();
	}
	
	public void Disable()
	{
		_checkMouseExit = true;
		HidePassable();
	}
	
	void Update()
	{
		if(_checkMouseExit)
		{
			RaycastHit hit;
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.PositiveInfinity, _playerMask, QueryTriggerInteraction.Ignore))
			{
				if(hit.transform == transform)
				{
					if(_mouseOver == false)
					{
						_mouseOver = true;
						CalculatePath();
						ShowPassable();
					}
				}
			}
			else
			{
				if(_mouseOver == true)
				{
					_mouseOver = false;
					HidePassable();
				}
			}
		}
	}
	
	private void CalculatePath()
	{
		if(_mouseOver)
		{
			if(_passablePath != null)
				_passablePath.Clear();
			//_passablePath = _pathfinding.FindPossibleMovement(transform.position, _entities._currentActionPoints, out _entities);
			Debug.Log(_entities.Count);
		}
	}
	
	private void ShowPassable()
	{
		if(_passablePath != null && _passablePath.Count > 0)
		{
			foreach(Node n in _passablePath)
			{
				_pathfinding.map.tiles[(int)n.Position.x, (int)n.Position.y].SetColor(_passableColor);
			}
		}
	}
	
	private void HidePassable()
	{
		if(_passablePath != null && _passablePath.Count > 0)
		{
			foreach(Node n in _passablePath)
			{
				_pathfinding.map.tiles[(int)n.Position.x, (int)n.Position.y].SetColor(Color.white);
			}
			if(_passablePath != null)
				_passablePath.Clear();
		}
	}
	
	//protected void OnDrawGizmos()
	//{
	//	if(_passablePath != null && _passablePath.Count > 0)
	//	{
	//		Gizmos.color = Color.red;
	//		foreach (Node n in _passablePath)
	//		{
	//			Vector3 v3 = new Vector3(n.Position.x, 1f, n.Position.y);
	//			Gizmos.DrawWireSphere(v3, 0.3f);
	//		}
	//	}
	//}
}
