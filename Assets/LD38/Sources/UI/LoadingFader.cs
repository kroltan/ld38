using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace LD38.UI {
    public class LoadingFader : SingletonBehaviour<LoadingFader> {
        private Graphic _graphic;

        [UsedImplicitly]
        private void Start() {
            _graphic = GetComponent<Graphic>();
            SetBlocking(false);
        }

        public void SetBlocking(bool blocking) {
            Time.timeScale = blocking ? 0 : 1;
            _graphic.enabled = blocking;
            foreach (Transform child in transform) {
                child.gameObject.SetActive(blocking);
            }
            PlayerController.Instance.gameObject.SetActive(!blocking);
        }

        public IEnumerator FadeWhile(Func<IEnumerator> awaitee) {
            yield return new WaitForEndOfFrame();

            SetBlocking(true);

            var enumerator = awaitee();
            while (enumerator.MoveNext()) {
                yield return enumerator.Current;
            }

            SetBlocking(false);
        }
    }
}
