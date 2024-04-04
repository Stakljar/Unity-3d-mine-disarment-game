using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MineInteraction : MonoBehaviour
{
    const float disarmTime = 2.5f;
    const float mineCheckRadius = 1.5f;
    const string animationDisarmingBoolName = "isDisarming";

    [SerializeField]
    AudioSource disarmSound;

    [SerializeField]
    LayerMask mineLayerMask;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Slider progressBar;

    bool isCoroutineRunning = false;
    bool isCoroutineCompleted = false;

    void Update()
    {
        CheckNearbyMines();
    }

    void CheckNearbyMines()
    {
        Vector3 offsetVector = new Vector3(0f, 1f, 0f);
        Collider[] colliders = Physics
            .OverlapCapsule(transform.position, (transform.position - offsetVector), mineCheckRadius, mineLayerMask);
        if (colliders.Length == 0)
        {
            return;
        }
        if ((Input.GetKeyUp(KeyCode.F) || animator.GetBool(GetComponent<Movement>().AnimationWalkingBoolName) == true) && isCoroutineRunning)
        {
            StopCoroutine("DisarmMine");
            PerformPostDisarmActions();
            isCoroutineRunning = false;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine("DisarmMine");
        }
        else if (isCoroutineCompleted)
        {
            DestroyImmediate(colliders[0].gameObject);
            FindObjectOfType<MineManager>().UpdateNumberOfMines();
            isCoroutineCompleted = false;
        }
    }

    IEnumerator DisarmMine()
    {
        animator.SetBool(animationDisarmingBoolName, true);
        disarmSound.Play();
        isCoroutineRunning = true;
        float loadingProgress = 0f;
        progressBar.gameObject.SetActive(true);
        while (loadingProgress < 1f)
        {
            loadingProgress += Time.deltaTime / disarmTime;
            yield return new WaitForSeconds(Time.deltaTime);
            progressBar.value = loadingProgress;
        }
        PerformPostDisarmActions();
        yield return null;
        isCoroutineCompleted = true;
        isCoroutineRunning = false;
    }

    private void PerformPostDisarmActions()
    {
        animator.SetBool(animationDisarmingBoolName, false);
        progressBar.gameObject.SetActive(false);
        progressBar.value = 0f;
        disarmSound.Stop();
    }
}
