
namespace Inspector.Views.EditWindows
{
    public partial class EditGridDocPage
    {


        public EditGridDocPage()
        {

            InitializeComponent();

        }



        //private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        //{
        //    if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        //    {
        //        var term = sender.Text.ToLower();
        //        var filteredSuggestions = ViewModel.DBCollection.Where(i => i.Id.ToString().StartsWith(term)).ToList();
        //        ObservableCollection<string> filtered = new ObservableCollection<string>();
        //        foreach (var item in filteredSuggestions)
        //        {
        //            filtered.Add(item.Id.ToString());
        //        }
        //        sender.ItemsSource = filtered;
        //    }
        //}

        //private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        //{

        //}

        //private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        //{

        //}

    }
    //public class BindingProxy : Freezable
    //{
    //    public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));

    //    public object Data
    //    {
    //        get { return GetValue(DataProperty); }
    //        set { SetValue(DataProperty, value); }
    //    }

    //    protected override Freezable CreateInstanceCore()
    //    {
    //        return new BindingProxy();
    //    }
    //}


}
