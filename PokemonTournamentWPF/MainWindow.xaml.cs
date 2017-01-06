﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokemonBusinessLayer;
using PokemonTournamentEntities;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusinessManager businessManager { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            businessManager = BusinessManager.Instance;
            businessManager.RunTournament();
            dataGridData.ItemsSource = businessManager.GetAllPokemons();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch(button.Name)
            {
                case "btnPokemons":
                    dataGridData.ItemsSource = businessManager.GetAllPokemons();
                    btnExportPokemons.Visibility = Visibility.Visible;
                    break;
                case "btnStades":
                    dataGridData.ItemsSource = businessManager.GetAllStades();
                    btnExportPokemons.Visibility = Visibility.Collapsed;
                    break;
                case "btnMatchs":
                    dataGridData.ItemsSource = businessManager.GetAllMatchs();
                    btnExportPokemons.Visibility = Visibility.Collapsed;
                    break;
                case "btnCarac":
                    dataGridData.ItemsSource = businessManager.GetAllCaracteristiques();
                    btnExportPokemons.Visibility = Visibility.Collapsed;
                    break;
                case "btnBonus":
                    dataGridData.ItemsSource = businessManager.GetAllPokemonsByType(ETypeElement.Dragon);
                    btnExportPokemons.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void btnExportPokemons_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Pokemon>));
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "Pokemons";
            save.DefaultExt = ".xml";
            save.Filter = "XML-File | *.xml";

            Nullable<bool> result = save.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = save.FileName;
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    ser.Serialize(fs, businessManager.GetAllPokemons());
                }
            }            
        }
    }
}
