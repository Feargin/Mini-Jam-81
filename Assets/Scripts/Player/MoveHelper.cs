﻿using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoveHelper : Singleton<MoveHelper>
{
	[ReadOnly] public Entity _selectedEntity;
	[SerializeField] private LayerMask _entityMask;
	[SerializeField] private Color _passableColor = Color.yellow;
	[SerializeField] private Color _enemyColor = Color.red;
	[SerializeField] private GameObject infoPanel;
	[SerializeField] private TMP_Text[] stats;
	[SerializeField] private Sprite[] imageInfo;
	[SerializeField] private Image imalePanel;
	
	private PF_AStar _pathfinding;
	private List<Node> _passablePath;
	private bool _mouseOverSelected = false;
	private List<Node> _obstacles;
	private bool _lockSelector = false;
	
	private void OnEnable() 
	{
		PlayerSelector.OnPlayerSelect += OnPlayerSelect;
		PlayerSelector.OnPlayerDeselect += OnPlayerDeselect;
	}
	
	private void OnDisable() 
	{
		PlayerSelector.OnPlayerSelect -= OnPlayerSelect;
		PlayerSelector.OnPlayerDeselect -= OnPlayerDeselect;
	}
	
	private void OnPlayerSelect(Entity player)
	{
		UnlockSelect();
		LockSelectTo(player);
	}
	
	private void OnPlayerDeselect(Entity player)
	{
		UnlockSelect();
	}
	
	private void Update()
	{
		if(!_lockSelector)
		{
			SelectEntity();
		}
		if(_passablePath != null && _passablePath.Count > 0)
		{
			ShowArea();
		}

		InfoPanel();
	}
	
	public void LockSelectTo(Entity entity)
	{
		_lockSelector = true;
		_mouseOverSelected = true;
		_selectedEntity = entity;
		_pathfinding = _selectedEntity.movement.pathfinding;
		CalculatePath();
	}
	
	public void UnlockSelect()
	{
		DeselectEntity();
		_lockSelector = false;
	}
	
	public void SelectEntity(Entity entity)
	{
		_mouseOverSelected = true;
		_selectedEntity = entity;
		_pathfinding = _selectedEntity.movement.pathfinding;
		CalculatePath();
	}
	
	public void DeselectEntity()
	{
		HideArea();
		_mouseOverSelected = false;
		_selectedEntity = null;
		_pathfinding = null;
		_passablePath.Clear();
		_obstacles.Clear();
	}
	
	private void SelectEntity()
	{
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.PositiveInfinity, _entityMask, QueryTriggerInteraction.Ignore))
		{
			if(hit.transform.TryGetComponent<Entity>(out Entity e) && _selectedEntity != e)
			{
				if(_mouseOverSelected == false)
				{
					_mouseOverSelected = true;
					_selectedEntity = e;
					
					_pathfinding = _selectedEntity.movement.pathfinding;
					CalculatePath();
				}
			}
		}
		else if(_mouseOverSelected == true)
		{
			_mouseOverSelected = false;
			DeselectEntity();
		}
	}

	private void InfoPanel()
	{
		if (_selectedEntity is { } && _selectedEntity.gameObject.layer == 8)
		{
			infoPanel.SetActive(true);
			imalePanel.sprite = imageInfo[_selectedEntity.TypeEnemy];
			stats[0].text = _selectedEntity.name;
			stats[1].text = "" + _selectedEntity.gameObject.GetComponent<Attak>()._damage;
			stats[2].text = "" + _selectedEntity.MaxActionPoints;
		}
		else
		{
			infoPanel.SetActive(false);
		}
	}
	
	private void CalculatePath()
	{
		if(_selectedEntity is Enemy)
			_passablePath = _pathfinding.FindPossibleMovement(_selectedEntity.transform.position, _selectedEntity.MaxActionPoints, out _obstacles);
		else
			_passablePath = _pathfinding.FindPossibleMovement(_selectedEntity.transform.position, _selectedEntity._currentActionPoints, out _obstacles);
	}
	
	private void ShowArea()
	{
		if(_passablePath != null && _passablePath.Count > 0)
		{
			foreach(Node n in _passablePath)
			{
				Tile tile = _pathfinding.map.tiles[(int)n.Position.x, (int)n.Position.y];
				tile.SetColor(_passableColor);
			}
			foreach(Node n in _obstacles)
			{
				Tile tile = _pathfinding.map.tiles[(int)n.Position.x, (int)n.Position.y];
				if(tile.EntityIn is Enemy)
					tile.SetColor(_enemyColor);
			}
		}
	}
	
	private void HideArea()
	{
		if(_passablePath != null && _passablePath.Count > 0)
		{
			foreach(Node n in _passablePath)
			{
				_pathfinding.map.tiles[(int)n.Position.x, (int)n.Position.y].SetColor(Color.white);
			}
			foreach(Node n in _obstacles)
			{
				_pathfinding.map.tiles[(int)n.Position.x, (int)n.Position.y].SetColor(Color.white);
			}
			if(_passablePath != null)
			{
				_passablePath.Clear();
				_obstacles.Clear();
			}
		}
	}
}
