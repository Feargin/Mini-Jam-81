using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(PF_AStar))]
public class Movement : MonoBehaviour
{
	[Header("----------- SETTINGS ----------")]
	[HideInInspector] public PF_AStar pathfinding;
	[HideInInspector] public Tile tile;
	public static event System.Action<Entity> EndMove;
	
	private List<Node> path;
	private Entity entity;
	
	private void Awake()
	{
		pathfinding = GetComponent<PF_AStar>();
		entity = GetComponent<Entity>();
	}

	private void CompliteMove()
	{
		EndMove?.Invoke(entity);
	}
	
	public void MoveTo(Vector3 position, bool ignoreDistance = false, TweenCallback completeEvent = null)
	{
		path = pathfinding.FindPath(transform.position, position);
		List<Vector3> pathL = new List<Vector3>();
		for(int i = 0; i < path.Count; i++)
		{
			if(i > entity._currentActionPoints && ignoreDistance)
				break;
			Vector3 v3 = Vector3.zero;
			v3.x = path[i].Position.x * pathfinding.map.GridSize;
			v3.y = transform.position.y;
			v3.z = path[i].Position.y * pathfinding.map.GridSize;
			pathL.Add(v3);
		}
		if(pathL.Count > 0 && (pathL.Count <= entity._currentActionPoints || ignoreDistance))
		{
			entity._currentActionPoints = 0;
			var sequence = DOTween.Sequence();
			sequence.Append(transform.DOPath(pathL.ToArray(), 1f).SetEase(Ease.Linear));
			sequence.OnComplete(CompliteMove);
			if(completeEvent != null)
				sequence.OnComplete(completeEvent);
		}
	}
	
	protected void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent<Tile>(out Tile _tile))
		{
			_tile.EntityIn = GetComponent<Entity>();
			tile = _tile;
		}
	}
	
	protected void OnTriggerExit(Collider other)
	{
		if(other.TryGetComponent<Tile>(out Tile _tile) && _tile.EntityIn == entity)
		{
			_tile.EntityIn = null;
		}
	}
	
	protected void OnDrawGizmos()
	{
		if(path == null)
			return;
		Gizmos.color = Color.red;
		foreach(var v in path)
		{
			Vector3 v3 = Vector3.zero;
			v3.x = v.Position.x * pathfinding.map.GridSize;
			v3.z = v.Position.y * pathfinding.map.GridSize;
			
			Gizmos.DrawWireSphere(v3 + Vector3.up * 0.5f, 0.4f);
		}
	}
}
