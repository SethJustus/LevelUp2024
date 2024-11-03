using UnityEngine;

public interface IPlayerController
{
    void Move(Vector2 movementVector);

    void Dash(Vector2 movementVector);
}
