using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class SceneSwitcher : MonoBehaviour
{
    public string sceneToSwitchTo;
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !string.IsNullOrEmpty(sceneToSwitchTo))
        {
            SceneManager.LoadScene(sceneToSwitchTo);
        }
    }
}
