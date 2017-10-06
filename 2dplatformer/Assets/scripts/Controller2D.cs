
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour {

	const float skinWidth = .015f;
	BoxCollider2D col; 
	RaycastOrigins raycastOrigins;

	public int horizontalRayCount = 4; 
	public int verticalRayCount = 4; 

	float horizontalRaySpacing; 
	float verticalRaySpacing; 

	public LayerMask collisionMask;


	// Use this for initialization
	void Start () {

		col = GetComponent<BoxCollider2D> (); 
		CalculateRaySpacing (); 
		
	}
	void UpdateRaycastOrigins()
	{
		Bounds bounds = col.bounds; 
		bounds.Expand (skinWidth * -2f);

		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y); 
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y); 
		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y); 
		raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y); 

	}

	void CalculateRaySpacing()
	{
		Bounds bounds = col.bounds; 
		bounds.Expand (skinWidth * -2f);

		horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp (verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);

		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	struct RaycastOrigins{
		public Vector2 topLeft, topRight; 
		public Vector2 bottomLeft, bottomRight; 



	}


	public void Move(Vector3 velocity)
	{
		UpdateRaycastOrigins (); 
		VerticalCollision (ref velocity);
		transform.Translate (velocity); 



	}

	void VerticalCollision(ref Vector3 velocity)
	{
		float directionY = Mathf.Sign (velocity.y);
		float rayLenght = Mathf.Abs (velocity.y) + skinWidth;

		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * 1 + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);
			Debug.DrawRay (raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.red);
			if (hit) {
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLenght = hit.distance;


			}

		}


	}
}
