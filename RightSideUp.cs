
using UnityEngine;

public class RightSideUp : MonoBehaviour

{
	[SerializeField]
	private GameObject player;

	public void TestAlignment()
	{
		if (Vector3.Dot(player.transform.up, Vector3.down) > 0)
        {
        	Debug.Log("Ooh what a feeling");
        }
	}
}