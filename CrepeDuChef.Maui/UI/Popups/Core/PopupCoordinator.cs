namespace CrepeDuChef.Maui.UI.Popups.Core
{
    public class PopupCoordinator
    {
        private readonly SemaphoreSlim _popupLock = new(1, 1);
        private readonly TimeSpan _delay = TimeSpan.FromMilliseconds(300);

        public async Task RunAsync(Func<Task> popupAction)
        {
            await _popupLock.WaitAsync();

            try
            {
                await popupAction();
                await Task.Delay(_delay);
            }
            finally
            {
                _popupLock.Release();
            }
        }

        public async Task<TResult?> RunAsync<TResult>(Func<Task<TResult?>> popupAction)
        {
            await _popupLock.WaitAsync();
            try
            {
                var result = await popupAction();
                await Task.Delay(_delay);
                return result;
            }
            finally
            {
                _popupLock.Release();
            }
        }
    }
}
