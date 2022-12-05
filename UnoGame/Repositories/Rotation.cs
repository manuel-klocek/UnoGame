using System;
namespace UnoGame.Repositories
{
	public class Rotation
	{
		bool clockwiseRotation;

		public Rotation()
		{
			this.clockwiseRotation = true;
		}

		public bool Get()
		{
			return this.clockwiseRotation;
		}

		public void Change()
		{
			if (this.clockwiseRotation) this.clockwiseRotation = false;
			else this.clockwiseRotation = true;
		}
	}
}

