﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Ear_Clipping_Algorithm {
	public static class Shapes {
		private static readonly Dictionary<String, List<Vector2>> circleCache = new Dictionary<string, List<Vector2>>();
		private static Texture2D pixel;

		private static void createPixel(SpriteBatch spriteBatch) {
			pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });
		}

		private static List<Vector2> CreateCircle(double radius, int sides) {
			String circleKey = radius + "x" + sides;
			if (circleCache.ContainsKey(circleKey)) {
				return circleCache[circleKey];
			}

			List<Vector2> vectors = new List<Vector2>();

			const double max = 2.0 * Math.PI;
			double step = max / sides;

			for (double theta = 0.0; theta < max; theta += step) {
				vectors.Add(new Vector2((float)(radius * Math.Cos(theta)), (float)(radius * Math.Sin(theta))));
			}

			vectors.Add(new Vector2((float)(radius * Math.Cos(0)), (float)(radius * Math.Sin(0))));

			circleCache.Add(circleKey, vectors);

			return vectors;
		}

		private static void DrawPoints(SpriteBatch spriteBatch, Vector2 position, List<Vector2> points, Color color, float thickness) {
			if (points.Count < 2)
				return;

			for (int i = 1; i < points.Count; i++) {
				drawLine(spriteBatch, points[i - 1] + position, points[i] + position, color, thickness);
			}
		}

		public static void drawRectangle(SpriteBatch spriteBatch, Rectangle rect, Color color, float thickness) {
			drawLine(spriteBatch, new Vector2(rect.X, rect.Y), new Vector2(rect.Right, rect.Y), color, thickness);
			drawLine(spriteBatch, new Vector2(rect.X + 1f, rect.Y), new Vector2(rect.X + 1f, rect.Bottom + thickness), color, thickness);
			drawLine(spriteBatch, new Vector2(rect.X, rect.Bottom), new Vector2(rect.Right, rect.Bottom), color, thickness);
			drawLine(spriteBatch, new Vector2(rect.Right + 1f, rect.Y), new Vector2(rect.Right + 1f, rect.Bottom + thickness), color, thickness);
		}

		public static void drawFilledRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float angle) {
			if (pixel == null) {
				createPixel(spriteBatch);
			}

			spriteBatch.Draw(pixel, rect, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
		}

		public static void drawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness) {
			float distance = Vector2.Distance(point1, point2);
			float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

			drawLine(spriteBatch, point1, distance, angle, color, thickness);
		}

		private static void drawLine(this SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness) {
			if (pixel == null) {
				createPixel(spriteBatch);
			}

			spriteBatch.Draw(pixel,
							 point,
							 null,
							 color,
							 angle,
							 Vector2.Zero,
							 new Vector2(length, thickness),
							 SpriteEffects.None,
							 0);
		}

		public static void drawPixel(this SpriteBatch spriteBatch, Vector2 position, Color color) {
			if (pixel == null) {
				createPixel(spriteBatch);
			}

			spriteBatch.Draw(pixel, position, color);
		}

		public static void drawCircle(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, Color color, float thickness) {
			DrawPoints(spriteBatch, center, CreateCircle(radius, sides), color, thickness);
		}
	}
}
