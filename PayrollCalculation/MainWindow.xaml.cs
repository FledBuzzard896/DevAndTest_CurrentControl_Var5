using System;
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

namespace PayrollCalculation
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int stavka;
        private double zp;
        private bool isNalog = false;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик кнопки Calculate_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на введенные данные
            int c = 0;
            if (CountOfHours.Text == "")
            {
                MessageBox.Show("Заполните поле \"Количество часов\"", "Отказано в проведении!", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
           
            foreach (var elem in prepodTypes.Children.OfType<RadioButton>())
            {
                if (elem.IsChecked == true) c++;
            }
            if (c == 0)
            {
                MessageBox.Show("Выберите тип преподавателя", "Отказано в проведении!", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
 
            // Определение типа преподавателя и его ставки
            foreach (var elem in prepodTypes.Children.OfType<RadioButton>())
            {
                if (elem.IsChecked == true) 
                {
                    switch (elem.Content) 
                    {
                        case "ассистент":
                            stavka = 150;
                            break;
                        case "доцент":
                            stavka = 250;
                            break;
                        case "профессор":
                            stavka = 350;
                            break;
                    }
                    break;
                }
            }

            // Проверка на налог
            if (Nalog.IsChecked == true) isNalog = true;

            // Расчет ЗП
            zp = Calculate(stavka, Convert.ToInt32(CountOfHours.Text), isNalog);

            // Вывод
            MessageBox.Show($"Ваша ЗП = {zp} руб.", "зпшка", MessageBoxButton.OK);
        }

        /// <summary>
        /// Функция расчета зарплаты
        /// </summary>
        /// <param name="stavka"> Почасовая оплата </param>
        /// <param name="countOfHours"> Количество отработанных часов </param>
        /// <param name="isNalog"> Учитывается ли подоходный налог </param>
        /// <returns></returns>
        public double Calculate(int stavka, int countOfHours, bool isNalog) 
        {
            double zp = stavka * countOfHours;
            if (isNalog) return zp - (zp * 0.13);
            else return zp;
        }

        /// <summary>
        /// Обработчик чекбокс, для отображения информационного textblock "Information"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            bool? isChecked = checkBox.IsChecked;

            // Обработка
            if (isChecked == true) Information.Visibility = Visibility.Visible;
            else Information.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Обработчик ввода строки в поле CountOfHours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        /// <summary>
        /// Функция возврата состояния, если введены все числа
        /// </summary>
        /// <param name="text"> Вводимый текст </param>
        /// <returns></returns>
        private static bool IsTextAllowed(string text)
        {
            return text.All(char.IsDigit);
        }
    }
}
