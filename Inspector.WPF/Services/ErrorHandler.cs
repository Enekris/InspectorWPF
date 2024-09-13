using Inspector.ViewModels.Pages;

namespace Inspector.Services
{
    public static class ErrorHandler
    {
        public static void ShowError(Exception ex, string context)
        {
            var errorMessage = ex.InnerException != null
                ? $"{context} \n{ex.Message}\n{ex.InnerException}"
                : $"Внутреннее исключение не определено. \n{context} \n{ex.Message}\n{ex}";

            var exceptionViewModel = new ExceptionViewModel
            {
                ErrorMessage = errorMessage
            };

            var errorMessageWindow = new Views.Exception.ExceptionPage
            {
                DataContext = exceptionViewModel,
            };

            exceptionViewModel.CloseCommand = new RelayCommand(() =>
            {
                errorMessageWindow.Close();
            });

            errorMessageWindow.Show();
        }
    }
}
