
namespace SyntaxSolutions.PdfBuilder
{
	/// <summary>
	/// PageSize
	/// </summary>
	public enum PageSize
	{
		/// <summary>
		/// A0 (841 x 1188mm)
		/// </summary>
		A0,

		/// <summary>
		/// A1 (549 x 841mm)
		/// </summary>
		A1,

		/// <summary>
		/// A2 (420 x 594mm)
		/// </summary>
		A2,

		/// <summary>
		/// A3 (297 x 420mm)
		/// </summary>
		A3,

		/// <summary>
		/// A4 (210 x 297mm)
		/// </summary>
		A4,

		/// <summary>
		/// A5 (148 x 210mm)
		/// </summary>
		A5,

		/// <summary>
		/// A6 (105 x 148mm)
		/// </summary>
		A6,

		/// <summary>
		/// A7 (74 x 105mm)
		/// </summary>
		A7,

		/// <summary>
		/// A8 (52 x 745mm)
		/// </summary>
		A8
	}

	internal static class PageSizeCalc
	{
		/// <summary>
		/// Return width of specified pageSize
		/// </summary>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public static double Width(PageSize pageSize)
		{
			double value = 0;

			switch (pageSize)
			{
				case PageSize.A0:
					value = 841;
					break;

				case PageSize.A1:
					value = 549;
					break;

				case PageSize.A2:
					value = 420;
					break;

				case PageSize.A3:
					value = 297;
					break;

				case PageSize.A4:
					value = 210;
					break;

				case PageSize.A5:
					value = 148;
					break;

				case PageSize.A6:
					value = 105;
					break;

				case PageSize.A7:
					value = 74;
					break;

				case PageSize.A8:
					value = 52;
					break;

				default:
					value = 0;
					break;
			}

			return value;
		}

		/// <summary>
		///  Return height of specified pageSize
		/// </summary>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public static double Height(PageSize pageSize)
		{
			double value = 0;

			switch (pageSize)
			{
				case PageSize.A0:
					value = 1188;
					break;

				case PageSize.A1:
					value = 841;
					break;

				case PageSize.A2:
					value = 594;
					break;

				case PageSize.A3:
					value = 420;
					break;

				case PageSize.A4:
					value = 297;
					break;

				case PageSize.A5:
					value = 210;
					break;

				case PageSize.A6:
					value = 148;
					break;

				case PageSize.A7:
					value = 105;
					break;

				case PageSize.A8:
					value = 74;
					break;

				default:
					value = 0;
					break;
			}

			return value;
		}

	}
}
