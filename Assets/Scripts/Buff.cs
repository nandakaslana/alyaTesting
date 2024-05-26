using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public float movBuff;
    private void OnTriggerEnter2D(Collider2D collision)

    {
        Debug.Log("Buff");

        if (collision.gameObject.tag == "Player" )
        {
            
            GameManager.Instance.playerController.moveSpeed = movBuff;

            GameManager.Instance.playerController.movbuff = true;
            Destroy(gameObject);

        }
    }

}
