using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;

    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) > 0.02f) return;
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, whatStopsMovement) == null)
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        }
        else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.2f, whatStopsMovement) == null)
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
        }
    }
}
