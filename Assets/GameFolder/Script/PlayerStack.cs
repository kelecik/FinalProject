using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerStack : MonoBehaviour
{
    [SerializeField] int pickableLayer;
    [SerializeField] int pickableNoneLayer;
    [SerializeField] bool switchMethod;
    [SerializeField] Transform container;
    [SerializeField] List<GameObject> pickableObject = new List<GameObject>();
    [Space]
    float currentHeight;

    private void Start()
    {
        currentHeight = container.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == pickableLayer)
        {
            StartCoroutine(ObjectMovement(0.11f,other.gameObject));
        }
    }
    IEnumerator ObjectMovement(float delay, GameObject other)
    {
        currentHeight += 0.5f;
        other.gameObject.layer = pickableNoneLayer;
        other.transform.DOMove(new Vector3(container.position.x, currentHeight, container.position.z), 0.1f);
        other.transform.parent = container.transform;
        if (switchMethod)
        {
            yield return new WaitForSeconds(delay);
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;         
            PickableObjectProperty(other);
            Debug.Log("Join");
        }   
    }

    void PickableObjectProperty(GameObject other)
    {
        #region Components
        GameObject cube = other;
        Rigidbody rbCube = other.GetComponent<Rigidbody>();
        HingeJoint fxCube = other.GetComponent<HingeJoint>(); 
        #endregion

        if(pickableObject.Count < 1)
        {
            rbCube.isKinematic = true;
            pickableObject.Add(cube);
        }
        else
        {
            pickableObject.Add(cube);
            //rbCube.useGravity = true;
            pickableObject[pickableObject.Count - 2].GetComponent<FixedJoint>().connectedBody = rbCube;
        }
    }
}
