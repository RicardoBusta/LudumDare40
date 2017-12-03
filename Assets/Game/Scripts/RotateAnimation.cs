using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
	public float Angle = 0;
	public float Speed = 1;

	public Vector2 Range = new Vector2(0,1);
	
	void Update ()
	{
		Angle += Time.deltaTime * Speed;
		transform.localRotation = Quaternion.Euler(0, 0, Angle);
	}
}
