using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CardRotation : MonoBehaviour
{
    public Transform cardFront;
    public Transform cardBack;
    public Transform targetFacePoint;
	public  Transform UITransform;
    public Collider col;
    private bool showingBack = false;

	private void Start()
	{
        col = GetComponent<Collider>();
	
	}
	private void Update()
	{
		RaycastHit[] hits;
		hits = Physics.RaycastAll(UITransform.position,
			(-UITransform.position + targetFacePoint.position).normalized,
			(-UITransform.position + targetFacePoint.position).magnitude);
		bool passedThorough = false;

		foreach (var h in hits)
		{
			if (h.collider == col)
			{
				passedThorough = true;
			}
		}
		if(passedThorough!= showingBack)
		{
			showingBack = passedThorough;
			if (showingBack)
			{
				cardFront.gameObject.SetActive(false);
				cardBack.gameObject.SetActive(true);
			}
			else
			{
				cardFront.gameObject.SetActive(true);
				cardBack.gameObject.SetActive(false);
			}
		}
	}
}
