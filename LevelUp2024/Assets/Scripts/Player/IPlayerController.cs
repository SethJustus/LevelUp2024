using UnityEngine;

public interface IPlayerController
{
    void HorizontalMove(Vector2 movementVector);
    void Dash(Vector2 movementVector);
}
