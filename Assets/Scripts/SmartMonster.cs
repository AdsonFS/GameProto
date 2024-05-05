using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SmartMonster : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public List<Transform> players = new List<Transform>();
    public List<Tuple<int, int>> directions = new List<Tuple<int, int>>()
    {
        new(1, 0),
        new (-1, 0),
        new (0, 1),
        new (0, -1)
    };
    void Start()
    {
        movePoint.parent = null;
        players = new List<Transform>(GameObject.FindGameObjectsWithTag("Player").Select(player => player.transform));
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) > 0f) return;

        float minDistance = float.MaxValue;
        Vector3 closestPlayer = transform.position;
        directions.ForEach(direction =>
        {
            var newPosition = movePoint.position + new Vector3(direction.Item1, direction.Item2, 0f);


            if (Physics2D.OverlapCircle(newPosition, 0.2f, whatStopsMovement) == null && players.Select(player => Vector3.Distance(newPosition, player.position)).Min() < minDistance)
            {
                minDistance = players.Select(player => Vector3.Distance(newPosition, player.position)).Min();
                closestPlayer = newPosition;
            }

        });
        movePoint.position = closestPlayer;
        // players.ForEach(player =>
        // {
        //     float distance = Vector3.Distance(transform.position, player.position);
        //     if (distance < minDistance)
        //     {
        //         minDistance = distance;
        //         movePoint.position = player.position;
        //     }
        // });
    }

}
