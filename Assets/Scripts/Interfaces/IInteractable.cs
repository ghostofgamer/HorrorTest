using PlayerContent;

namespace Interfaces
{
    public interface IInteractable
    {
        public void Action(PlayerInteraction playerInteraction);
        public void Highlight(bool state);
    }
}