namespace Phoenix.Core
{
    public class Broadcast
    {
        private Store _store;

        public Broadcast(Store store)
        {
            _store = store;

            _store.Subscribe(Merge);
        }

        /// <summary>
        /// The method restarts the broadcast.
        /// </summary>
        public void Restart(bool isRun = false)
        {
            Stop();
            _store.Subscribe(Merge);

            if (isRun) Merge();
        }

        /// <summary>
        /// The method stops broadcasting data to the global store.
        /// </summary>
        public void Stop()
        {
            _store.Unsubscribe(Merge);
        }

        private void Merge()
        {
            Provider.GlobalStore.MergeStores(_store);
        }
    }
}
