using System.Collections;
using LD38.UI;
using UnityEngine;

namespace LD38 {
    public class LevelServerConnector : SingletonBehaviour<LevelServerConnector> {
        public string ServerBase = "http://localhost/ld38";

        private IEnumerator LoadRandomInternal() {
            var request = new WWW($"{ServerBase}/random.php");
            yield return request;
            if (request.error != null || request.text.Length < 10) {
                yield break;
            }
            MapLoader.Instance.FromMinicode(request.text);
        }
        public IEnumerator LoadRandom() => LoadingFader.Instance.FadeWhile(LoadRandomInternal);

        private IEnumerator UploadInternal() {
            var request = new WWW($"{ServerBase}/upload.php?minicode={WWW.EscapeURL(MapLoader.Instance.ToMinicode())}");
            yield return request;
        }
        public IEnumerator Upload() => LoadingFader.Instance.FadeWhile(UploadInternal);

        public void InteractiveUpload() => StartCoroutine(Upload());

        public void InteractiveLoadRandom() => StartCoroutine(LoadRandom());
    }
}
