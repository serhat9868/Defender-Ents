namespace RPG.Control
{
    public interface IRaycastable
    {
        CursorType GetCursorType();
        bool HandleRaycast(MainPlayerController callingController);
    }
}
