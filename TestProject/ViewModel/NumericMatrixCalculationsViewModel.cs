using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using TestProject.Model;

namespace TestProject.ViewModel
{
    public class NumericMatrixCalculationsViewModel : INotifyPropertyChanged
    {
        private Command? _addCommand;
        private double[,] _firstMatrix;
        private double[,] _secondMatrix;
        private double[,] _resultingMatrix;


        public event PropertyChangedEventHandler? PropertyChanged;


        public Command AddReadFirstMatrixCommand => _addCommand = new Command(obj => ReadFirstMatrix());

        public Command AddReadSecondMatrixCommand => _addCommand = new Command(obj => ReadSecondMatrix());

        public Command AddMultiplyMatrixCommand => _addCommand = new Command(obj => MultiplyMatrices());


        public string ResultingMatrixView
        {
            get {
                if(_resultingMatrix != null)
                    return ConvertMatrixToString(_resultingMatrix);
                else
                    {
                        return String.Empty;
                    }
                }
        }

        public string? Message { get; set; }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void ReadFirstMatrix()
        {
            var str = ReadMatrixStringFromFile();
            _firstMatrix = ConvertStringArrayToMatrix(str);
        }

        private void ReadSecondMatrix()
        {
            var str = ReadMatrixStringFromFile();
            _secondMatrix = ConvertStringArrayToMatrix(str);
        }

        private void MultiplyMatrices()
        {
            _resultingMatrix = MatrixCalculations.Multiply(_firstMatrix, _secondMatrix);

            if (_resultingMatrix != null)
                Message = String.Empty;
            else Message = "Multiplication is impossible.";

            OnPropertyChanged(nameof(ResultingMatrixView));
            OnPropertyChanged(nameof(Message));
        }

        private static string[,]? ReadMatrixStringFromFile()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Text files (*.txt;*.csv)|*.txt;*.csv|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string[] linesArray = File.ReadAllLines(openFileDialog.FileName);
                if(linesArray != null && linesArray.Length > 0)
                {
                    string[,] result = new string[linesArray.Length, linesArray[0].Split(' ').Length];
                    for (int i = 0; i < linesArray.Length; i++)
                    {
                        string[] wordsArray = linesArray[i].Split(' ');
                        for (int j = 0; j < wordsArray.Length; j++)
                        {
                            result[i, j] = wordsArray[j];
                        }
                    }

                    return result;
                }
            }

            return null;
        }

        private static string? ConvertMatrixToString(double[,] matrix)
        {
            if (matrix == null)
                return null;

            string s = string.Empty;
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                    s += matrix[i,j].ToString("F2").PadLeft(30)+ " ";
                s += Environment.NewLine;
            }
            return s;
        }

        private static double[,]? ConvertStringArrayToMatrix(string[,] matrixString)
        {
            if(matrixString == null || matrixString.Length < 1)
                return null;

            var matrix = new double[matrixString.GetLength(0), matrixString.GetLength(1)];
            for (int i = 0; i < matrixString.GetLength(0); i++)
            {
                for (int j = 0; j < matrixString.GetLength(1); j++)
                {
                    var result = double.TryParse(matrixString[i, j], out matrix[i, j]);
                    if (!result)
                        return null;
                }
            }

            return matrix;
        }
    }
}