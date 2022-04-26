using Miscellaneous.Optimization.MonoBehaviourCache;

namespace Miscellaneous.Console
{
	public static class TextExtentions
	{
		public const string WHITE_COLOR = "FFFFFF";
		public const string BLUE_COLOR = "00FFF7";
		public const string ORANGE_COLOR = "F4CA16";
		public const string RED_COLOR = "E22121";
		
		public static string GetExceptionBaseText(string methodName, string className)
		{
			var classNameColored = GetColoredHtmlText(RED_COLOR, className);
			var monoCacheNameColored = GetColoredHtmlText(ORANGE_COLOR, nameof(MonoBehaviourCache));
			var methodNameColored = GetColoredHtmlText(RED_COLOR, methodName);
			var baseTextColored = GetColoredHtmlText(WHITE_COLOR,
				$"can't be implemented in subclass {classNameColored} of {monoCacheNameColored}. Use ");
            
			return $"{methodNameColored} {baseTextColored}";
		}
		
		public static string GetWarningBaseText(string methodName, string recommendedMethod, string className)
		{
			var coloredClass = GetColoredHtmlText(ORANGE_COLOR, className);
			var monoCacheNameColored = GetColoredHtmlText(ORANGE_COLOR, nameof(MonoBehaviourCache));
			var coloredMethod = GetColoredHtmlText(ORANGE_COLOR, methodName);
            
			var coloredRecommendedMethod =
				GetColoredHtmlText(BLUE_COLOR, "protected override void ") + 
				GetColoredHtmlText(ORANGE_COLOR, recommendedMethod);
            
			var coloredBaseText = GetColoredHtmlText(WHITE_COLOR, 
				$"It is recommended to replace {coloredMethod} method with {coloredRecommendedMethod} " +
				$"in subclass {coloredClass} of {monoCacheNameColored}");
            
			return coloredBaseText;
		}

		public static string GetColoredHtmlText(string color, string text)
		{
			if (color.IndexOf('#') == -1)
				color = '#' + color;

			return $"<color={color}>{text}</color>";
		}
	}
}