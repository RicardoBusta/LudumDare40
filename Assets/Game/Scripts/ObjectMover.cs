using UnityEngine;

namespace Game.Scripts
{
    public class ObjectMover : MonoBehaviour
    {
        private void Update()
        {
            transform.position += Vector3.left * GameManager.Instance.PlayerSpeed *
                                  Time.deltaTime * GameManager.Instance.Player.Multiplier * 2;

            if (transform.position.x < -15)
            {
                gameObject.SetActive(false);
            }
        }
    }
}