using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[ReadOnly] public Entity EntityIn;
	[SerializeField] private MeshRenderer _meshRender;
	public Color TileColor = Color.white;
	private Color _oldColor;
	[SerializeField] private bool _walkable = true;
	public bool Passable
	{
		get
		{
			return (_walkable && EntityIn == null);
		}
		private set
		{
			_walkable = value;
		}
	}

	public void ResetColor()
	{
		_meshRender.material.color = _oldColor;
	}

	public void SetColor(Color color)
	{
		_oldColor = _meshRender.material.color;
		_meshRender.material.color = color;
		TileColor = color;
	}
}
