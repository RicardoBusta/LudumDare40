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

        private const float TRANSITION_TIME = 3;

        public Text PointMultiplier;

        private float goalSize = 0.5f;
        private float currentSize = 0.5f;

        private void Start()
        {
            Scale(goalSize);
        }

        public void Tap()
        {
            speed = TapSpeed;
        }

        public void ChangeSize(float amount)
        {
            var previousSize = goalSize;
            goalSize = Mathf.Clamp(goalSize+amount, 0.5f, 5.0f);
            PointMultiplier.text = goalSize.ToString(CultureInfo.InvariantCulture) + "x";
        }

        private void Update()
        {
            speed -= Gravity * Time.deltaTime;
            transform.position += Vector3.up * speed * Time.deltaTime;
            currentSize = Mathf.MoveTowards(currentSize, goalSize, Time.deltaTime);
            Scale(currentSize);
        }

        private void Scale(float scale)
        {
            Debug.Log(scale);
            transform.localScale = Vector3.one * scale;
            foreach (Transform child in transform)
            {
                child.localScale = Vector3.one * 1 / scale;
            }
        }
    }
}