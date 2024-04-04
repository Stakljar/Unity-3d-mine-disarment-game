using UnityEngine;
using TMPro;
using System.Collections;

public class Mine : MonoBehaviour
{
    const string endGameText = "You stepped on a mine";

    [SerializeField]
    AudioSource explosionSound;

    bool didTriggerHappen = false;

    void Start()
    {
        GetComponent<ParticleSystem>().Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Movement.playerTag))
        {
            if (!didTriggerHappen)
            {
                other.gameObject.GetComponent<Rotation>().enabled = false;
                other.gameObject.GetComponent<MineInteraction>().enabled = false;
                other.gameObject.GetComponent<Movement>().IsMovable = false;
                FindObjectOfType<MineManager>().DidAnyMineExplode = true;
                GetComponent<ParticleSystem>().Play();
                FindObjectOfType<GameManager>().IsGameOver = true;
                StartCoroutine("EndGame");

            }
            didTriggerHappen = true;
        }
    }

    IEnumerator EndGame()
    {
        explosionSound.Play();
        yield return new WaitForSeconds(1f);
        explosionSound.Stop();
        FindObjectOfType<GameManager>().EndGame(endGameText);
        yield return null;
    }
}
