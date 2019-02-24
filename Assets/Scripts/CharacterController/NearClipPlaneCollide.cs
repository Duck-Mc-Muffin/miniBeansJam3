using UnityEngine;
using System.Collections;

public class NearClipPlaneCollide : MonoBehaviour
{
	private Camera myCam;
    private Vector3 fixPoint;

	void Start()
	{
		// Initialisierungen
		myCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        fixPoint = transform.localPosition;
    }

	void LateUpdate()
	{
        // Object auf der lokalen Z-Achse positionieren.
		RaycastHit hitInfo;
		if (Physics.BoxCast(transform.parent.position, CameraBox(), transform.position - transform.parent.position, out hitInfo, transform.rotation, Mathf.Abs(fixPoint.z)))
		{
			// Position noch vor das Hindernis setzen.
			transform.localPosition = new Vector3(fixPoint.x, fixPoint.y, -hitInfo.distance);
		}
		else
		{
			// Position auf die Maximaldistanz zwischen Objekt und ParrentObjekt setzen.
			transform.localPosition = fixPoint;
		}
	}

    /// <summary>
    /// Die größe der NearClipPlane für den "Physics.BoxCast(...)" zurückgeben.
    /// </summary>
	private Vector3 CameraBox()
	{
		// Die Größe der NearClipPlane von der MainCamera im Worldspace herrausfinden.
		Vector3 size = myCam.ViewportToWorldPoint(new Vector3(1f, 1f, myCam.nearClipPlane)) - myCam.ViewportToWorldPoint(new Vector3(0f, 0f, myCam.nearClipPlane));

		// Die größe der NearClipPlane als Vector3 zurückgeben.
		return new Vector3(size.x / 2, size.y / 2, 0.05f);
	}
}