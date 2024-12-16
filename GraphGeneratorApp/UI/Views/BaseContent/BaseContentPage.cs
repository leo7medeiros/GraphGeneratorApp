namespace GraphGeneratorApp.UI.Views.BaseContent
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {

        }

        private bool _isNavigating = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (height > width)
            {
                SetLayoutVertical();
            }
            else
            {
                SetLayoutHorizontal();
            }
        }

        protected virtual void SetLayoutVertical() { }

        protected virtual void SetLayoutHorizontal() { }

        public async Task Navegar(Page pagina, bool fazAnimacao = true)
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(pagina, fazAnimacao);
            }
            catch (Exception)
            {

            }
        }

        public async Task VoltarPagina(bool fazAnimacao = true)
        {
            try
            {
                if (_isNavigating) return;

                _isNavigating = true;
                await Application.Current.MainPage.Navigation.PopAsync(fazAnimacao);
                _isNavigating = false;
            }
            catch (Exception)
            {

            }
        }

        public void SetMainPage(Page pagina)
        {
            Application.Current.MainPage = new NavigationPage(pagina);
        }

        public async void VoltarParaMainPage(bool fazAnimacao = false)
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync(fazAnimacao);
        }
    }
}