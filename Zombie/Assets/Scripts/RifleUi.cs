using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RifleUi : MonoBehaviour
{
    public Text ammo;
    public Text mag;

    public static RifleUi occurence;
    private void Awake()
    {
        occurence = this;
    }
    public void UpdateAmmoText(int presenammo)
    {
        ammo.text = "Ammo: "+ presenammo;
    }
    public void UpdateMag(int magamt)
    {
        mag.text = "Mag: "+ magamt;
    }
}
