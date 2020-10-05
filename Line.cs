using UnityEngine;
using System.Collections.Generic;

public class Line : MonoBehaviour {

	public LineRenderer lineRenderer;
	public EdgeCollider2D edgeCollider;
	public Rigidbody2D rigidBody;

	[HideInInspector] public List<Vector2> points = new List<Vector2> ( );
	[SerializeField] public int pointsCount = 0,x=0;
	public GameObject ball,balls,gaga;
	[SerializeField]Vector2 firstpoint,lastpoint;
	public Transform konum,changeposition;
	Vector2 bas,son;
	bool onetime = true, onetinee=true;
	GameObject deneme,wheel,tasima,parent;
	//The minimum distance between line's points.
	float pointsMinDistance = 0.1f;

	//Circle collider added to each line's point
	float circleColliderRadius, startpos;
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			x++;
		}
		if ( x==1 && Input.GetMouseButton(0))
		{
			x = 0;
			Destroy(deneme);
			Destroy(balls);
			Destroy(wheel);

		}
		

		/*if (onetime && !Input.GetMouseButton(0))
		{
			crate();
			//Invoke("crate", 0.5f);
			onetime = false;
		}*/
		//ball.transform.position = new Vector2(firstpoint.x,firstpoint.y);
		
		parent = GameObject.Find("LinesDrawer");
		deneme = GameObject.FindWithTag("Finish");
		wheel = GameObject.FindWithTag("Player");
		
		if (onetinee && !Input.GetMouseButton(0))
		{
			crate();
			Invoke("changepos", 0.01f);
			startpos = Vector2.Distance(parent.transform.position, konum.transform.position);
			onetinee = false;
		}
	}



	 void crate()
	{


		
		balls = Instantiate(ball, firstpoint, Quaternion.identity);
		balls.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		//balls.GetComponent<Rigidbody2D>().gravityScale = 0;
		balls.transform.parent = parent.transform;
		balls.transform.localPosition = new Vector3(konum.transform.position.x+startpos, konum.transform.localPosition.y+startpos, 0);
		balls = Instantiate(ball, lastpoint, Quaternion.identity);
		balls.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		//balls.GetComponent<Rigidbody2D>().gravityScale = 0;
		balls.transform.parent = parent.transform;

	}
	private void changepos()
	{

		deneme.transform.localPosition = new Vector3(konum.transform.position.x, konum.transform.localPosition.y, 0);
		parent.transform.localPosition= new Vector3(konum.transform.position.x, konum.transform.localPosition.y, 0);
		//deneme.GetComponent<Rigidbody2D>().gravityScale = 0;
		deneme.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		deneme.transform.rotation = Quaternion.Euler(0, 0, 0);
		
	}



	public void AddPoint ( Vector2 newPoint ) {
		//If distance between last point and new point is less than pointsMinDistance do nothing (return)
		if ( pointsCount >= 1 && Vector2.Distance ( newPoint, GetLastPoint ( ) ) < pointsMinDistance )
			return;

		points.Add ( newPoint );
		pointsCount++;

		//Add Circle Collider to the Point
		CircleCollider2D circleCollider = this.gameObject.AddComponent <CircleCollider2D> ( );
		circleCollider.offset = newPoint;
		circleCollider.radius = circleColliderRadius;

		//Line Renderer
		lineRenderer.positionCount = pointsCount;
		lineRenderer.SetPosition ( pointsCount - 1, newPoint );

		//Edge Collider
		//Edge colliders accept only 2 points or more (we can't create an edge with one point :D )
		if ( pointsCount > 1 )
			edgeCollider.points = points.ToArray ( );
	}

	public Vector2 GetLastPoint ( ) {
		firstpoint = lineRenderer.GetPosition(pointsCount - 1);
		lastpoint = lineRenderer.GetPosition(0);
		
		return ( Vector2 )lineRenderer.GetPosition ( pointsCount - 1 );
	}

	public void UsePhysics ( bool usePhysics ) {
		// isKinematic = true  means that this rigidbody is not affected by Unity's physics engine
		rigidBody.isKinematic = !usePhysics;
	}

	public void SetLineColor ( Gradient colorGradient ) {
		lineRenderer.colorGradient = colorGradient;
	}

	public void SetPointsMinDistance ( float distance ) {
		pointsMinDistance = distance;
	}

	public void SetLineWidth ( float width ) {
		lineRenderer.startWidth = width;
		lineRenderer.endWidth = width;

		circleColliderRadius = width / 2f;

		edgeCollider.edgeRadius = circleColliderRadius;
	}

}
