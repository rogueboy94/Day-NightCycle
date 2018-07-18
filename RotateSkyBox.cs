using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyBox : MonoBehaviour {

	public float rotateSpeed = 1.2f;
	public float intensitySpeed;
	public Light sun;
	public float exposure;
	public float exposureSpeed;
	public Material skyboxDay;
	public Material skyboxNight;

	Color32 sunColor, moonColor;

	bool night = false;

	void Start(){
		moonColor = new Color32 (150, 190, 190, 255);
		sunColor = new Color32 (255, 255, 255, 255);

		if (sun.gameObject.transform.eulerAngles.x < 180f)
			RenderSettings.skybox.SetFloat ("_Exposure", 4f);
	}

	// Update is called once per frame
	void Update () {
		StartCoroutine (Cycle ());
	}

	IEnumerator Cycle(){
		exposure = RenderSettings.skybox.GetFloat ("_Exposure");

		RenderSettings.skybox.SetFloat ("_Rotation", Time.time * rotateSpeed);

		Debug.Log ("EXPOSURE :" + RenderSettings.skybox.GetFloat ("_Exposure"));
		Debug.Log ("SUNLIGHT :" + sun.gameObject.transform.eulerAngles.x);

		if (!night && sun.gameObject.transform.eulerAngles.x > 270f && sun.gameObject.transform.eulerAngles.x < 360f) {
			if (RenderSettings.ambientIntensity >=0.1f) {
				RenderSettings.ambientIntensity -= Time.deltaTime * intensitySpeed;
				//sun.intensity -= Time.deltaTime * intensitySpeed;
				sun.color = Color32.Lerp(sunColor, moonColor, 1f); 
			}

			if (exposure < 0.35f) {
				night = true;
				RenderSettings.skybox = skyboxNight;
				exposure = 1f;
			}

			RenderSettings.skybox.SetFloat ("_Exposure", exposure - exposureSpeed);
		}
		else if(night && sun.gameObject.transform.eulerAngles.x <= 25f){
			if (RenderSettings.ambientIntensity <= 1f) {
				sun.color = Color32.Lerp (moonColor, sunColor, 1f); 
				//sun.intensity += Time.deltaTime * intensitySpeed;
				RenderSettings.ambientIntensity += Time.deltaTime * intensitySpeed;
			}

			RenderSettings.skybox.SetFloat ("_Exposure", exposure + exposureSpeed);
			RenderSettings.skybox = skyboxDay;
		}

		if (sun.gameObject.transform.eulerAngles.x > 25f && sun.gameObject.transform.eulerAngles.x < 270f) {
			night = false;
			exposure = 4f;		
		}

		yield return null;
	}
}
