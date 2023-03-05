using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDangerLevel : MonoBehaviour
{
    
    [Header("Wwise Switches")]
    public AK.Wwise.Switch hiddenSwitch;
    public AK.Wwise.Switch dangerLowSwitch;
    public AK.Wwise.Switch dangerMediumSwitch;
    public AK.Wwise.Switch dangerHighSwitch;
    
    public void TriggerHidden()
    {
        hiddenSwitch.SetValue(gameObject);
    }
    public void TriggerDangerLow()
    {
        dangerLowSwitch.SetValue(gameObject);
    }
    public void TriggerDangerMedium()
    {
        dangerMediumSwitch.SetValue(gameObject);
    }
    public void TriggerDangerHigh()
    {
        dangerHighSwitch.SetValue(gameObject);
    }

}
