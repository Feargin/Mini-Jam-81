using UnityEngine;

public class Entity : MonoBehaviour
{
	[HideInInspector] public Movement movement;
	
	private void Awake()
	{
		movement = GetComponent<Movement>();
	}
}
