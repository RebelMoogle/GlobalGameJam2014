using UnityEngine;
using System.Collections;

public class GlobalManager : MonoBehaviour {

    Dictionary<Dude.faction,

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
