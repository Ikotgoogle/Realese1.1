using LibraryHome.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace LibraryHome
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<People> peoples = new List<People>() // Информация о пользователях
        {
            new People(0, "Василий Иванович", "vasiliy.ivanovitch@outlook.com", new List<int>(){1,2,3,2}),
            new People(1, "Мария Соколова", "maria.sokolova_87@yahoo.com", new List<int>(){}),
            new People(2, "Елена Соловьева ", "elena_solovyeva@live.com", new List<int>(){}),
            new People(3, "Алексей Егоров", "lex.egorov@gmail.com", new List<int>(){}),
            new People(4, "Дмитрий Васильев", "dmitry.vasilev@yandex.com", new List<int>(){}),
            new People(5, "Михаил Козлов", "m.kozlov@protonmail.com", new List<int>(){}),
            new People(6, "Максим Кузнецов", "kuznetsov.maxim@gmail.com", new List<int>(){}),
            new People(7, "Дмитрий Петров", "petrov.dmitry@yandex.com", new List<int>(){}),
            new People(8, "Иван Лебедев", "ivan_lebedev@icloud.com", new List<int>(){}),
            new People(9, "Ольга Петрова", "o.petrova@hotmail.com", new List<int>(){}),
        };

        List<Books> books = new List<Books>() // Информация о книгах
        {
            new Books(1, "Кодекс тайн", 5, 2011),
            new Books(2, "Легенды прошлого", 13, 1976),
            new Books(3, "Потерянные артефакты", 1, 2001),
            new Books(4, "Тайны и загадки 18 века", 2, 1754),
            new Books(5, "Приключения Тома Сойера", 12, 2003),
            new Books(6, "Забытые истории", 7, 2020),
            new Books(7, "Загадки времени", 9, 1985),
            new Books(8, "Война и мир", 13, 1869),
            new Books(9, "Али-Баба и сорок разбойников", 11, 1704),
            new Books(10, "1984", 18, 1949),
            new Books(11, "Гарри Поттер и философский камень", 18, 1997),
        };

        public MainWindow()
        {
            InitializeComponent();
            PeopleListView.ItemsSource = peoples; // Отображение списка читателей
            BookListView.ItemsSource = books; // Отображение списка книг
        }

        private void PeopleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Reader.Visibility = Visibility.Collapsed; // Скрытие поля "кому выдать"
            People people = PeopleListView.SelectedItem as People;

            if (people.Arts.Count > 0)
            {
                InfoListView.Visibility = Visibility.Visible; // Отображение поля, в котором перечислены названия и количество книг у выбранного пользователя
            }
            else
            {
                InfoListView.Visibility = Visibility.Collapsed;
            }

            if (PeopleListView.SelectedItem != null){ 
                TextID.Text = people.Id.ToString(); // Отображение информации о выбранном пользователе
                TextName.Text = people.Name;
                TextEmail.Text = people.Email;

                List<BookCount> bookCounts = new List<BookCount>(); // Определение и подсчет книг у пользователя
                foreach (var kvp in people.Arts)
                {
                    int bookArt = kvp.Key;
                    int count = kvp.Value;
                    Books book = books.FirstOrDefault(b => b.Article == bookArt);

                    if (book != null)
                    {
                        bookCounts.Add(new BookCount(book, count));
                    }
                }

                InfoListView.ItemsSource = bookCounts; // Вывод названий и кол-ва книг у пользователяп
            }
        }

        private void BookListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Books books = BookListView.SelectedItem as Books;

            InfoListView.Visibility = Visibility.Collapsed; // Скрытие поля, в котором перечислены названия и количество книг у выбранного пользователя
            Reader.Visibility = Visibility.Visible; // Отображение поля "кому выдать"

            Reader.ItemsSource = peoples; // Отображение списка "кому выдать"

            if(BookListView.SelectedItem != null) { // Отображение информации о выбранной книге
                TextID.Text = books.Article.ToString();
                TextName.Text = books.BookName;
                TextEmail.Text = books.Num.ToString();
            }
        }

        private void PeopleFind_Click(object sender, RoutedEventArgs e) // Поиск пользователя
        {
            string peopleSearchText = InputPeopleName.Text; // Взятие текста из поля
            if (!string.IsNullOrEmpty(peopleSearchText)) // Если не пустой и не Null выполняем поиск
            {
                int id;
                bool isNumeric = int.TryParse(peopleSearchText, out id);
                List<People> searchResults = new List<People>();

                if (isNumeric){ // поиск по ID
                    searchResults = peoples.Where(p => p.Id == id).ToList();
                }
                else{ // Поиск по имени
                    searchResults = peoples.Where(p => p.Name != null && p.Name.Contains(peopleSearchText)).ToList();
                }
                PeopleListView.ItemsSource = searchResults;
            }
        }

        private void BookFind_Click(object sender, RoutedEventArgs e) // Поиск книги
        {
            string bookSearchText = InputBookName.Text; //Взятие текста из поля
            if (!string.IsNullOrEmpty(bookSearchText)) // Если не пустой и не Null выполняем поиск
            {
                int art;
                bool isNumeric = int.TryParse(bookSearchText, out art);
                List<Books> searchResults = new List<Books>();

                if (isNumeric) // поиск артикулу 
                {
                    searchResults = books.Where(p => p.Article == art).ToList();
                }
                else // Поиск по названию
                {
                    searchResults = books.Where(p => p.BookName != null && p.BookName.Contains(bookSearchText)).ToList();
                }
                BookListView.ItemsSource = searchResults;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            PeopleListView.ItemsSource = peoples; // Отображение всех пользователей
        }

        private void BookClear_Click(object sender, RoutedEventArgs e)
        {
            BookListView.ItemsSource = books; // Отображение всех книг
        }

        private void PassBook_Click(object sender, RoutedEventArgs e) // Выдача выбранной книги выбранному пользователю
        {
            Books selectedBook = BookListView.SelectedItem as Books;
            People selectedPeople = Reader.SelectedItem as People;

            if (selectedBook != null && selectedPeople != null && selectedBook.Num > 0)
            {
                selectedBook.Num--; //Уменьшение кол-ва оставшихся книг
                TextEmail.Text = selectedBook.Num.ToString(); // Обновление кол-ва оставшихся книг на экране

                if (selectedPeople.Arts.ContainsKey(selectedBook.Article)) //Если книга у пользователя уже есть, то +1 к кол-ву
                    selectedPeople.Arts[selectedBook.Article]++;
                else // Иначе кол-во = 1
                    selectedPeople.Arts[selectedBook.Article] = 1;

                MessageBox.Show($"Книга '{selectedBook.BookName}' выдана пользователю '{selectedPeople.Name}'.", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information); // Уведомление об успешной выдаче
            }
            else
            {
                MessageBox.Show("Выберите пользователя, которому вы хотите выдать книгу.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }            
        }
    }
}
