using UnityEngine;

public interface IPlayerController
{
    void Move(Vector2 movementVector, bool startDash);
}
