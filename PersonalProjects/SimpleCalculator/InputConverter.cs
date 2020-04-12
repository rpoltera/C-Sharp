
using System;

namespace SimpleCalculator
{
	public class InputConverter
	{
		public double ConvertInputToNumberic(string argTextImput)
		{
			double convertedNumber;
			if (!double.TryParse(argTextImput, out convertedNumber)) throw new ArgumentException("Exp[ected a numeric value.");
			return convertedNumber;
		}
	}
}