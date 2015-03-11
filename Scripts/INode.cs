using System;
using System.Collections;

namespace PathFinding {
	
	public interface INode {

		int PosX {
			get;
		}

		int PosY {
			get;
		}

		bool IsWalkable {
			get;
			set;
		}

		NodeUtils Utils {
			get;
		}
	}
}