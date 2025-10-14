using UnityEngine;


public interface IMoveable
{
	void MoveVertical(Vector3 direction);
	void MoveHorizontal(Vector3 direction);
	void Rotate(float rotateOffset, float moveOffset);
}
