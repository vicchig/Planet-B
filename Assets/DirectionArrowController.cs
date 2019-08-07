using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionArrowController : MonoBehaviour
{
    public GameObject nextLevelMarker;
    public GameObject player;
    public bool levelCompleted;
    public float radius;
    public int renderDistance;

    private SpriteRenderer image;
    private float angle;
    private Vector3 direction;
    private float distanceToMarker;
    private float renderRadius;

    private void Start()
    {
        image = this.GetComponent<SpriteRenderer>();
        distanceToMarker = 0;
        renderRadius = radius;
        angle = 0;
    }

    private void Update()
    {
        distanceToMarker = Vector3.Distance(nextLevelMarker.transform.position, player.transform.position);
        direction = nextLevelMarker.transform.position - player.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x);

        if (distanceToMarker < 10)
        {
            renderRadius = distanceToMarker * 0.5f;
        }
        else {
            renderRadius = radius;
        }

        transform.position = new Vector3(Mathf.Cos(angle) * renderRadius, Mathf.Sin(angle) * renderRadius, 0) + player.transform.position;
        transform.eulerAngles = new Vector3(0f, 0f, angle * Mathf.Rad2Deg);
   
        if (levelCompleted && distanceToMarker > renderDistance)
        {
            image.color = new Color(1, 1, 1, 1);
        }
        else {
            image.color = new Color(1, 1, 1, 0);
        }
    }
}
