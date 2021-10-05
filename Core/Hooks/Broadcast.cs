namespace Phoenix.Core
{
    public class Broadcast
    {
        private PhoenixForm _form;
        private Store _store;

        public Broadcast(PhoenixForm form, Store store)
        {
            _store = store;
            _form = form;

            _store.Subscribe(Merge);
        }

        /// <summary>
        /// The method restarts the broadcast.
        /// </summary>
        public void Restart(bool isRun = false)
        {
            _store.Unsubscribe(Merge);
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
            _form.GlobalStore.MergeStores(_store);
        }
    }
}
