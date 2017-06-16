using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureBox : MonoBehaviour {

    public enum Type { Aoe, Index, Throw, Kill };
    public Type type;
    public float openRate = 1.0f;
    public GameObject countText;

    [HideInInspector]
    public static PlayerController pc;
    public static TresureBox t0, t1, t2, t3;
    public int objective;
    public int left;

    protected AudioSource _audio;
    protected TextMesh _text;

    protected bool _bOpen;

    protected void Awake()
    {
        _bOpen = false;
        left = 1;
        _audio = GetComponent<AudioSource>();
        _text = countText.GetComponent<TextMesh>();
        switch (type)
        {
            case Type.Aoe:
                t0 = this;
                break;
            case Type.Index:
                t1 = this;
                break;
            case Type.Throw:
                t2 = this;
                break;
            case Type.Kill:
                t3 = this;
                break;
        }
    }

    public void Open()
    {
        _audio.Play();
        _bOpen = true;
    }

    public int GetCurrentCount()
    {
        switch (type)
        {
            case Type.Aoe:
                return pc.countSpellAoe;
            case Type.Index:
                return pc.countSpellIndex;
            case Type.Throw:
                return pc.countWeaponThrow;
            case Type.Kill:
                return pc.countEnemyKill;
        }
        return 0;
    }

    public void Update()
    {
        if (_bOpen)
        {
            if (transform.localEulerAngles.x > -86.0f)
                transform.localEulerAngles -= new Vector3(openRate * Time.deltaTime, 0, 0);
        }
        left = objective - GetCurrentCount();
        if (left <= 0)
        { _text.text = "Completed"; _text.color = new Color(0, 255, 0); }
        else
        { _text.text = left + " Left"; }
    }
}
