using PokemonBusinessLayer;
using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private Utilisateur user { get; set; }
        public Login()
        {
            InitializeComponent();
            user = new Utilisateur { Login = "Lingfar" };
            DataContext = user;
            txtPassword.Password = "azertyuiop";
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(BusinessManager.Instance.CheckConnectionUser(txtLogin.Text, GetHash(txtPassword.Password)))
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Le login et/ou mot de passe est invalide", "Failed");
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        { 
            user.Password = GetHash(txtPassword.Password);
            if (BusinessManager.Instance.RegisterLogin(user))
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Erreur pendant la création (Login déjà utilisé)", "Failed");
            }
        }

        private string GetHash(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(password), 0, Encoding.ASCII.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}
