using NevoX.ViewModels.Base;
using Xamarin.Forms;

namespace NevoX.Views.Authentication
{
    public class SubscribePage : ContentPage
    {
        public SubscribePage()
        {
            var header = new Label
            {
                Text = "RelativeLayout",
                FontSize = 40,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center
            };

            // Create the RelativeLayout
            var relativeLayout = new RelativeLayout();

            // A Label whose upper-left is centered vertically.
            var referenceLabel = new Label
            {
                Text = "Not visible",
                Opacity = 0,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            relativeLayout.Children.Add(referenceLabel,
                Constraint.Constant(0),
                Constraint.RelativeToParent(parent => { return parent.Height/2; }));

            // A Label centered vertically.
            var centerLabel = new Label
            {
                Text = "Center",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            relativeLayout.Children.Add(centerLabel,
                Constraint.Constant(0),
                Constraint.RelativeToView(referenceLabel, (parent, sibling) => { return sibling.Y - sibling.Height/2; }));

            // A Label above the centered Label.
            var aboveLabel = new Label
            {
                Text = "Above",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            relativeLayout.Children.Add(aboveLabel,
                Constraint.RelativeToView(centerLabel, (parent, sibling) => { return sibling.X + sibling.Width; }),
                Constraint.RelativeToView(centerLabel, (parent, sibling) => { return sibling.Y - sibling.Height; }));

            // A Label below the centered Label.
            var belowLabel = new Label
            {
                Text = "Below",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            relativeLayout.Children.Add(belowLabel,
                Constraint.RelativeToView(centerLabel, (parent, sibling) => { return sibling.X + sibling.Width; }),
                Constraint.RelativeToView(centerLabel, (parent, sibling) => { return sibling.Y + sibling.Height; }));

            // Finish with another on top...
            var furtherAboveLabel = new Label
            {
                Text = "Further Above",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            relativeLayout.Children.Add(furtherAboveLabel,
                Constraint.RelativeToView(aboveLabel, (parent, sibling) => { return sibling.X + sibling.Width; }),
                Constraint.RelativeToView(aboveLabel, (parent, sibling) => { return sibling.Y - sibling.Height; }));

            // ...and another on the bottom.
            var furtherBelowLabel = new Label
            {
                Text = "Further Below",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            relativeLayout.Children.Add(furtherBelowLabel,
                Constraint.RelativeToView(belowLabel, (parent, sibling) => { return sibling.X + sibling.Width; }),
                Constraint.RelativeToView(belowLabel, (parent, sibling) => { return sibling.Y + sibling.Height; }));

            // Four BoxView's
            relativeLayout.Children.Add(
                new BoxView {Color = Color.Red},
                Constraint.Constant(0),
                Constraint.Constant(0));

            relativeLayout.Children.Add(
                new BoxView {Color = Color.Green},
                Constraint.RelativeToParent(parent => { return parent.Width - 40; }),
                Constraint.Constant(0));

            relativeLayout.Children.Add(
                new BoxView {Color = Color.Blue},
                Constraint.Constant(0),
                Constraint.RelativeToParent(parent => { return parent.Height - 40; }));

            relativeLayout.Children.Add(
                new BoxView {Color = Color.Yellow},
                Constraint.RelativeToParent(parent => { return parent.Width - 40; }),
                Constraint.RelativeToParent(parent => { return parent.Height - 40; }));

            // Build the page.
            var grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)}
                }
            };
            grid.Children.Add(header, 0, 0);
            grid.Children.Add(relativeLayout, 0, 1);

            Content = grid;
        }
    }
}