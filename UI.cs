using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Maui.Layouts;

namespace Reproduce.RadioGridVisible
{
	internal static class UI
	{
		#region Fields
		private static string[] days = new string[32]
		{
			"00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
			"10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
			"20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
			"30", "31",
		};
		private static string[][] periodsDays = new string[14][];

		private static string timeFormat = "HH:mm:ss.fff";
		private static string dateTimeFormat = "ddd MM-dd HH:mm:ss.fff";

		private static Color moonColor = Colors.OrangeRed;
		private static Action<DayCellFull> moonSelected = MoonSelected;
		private static Action<DayCellFull> moonUnselect = MoonUnselect;
		private static Color tranColor = Colors.Tomato;
		private static Action<DayCellFull> tranSelected = TranSelected;
		private static Action<DayCellFull> tranUnselect = TranUnselect;
		private static Color sabbColor = Colors.RoyalBlue;
		private static Action<DayCellFull> sabbSelected = SabbSelected;
		private static Action<DayCellFull> sabbUnselect = SabbUnselect;
		private static Color workColor = Colors.Black;
		private static Action<DayCellFull> workSelected = WorkSelected;
		private static Action<DayCellFull> workUnselect = WorkUnselect;
		private static Color holiColor = Colors.DeepSkyBlue;
		private static Color white = Colors.White;
		private static Color black = Colors.Black;

		private static ControlTemplate template = new ControlTemplate(() => new ContentPresenter());
		#endregion //Fields

		#region Constructor
		static UI()
		{
			//PeriodsDays
			for (var p = 13; p >= 0; --p)
			{
				var period = days[p];
				var periodDays = new string[32];
				for (var d = 31; d >= 0; --d)
				{
					var day = days[d];
					var periodDay = string.Concat(period, "-", day);
					periodDays[d] = periodDay;
				}
				periodsDays[p] = periodDays;
			}
		}
		#endregion //Constructor

		#region Methods
		public static DayCellFull CreateLuniSolarMoonthNewMoon()
		{
			var cell = CreateLuniSolarMoonth(01);
			cell.SetSelections(moonSelected, moonUnselect);
			return cell;
		}

		public static DayCellFull CreateLuniSolarMoonthTransition()
		{
			var cell = CreateLuniSolarMoonth(30);
			cell.SetSelections(tranSelected, tranUnselect);
			return cell;
		}

		public static DayCellFull CreateLuniSolarMoonthSabbath(int day)
		{
			var cell = CreateLuniSolarMoonth(day);
			cell.SetSelections(sabbSelected, sabbUnselect);
			return cell;
		}

		public static DayCellFull CreateLuniSolarMoonthWork(int day)
		{
			var cell = CreateLuniSolarMoonth(day);
			cell.SetSelections(workSelected, workUnselect);
			return cell;
		}

		private static DayCellFull CreateLuniSolarMoonth(int day)
		{
			var cell = CreateFull();
			cell.Focus.Text = days[day];
			return cell;
		}

