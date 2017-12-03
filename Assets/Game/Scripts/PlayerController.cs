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

        private const float TRANSITION_TIME = 3;

        public Text PointMultiplier;

        public float Multiplier
        {
            get { return currentSize; }
        }

        private float currentSize = 0.5f;
        private float currentSpriteSize = 0.5f;

        private void Start()
        {
            currentSize = MinSize;
            currentSpriteSize = MinSize;
            Scale(currentSize);
            PointMultiplier.text = currentSize.ToString(CultureInfo.InvariantCulture) + "x";
        }

        public void Tap()
        {
            speed = TapSpeed;
        }

        public void ChangeSize(float amount)
        {
            if (currentSize <= MinSize && amount < 0)
            {
                gameObject.SetActive(false);
            }

            var previousSize = currentSize;
            currentSize = Mathf.Clamp(currentSize + amount, MinSize, MaxSize);
            PointMultiplier.text = currentSize.ToString(CultureInfo.InvariantCulture) + "x";
        }

        private void Update()
        {
            speed -= Gravity * Time.deltaTime;
            var pos = transform.position;
            pos.y = Mathf.Clamp(pos.y + speed * Time.deltaTime, MinHeight, MaxHeight);
            transform.position = pos;
            currentSpriteSize = Mathf.MoveTowards(currentSpriteSize, currentSize, Time.deltaTime);
            Scale(currentSpriteSize);
        }

        private void Scale(float scale)
        {
            transform.localScale = Vector3.one * scale;
            foreach (Transform child in transform)
            {
                child.localScale = Vector3.one * 0.75f / scale;
            }
        }
    }
}