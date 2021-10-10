namespace Phoenix.Testing
{
    public abstract class UnitTest : Testing
    {
        private PhoenixForm _form;

        protected PhoenixForm GetForm => _form;

        public UnitTest(PhoenixForm form)
        {
            _form = form;
        }

        protected abstract void Init();

        public void Exec()
        {
            Init();
            RunTesting();
        }
    }
}
