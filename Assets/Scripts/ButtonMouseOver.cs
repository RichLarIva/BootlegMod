using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMouseOver : MonoBehaviour
{
    private AudioSource audioSourceBtn;
    public AudioClip buttonSound;
    // Start is called before the first frame update
    void Awake()
    {
        audioSourceBtn = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        audioSourceBtn.PlayOneShot(buttonSound);
    }
}
