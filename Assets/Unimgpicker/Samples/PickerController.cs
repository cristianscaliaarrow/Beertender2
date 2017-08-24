using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Kakera
{
    public class PickerController : MonoBehaviour
    {
        [SerializeField]
        private Unimgpicker imagePicker;

        [SerializeField]
        private Image imageRenderer;

        public string error;

        void Awake()
        {
            imagePicker.Completed += (string path) =>
            {
                StartCoroutine(LoadImage(path, imageRenderer));
            };
            imagePicker.Failed += (string path) =>
            {
                error += ("ERROR!  "+path) + "\n";
            };
        }

        public void OnPressShowPicker()
        {
            imagePicker.Show("Select Image", "unimgpicker", 1024);
            error = "Selection";
        }

        private IEnumerator LoadImage(string path, Image output)
        {
            var url = "file://" + path;
            var www = new WWW(url);
            yield return www;

            var texture = www.texture;
            if (texture == null)
            {
                error += ("Failed to load texture url:" + url) + "\n";
            }else
            {
                error += ("Texture url:" + path) + "\n";
            }

            output.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);

        }
        private void OnGUI()
        {
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height),error);
        }
    }
}