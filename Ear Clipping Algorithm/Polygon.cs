using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ear_Clipping_Algorithm {
	public class Polygon {
		public List<Node> nodes;
		public Color color;

		private float lineThickness = 2f;

		public Polygon() {
			nodes = new List<Node>();
			color = Color.Black;
		}

		public Polygon(Color color) {
			nodes = new List<Node>();
			this.color = color;
		}

		public void AddNode(Point pos) {
			Node node = new Node(pos, null, null);
			if (nodes.Count > 0) {
				node.n1 = nodes.First();
				node.n2 = nodes.Last();
				nodes.Last().n1 = node;
				nodes.First().n2 = node;
			}
			nodes.Add(node);
		}

		public void AddNode(Node node) {
			if (nodes.Count > 0) {
				node.n1 = nodes.First();
				node.n2 = nodes.Last();
				nodes.Last().n1 = node;
				nodes.First().n2 = node;
			}
			nodes.Add(node);
		}

		public void DeleteNode(Node node) {
			if (!nodes.Contains(node)) {
				return;
			}
			if (node.n1 != null && node.n2 != null) {
				node.n1.n2 = node.n2;
				node.n2.n1 = node.n1;
			}
			nodes.Remove(node);
		}

		public List<Line> GetEdges() {
			List<Line> edges = new List<Line>();
			foreach (Node node in nodes) {
				edges.Add(new Line(node.pos, node.n1.pos));
			}
			return edges;
		}

		public void DrawEdge(SpriteBatch spriteBatch, Node n1, Node n2) {
			Shapes.drawLine(spriteBatch, n1.pos.ToVector2(), n2.pos.ToVector2(), color, lineThickness);
		}

		public void Draw(SpriteBatch spriteBatch) {
			foreach (Node node in nodes) {
				node.Draw(spriteBatch, color);
				if (node.n1 != null) {
					DrawEdge(spriteBatch, node, node.n1);
				}
			}
		}
	}
}
