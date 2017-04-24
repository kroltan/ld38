using System.IO;
using UnityEditor;

namespace Assets.LD38.Sources.Editor {
    public class PreviewGenerator {
        [MenuItem("LD38/Create Preview", false, 0)]
        public static void CreatePreview() {
            var obj = Selection.activeObject;
            if (obj == null) return;

            var path = EditorUtility.SaveFilePanelInProject("Save Thumbnail", obj.name, "png", "");
            if (string.IsNullOrWhiteSpace(path)) return;

            var thumb = AssetPreview.GetAssetPreview(obj);
            File.WriteAllBytes(path, thumb.EncodeToPNG());
        }

        [MenuItem("LD38/Create Preview", true)]
        public static bool ValidateCreatePreview() {
            var obj = Selection.activeObject;
            return obj != null
                && PrefabUtility.GetPrefabParent(obj) == null
                && PrefabUtility.GetPrefabObject(obj) != null;
        }
    }
}
