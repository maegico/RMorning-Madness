using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    RaycastHit hit;
    public Transform cam;
    private Vector3 playerOrigin;
    public Transform knifeHoldPos;
    public Transform knife;

    GameManager gm;

	// Use this for initialization
    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
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
                    if(hit.collider.name == "kitchenKnife"){
                        if(gm.day == 4){
                            knife.parent = knifeHoldPos;
                            knife.position = knifeHoldPos.position;
                            knife.localEulerAngles = new Vector3(0.3477f, 119.83f, 17.4f);
                        }
                    }
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
    private void PickupKnife()
    {

    }
    public void ResetPlayer()
    {
        transform.position = playerOrigin;
    }
}
