namespace CrepeDuChef.Maui.Behaviors
{
    /// <summary>
    /// Gently pulses the attached element while <see cref="IsActive"/> is true.
    /// Runs on MAUI's shared animation ticker; stops when inactive or detached.
    /// </summary>
    public class PulseBehavior : Behavior<VisualElement>
    {
        private const string AnimationName = "Pulse";
        private const uint CycleLength = 1400;
        private const double MaxScale = 1.12;

        public static readonly BindableProperty IsActiveProperty =
            BindableProperty.Create(
                nameof(IsActive),
                typeof(bool),
                typeof(PulseBehavior),
                false,
                propertyChanged: (bindable, _, _) => ((PulseBehavior)bindable).Update());

        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        private VisualElement? _element;

        protected override void OnAttachedTo(VisualElement element)
        {
            base.OnAttachedTo(element);
            _element = element;

            // Inherit the element's binding context so IsActive can be bound to the item
            BindingContext = element.BindingContext;
            element.BindingContextChanged += OnBindingContextChanged;
            element.Loaded += OnLoaded;
            element.Unloaded += OnUnloaded;

            Update();
        }

        protected override void OnDetachingFrom(VisualElement element)
        {
            Stop();
            element.BindingContextChanged -= OnBindingContextChanged;
            element.Loaded -= OnLoaded;
            element.Unloaded -= OnUnloaded;
            _element = null;

            base.OnDetachingFrom(element);
        }

        private void OnBindingContextChanged(object? sender, EventArgs e)
            => BindingContext = _element?.BindingContext;

        private void OnLoaded(object? sender, EventArgs e) => Update();

        private void OnUnloaded(object? sender, EventArgs e) => Stop();

        private void Update()
        {
            if (IsActive && _element?.IsLoaded == true)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }

        private void Start()
        {
            if (_element is null || _element.AnimationIsRunning(AnimationName))
            {
                return;
            }

            // Grow then shrink back, eased, repeated
            Animation pulse = new()
            {
                { 0.0, 0.5, new Animation(v => _element.Scale = v, 1, MaxScale, Easing.SinInOut) },
                { 0.5, 1.0, new Animation(v => _element.Scale = v, MaxScale, 1, Easing.SinInOut) },
            };

            pulse.Commit(_element, AnimationName, length: CycleLength, repeat: () => IsActive);
        }

        private void Stop()
        {
            if (_element is null)
            {
                return;
            }

            _element.AbortAnimation(AnimationName);
            _element.Scale = 1;
        }
    }
}
