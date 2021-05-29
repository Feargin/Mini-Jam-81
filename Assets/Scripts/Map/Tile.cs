using UnityEngine;

public class Tile : MonoBehaviour
{
	[ReadOnly] public Entity EntityIn;
	[SerializeField] private MeshRenderer _meshRender;
	
	[SerializeField] private bool _walkable = true;
	public bool Passable
	{
		get
		{
			return (_walkable);
		}
		private set
		{
			_walkable = value;
		}
	}
	
	public void SetColor(Color color)
	{
		_meshRender.material.color = color;
	}
}
