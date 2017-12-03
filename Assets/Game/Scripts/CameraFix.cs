using System;
using UnityEngine;

namespace Game.Scripts
{
    [ExecuteInEditMode]
    public class CameraFix : MonoBehaviour
    {
        private new Camera camera;

        private float previousAspect = 0;

        public float CameraWidth = 13.66f;

        private void Start()
        {
            camera = GetComponent<Camera>();
            if (camera == null)
            {
                throw new MissingComponentException("Camera");
            }
        }

        private void Update()
        {
            if (Math.Abs(camera.aspect - previousAspect) < 0.1f)
            {
                return;
            }

            previousAspect = camera.aspect;
            camera.orthographicSize = CameraWidth / previousAspect;
        }
    }
}