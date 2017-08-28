using UnityEngine;
using System.Collections;
using UnityEngine.UI;


    public class PickerController : MonoBehaviour
    {
        [SerializeField]
        private Unimgpicker imagePicker;

        [SerializeField]
        private Image imageRenderer;

        void Awake()
        {
            imagePicker.Completed += (string path) =>
            {
                StartCoroutine(LoadImage(path, imageRenderer));
            };
            imagePicker.Failed += (string path) =>
            {
                Debug.Log("ERROR!  " +path);
            };
        }

        public void OnPressShowPicker()
        {
            imagePicker.Show("Select Image", "unimgpicker", 1024);
        }

        private IEnumerator LoadImage(string path, Image output)
        {
            var url = "file://" + path;
            var www = new WWW(url);
            yield return www;

            var texture = www.texture;
            if (texture == null)
            {
                Debug.Log("Failed to load texture url:" + url);
            }

            output.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);

        }
       
    }
