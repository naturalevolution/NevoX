using Xamarin.Forms;

namespace NevoX.Helpers.Boxs
{ 
    public class RoundedBox : Button
    {
        /// <summary>
        /// The corner radius property.
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty = 
        BindableProperty.Create("CornerRadius", typeof(double), typeof(RoundedBox), 0.0);

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        } 
        /// <summary>
        /// The border with property.
        /// </summary>
        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create("BorderWidth", typeof(float), typeof(RoundedBox), 0F);

        /// <summary>
        /// Gets or sets the border with.
        /// </summary>
        public float BorderWidth
        {
            get { return (float) GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }
        /// <summary>
        /// The border with property.
        /// </summary>
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create("BorderColor", typeof(Color), typeof(RoundedBox), Color.White);

        /// <summary>
        /// Gets or sets the border with.
        /// </summary>
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
    }
}