using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD38.UI.Menu {
    public class SceneLoader : MonoBehaviour {
        public void LoadScene(string scene) {
            SceneManager.LoadScene(scene);
        }
    }
}
