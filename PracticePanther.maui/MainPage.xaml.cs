namespace PracticePanther.maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;


        }

        public string Hello { get
            {
                return "Hello, world!";
            } 
        }

        public string Interface { get
            {
                return "THIS WILL BE INTERFACE";
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}