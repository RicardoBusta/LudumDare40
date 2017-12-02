using UnityEngine;

namespace Game.Scripts
{
    public class SizeChanger : MonoBehaviour
    {
        public float FoodAmount = 0.5f;
       
        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponent<PlayerController>();
            if (!player)
            {
                return;
            }
        
            player.ChangeSize(FoodAmount);
            gameObject.SetActive(false);
        }

        private void Update()
        {
            transform.position += Vector3.left * GameManager.Instance.PlayerSpeed * Time.deltaTime;
        }
    }
}