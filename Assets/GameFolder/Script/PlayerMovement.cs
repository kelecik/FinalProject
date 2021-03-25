using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    private void Update()
    {
        PlayerMove();
    }
    void PlayerMove()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
