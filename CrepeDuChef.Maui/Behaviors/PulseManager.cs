using System.Diagnostics;

namespace CrepeDuChef.Maui.Behaviors
{
    public static class PulseManager
    {
        private static readonly List<(Label label, double phase)> _labels = new();
        private static bool _isRunning = false;

        private const double ScaleAmplitude = 0.2; // pulse amplitude
        private const uint Interval = (uint)(1000/40);  // 1000ms/40fps -> interval in ms
        private static readonly double _step = Math.PI / 30; // vitesse du pulse

        public static void Register(Label? label)
        {
            if (label == null)
                return;

            if (!_labels.Exists(l => l.label == label))
            {
                var randomPhase = new Random().NextDouble() * 2 * Math.PI;
                _labels.Add((label, randomPhase));
            }

            if (!_isRunning)
            {
                _isRunning = true;
                StartBatchAnimation();
            }
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

            App.Current?.Dispatcher.StartTimer(TimeSpan.FromMilliseconds(
                Interval),
                () =>
                {
                    if (_labels.Count == 0)
                    {
                        _isRunning = false;
                        return false;
                    }

                    try
                    {
                        // remove deleted labels
                        _labels.RemoveAll(l => l.label == null || l.label.Handler == null || l.label.Parent == null);

                        for (int i = 0; i < _labels.Count; i++)
                        {
                            var (label, phase) = _labels[i];

                            if (label == null || label.Handler == null || label.Parent == null)
                            {
                                continue;
                            }

                            try
                            {
                                // sinus scale
                                label.Scale = 1.0 + ScaleAmplitude * Math.Sin(phase);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }

                            // phase increment
                            _labels[i] = (label, phase + _step);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }

                    // continue as long as there is at least one label left
                    return _labels.Count > 0;
                });
        }
    }

}
