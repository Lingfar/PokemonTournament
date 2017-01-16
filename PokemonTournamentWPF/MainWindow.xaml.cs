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
        public BusinessManager businessManager { get; set; }
        public bool otherWindowsOpened { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            otherWindowsOpened = false;

            businessManager = BusinessManager.Instance;
            businessManager.RunTournament();
            dataGridData.ItemsSource = businessManager.GetAllPokemons();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "btnPokemons":
                    dataGridData.ItemsSource = businessManager.GetAllPokemons();
                    btnExportPokemons.Visibility = Visibility.Visible;
                    btnAddStade.Visibility = Visibility.Collapsed;
                    break;
                case "btnStades":
                    dataGridData.ItemsSource = businessManager.GetAllStades();
                    btnExportPokemons.Visibility = Visibility.Collapsed;
                    btnAddStade.Visibility = Visibility.Visible;
                    break;
                case "btnMatchs":
                    dataGridData.ItemsSource = businessManager.GetAllMatchs();
                    btnExportPokemons.Visibility = Visibility.Collapsed;
                    btnAddStade.Visibility = Visibility.Collapsed;
                    break;
                case "btnCarac":
                    dataGridData.ItemsSource = businessManager.GetAllCaracteristiques();
                    btnExportPokemons.Visibility = Visibility.Collapsed;
                    btnAddStade.Visibility = Visibility.Collapsed;
                    break;
                case "btnBonus":
                    dataGridData.ItemsSource = businessManager.GetAllPokemonsByType(ETypeElement.Dragon);
                    btnExportPokemons.Visibility = Visibility.Collapsed;
                    btnAddStade.Visibility = Visibility.Collapsed;
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
            if (result == true)
            {
                using (FileStream fs = new FileStream(save.FileName, FileMode.Create))
                {
                    ser.Serialize(fs, businessManager.GetAllPokemons());
                }
            }
        }

        private void dataGridData_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dataGridData.ItemsSource.GetType() == typeof(List<Stade>))
            {
                if (!otherWindowsOpened)
                {
                    DataGridRow row = (DataGridRow)ItemsControl.ContainerFromElement((DataGrid)sender,
                        (DependencyObject)e.OriginalSource);
                    if (row != null)
                    {
                        ModifStade modStade = new ModifStade(this, (Stade)row.DataContext);
                        modStade.Closed += modStade_Closed;
                        modStade.Show();
                    }
                }
            }
        }

        private void dataGridData_AutoGeneratedColumns(object sender, EventArgs e)
        {
            var grid = (DataGrid)sender;
            foreach (var item in grid.Columns)
            {
                if (item.Header.ToString() == "ID")
                {
                    item.DisplayIndex = 0;
                    break;
                }
            }
        }

        private void btnAddStade_Click(object sender, RoutedEventArgs e)
        {
            if (!otherWindowsOpened)
            {
                ModifStade modStade = new ModifStade(this);
                modStade.Closed += modStade_Closed;
                modStade.Show();
            }
        }

        private void modStade_Closed(object sender, EventArgs e)
        {
            List<Stade> allStades = businessManager.GetAllStades();
            dataGridData.ItemsSource = businessManager.GetAllStades();
            dataGridData.Items.Refresh();
        }
    }
}
