namespace GameConstants {

	using UnityEngine;

	public static class PlayerConstants {
		public static int ATTACK1 = Animator.StringToHash ("attack1");
		public static int ATTACK2 = Animator.StringToHash ("attack2");

		public const int MODE_OFFENSIVE = 1;
		public const int MODE_ATTACK1 = 2;
		public const int MODE_ATTACK2 = 3;
		public const int MODE_ATTACK_NOW = 99;
	}
}
