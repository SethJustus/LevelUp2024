using UnityEngine;

public interface IPlayerController
{
    PlayerControllerStatus Status { get; }
    void Move(Vector2 movementVector, bool startDash);
}
