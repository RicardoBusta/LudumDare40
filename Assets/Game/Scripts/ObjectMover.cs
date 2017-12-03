using UnityEngine;

namespace Game.Scripts
{
	public class ObjectMover : MonoBehaviour {
		private void Update()
		{
			transform.position += Vector3.left * GameManager.Instance.PlayerSpeed * Time.deltaTime;

			if (transform.position.x < -11)
			{
				gameObject.SetActive(false);
			}
		}
	}
}
