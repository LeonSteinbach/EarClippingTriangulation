using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ear_Clipping_Algorithm {
	public class Node {
		public Point pos;
		public Node n1, n2;

		private float radius = 5;
		private int sides = 10;

		public Node(Point pos, Node n1, Node n2) {
			this.pos = pos;
			this.n1 = n1;
			this.n2 = n2;
		}

		public void Draw(SpriteBatch spriteBatch, Color color) {
			Shapes.drawCircle(spriteBatch, pos.ToVector2(), radius, sides, color, radius * 2);
		}
	}
}
