using System;
using UnityEngine;

public class SpawnFruit : MonoBehaviour
{
    public GameObject setSpawn(GameObject prefeb, Transform? transform)
    {
        Vector3 position = transform?.position ?? gameObject.transform.position;
        GameObject fruitStay = Instantiate(prefeb, position, Quaternion.identity);
        SetGravityScale(fruitStay);
        return fruitStay;
    }


    private void SetGravityScale(GameObject fruitStay)
    {
        fruitStay.GetComponent<Rigidbody2D>().gravityScale = 0;
    }
}