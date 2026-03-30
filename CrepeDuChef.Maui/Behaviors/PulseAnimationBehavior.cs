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

            // IMPORTANT : waiting label is ready
            bindable.Loaded += OnLabelLoaded;
        }

        private void OnLabelLoaded(object? sender, EventArgs e)
        {
            if (_label == null)
                return;

            try
            {
                if (IsToday)
                {
                    PulseManager.Register(_label);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PulseBehavior loaded error: {ex}");
            }
        }

        private void OnBindingContextChanged(object? sender, EventArgs e)
        {
            if (_label == null)
                return;

            BindingContext = _label?.BindingContext;
        }

        protected override void OnDetachingFrom(Label bindable)
        {
            bindable.Loaded -= OnLabelLoaded;
            bindable.BindingContextChanged -= OnBindingContextChanged;
            try
            {
                if (_label != null)
                {
                    PulseManager.Unregister(_label);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PulseBehavior detach error: {ex}");
            }

            _label = null;
        }

        private static void OnIsTodayChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not PulseAnimationBehavior behavior)
                return;

            var label = behavior._label;

            if (label == null || !label.IsLoaded)
                return;

            try
            {
                if ((bool)newValue)
                    PulseManager.Register(label);
                else
                    PulseManager.Unregister(label);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PulseBehavior property change error: {ex}");
            }
        }
    }
}