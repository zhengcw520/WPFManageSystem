using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace MS.Client.Common.Attach
{
    public class TitleElement
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.RegisterAttached("Title", typeof(string), typeof(TitleElement), new PropertyMetadata((object)null));

        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(TitleElement), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.RegisterAttached("Foreground", typeof(Brush), typeof(TitleElement), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.RegisterAttached("BorderBrush", typeof(Brush), typeof(TitleElement), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty TitlePlacementProperty = DependencyProperty.RegisterAttached("TitlePlacement", typeof(TitlePlacementType), typeof(TitleElement), new FrameworkPropertyMetadata(TitlePlacementType.Top, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty TitleWidthProperty = DependencyProperty.RegisterAttached("TitleWidth", typeof(GridLength), typeof(TitleElement), new FrameworkPropertyMetadata(GridLength.Auto, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty HorizontalAlignmentProperty = DependencyProperty.RegisterAttached("HorizontalAlignment", typeof(HorizontalAlignment), typeof(TitleElement), new FrameworkPropertyMetadata(HorizontalAlignment.Left, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty VerticalAlignmentProperty = DependencyProperty.RegisterAttached("VerticalAlignment", typeof(VerticalAlignment), typeof(TitleElement), new FrameworkPropertyMetadata(VerticalAlignment.Top, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MarginOnTheLeftProperty = DependencyProperty.RegisterAttached("MarginOnTheLeft", typeof(Thickness), typeof(TitleElement), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty PaddingProperty = DependencyProperty.RegisterAttached("Padding", typeof(Thickness), typeof(TitleElement), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MinHeightProperty = DependencyProperty.RegisterAttached("MinHeight", typeof(double), typeof(TitleElement), new PropertyMetadata(ValueBoxes.Double0Box));

        public static readonly DependencyProperty MinWidthProperty = DependencyProperty.RegisterAttached("MinWidth", typeof(double), typeof(TitleElement), new PropertyMetadata(ValueBoxes.Double0Box));

        public static void SetTitle(DependencyObject element, string value)
        {
            element.SetValue(TitleProperty, value);
        }

        public static string GetTitle(DependencyObject element)
        {
            return (string)element.GetValue(TitleProperty);
        }

        public static void SetBackground(DependencyObject element, Brush value)
        {
            element.SetValue(BackgroundProperty, value);
        }

        public static Brush GetBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(BackgroundProperty);
        }

        public static void SetForeground(DependencyObject element, Brush value)
        {
            element.SetValue(ForegroundProperty, value);
        }

        public static Brush GetForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(ForegroundProperty);
        }

        public static void SetBorderBrush(DependencyObject element, Brush value)
        {
            element.SetValue(BorderBrushProperty, value);
        }

        public static Brush GetBorderBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(BorderBrushProperty);
        }

        public static void SetTitlePlacement(DependencyObject element, TitlePlacementType value)
        {
            element.SetValue(TitlePlacementProperty, value);
        }

        public static TitlePlacementType GetTitlePlacement(DependencyObject element)
        {
            return (TitlePlacementType)element.GetValue(TitlePlacementProperty);
        }

        public static void SetTitleWidth(DependencyObject element, GridLength value)
        {
            element.SetValue(TitleWidthProperty, value);
        }

        public static GridLength GetTitleWidth(DependencyObject element)
        {
            return (GridLength)element.GetValue(TitleWidthProperty);
        }

        public static void SetHorizontalAlignment(DependencyObject element, HorizontalAlignment value)
        {
            element.SetValue(HorizontalAlignmentProperty, value);
        }

        public static HorizontalAlignment GetHorizontalAlignment(DependencyObject element)
        {
            return (HorizontalAlignment)element.GetValue(HorizontalAlignmentProperty);
        }

        public static void SetVerticalAlignment(DependencyObject element, VerticalAlignment value)
        {
            element.SetValue(VerticalAlignmentProperty, value);
        }

        public static VerticalAlignment GetVerticalAlignment(DependencyObject element)
        {
            return (VerticalAlignment)element.GetValue(VerticalAlignmentProperty);
        }

        public static void SetMarginOnTheLeft(DependencyObject element, Thickness value)
        {
            element.SetValue(MarginOnTheLeftProperty, value);
        }

        public static Thickness GetMarginOnTheLeft(DependencyObject element)
        {
            return (Thickness)element.GetValue(MarginOnTheLeftProperty);
        }

        public static void SetPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(PaddingProperty, value);
        }

        public static Thickness GetPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(PaddingProperty);
        }

        public static double GetMinHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(MinHeightProperty);
        }

        public static void SetMinHeight(DependencyObject obj, double value)
        {
            obj.SetValue(MinHeightProperty, value);
        }

        public static double GetMinWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(MinWidthProperty);
        }

        public static void SetMinWidth(DependencyObject obj, double value)
        {
            obj.SetValue(MinWidthProperty, value);
        }
    }
    public enum TitlePlacementType
    {
        Left,
        Top
    }

    internal static class ValueBoxes
    {
        internal static object TrueBox = true;

        internal static object FalseBox = false;

        internal static object VerticalBox = Orientation.Vertical;

        internal static object HorizontalBox = Orientation.Horizontal;

        internal static object VisibleBox = Visibility.Visible;

        internal static object CollapsedBox = Visibility.Collapsed;

        internal static object HiddenBox = Visibility.Hidden;

        internal static object Double01Box = 0.1;

        internal static object Double0Box = 0.0;

        internal static object Double1Box = 1.0;

        internal static object Double10Box = 10.0;

        internal static object Double20Box = 20.0;

        internal static object Double100Box = 100.0;

        internal static object Double200Box = 200.0;

        internal static object Double300Box = 300.0;

        internal static object DoubleNeg1Box = -1.0;

        internal static object Int0Box = 0;

        internal static object Int1Box = 1;

        internal static object Int2Box = 2;

        internal static object Int5Box = 5;

        internal static object Int99Box = 99;

        internal static object BooleanBox(bool value)
        {
            if (!value)
            {
                return FalseBox;
            }

            return TrueBox;
        }

        internal static object OrientationBox(Orientation value)
        {
            if (value != 0)
            {
                return VerticalBox;
            }

            return HorizontalBox;
        }

        internal static object VisibilityBox(Visibility value)
        {
            return value switch
            {
                Visibility.Visible => VisibleBox,
                Visibility.Hidden => HiddenBox,
                Visibility.Collapsed => CollapsedBox,
                _ => throw new ArgumentOutOfRangeException("value", value, null),
            };
        }
    }
}
