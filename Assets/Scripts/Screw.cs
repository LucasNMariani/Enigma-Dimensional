using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : MonoBehaviour
{
    Animator _anim;
    AudioSource _as;
    [SerializeField] Vent _vent;
    bool _alreadyUnscrewed;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _as = GetComponent<AudioSource>();        
    }

    public void Unscrew()
    {
        if (GameManager.instance.HasScrewdriver)
        {
            if (!_alreadyUnscrewed)
            {
                _alreadyUnscrewed = true;
                _anim.SetTrigger("Unscrew");
                _as.Play();
            }
        }
    }

    public void DestroyScrew()
    {
        _vent.UnscrewVent();
        Destroy(this.gameObject);
    }



}
