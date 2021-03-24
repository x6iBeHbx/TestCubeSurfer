using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAmin : MonoBehaviour
{
    // Start is called before the first frame update
    Animator _animator;
    Animation anim;
    void Start()
    {
        //_animator = GetComponent<Animator>();
        anim = GetComponent<Animation>();
        //StartCoroutine(StartWinAnim());
        anim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void test(AnimationEvent t)
    {
        Debug.Log("EVENT"+ t.objectReferenceParameter.name);
    }

    private IEnumerator StartWinAnim()
    {
        yield return new WaitForSeconds(4f);

        _animator.SetTrigger("Win");
    }
}
