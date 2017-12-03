using UnityEngine;

namespace Game.Scripts
{
    public class SizeChanger : MonoBehaviour
    {
        public float FoodAmount = 0.5f;
        public AudioClip SoundEffect;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponent<PlayerController>();
            if (!player)
            {
                return;
            }

            SoundManager.Instance.PlayAudio(SoundEffect);
            player.ChangeSize(FoodAmount);
            gameObject.SetActive(false);
        }
    }
}