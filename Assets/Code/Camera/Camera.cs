using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float dumping = 1.5f;
    [SerializeField] private Vector3 offset = new Vector3(2f, 1f,-15f);
    private bool isLeft;
    private int lastX;

    void Start()
    {
        offset = new Vector3(Mathf.Abs(offset.x), offset.y, offset.z);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > lastX) isLeft = false; else if (currentX < lastX) isLeft = true;
            lastX = currentX;

            Vector3 target;
            if (isLeft)
            {
                target = new Vector3(player.position.x - offset.x, player.position.y + offset.y,transform.position.z);
            }
            else
            {
                target = new Vector3(player.position.x + offset.x, player.position.y + offset.y,transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}
