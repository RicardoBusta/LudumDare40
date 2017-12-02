using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFix : MonoBehaviour
{
	private Camera camera;

	private float previousAspect = 0;
	
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
		if (camera.aspect != previousAspect)
		{
			previousAspect = camera.aspect;

			camera.orthographicSize = 9 / previousAspect;
		}
	}
}
