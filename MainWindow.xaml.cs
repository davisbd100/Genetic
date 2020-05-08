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
            Population population = new Population(int.Parse(tbPopulation.Text), int.Parse(tbDValue.Text), double.Parse(tbMinorRange.Text), double.Parse(tbMaximumRange.Text),
                int.Parse(tbEvaluation.Text), double.Parse(tbMutation.Text), int.Parse(tbGeneration.Text), double.Parse(tbAlphaValue.Text), (bool)rbFirstFunction.IsChecked);
            population.StartEvolutionProcess();
            populations.Add(population);
            FillGridGenerations(population);
            SetCharts();
        }
        private void rbFirstFunction_Click(object sender, RoutedEventArgs e)
        {
            tbDValue.IsReadOnly = false;
            tbMinorRange.Text = "-5.12";
            tbMaximumRange.Text = "5.12";
            rbSecondFunction.IsChecked = false;
            rbFirstFunction.IsChecked = true;
        }

        private void rbSecondFunction_Click(object sender, RoutedEventArgs e)
        {
            tbDValue.IsReadOnly = true;
            tbMinorRange.Text = "-10";
            tbMaximumRange.Text = "10";
            rbSecondFunction.IsChecked = true;
            rbFirstFunction.IsChecked = false;
        }


        void SetCharts()
        {
            ChartValues<double> generationNumberChart = new LiveCharts.ChartValues<double>();
            ChartValues<double> evaluationNumberChart = new LiveCharts.ChartValues<double>();
            ChartValues<double> bestByGenerationChart = new LiveCharts.ChartValues<double>();
            ChartValues<double> worstByGenerationChart = new LiveCharts.ChartValues<double>();
            for (int i = 0; i < populations.Count; i++)
            {
                generationNumberChart.Add(populations[i].CurrentGeneration);
                evaluationNumberChart.Add(populations[i].CurrentEvaluation);
                bestByGenerationChart.Add(populations[i].Generations.Last().BetterSubject.FitnessValue);
                worstByGenerationChart.Add(populations[i].Generations.Last().WorstSubject.FitnessValue);
            }

           SeriesCollection generationNumberSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = generationNumberChart
                }
            };
            SeriesCollection evaluationNumberSeries = new SeriesCollection
            {
                new LineSeries
                {

                    Values = evaluationNumberChart
                }
            };
            SeriesCollection bestByGenerationSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = bestByGenerationChart
                }
            };
            SeriesCollection worstByGenerationSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = worstByGenerationChart
                }
            };

            GenerationNumberChart.Series = generationNumberSeries;
            EvaluationNumberChart.Series = evaluationNumberSeries;
            BestByGenerationChart.Series = bestByGenerationSeries;
            WorstByGenerationChart.Series = worstByGenerationSeries;
        }

    }
}
