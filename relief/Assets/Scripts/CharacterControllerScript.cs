using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float speed = 10f;
    public int nrCollectibles;
    private int nrPickedUp;

    public GameObject closedDoor;
    public GameObject openDoor;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        openDoor.SetActive(false);
        closedDoor.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        unlockCursor();
        checkObjectiveCleared();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Collectible")
        {
            nrPickedUp++;
            Destroy(other);
        }
        if(other == closedDoor)
        {
            if (checkObjectiveCleared())
            {
                closedDoor.SetActive(false);
                openDoor.SetActive(true);
            }   
        }
    }

    bool checkObjectiveCleared()
    {
        if(nrCollectibles == nrPickedUp)
        {
            return true;
        }
        return false;
    }

    void movement()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);
    }

    void unlockCursor()
    {
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
