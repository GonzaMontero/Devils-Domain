using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class CharacterFSM : MonoBehaviour
{
    enum States {idle,attacking,dead };
    States current;

    void Start()
    {
        current = States.idle;
    }

    void Update()
    {
        switch (current)
        {
            case States.idle:                
                break;
            case States.attacking:
                break;
            case States.dead:
                break;
        }
    }

    IEnumerator SwitchIdleAttack()
    {
        yield return null;
    }
}
