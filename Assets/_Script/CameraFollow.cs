using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.instance.gamestate == GameState.play)
        {
            transform.position =Vector3.Lerp(transform.position,new Vector3 (GameManager.Instance.transform.position.x, 5f, GameManager.Instance.transform.position.z-4f),1);
        }
    }
}
