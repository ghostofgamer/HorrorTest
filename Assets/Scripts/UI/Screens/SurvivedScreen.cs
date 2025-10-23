using UnityEngine;

namespace UI.Screens
{
    public class SurvivedScreen : AbstractScreen
    {
        public override void Open()
        {
            base.Open();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }
}