		private static DayCellFull CreateFull()
		{
			var radio = new RadioButton();
			radio.ControlTemplate = template;
			var container = new VerticalStackLayout();
			var focus = new Label()
			{
				FontSize = 14,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
			};
			container.Add(focus);
			var other = new Label()
			{
				FontSize = 8,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
			};
			container.Add(other);
			var holidays = new AbsoluteLayout()
			{
				HeightRequest = 2,
			};
			var holidayEnding = new BoxView()
			{
				CornerRadius = 0,
				VerticalOptions = LayoutOptions.End,
				HorizontalOptions = LayoutOptions.Fill,
			};
			holidays.Add(holidayEnding);
			AbsoluteLayout.SetLayoutFlags(holidayEnding, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(holidayEnding, new Rect(0.5, 0.0, 1.0, 1.0));
			var holidayOutset = new BoxView()
			{
				CornerRadius = 0,
				VerticalOptions = LayoutOptions.End,
				HorizontalOptions = LayoutOptions.Fill,
			};
			holidays.Add(holidayOutset);
			AbsoluteLayout.SetLayoutFlags(holidayOutset, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(holidayOutset, new Rect(0.0, 0.0, 0.5, 1.0));
			container.Add(holidays);
			radio.Content = container;
			var cell = new DayCellFull(radio, container, focus, other, holidayOutset, holidayEnding);
			radio.Value = cell;
			return cell;
		}

		public static void UpdateLuniSolarMoonth(DayCellFull cell, DayInfo info)
		{
			var otherText = periodsDays[info.Gregorian.Month][info.Gregorian.Day];
			UpdateFull(cell, info, otherText);
		}

		private static void UpdateFull(DayCellFull cell, DayInfo info, string otherText)
		{
			cell.Other.Text = otherText;
			if (info.HolidayOutset)
			{
				var holidayOutset = cell.HolidayOutset;
				holidayOutset.Color = holiColor;
				holidayOutset.BackgroundColor = holiColor;
			}
			if (info.HolidayEnding)
			{
				var holidayEnding = cell.HolidayEnding;
				holidayEnding.Color = holiColor;
				holidayEnding.BackgroundColor = holiColor;
			}
			cell.Info = info;
		}

		public static void Selected(DayCellFull cell)
		{
			cell.Selected();
		}

		public static void Unselect(DayCellFull cell)
		{
			cell.Unselect();
		}

		private static void MoonSelected(DayCellFull value)
		{
			ColorSelected(value, moonColor);
		}

		private static void MoonUnselect(DayCellFull value)
		{
			ColorUnselect(value, moonColor);
		}

		private static void TranSelected(DayCellFull value)
		{
			ColorSelected(value, tranColor);
		}

		private static void TranUnselect(DayCellFull value)
		{
			ColorUnselect(value, tranColor);
		}

		private static void SabbSelected(DayCellFull value)
		{
			ColorSelected(value, sabbColor);
		}

		private static void SabbUnselect(DayCellFull value)
		{
			ColorUnselect(value, sabbColor);
		}

		private static void ColorSelected(DayCellFull value, Color color)
		{
			value.Container.BackgroundColor = color;
			var focus = value.Focus;
			focus.SetAppTheme<Color>(Label.TextColorProperty, white, black);
			focus.FontAttributes = FontAttributes.Bold;
			var other = value.Other;
			other.SetAppTheme<Color>(Label.TextColorProperty, white, black);
			other.FontAttributes = FontAttributes.Bold;
			var info = value.Info;
			if (!info.HolidayOutset)
			{
				var holidayOutset = value.HolidayOutset;
				holidayOutset.Color = color;
				holidayOutset.BackgroundColor = color;
			}
			if (!info.HolidayEnding)
			{
				var holidayEnding = value.HolidayEnding;
				holidayEnding.Color = color;
				holidayEnding.BackgroundColor = color;
			}
		}

		private static void ColorUnselect(DayCellFull value, Color color)
		{
			value.Container.SetAppTheme<Color>(View.BackgroundColorProperty, white, black);
			var focus = value.Focus;
			focus.TextColor = color;
			focus.FontAttributes = FontAttributes.None;
			var other = value.Other;
			other.TextColor = color;
			other.FontAttributes = FontAttributes.None;
			var info = value.Info;
			if (!info.HolidayOutset)
			{
				var holidayOutset = value.HolidayOutset;
				holidayOutset.SetAppTheme<Color>(BoxView.ColorProperty, white, black);
				holidayOutset.SetAppTheme<Color>(BoxView.BackgroundColorProperty, white, black);
			}
			if (!info.HolidayEnding)
			{
				var holidayEnding = value.HolidayEnding;
				holidayEnding.SetAppTheme<Color>(BoxView.ColorProperty, white, black);
				holidayEnding.SetAppTheme<Color>(BoxView.BackgroundColorProperty, white, black);
			}
		}

		private static void WorkSelected(DayCellFull value)
		{
			value.Container.SetAppTheme<Color>(View.BackgroundColorProperty, black, white);
			var focus = value.Focus;
			focus.SetAppTheme<Color>(Label.TextColorProperty, white, black);
			focus.FontAttributes = FontAttributes.Bold;
			var other = value.Other;
			other.SetAppTheme<Color>(Label.TextColorProperty, white, black);
			other.FontAttributes = FontAttributes.Bold;
			var info = value.Info;
			if (!info.HolidayOutset)
			{
				var holidayOutset = value.HolidayOutset;
				holidayOutset.SetAppTheme<Color>(BoxView.ColorProperty, black, white);
				holidayOutset.SetAppTheme<Color>(BoxView.BackgroundColorProperty, black, white);
			}
			if (!info.HolidayEnding)
			{
				var holidayEnding = value.HolidayEnding;
				holidayEnding.SetAppTheme<Color>(BoxView.ColorProperty, black, white);
				holidayEnding.SetAppTheme<Color>(BoxView.BackgroundColorProperty, black, white);
			}
		}

		private static void WorkUnselect(DayCellFull value)
		{
			value.Container.SetAppTheme<Color>(View.BackgroundColorProperty, white, black);
			var focus = value.Focus;
			focus.SetAppTheme<Color>(Label.TextColorProperty, black, white);
			focus.FontAttributes = FontAttributes.None;
			var other = value.Other;
			other.SetAppTheme<Color>(Label.TextColorProperty, black, white);
			other.FontAttributes = FontAttributes.None;
			var info = value.Info;
			if (!info.HolidayOutset)
			{
				var holidayOutset = value.HolidayOutset;
				holidayOutset.SetAppTheme<Color>(BoxView.ColorProperty, white, black);
				holidayOutset.SetAppTheme<Color>(BoxView.BackgroundColorProperty, white, black);
			}
			if (!info.HolidayEnding)
			{
				var holidayEnding = value.HolidayEnding;
				holidayEnding.SetAppTheme<Color>(BoxView.ColorProperty, white, black);
				holidayEnding.SetAppTheme<Color>(BoxView.BackgroundColorProperty, white, black);
			}
		}
		#endregion //Methods
	}
}
