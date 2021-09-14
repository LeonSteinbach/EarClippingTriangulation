using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Ear_Clipping_Algorithm {
	public class Game1 : Game {
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Vector2 displaySize;
		Vector2 screenSize;

		Polygon polygon;
		List<Polygon> triangles;

		public Game1() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			displaySize = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
			screenSize = new Vector2(800, 800);

			IsMouseVisible = true;
			Window.AllowUserResizing = false;
			Window.IsBorderless = false;
			Window.Position = new Point((int)(displaySize.X / 2 - screenSize.X / 2), (int)(displaySize.Y / 2 - screenSize.Y / 2));
			Window.Title = "Ear Clipping Algorithm";

			IsFixedTimeStep = false;
			graphics.SynchronizeWithVerticalRetrace = false;

			graphics.IsFullScreen = false;
			graphics.PreferredBackBufferWidth = (int)screenSize.X;
			graphics.PreferredBackBufferHeight = (int)screenSize.Y;
			graphics.ApplyChanges();
		}

		protected override void Initialize() {
			polygon = new Polygon();
			triangles = new List<Polygon>();

			base.Initialize();
		}

		protected override void LoadContent() {
			spriteBatch = new SpriteBatch(GraphicsDevice);

		}

		protected override void UnloadContent() {

		}

		public Color RandomColor() {
			int r = Utility.randInt(0, 255);
			int g = Utility.randInt(0, 255);
			int b = Utility.randInt(0, 255);
			return new Color(r, g, b);
		}

		public bool IntersectLines(Line a, Line b) {
			float derivative = (a.a.X - a.b.X) * (b.a.Y - b.b.Y) - (a.a.Y - a.b.Y) * (b.a.X - b.b.X);
			float t = ((a.a.X - b.a.X) * (b.a.Y - b.b.Y) - (a.a.Y - b.a.Y) * (b.a.X - b.b.X)) / derivative;
			float u = -((a.a.X - a.b.X) * (a.a.Y - b.a.Y) - (a.a.Y - a.b.Y) * (a.a.X - b.a.X)) / derivative;

			return t > 0 && t < 1 && u > 0 && u < 1;
		}

		public bool IsPointInPolygon(Point point, Polygon polygon) {
			int n = 1000;
			int rayLength = 10000;

			int collisions = 0;

			for (int i = 0; i < n; i++) {
				Vector2 dir = new Vector2((float)Math.Cos(Math.PI * 2 * i / n), (float)Math.Sin(Math.PI * 2 * i / n));
				Line ray = new Line(point, point + (dir * rayLength).ToPoint());
				foreach (Line line in polygon.GetEdges()) {
					if (IntersectLines(ray, line)) {
						collisions++;
					}
				}
				if (collisions == 0) {
					continue;
				}
				return collisions % 2 == 1;
			}
			return true;
		}

		public void GetTriangles(Polygon polygon, List<Polygon> triangles) {
			Polygon rest = polygon;
			int i = 0;
			while (rest.nodes.Count > 0) {
				if (i >= rest.nodes.Count) {
					break;
				}
				Node node = rest.nodes[i];

				Point p1 = node.n1.pos;
				Point p = node.pos;
				Point p2 = node.n2.pos;

				Polygon t = new Polygon();
				t.AddNode(p1);
				t.AddNode(p);
				t.AddNode(p2);

				// TODO: Don't check, if the corner is convex, but check every point
				// on the third line if it is inside the rest polygon.
				int l = (p1.X - p.X) * (p2.Y - p.Y) - (p1.Y - p.Y) * (p2.X - p.X);
				if (l > 0) {  // Ignore concarve
					Console.WriteLine("concarve");
					i++;
					continue;
				}

				bool inTriangle = false;
				foreach (Node n in rest.nodes) {
					if (n.pos == p1 || n.pos == p || n.pos == p2) {
						continue;
					}
					if (IsPointInPolygon(n.pos, t)) {
						inTriangle = true;
						break;
					}
				}
				
				if (inTriangle) {
					Console.WriteLine("inside");
					i++;
					continue;
				}

				t.color = RandomColor();
				triangles.Add(t);
				Console.WriteLine(triangles.Count);
				rest.DeleteNode(node);
				i = 0;
			}
		}

		protected override void Update(GameTime gameTime) {
			Input.Update();

			if (Input.isKeyPressed(Keys.Escape)) {
				Exit();
			}

			if (Input.isKeyPressed(Keys.C)) {
				polygon = new Polygon();
				triangles = new List<Polygon>();
			}

			if (Input.isLeftPressed()) {
				polygon.AddNode(Input.mousePosition());
			}

			if (Input.isRightPressed()) {
				if (polygon.nodes.Count > 0) {
					polygon.DeleteNode(polygon.nodes[polygon.nodes.Count - 1]);
				}
			}

			if (Input.isKeyPressed(Keys.Space)) {
				GetTriangles(polygon, triangles);
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.White);

			spriteBatch.Begin();

			polygon.Draw(spriteBatch);

			foreach (Polygon triangle in triangles) {
				triangle.Draw(spriteBatch);
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
