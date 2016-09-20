using System.ComponentModel;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using NevoX.Droid.Helpers.Boxs;
using NevoX.Helpers.Boxs;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(RoundedBox), typeof(RoundedBoxRenderer))]

namespace NevoX.Droid.Helpers.Boxs
{
public class RoundedBoxRenderer : ButtonRenderer
    { 

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == RoundedBox.CornerRadiusProperty.PropertyName)
                Invalidate();
        }

        public override void Draw(Canvas canvas)
        {
            var box = Element as RoundedBox; 
              
            var rect = new Rect(); 
            var paint = new Paint
            {
                Color = box.BorderColor.ToAndroid(), 
                AntiAlias = true,
                StrokeWidth = box.BorderWidth, 
            };
            if (box.BorderWidth > 0)
            { 
                paint.SetStyle(Paint.Style.Stroke); 
            }
            else
            {
                paint.SetStyle(Paint.Style.Fill);
            }
            GetDrawingRect(rect);

            var radius = (float) (rect.Width()/box.Width*box.CornerRadius);

            canvas.DrawRoundRect(new RectF(rect), radius, radius, paint); 
        }
    }
}