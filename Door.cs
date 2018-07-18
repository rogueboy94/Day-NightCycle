using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public float timeLeft = 0;

    public RaycastHit hit;

	public AudioClip openDoorAudio;
	public AudioClip closeDoorAudio;

    public Transform currentDoor;

	public float openingAngle = 80f;

    public bool open;

    public bool IsOpening;

    public Transform cam;

    public LayerMask mask;

	private AudioSource source;
    
	// Update is called once per frame
	void Start(){
		source = gameObject.AddComponent<AudioSource> ();
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.E) && timeLeft == 0.0f)
            CheckDoor();

		if (IsOpening) {
			OpenAndCloseDoor ();
		}
	}

    public void CheckDoor()
    {
        if(Physics.Raycast(cam.position, cam.forward, out hit, 5, mask))
		{
            open = false;
			if (hit.transform.localRotation.eulerAngles.y >= 45)
				open = true;

			if (!open) {
				source.clip = openDoorAudio;
				source.Play();
			} else {
				source.clip = closeDoorAudio;
				source.Play();
			}

            IsOpening = true;
            currentDoor = hit.transform;
        }
    }

    public void OpenAndCloseDoor()
    {
		timeLeft += Time.deltaTime * 0.07f;

        if (open)
        	currentDoor.localRotation = Quaternion.Slerp(currentDoor.localRotation, Quaternion.Euler(0f, 0f, 0f), timeLeft);
        else
			currentDoor.localRotation = Quaternion.Slerp(currentDoor.localRotation, Quaternion.Euler(0f, openingAngle, 0f), timeLeft);

		if(currentDoor.rotation.eulerAngles.y >= openingAngle || currentDoor.rotation.eulerAngles.y <= 0.01f)
        {
            timeLeft = 0;
            IsOpening = false;
        }
    }
}
