using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WackyBag.Structures;

namespace WackyBagTr.Structures
{

	public class FloatAsIntSeparator
	{
		public FloatAsIntSeparator(params (int offset, uint distance)[] Values) {
			intSeparator = new(Values);
		}

		public readonly IntSeparator intSeparator;

		public int Get(float SeparatedNumber, int index)
		{
			return intSeparator.Get(BitConverter.SingleToInt32Bits(SeparatedNumber),index);
		}

		public float Set(float SeparatedNumber,int index,int value) {
			return BitConverter.Int32BitsToSingle(
				intSeparator.Set(BitConverter.SingleToInt32Bits(SeparatedNumber),index,value)
				);
		}

		public float Build(params int[] ints) {
			return BitConverter.Int32BitsToSingle(
					intSeparator.Build(ints)
				);
		}
	}
}
