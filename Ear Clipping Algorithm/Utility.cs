using System;

namespace Ear_Clipping_Algorithm {
	public static class Utility {
		private static Random rand = new Random();

		public static int randInt(int a, int b) {
			return rand.Next(a, b + 1);
		}

		public static float random() {
			return (float)rand.NextDouble();
		}
	}
}
