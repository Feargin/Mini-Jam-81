﻿using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
	private PF_AStar pathfinding;
	private List<Node> path;
	private Entity entity;
	
	private void Awake()
	{
		pathfinding = PF_AStar.Instance;
	}
	
	public void MoveTo(Vector3 position)
	{
		path = pathfinding.FindPath(transform.position, position);
		List<Vector3> pathL = new List<Vector3>();
		foreach(var v in path)
		{
			Vector3 v3 = Vector3.zero;
			v3.x = v.Position.x * pathfinding.map.GridSize;
			v3.y = transform.position.y;
			v3.z = v.Position.y * pathfinding.map.GridSize;
			pathL.Add(v3);
		}
		if(pathL.Count > 0)
			transform.DOPath(pathL.ToArray(), 1f).SetEase(Ease.Linear);
	}
	
	protected void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent<Tile>(out Tile tile))
		{
			tile.EntityIn = GetComponent<Entity>();
		}
	}
	
	protected void OnTriggerExit(Collider other)
	{
		if(other.TryGetComponent<Tile>(out Tile tile) && tile.EntityIn == entity)
		{
			tile.EntityIn = null;
		}
	}
	
	//protected void OnDrawGizmos()
	//{
	//	if(path == null)
	//		return;
	//	Gizmos.color = Color.red;
	//	foreach(var v in path)
	//	{
	//		Vector3 v3 = Vector3.zero;
	//		v3.x = v.Position.x * pathfinding.map.GridSize;
	//		v3.z = v.Position.y * pathfinding.map.GridSize;
			
	//		Gizmos.DrawWireSphere(v3 + Vector3.up * 0.5f, 0.4f);
	//	}
	//}
}
