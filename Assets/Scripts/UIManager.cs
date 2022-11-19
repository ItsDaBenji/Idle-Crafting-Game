using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] Vector3 closePos;
    [SerializeField] Vector3 openPos;

    public void Inventory()
    {
        if(transform.position == closePos)
        {
            while(transform.position != openPos)
            {
                transform.position = Vector3.Lerp(transform.position, openPos, speed);
            }
        }
        else
        {
            while(transform.position != closePos)
            {
                transform.position = Vector3.Lerp(transform.position, closePos, speed);
            }
        }
    }

}
