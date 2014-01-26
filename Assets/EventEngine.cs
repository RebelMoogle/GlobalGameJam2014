using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventEngine : MonoBehaviour {

    public delegate void Event();
    private static Dictionary<string, List<Event>> _events = new Dictionary<string, List<Event>>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public static void fireEvent(string eventName)
    {
        if (_events.ContainsKey(eventName))
        {
            foreach (var ev in _events[eventName]) {
                ev();
            }
        }
    }

    public static void listenToEvent(string eventName, Event ev) {
        if (!_events.ContainsKey(eventName)) {
            _events[eventName] = new List<Event>();
        }
        _events[eventName].Add(ev);
    }

    public static void removeListenToEvent(string eventName, Event ev)
    {
        if (_events.ContainsKey(eventName))
        {
            _events[eventName].Remove(ev);
        }
    }
}