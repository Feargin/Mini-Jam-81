using UnityEngine;

public class Tile : MonoBehaviour
{
	[ReadOnly] public Entity EntityIn;
	[SerializeField] private MeshRenderer _meshRender;
	
	[SerializeField] private bool _walkable = true;
	
	public bool IsPassable(bool withEntity)
	{
		return (_walkable && EntityIn == null && withEntity == false) 
			|| (_walkable && EntityIn != null && withEntity == true);
	}
	
	public void SetColor(Color color)
	{
		_meshRender.material.color = color;
	}
}
