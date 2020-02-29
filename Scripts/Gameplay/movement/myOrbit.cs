using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myOrbit : MonoBehaviour
{
	public float GravityScale = 10;

	public Rigidbody2D rigidbodyCast;
	protected Collider2D wallCollider;
    protected Collider2D groundCollider;
    public int onGround = 0;
	public int onWall = 0;

	// Use this for initialization
	protected void Start ()
	{
		try {
			rigidbodyCast = GetComponent<Rigidbody2D> ();
		} catch {
			Debug.Log ("expected to have Rigidbody on object " + gameObject.name + " with Orbit Script.");
		}
	}
	
    public groundNav getNavMesh()
    {
        if (onGround == 0) return null;
        return groundCollider.GetComponent<groundNav>();
    }

    public Vector3 getGravity(Vector3 pos)
    {
        Vector3 grav = Vector3.zero;
        foreach (GameObject source in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            myTags tag_object = source.GetComponent<myTags>();
            if (tag_object != null && tag_object.gravityStrength != 0)
            {
                Vector3 sVec = source.transform.position - pos;
                float sourceScale = GravityScale * tag_object.gravityStrength;
                sVec = sVec / Mathf.Pow(sVec.magnitude, 2f);
                sVec = sVec * sourceScale;
                grav = grav + sVec;
            }
        }
        grav = grav * rigidbodyCast.mass;
        return grav;
    }

	// Update is called once per frame
	public virtual void FixedUpdate()
	{
        if (onWall != 0) { }
        else if (onGround != 0) { }
        else rigidbodyCast.AddForce(getGravity(transform.position));
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		myTags tag_object = other.gameObject.GetComponent<myTags>();
		if (tag_object != null){
            if (tag_object.isGround)
            {
                onGround += 1;
                groundCollider = other;
            }
			if (tag_object.isWall)
            {
				onWall += 1;
				wallCollider = other;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		myTags tag_object = other.gameObject.GetComponent<myTags>();
		if (tag_object != null){
			if (tag_object.isGround) onGround -= 1;
			if (tag_object.isWall) onWall -= 1;
		}
	}
}
