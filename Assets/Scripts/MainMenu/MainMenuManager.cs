using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public Animator initiallyOpen;

    private int openParameterId;
    private Animator mOpen;
    private GameObject previouslySelected;

    const string kOpenTransitionName = "Open";
    const string kCloseStateName = "Closed";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        openParameterId = Animator.StringToHash(kOpenTransitionName);
        if (initiallyOpen == null)
        {
            return;
        }
        OpenPanel(initiallyOpen);
    }

    public void OpenPanel(Animator anim)
    {
        if (mOpen == anim)
        {
            return;
        }

        anim.gameObject.SetActive(true);
        var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;

        anim.transform.SetAsLastSibling();

        CloseCurrent();
        previouslySelected = newPreviouslySelected;
        mOpen = anim;
        mOpen.SetBool(openParameterId, true);

        GameObject gameObject = FindFirstEnabledSelectable(anim.gameObject);

        SetSelected(gameObject);
    }

    public void SetSelected(GameObject game)
    {
        EventSystem.current.SetSelectedGameObject(game);
    }

    static GameObject FindFirstEnabledSelectable(GameObject game)
    {
        GameObject gameObject = null;
        var selectables = game.GetComponentsInChildren<Selectable>(true);
        foreach (var selectable in selectables)
        {
            if (selectable.IsActive() && selectable.IsInteractable())
            {
                gameObject = selectable.gameObject;
                break;
            }
        }
        return gameObject;
    }

    public void CloseCurrent()
    {
        if (mOpen == null)
        {
            return;
        }

        mOpen.SetBool(openParameterId, false);
        SetSelected(previouslySelected);
        StartCoroutine(DisablePanelDeleyed(mOpen));
        mOpen = null;
    }

    IEnumerator DisablePanelDeleyed(Animator animator)
    {
        bool closedStateReached = false;
        bool wantToClose = true;

        while (!closedStateReached && wantToClose)
        {
            if (!animator.IsInTransition(0))
            {
                closedStateReached = animator.GetCurrentAnimatorStateInfo(0).IsName(kCloseStateName);
            }
            wantToClose = !animator.GetBool(openParameterId);
            yield return new WaitForEndOfFrame();
        }
        if (wantToClose)
        {
            animator.gameObject.SetActive(false);
        }
    }

    public void OpenNewGame(){
        SceneManager.LoadScene("GamePlayScene",LoadSceneMode.Single);
    }

}
