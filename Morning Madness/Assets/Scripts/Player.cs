using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    RaycastHit hit;
    public Transform cam;
    private Vector3 playerOrigin;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        //nothing so far

        playerOrigin = transform.position;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit)) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Jordan's Interactability Test
                if (hit.collider.tag == "interactable")
                {
                    hit.collider.GetComponent<Items>().Interacted();
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void ResetPlayer()
    {
        transform.position = playerOrigin;
    }
}
