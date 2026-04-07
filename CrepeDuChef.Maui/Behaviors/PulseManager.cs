using System.Diagnostics;

namespace CrepeDuChef.Maui.Behaviors
{
    public static class PulseManager
    {
        private static readonly List<(Label label, double phase)> _labels = [];
        private static readonly Random _rng = new();

        private static bool _isRunning = false;

        private const double ScaleAmplitude = 0.2;
        private const uint Interval = (uint)(1000 / 40);  // 40 FPS
        private static readonly double _step = Math.PI / 30;
        private const double Pi2 = 2 * Math.PI;

        public static void Register(Label? label)
        {
            if (label == null)
                return;

            if (!_labels.Any(l => l.label == label))
            {
                double randomPhase = _rng.NextDouble() * Pi2;
                _labels.Add((label, randomPhase));
            }

            if (_isRunning)
            {
                return;
            }

            _isRunning = true;
            StartBatchAnimation();
        }

        public static void Unregister(Label? label)
        {
            if (label == null)
            {
                return;
            }

            _labels.RemoveAll(l => l.label == label);

            if (_labels.Count == 0)
            {
                _isRunning = false;
            }
        }

        private static void StartBatchAnimation()
        {
            if (_labels.Count == 0)
            {
                _isRunning = false;
                return;
            }

            App.Current?.Dispatcher.StartTimer(
                TimeSpan.FromMilliseconds(Interval),
                () =>
                {
                    if (_labels.Count == 0)
                    {
                        _isRunning = false;
                        return false;
                    }

                    try
                    {
                        // remove deleted labels, optimized version without allocation
                        for (int i = _labels.Count - 1; i >= 0; i--)
                        {
                            var (label, _) = _labels[i];
                            
                            if (label == null
                                || label.Handler == null
                                || label.Parent == null)
                            {
                                _labels.RemoveAt(i);
                            }
                        }

                        // Animation
                        for (int i = 0; i < _labels.Count; i++)
                        {
                            var (label, phase) = _labels[i];

                            if (label == null
                                || label.Handler == null
                                || label.Parent == null
                                || !label.IsVisible)
                            {
                                continue;
                            }

                            try
                            {
                                double noise = (_rng.NextDouble() - 0.5) * 0.05; // ±5% variation
                                double eased = Math.Sin(phase) * (1.0 + noise);
                                label.Scale = 1.0 + ScaleAmplitude * eased;
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Pulse error: {ex.Message}");
                            }

                            // phase increment
                            _labels[i] = (label, phase + _step);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Pulse loop error: {ex.Message}");
                    }

                    // continue as long as there is at least one label left
                    return _labels.Count > 0;
                });
        }
    }

}
