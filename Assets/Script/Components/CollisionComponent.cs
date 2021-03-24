using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent : MonoBehaviour
{
    public PlayerContainerComponent ContainerComponent { get; set; }

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Item" && coll.gameObject != transform.gameObject)
        {
            Debug.Log("COLLITION WITH ITEM: " + coll.gameObject.name + "_" + transform.gameObject.name);
            coll.transform.parent = transform.parent;

            ContainerComponent.AddCube(coll.transform);
            ContainerComponent.CalculatePostionForChilds();
        }

        if (coll.tag == "Obstacle")
        {
            ContainerComponent.RemoveCube(this.transform);
            transform.parent = null;
            Destroy(transform.gameObject);
        }
    }
}
