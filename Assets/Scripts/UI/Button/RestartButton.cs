using UnityEngine.SceneManagement;

namespace UI.Button
{
    public class RestartButton : AbstractButton
    {
        public override void OnClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}