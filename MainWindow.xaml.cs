using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using LiveCharts;
using LiveCharts.Wpf;

namespace EigthQueens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Population> populations = new List<Population>();
        public MainWindow()
        {
            InitializeComponent();
        }
        void FillGridGenerations(Population population)
        {
            gridGenerations.ItemsSource = null;
            gridGenerations.ItemsSource = population.Generations;
        }

        private void btEvolution_Click(object sender, RoutedEventArgs e)
        {
            Population population = new Population(50, 10, -5.120, 5.120, 10000, 0.001, 50000, 0.5);
            population.StartEvolutionProcess();
            populations.Add(population);
            Console.WriteLine(population.CurrentEvaluation);
            FillGridGenerations(population);
            SetCharts();
        }

        void SetCharts()
        {
            ChartValues<double> standardDeviationChart = new LiveCharts.ChartValues<double>();
            ChartValues<double> mediaChart = new LiveCharts.ChartValues<double>();
            ChartValues<double> bestValueChart = new LiveCharts.ChartValues<double>();
            ChartValues<double> worstValueChart = new LiveCharts.ChartValues<double>();
            ChartValues<double> convergenceValue = new LiveCharts.ChartValues<double>();
            for (int i = 0; i < populations.Count; i++)
            {
                bestValueChart.Add(populations[i].Generations.Last().BetterSubject.FitnessValue);
                worstValueChart.Add(populations[i].Generations.Last().WorstSubject.FitnessValue);
            }

            SeriesCollection standardDeviationSeries =new SeriesCollection
            {
                new LineSeries
                {
                    Values = standardDeviationChart
                }
            };
            SeriesCollection mediaSeries = new SeriesCollection
            {
                new LineSeries
                {

                    Values = mediaChart
                },
                new LineSeries
                {
                    Title = "Valor de convergencia",
                    Values = convergenceValue,
                    PointGeometry = null,

                }
            };
            SeriesCollection bestValueSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = bestValueChart
                }
            };
            SeriesCollection worstValueSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = worstValueChart
                }
            };

            StandarDeviationChart.Series = standardDeviationSeries;
            MediaChart.Series = mediaSeries;
            BestValueChart.Series = bestValueSeries;
            WorstValueChart.Series = worstValueSeries;
        }
    }
}
