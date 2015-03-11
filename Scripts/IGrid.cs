using System.Collections;
using System.Collections.Generic;

namespace PathFinding {
	public interface IGrid {

		void SetWalkableAt(int posX, int posY, bool isWalkable);

		bool IsWalkableAt(int posX, int posY);

		INode GetNodeAt(int posX, int posY);

		bool IsInside(int posX, int posY);

		List<INode> GetNeighbours(INode node, DiagonalMovement diagonalMovement);
	}
}