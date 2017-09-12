using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventArgs { }


public class BlurSwitchEvent : EventArgs {
    private string eventMsg;

    public BlurSwitchEvent(string msg) {
        this.eventMsg = msg;
    }

    public string EventMsg{ get { return this.eventMsg; } }
}