using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PathFinding {
	public class Node : INode {

		private int _posX = 0;

		private int _posY = 0;

		private bool _isWalkable = true;

		private NodeUtils _utils;

		/// <summary>
		/// Initializes a new instance of the <see cref="Node"/> class.
		/// </summary>
		/// <param name="posX">Position x.</param>
		/// <param name="posY">Position y.</param>
		/// <param name="isWalkable">If set to <c>true</c> is walkable.</param>
		public Node(int posX, int posY, bool isWalkable = true) {
			PosX = posX;
			PosY = posY;
			IsWalkable = isWalkable;

			// Create the new utils
			_utils = new NodeUtils();
		}

		/// <summary>
		/// Gets the position x.
		/// </summary>
		/// <value>The position x.</value>
		public int PosX {
			get {
				return _posX;
			}
			private set {
				_posX = value;
			}
		}

		/// <summary>
		/// Gets the position y.
		/// </summary>
		/// <value>The position y.</value>
		public int PosY {
			get {
				return _posY;
			}
			private set {
				_posY = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is walkable.
		/// </summary>
		/// <value><c>true</c> if this instance is walkable; otherwise, <c>false</c>.</value>
		public bool IsWalkable {
			get {
				return _isWalkable;
			}
			set {
				_isWalkable = value;
			}
		}

		/// <summary>
		/// Gets the utils.
		/// </summary>
		/// <value>The utils.</value>
		public NodeUtils Utils {
			get {
				return _utils;
			}
		}
	}
}
