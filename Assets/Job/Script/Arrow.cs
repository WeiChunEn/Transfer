using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject _gEmney;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.rotation = Quaternion.LookRotation(_gEmney.transform.position);
        transform.LookAt(new Vector3(_gEmney.transform.position.x, _gEmney.transform.position.y+1.0f, _gEmney.transform.position.z), Vector3.up);
        //transform.rotation = Quaternion.Euler(Quaternion.LookRotation(_gEmney.transform.position).x, Quaternion.LookRotation(_gEmney.transform.position).y , Quaternion.LookRotation(_gEmney.transform.position).z);
        // transform.forward *= 1.0f;
        // transform.position = Vector3.MoveTowards(transform.position, new Vector3(_gEmney.transform.position.x, _gEmney.transform.position.y + 0.8f, _gEmney.transform.position.z),2.0f*Time.deltaTime);
        transform.position += transform.forward*2.0f*Time.deltaTime;
    }

}
