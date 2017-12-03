using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private float speed = 0;
        public float Gravity = 10;
        public float TapSpeed = 5;

        public float MinSize = 0.5f;
        public float MaxSize = 5;

        public float MinHeight = -4.5f;
        public float MaxHeight = 4.5f;

        private const float TRANSITION_TIME = 1;

        public Text PointMultiplier;

        public Material BirdMaterial;

        public AudioClip FlapSound;

        private const string MULTIPLIER_FORMAT = "Multiplier: {0}x";

        public float Multiplier
        {
            get { return currentSize; }
        }

        private float currentSize = 0.5f;
        private float currentSpriteSize = 0.5f;

        private void Start()
        {
            BirdMaterial.SetFloat("_Hue", Random.Range(0.0f, 1.0f));

            currentSize = MinSize;
            currentSpriteSize = MinSize;
            Scale(currentSize);
            PointMultiplier.text = string.Format(MULTIPLIER_FORMAT,currentSize.ToString("0.0"));
        }

        public void Tap()
        {
            SoundManager.Instance.PlayAudio(FlapSound, pitch: 0.8f, volume: 0.75f);
            speed = TapSpeed;
        }

        public void ChangeSize(float amount)
        {
            if (currentSize <= MinSize && amount < 0)
            {
                Die();
            }

            var previousSize = currentSize;
            currentSize = Mathf.Clamp(currentSize + amount, MinSize, MaxSize);
            PointMultiplier.text = string.Format(MULTIPLIER_FORMAT,currentSize.ToString("0.0"));
        }

        private void Update()
        {
            speed -= Gravity * Time.deltaTime;
            var pos = transform.position;
            pos.y = Mathf.Clamp(pos.y + speed * Time.deltaTime, MinHeight, MaxHeight);
            transform.position = pos;
            currentSpriteSize = Mathf.MoveTowards(currentSpriteSize, currentSize, Time.deltaTime);
            Scale(currentSpriteSize);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 5*speed), 0.1f);
        }

        private void Scale(float scale)
        {
            transform.localScale = Vector3.one * scale;
            foreach (Transform child in transform)
            {
                child.localScale = Vector3.one * 0.75f / scale;
            }
        }

        private void Die()
        {
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }
    }
}