using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTiles : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMoveController>().IsMoving == false)
        {
            collision.transform.position = collision.GetComponent<PlayerMoveController>().Position;
        }
        
    }
}
