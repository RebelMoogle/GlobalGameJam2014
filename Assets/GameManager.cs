using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public AudioSource deadSound;

	// Use this for initialization
	void Start () {
        EventEngine.listenToEvent("playerDies", () =>
        {
            if (deadSound != null)
            {
                deadSound.Play();
            }
        });
	}
	
	// Update is called once per frame
	void Update () {
	}
}
