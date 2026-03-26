using System.Diagnostics;

namespace CrepeDuChef.Maui.Behaviors
{
    public partial class PulseAnimationBehavior : Behavior<Label>
    {
        public static readonly BindableProperty IsTodayProperty =
            BindableProperty.Create(
                nameof(IsToday),
                typeof(bool),
                typeof(PulseAnimationBehavior),
                false,
                propertyChanged: OnIsTodayChanged);

        public bool IsToday
        {
            get => (bool)GetValue(IsTodayProperty);
            set => SetValue(IsTodayProperty, value);
        }

        private Label? _label;

        protected override void OnAttachedTo(Label bindable)
        {
            if (bindable == null)
            {
                return;
            }

            _label = bindable;

            BindingContext = bindable.BindingContext;
            bindable.BindingContextChanged += OnBindingContextChanged;

            if (IsToday)
            {
                PulseManager.Register(_label);
            }
        }

        private void OnBindingContextChanged(object? sender, EventArgs e)
        {
            BindingContext = _label?.BindingContext;
        }

        protected override void OnDetachingFrom(Label bindable)
        {
            if (_label != null)
                PulseManager.Unregister(_label);

            bindable.BindingContextChanged -= OnBindingContextChanged;
            _label = null;
        }

        private static void OnIsTodayChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is PulseAnimationBehavior behavior
                && behavior._label != null)
            {
                if ((bool)newValue)
                {
                    PulseManager.Register(behavior._label);
                }
                else
                {
                    PulseManager.Unregister(behavior._label);
                }
            }
        }
    }
}