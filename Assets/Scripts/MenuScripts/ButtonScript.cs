using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    //Functions that buttons can do, one function that does the action wholescale, and another that does the animation then the action.
    //This is to be attached to the button and then a selected function activated on the on click event.

    //If you want to enable and disable the button this is tied to enable then disable otherwise the enable will not work.

    //The default value to increase the wait after animation for.
    private float AdditionalDelay = 0f;

    //Setting up a empty animator to have when we look for the component's animator.
    Animator anim;

    void Start()
    {
        //Grab the animator from the object we latched to.
        anim = GetComponent<Animator>();

        //Find the Parent of the button this script is refrenced to
        GameObject ParentGameObject = transform.parent.gameObject;

        //Make sure that object was set up to be something
        if (ParentGameObject != null)
        {
            //Set the DelayScript to the Script the Parent Gameobject has 
            ParentDelayScript DelayScript = ParentGameObject.GetComponent<ParentDelayScript>();

            //Make sure that delayscript was set to something.
            if (DelayScript != null)
            {
                //If that all goes to plan just add to the additional delay what the delayscript has for its delay value.
                AdditionalDelay += DelayScript.delay;
            }

        }

        //Debug.Log(anim);
    }



    public void NewGame()
    {
        GameManager.GetManager().NewGame();
    }

    public void NewGameAfterAnimation()
    {
        StartCoroutine(DoNewGameAfterAnimation((anim.GetCurrentAnimatorStateInfo(0).length)));
    }

    IEnumerator DoNewGameAfterAnimation(float _delay)
    {
        yield return new WaitForSeconds(_delay + AdditionalDelay);

        NewGame();
    }



    public void LoadGame()
    {
        //Loading the game from the gamemanager
        //GameManager.GetManager().LoadGame();
    }

    public void LoadGameAfterAnimation()
    {
        StartCoroutine(DoLoadGameAfterAnimation((anim.GetCurrentAnimatorStateInfo(0).length)));
    }

    IEnumerator DoLoadGameAfterAnimation(float _delay)
    {
        yield return new WaitForSeconds(_delay + AdditionalDelay);

        LoadGame();
    }



    public void EnableObject(GameObject ToEnableObject)
    {
        //Enables the Object that is ToEnableObject
        ToEnableObject.SetActive(true);
    }

    public void EnableObjectAfterAnimation(GameObject ToEnableObject)
    {
        StartCoroutine(DoEnableObjectAfterAnimation((anim.GetCurrentAnimatorStateInfo(0).length), ToEnableObject));
    }

    IEnumerator DoEnableObjectAfterAnimation(float _delay, GameObject ToEnableObject)
    {
        yield return new WaitForSeconds(_delay + AdditionalDelay);

        EnableObject(ToEnableObject);
    }



    public void DisableObject(GameObject ToDisableObject)
    {
        //Disables the Object that is ToDisableObject
        ToDisableObject.SetActive(false);
    }

    public void DisableObjectAfterAnimation(GameObject ToDisableObject)
    {
        StartCoroutine(DoDisableObjectAfterAnimation((anim.GetCurrentAnimatorStateInfo(0).length), ToDisableObject));
    }

    IEnumerator DoDisableObjectAfterAnimation(float _delay, GameObject ToDisableObject)
    {
        yield return new WaitForSeconds(_delay + AdditionalDelay);

        DisableObject(ToDisableObject);
    }



    public void LoadScene(string LoadSceneNumber)
    {
        //Loads whatever scene number is pushed to it
        SceneManager.LoadScene(LoadSceneNumber);
    }

    public void LoadSceneAfterAnimation(string LoadSceneNumber)
    {
        StartCoroutine(DoLoadSceneAfterAnimation((anim.GetCurrentAnimatorStateInfo(0).length), LoadSceneNumber));
    }

    IEnumerator DoLoadSceneAfterAnimation(float _delay, string LoadSceneNumber)
    {
        yield return new WaitForSeconds(_delay + AdditionalDelay);

        LoadScene(LoadSceneNumber);
    }



    public void Quit()
    {
        //Quits the Application Outright
        Application.Quit();
    }

    public void QuitAfterAnimation()
    {
        StartCoroutine(DoQuitAfterAnimation((anim.GetCurrentAnimatorStateInfo(0).length)));
    }

    IEnumerator DoQuitAfterAnimation(float _delay)
    {
        yield return new WaitForSeconds(_delay + AdditionalDelay);

        Application.Quit();
    }


    //A checker for getting the animation state length correctly
    public void AnimatorStateLength()
    {
        Debug.Log(anim.GetCurrentAnimatorStateInfo(0).length);
    }

}
