using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace hw3_05._12
{
    public partial class main_window : Window
    {
        private Button active_button = null;

        public main_window()
        {
            InitializeComponent();
        }
        
        private void matrix_click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                active_button = btn;
            }
        }
        
        private void num_click(object sender, RoutedEventArgs e)
        {
            if (active_button == null)
            {
                MessageBox.Show("Please select a matrix cell to input a number", "No Cell Selected");
                return;
            }

            if (sender is Button btn && btn.Content is string digit)
            {
                if (active_button.Content == null || active_button.Content.ToString() == "0")
                {
                    active_button.Content = digit;
                }
                else
                {
                    active_button.Content += digit;
                }
            }
        }
        
        private void clear_click(object sender, RoutedEventArgs e)
        {
            if (active_button != null)
            {
                active_button.Content = "0";
            }
        }
        
        private void equals_click(object sender, RoutedEventArgs e)
        {
            double[,] matrix = new double[3, 3];
            foreach (var child in matrix_grid.Children)
            {
                if (child is Button btn)
                {
                    string name = btn.Name; 
                    if (name.Length >= 7)
                    {
                        try
                        {
                            int row = int.Parse(name.Substring(5, 1));
                            int col = int.Parse(name.Substring(6, 1));

                            if (double.TryParse(btn.Content?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                            {
                                matrix[row, col] = value;
                            }
                            else
                            {
                                MessageBox.Show($"Invalid value in {name}", "Input Error");
                                return;
                            }
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show($"Invalid button name format: {name}", "Error");
                            return;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            MessageBox.Show($"Button name too short: {name}", "Error");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Invalid button name: {name}", "Error");
                        return;
                    }
                }
            }
            
            double det = calculate_determinant(matrix);
            
            result_text_block.Text = $"Determinant: {det}";
        }

        private double calculate_determinant(double[,] m)
        {
            return m[0,0]*(m[1,1]*m[2,2] - m[1,2]*m[2,1])
                 - m[0,1]*(m[1,0]*m[2,2] - m[1,2]*m[2,0])
                 + m[0,2]*(m[1,0]*m[2,1] - m[1,1]*m[2,0]);
        }
    }
}
