using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TankText : MonoBehaviour {

    TankBag TB_bag;
    TankMovement TMVitesse;
    public Text speedText;
    public Text ammoText;
    public Text boostText;

    // Use this for initialization
    void Start () {
        TB_bag = gameObject.GetComponent<TankBag>();
        TMVitesse = gameObject.GetComponent<TankMovement>();
    }

    void SetSpeedText()
    {
        if (TMVitesse.countSpeed < 0.01f)
        {
            TMVitesse.countSpeed = 0;
        }
        TMVitesse.countSpeed *= 50;
        TMVitesse.countSpeed = (int)TMVitesse.countSpeed;
        speedText.text = TMVitesse.countSpeed.ToString() + " MPH";
    }

    void SetAmmoText()
    {
        ammoText.text = "AMMO : " + TB_bag.m_nombreMunition.ToString();
    }

    void SetBoostText()
    {
        boostText.text = "BOOST : " + TB_bag.m_nombreBoost.ToString();
    }

    void FixedUpdate () {
        SetSpeedText();
        SetAmmoText();
        SetBoostText();
    }
}
