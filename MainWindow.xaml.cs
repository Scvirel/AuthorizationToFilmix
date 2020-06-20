using System.Net;
using System.Windows;
using System.Text;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Controls;
using System.IO;
using FilmixPOST.Components;

namespace FilmixPOST
{
   
    public partial class MainWindow : Window
    {
       private string userName;
       private string userPassword;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            userName = Loggin.Text;
            userPassword = Password.Text;


            Requester r = new Requester();

            CookieContainer containerZeroRequest = r.ZeroRequest(@"https://filmix.co/");

            CookieContainer containerAuthorized = r.TryAuthorizeRequest(@"https://filmix.co/engine/ajax/user_auth.php", containerZeroRequest, userName, userPassword);

            string result = r.Request($"https://filmix.co/profile/{userName}", containerAuthorized);

            FileSaver.SaveTextToFile("page.html", result);

            FileSaver.OpenInBrowser("page.html");
        }
    }
}
