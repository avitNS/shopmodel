using System;
using Forms=System.Windows.Forms;
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
using shopmodel.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;

namespace shopmodel
{

    public partial class MainWindow : Window
    {
        MyContext db;
        
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using(MyContext db=new MyContext())
            {
                db.Genres.Load();
                dgGenres.ItemsSource = db.Genres.Local.ToBindingList();

                db.MediaTypes.Load();
                dgMediaTypes.ItemsSource = db.MediaTypes.Local.ToBindingList();
                

                db.Artists.Load();
                dgArtists.ItemsSource = db.Artists.Local.ToBindingList();
               

                db.Albums.Load();
                dgAlbums.ItemsSource = db.Albums.Local.ToBindingList();
                cmbArtists.ItemsSource = db.Artists.Local.ToBindingList();


                db.Employees.Load();
                dgEmployees.ItemsSource = db.Employees.Local.ToBindingList();
                cmbEmpFunctions.ItemsSource = db.EmpFunctions.Local.ToBindingList();


                db.Publishers.Load();
                dgPublishers.ItemsSource = db.Publishers.Local.ToBindingList();

                db.EmpFunctions.Load();
                db.EnableStatuses.Load();

                db.SalesReceipts.Load();
                dgSaleReceipts.ItemsSource = db.SalesReceipts.Local.ToBindingList();
                cmbProducts.ItemsSource = db.Products.Local.ToBindingList();


                db.Sales.Load();
                dgSales.ItemsSource = db.Sales.Local.ToBindingList();
                cmbEmps.ItemsSource = db.Employees.Local.ToBindingList();
                cmbSRs.ItemsSource = db.SalesReceipts.Local.ToBindingList();

                db.Products.Load();
                dgProducts.ItemsSource = db.Products.Local.ToBindingList();
                cmbGenres.ItemsSource = db.Genres.Local.ToBindingList();
                cmbAlbums.ItemsSource = db.Albums.Local.ToBindingList();
                cmbPubs.ItemsSource = db.Publishers.Local.ToBindingList();
                cmbMediaTypes.ItemsSource = db.MediaTypes.Local.ToBindingList();
                cmbEnableStatuses.ItemsSource = db.EnableStatuses.Local.ToBindingList();

                cmbEmpsFunc.ItemsSource = db.Employees.Local.ToBindingList();

            }
        }

        private void btAddGenre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbGenreName.Text))
                {
                    db = new MyContext();
                    Genre genre = new Genre
                    {
                        Name = tbGenreName.Text
                    };
                    db.Genres.Add(genre);
                    db.SaveChanges();
                    tbGenreName.Clear();
                    db.Genres.Load();
                    dgGenres.ItemsSource = db.Genres.Local.ToBindingList();
                    cmbGenres.ItemsSource = db.Genres.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Заполните строку Название жанра для добавления в БД", "Добавление жанра", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                    tbGenreName.Clear();
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }
        }

        private void btUpdGenre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgGenres.SelectedItem != null)
                {
                    db = new MyContext();
                    var UpdId = (dgGenres.SelectedItem as Genre).Id;
                    var genre = db.Genres.Where(a => a.Id == UpdId).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(tbGenreName.Text))
                        genre.Name = tbGenreName.Text;
                    db.SaveChanges();
                    tbGenreName.Clear();
                    db.Genres.Load();
                    dgGenres.ItemsSource = db.Genres.Local.ToBindingList();
                    cmbGenres.ItemsSource = db.Genres.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите жанр и заполните строку Название жанра для внесения изменений в БД", "Изменение жанра", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                    tbGenreName.Clear();
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }


        }

        private void btDelGenre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgGenres.SelectedItem != null)
                {
                    db = new MyContext();
                    var DelId = (dgGenres.SelectedItem as Genre).Id;
                    var genre = db.Genres.Where(a => a.Id == DelId).FirstOrDefault();
                    var answer = Forms.MessageBox.Show($"Вы уверены, что хотите удалить данную запись?\nНазвание жанра: {genre.Name.ToString()}", "Подтверждение удаления", Forms.MessageBoxButtons.YesNo, Forms.MessageBoxIcon.Question);
                    switch (answer)
                    {
                        case Forms.DialogResult.Yes:
                            db.Genres.Remove(genre);
                            db.SaveChanges();
                            break;
                        case Forms.DialogResult.No:
                            break;

                    }
                    db.Genres.Load();
                    dgGenres.ItemsSource = db.Genres.Local.ToBindingList();
                    cmbGenres.ItemsSource = db.Genres.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите жанр для удаления из БД", "Изменение жанра", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }
        }

        private void dgGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedId = (dgGenres.SelectedItem as Genre).Id.ToString(); ;
                tbGenreId.Text = SelectedId;
            }
            catch
            {
                
            }

        }

        private void dgMediaTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedId = (dgMediaTypes.SelectedItem as MediaType).Id.ToString();
                tbMediaTypeId.Text = SelectedId;
            }
            catch
            {
                
            }

        }

        private void btAddMediaType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbMediaTypeName.Text))
                {
                    db = new MyContext();
                    MediaType mt = new MediaType
                    {
                        Name = tbMediaTypeName.Text
                    };
                    db.MediaTypes.Add(mt);
                    db.SaveChanges();
                    tbMediaTypeName.Clear();
                    db.MediaTypes.Load();
                    dgMediaTypes.ItemsSource = db.MediaTypes.Local.ToBindingList();
                    cmbMediaTypes.ItemsSource = db.MediaTypes.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Заполните строку Название типа носителя для добавления в БД", "Добавление типа носителя", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                    tbMediaTypeName.Clear();
                }

            }
            catch(Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btUpdMediaType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgMediaTypes.SelectedItem != null)
                {
                    db = new MyContext();
                    var UpdId = (dgMediaTypes.SelectedItem as MediaType).Id;
                    var mt = db.MediaTypes.Where(a => a.Id == UpdId).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(tbMediaTypeName.Text))
                        mt.Name = tbMediaTypeName.Text;
                    db.SaveChanges();
                    tbMediaTypeName.Clear();
                    db.MediaTypes.Load();
                    dgMediaTypes.ItemsSource = db.MediaTypes.Local.ToBindingList();
                    cmbMediaTypes.ItemsSource = db.MediaTypes.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите тип носителя и заполните строку Название типа носителя для добавления в БД", "Изменение типа носителя", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                    
                }

            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btDelMediaType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgMediaTypes.SelectedItem != null)
                {
                    db = new MyContext();
                    var DelId = (dgMediaTypes.SelectedItem as MediaType).Id;
                    var mt = db.MediaTypes.Where(a => a.Id == DelId).FirstOrDefault();
                    var answer = Forms.MessageBox.Show($"Вы уверены, что хотите удалить данную запись?\n{mt.Name.ToString()}", "Подтверждение удаления",Forms.MessageBoxButtons.YesNo, Forms.MessageBoxIcon.Question);
                    switch (answer)
                    {
                        case Forms.DialogResult.Yes:
                            db.MediaTypes.Remove(mt);
                            db.SaveChanges();
                            break;
                        case Forms.DialogResult.No:
                            break;
                    }

                    db.MediaTypes.Load();
                    dgMediaTypes.ItemsSource = db.MediaTypes.Local.ToBindingList();
                    cmbMediaTypes.ItemsSource = db.MediaTypes.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите тип носителя для удаления из БД", "Удаление типа носителя", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void dgArtists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelId = (dgArtists.SelectedItem as Artist).Id.ToString();
                tbArtistId.Text = SelId;
            }
            catch
            {
                
            }

        }

        private void btAddArtist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbArtistName.Text))
                {
                    if (!string.IsNullOrWhiteSpace(tbArtistStartActivity.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(tbArtistDesc.Text))
                        {
                            db = new MyContext();
                            Artist artist = new Artist
                            {
                                Name = tbArtistName.Text,
                                StartActivity = tbArtistStartActivity.Text,
                                Desc = tbArtistDesc.Text
                            };
                            db.Artists.Add(artist);
                            db.SaveChanges();
                            tbArtistName.Clear();
                            tbArtistStartActivity.Clear();
                            tbArtistDesc.Clear();
                            db.Artists.Load();
                            dgArtists.ItemsSource = db.Artists.Local.ToBindingList();
                            cmbArtists.ItemsSource = db.Artists.Local.ToBindingList();
                            db.Dispose();
                        }
                        else
                        {
                            Forms.MessageBox.Show("Заполните поле Описание для добавления записи в БД", "Добавления исполнителя",
                                Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        Forms.MessageBox.Show("Заполните поле Начало Активности для добавления записи в БД", "Добавления исполнителя",
                                Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                    }
                    

                }
                else
                {
                    Forms.MessageBox.Show("Заполните поле Навзание для добавления записи в БД", "Добавления исполнителя",
                                Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btUpdArtist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgArtists.SelectedItem != null)
                {
                    db = new MyContext();
                    var UpdId = (dgArtists.SelectedItem as Artist).Id;
                    var artist = db.Artists.Where(a => a.Id == UpdId).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(tbArtistName.Text))
                        artist.Name = tbArtistName.Text;
                    if (!string.IsNullOrWhiteSpace(tbArtistStartActivity.Text))
                        artist.StartActivity = tbArtistStartActivity.Text;
                    if (!string.IsNullOrWhiteSpace(tbArtistDesc.Text))
                        artist.Desc = tbArtistDesc.Text;
                    db.SaveChanges();
                    tbArtistName.Clear();
                    tbArtistStartActivity.Clear();
                    tbArtistDesc.Clear();
                    db.Artists.Load();
                    dgArtists.ItemsSource = db.Artists.Local.ToBindingList();
                    cmbArtists.ItemsSource = db.Artists.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выбеирте исполнителя и заполните поле Исполнитель, Начало активности и Описание для добавления записи в БД",
                                    "Изменение исполнителя",
                                Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
                            
            }
            catch(Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btDelArtist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgArtists.SelectedItem != null)
                {
                    db = new MyContext();
                    var DelId = (dgArtists.SelectedItem as Artist).Id;
                    var artist = db.Artists.Where(a => a.Id == DelId).FirstOrDefault();
                    var answer = Forms.MessageBox.Show($"Вы уверены, что хотите удалить данную запись?\n{artist.Name.ToString()}", "Подтверждение удаления", Forms.MessageBoxButtons.YesNo, Forms.MessageBoxIcon.Question);
                    switch (answer)
                    {
                        case Forms.DialogResult.Yes:
                            db.Artists.Remove(artist);
                            db.SaveChanges();
                            break;
                        case Forms.DialogResult.No:
                            break;
                    }
                    db.Artists.Load();
                    dgArtists.ItemsSource = db.Artists.Local.ToBindingList();
                    cmbArtists.ItemsSource = db.Artists.Local.ToBindingList();
                    db.Dispose();

                }
                else
                {
                    Forms.MessageBox.Show("Выбеирте исполнителя для удаления записи из БД",
                                    "Удаление исполнителя",
                                Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }
        }

        private void dgAlbums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelId = (dgAlbums.SelectedItem as Album).Id.ToString();
                tbAlbumId.Text = SelId;
            }
            catch
            {
            }
        }

        private void btAddAlbum_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbAlbumName.Text))
                {
                    if (!string.IsNullOrWhiteSpace(tbAlbumDateCration.Text))
                    {
                        if (cmbArtists.SelectedItem != null)
                        {
                            db = new MyContext();
                            Album album = new Album
                            {
                                Name = tbAlbumName.Text,
                                DateCreation = tbAlbumDateCration.Text,
                                IdArtist = (cmbArtists.SelectedItem as Artist).Id
                            };
                            db.Albums.Add(album);
                            db.SaveChanges();
                            tbAlbumName.Clear();
                            tbAlbumDateCration.Clear();
                            cmbArtists.SelectedItem = null;
                            db.Albums.Load();
                            db.Artists.Load();
                            dgAlbums.ItemsSource = db.Albums.Local.ToBindingList();
                            cmbAlbums.ItemsSource = db.Albums.Local.ToBindingList();
                            db.Dispose();
                        }
                        else
                        {
                            Forms.MessageBox.Show("Заполните поле Исполнитель для добавления записи в БД", "Добавление альбома",
                                Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        Forms.MessageBox.Show("Заполните поле Дата создания для добавления записи в БД", "Добавление альбома",
                                Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Forms.MessageBox.Show("Заполните поле Название для добавления записи в БД", "Добавление альбома",
                                Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
      
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btUpdAlbum_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgAlbums.SelectedItem != null)
                {
                    db = new MyContext();
                    var UpdId = (dgAlbums.SelectedItem as Album).Id;
                    var album = db.Albums.Where(a => a.Id == UpdId).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(tbAlbumName.Text))
                        album.Name = tbAlbumName.Text;
                    if (!string.IsNullOrWhiteSpace(tbAlbumDateCration.Text))
                        album.DateCreation = tbAlbumDateCration.Text;
                    if (cmbArtists.SelectedItem != null)
                        album.IdArtist = (cmbArtists.SelectedItem as Artist).Id;
                    db.SaveChanges();
                    tbAlbumName.Clear();
                    tbAlbumDateCration.Clear();
                    cmbArtists.SelectedItem = null;
                    db.Albums.Load();
                    db.Artists.Load();
                    dgAlbums.ItemsSource = db.Albums.Local.ToBindingList();
                    cmbAlbums.ItemsSource = db.Albums.Local.ToBindingList();
                    db.Dispose();

                }
                else
                {
                    Forms.MessageBox.Show("Выберите альбом и заполните поле Название, Дата создания, Исполнитель для изменения записи в БД", "Изменение альбома",
                                Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btDelAlbum_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgAlbums.SelectedItem != null)
                {
                    db = new MyContext();
                    var DelId = (dgAlbums.SelectedItem as Album).Id;
                    var album = db.Albums.Where(a => a.Id == DelId).FirstOrDefault();
                    var answer = Forms.MessageBox.Show($"Вы уверены, что хотите удалить данную запись?\n{album.Name.ToString()}", "Подтверждение удаления", Forms.MessageBoxButtons.YesNo, Forms.MessageBoxIcon.Question);
                    switch (answer)
                    {
                        case Forms.DialogResult.Yes:
                            db.Albums.Remove(album);
                            db.SaveChanges();
                            break;
                        case Forms.DialogResult.No:
                            break;
                    }
                    db.Albums.Load();
                    db.Artists.Load();
                    dgAlbums.ItemsSource = db.Albums.Local.ToBindingList();
                    cmbAlbums.ItemsSource = db.Albums.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите альбом для удаления записи из БД", "Удаление альбома",
                                Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);

                }
            }

            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void dgEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelId = (dgEmployees.SelectedItem as Employee).Id.ToString();
                tbEmpId.Text = SelId;
            }
            catch
            {
            }
        }

        private void btAddEmp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbEmpLastName.Text))
                {
                    if (!string.IsNullOrWhiteSpace(tbEmpFirstName.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(tbEmpMiddleName.Text))
                        {
                            if (cmbEmpFunctions.SelectedItem != null)
                            {
                                db = new MyContext();
                                Employee employee = new Employee
                                {
                                    LastName = tbEmpLastName.Text,
                                    FirstName = tbEmpFirstName.Text,
                                    MiddleName = tbEmpMiddleName.Text,
                                    IdEmpFunction = (cmbEmpFunctions.SelectedItem as EmpFunction).Id
                                };
                                db.Employees.Add(employee);
                                db.SaveChanges();
                                tbEmpLastName.Clear();
                                tbEmpFirstName.Clear();
                                tbEmpMiddleName.Clear();
                                cmbEmpFunctions.SelectedItem = null;
                                db.Employees.Load();
                                db.EmpFunctions.Load();
                                dgEmployees.ItemsSource = db.Employees.Local.ToBindingList();
                                cmbEmps.ItemsSource = db.Employees.Local.ToBindingList();
                                cmbEmpsFunc.ItemsSource = db.Employees.Local.ToBindingList();
                                
                                db.Dispose();

                            }
                            else
                            {
                                Forms.MessageBox.Show("Заполните поле Должность для добавления записи в БД", "Добавление сотрудника",
                                Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            Forms.MessageBox.Show("Заполните поле Отчество для добавления записи в БД", "Добавление сотрудника",
                            Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        Forms.MessageBox.Show("Заполните поле Имя для добавления записи в БД", "Добавление сотрудника",
                        Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Forms.MessageBox.Show("Заполните поле Фамилия для добавления записи в БД", "Добавление сотрудника",
                    Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }
        }

        private void btUpdEmp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgEmployees.SelectedItem != null)
                {
                    db = new MyContext();
                    var UpdId = (dgEmployees.SelectedItem as Employee).Id;
                    var employee = db.Employees.Where(a => a.Id == UpdId).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(tbEmpLastName.Text))
                        employee.LastName = tbEmpLastName.Text;
                    if (!string.IsNullOrWhiteSpace(tbEmpFirstName.Text))
                        employee.FirstName = tbEmpFirstName.Text;
                    if (!string.IsNullOrWhiteSpace(tbEmpMiddleName.Text))
                        employee.MiddleName = tbEmpMiddleName.Text;
                    if (cmbEmpFunctions.SelectedItem != null)
                        employee.IdEmpFunction = (cmbEmpFunctions.SelectedItem as EmpFunction).Id;
                    db.SaveChanges();
                    tbEmpLastName.Clear();
                    tbEmpFirstName.Clear();
                    tbEmpMiddleName.Clear();
                    cmbEmpFunctions.SelectedItem = null;
                    db.Employees.Load();
                    db.EmpFunctions.Load();
                    dgEmployees.ItemsSource = db.Employees.Local.ToBindingList();
                    cmbEmps.ItemsSource = db.Employees.Local.ToBindingList();
                    cmbEmpsFunc.ItemsSource = db.Employees.Local.ToBindingList();
                    
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите и заполните поле Фамилия, Имя, Отчество, Должность для изменения записи в БД", "Изменение сотрудника",
                                Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btDelEmp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgEmployees.SelectedItem != null)
                {
                    db = new MyContext();
                    var DelId = (dgEmployees.SelectedItem as Employee).Id;
                    var employee = db.Employees.Where(a => a.Id == DelId).FirstOrDefault();
                    var answer = Forms.MessageBox.Show($"Вы уверены, что хотите удалить данную запись?\n{employee.LastName+" "+employee.FirstName+" "+employee.MiddleName}", "Подтверждение удаления", Forms.MessageBoxButtons.YesNo, Forms.MessageBoxIcon.Question);
                    switch (answer)
                    {
                        case Forms.DialogResult.Yes:
                            db.Employees.Remove(employee);
                            db.SaveChanges();
                            break;
                        case Forms.DialogResult.No:
                            break;
                    }
                    
                    db.Employees.Load();
                    db.EmpFunctions.Load();
                    dgEmployees.ItemsSource = db.Employees.Local.ToBindingList();
                    cmbEmps.ItemsSource = db.Employees.Local.ToBindingList();
                    cmbEmpsFunc.ItemsSource = db.Employees.Local.ToBindingList();
                    
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите сотрудника для удаления записи из БД", "Удаление сотрудника",
                                Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }
        }


        private void dgPublisher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelId = (dgPublishers.SelectedItem as Publisher).Id.ToString();
                tbPubId.Text = SelId;
            }
            catch
            {
            }
        }

        private void btAddPub_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbPubName.Text))
                {
                    if (!string.IsNullOrWhiteSpace(tbPubDesc.Text))
                    {
                        db = new MyContext();
                        Publisher publisher = new Publisher
                        {
                            Name = tbPubName.Text,
                            Desc = tbPubDesc.Text
                        };
                        db.Publishers.Add(publisher);
                        db.SaveChanges();
                        tbPubName.Clear();
                        tbPubDesc.Clear();
                        db.Publishers.Load();
                        dgPublishers.ItemsSource = db.Publishers.Local.ToBindingList();
                        cmbPubs.ItemsSource = db.Publishers.Local.ToBindingList();
                        db.Dispose();
                    }
                    else
                    {
                        Forms.MessageBox.Show("Заполните поле Описание для добавления записи в БД", "Добавление издателя", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Forms.MessageBox.Show("Заполните поле Название для добавления записи в БД", "Добавление издателя", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }
        }

        private void btUpdPub_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgPublishers.SelectedItem != null)
                {
                    db = new MyContext();
                    var UpdId = (dgPublishers.SelectedItem as Publisher).Id;
                    var publisher = db.Publishers.Where(a => a.Id == UpdId).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(tbPubName.Text))
                        publisher.Name = tbPubName.Text;
                    if (!string.IsNullOrWhiteSpace(tbPubDesc.Text))
                        publisher.Desc = tbPubDesc.Text;
                    db.SaveChanges();
                    tbPubName.Clear();
                    tbPubDesc.Clear();
                    db.Publishers.Load();
                    dgPublishers.ItemsSource = db.Publishers.Local.ToBindingList();
                    cmbPubs.ItemsSource = db.Publishers.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите издателя и заполните поле Название, Описани для изменения записи в БД","Изменение издателя",Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }
        }

        private void btDelPub_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgPublishers.SelectedItem != null)
                {
                    db = new MyContext();
                    var DelId = (dgPublishers.SelectedItem as Publisher).Id;
                    var publisher = db.Publishers.Where(a => a.Id == DelId).FirstOrDefault();
                    var answer = Forms.MessageBox.Show($"Вы уверены, что хотите удалить данную запись?\n{publisher.Name}", "Удаление издателя", Forms.MessageBoxButtons.YesNo, Forms.MessageBoxIcon.Question);
                    switch (answer)
                    {
                        case Forms.DialogResult.Yes:
                            db.Publishers.Remove(publisher);
                            db.SaveChanges();
                            break;
                        case Forms.DialogResult.No:
                            break;
                    }
                    db.Publishers.Load();
                    dgPublishers.ItemsSource = db.Publishers.Local.ToBindingList();
                    cmbPubs.ItemsSource = db.Publishers.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите издателя для удаления записи из БД", "Удаление издателя", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelId = (dgProducts.SelectedItem as Product).Id.ToString();
                tbProdId.Text = SelId;
            }
            catch
            {

            }
        }

        private void btAddProd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbAlbums.SelectedItem != null)
                {
                    if (!string.IsNullOrWhiteSpace(tbProdDesc.Text))
                    {
                        if (cmbGenres.SelectedItem != null)
                        {
                            if (cmbPubs.SelectedItem != null)
                            {
                                if (cmbMediaTypes.SelectedItem != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(tbProdDateAdd.Text))
                                    {
                                        if (cmbEnableStatuses.SelectedItem != null)
                                        {
                                            if (!string.IsNullOrWhiteSpace(tbProdPrice.Text))
                                            {
                                                db = new MyContext();
                                                Product product = new Product
                                                {
                                                    IdAlbum = (cmbAlbums.SelectedItem as Album).Id,
                                                    Desc = tbProdDesc.Text,
                                                    IdGenre = (cmbGenres.SelectedItem as Genre).Id,
                                                    IdPublisher = (cmbPubs.SelectedItem as Publisher).Id,
                                                    IdMediaType = (cmbMediaTypes.SelectedItem as MediaType).Id,
                                                    DateAdd = tbProdDateAdd.Text,
                                                    IdEnableStatus = (cmbEnableStatuses.SelectedItem as EnableStatuses).Id,
                                                    Price = Convert.ToDecimal(tbProdPrice.Text)
                                                };
                                                db.Products.Add(product);
                                                db.SaveChanges();
                                                cmbAlbums.SelectedItem = null;
                                                tbProdDesc.Clear();
                                                cmbGenres.SelectedItem = null;
                                                cmbPubs.SelectedItem = null;
                                                cmbMediaTypes.SelectedItem = null;
                                                tbProdDateAdd.Clear();
                                                cmbEnableStatuses.SelectedItem = null;
                                                tbProdPrice.Clear();
                                                db.Products.Load();
                                                db.Albums.Load();
                                                db.Genres.Load();
                                                db.Publishers.Load();
                                                db.MediaTypes.Load();
                                                db.EnableStatuses.Load();
                                                dgProducts.ItemsSource = db.Products.Local.ToBindingList();
                                                cmbProducts.ItemsSource = db.Products.Local.ToBindingList();
                                                db.Dispose();
                                            }
                                            else
                                            {
                                                Forms.MessageBox.Show("Заполните поле Цена для добавления записи в БД",
                                                    "Добавление товара", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                                            }
                                        }
                                        else
                                        {
                                            Forms.MessageBox.Show("Заполните поле Наличие для добавления записи в БД",
                                                "Добавление товара", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                                        }
                                    }
                                    else
                                    {
                                        Forms.MessageBox.Show("Заполните поле Дата добавления для добавления записи в БД",
                                            "Добавление товара", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    Forms.MessageBox.Show("Заполните поле Тип носителя для добавления записи в БД",
                                        "Добавление товара", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                Forms.MessageBox.Show("Заполните поле Издатель для добавления записи в БД",
                                    "Добавление товара", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            Forms.MessageBox.Show("Заполните поле Жанр для добавления записи в БД",
                                "Добавление товара", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        Forms.MessageBox.Show("Заполните поле Описание для добавления записи в БД",
                            "Добавление товара", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Forms.MessageBox.Show("Заполните поле Альбом для добавления записи в БД",
                        "Добавление товара", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btUpdProd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgProducts.SelectedItem != null)
                {
                    db = new MyContext();
                    var UpdId = (dgProducts.SelectedItem as Product).Id;
                    var product = db.Products.Where(a => a.Id == UpdId).FirstOrDefault();
                    if (cmbAlbums.SelectedItem != null)
                        product.IdAlbum = (cmbAlbums.SelectedItem as Album).Id;
                    if (!string.IsNullOrWhiteSpace(tbProdDesc.Text))
                        product.Desc = tbProdDesc.Text;
                    if (cmbGenres.SelectedItem != null)
                        product.IdGenre = (cmbGenres.SelectedItem as Genre).Id;
                    if (cmbPubs.SelectedItem != null)
                        product.IdPublisher = (cmbPubs.SelectedItem as Publisher).Id;
                    if (cmbMediaTypes.SelectedItem != null)
                        product.IdMediaType = (cmbMediaTypes.SelectedItem as MediaType).Id;
                    if (!string.IsNullOrWhiteSpace(tbProdDateAdd.Text))
                        product.DateAdd = tbProdDateAdd.Text;
                    if (cmbEnableStatuses.SelectedItem != null)
                        product.IdEnableStatus = (cmbEnableStatuses.SelectedItem as EnableStatuses).Id;
                    if (!string.IsNullOrWhiteSpace(tbProdPrice.Text))
                        product.Price = Convert.ToDecimal(tbProdPrice.Text);
                    db.SaveChanges();
                    cmbAlbums.SelectedItem = null;
                    tbProdDesc.Clear();
                    cmbGenres.SelectedItem = null;
                    cmbPubs.SelectedItem = null;
                    cmbMediaTypes.SelectedItem = null;
                    tbProdDateAdd.Clear();
                    cmbEnableStatuses.SelectedItem = null;
                    tbProdPrice.Clear();
                    db.Products.Load();
                    db.Albums.Load();
                    db.Genres.Load();
                    db.Publishers.Load();
                    db.MediaTypes.Load();
                    db.EnableStatuses.Load();
                    dgProducts.ItemsSource = db.Products.Local.ToBindingList();
                    cmbProducts.ItemsSource = db.Products.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите товар и заполните поле Альбом, Описание, Жанр, Издатель, Тип носителя, Дата добавления, Наличие, Цена для добавления записи в БД", 
                        "Изменение товара", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                    cmbAlbums.SelectedItem = null;
                    tbProdDesc.Clear();
                    cmbGenres.SelectedItem = null;
                    cmbPubs.SelectedItem = null;
                    cmbMediaTypes.SelectedItem = null;
                    tbProdDateAdd.Clear();
                    cmbEnableStatuses.SelectedItem = null;
                    tbProdPrice.Clear();
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }
        }

        private void btDelProd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgProducts.SelectedItem != null)
                {
                    db = new MyContext();
                    var DelId = (dgProducts.SelectedItem as Product).Id;
                    var product = db.Products.Where(a => a.Id == DelId).FirstOrDefault();
                    var answer = Forms.MessageBox.Show($"Вы уверены, что хотите удалить данну запись\n{ product.Desc}","Удаление товара",Forms.MessageBoxButtons.YesNo, Forms.MessageBoxIcon.Question);
                    switch (answer)
                    {
                        case Forms.DialogResult.Yes:
                            db.Products.Remove(product);
                            db.SaveChanges();
                            break;
                        case Forms.DialogResult.No:
                            break;
                    }
                    db.Products.Load();
                    db.Albums.Load();
                    db.Genres.Load();
                    db.Publishers.Load();
                    db.MediaTypes.Load();
                    db.EnableStatuses.Load();
                    dgProducts.ItemsSource = db.Products.Local.ToBindingList();
                    cmbProducts.ItemsSource = db.Products.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите товар для удаления записи из БД",
                        "Удаление товара", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void dgSaleReceipts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelId = (dgSaleReceipts.SelectedItem as SaleReceipt).Id.ToString();
                tbSRId.Text = SelId;
            }
            catch
            {
            }
        }

        private void btAddSR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(cmbProducts.SelectedItem != null)
                {
                    if (!string.IsNullOrWhiteSpace(tbProdQuant.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(tbDateReceipt.Text))
                        {
                            db = new MyContext();
                            SaleReceipt saleReceipt = new SaleReceipt();
                            saleReceipt.IdProduct = (cmbProducts.SelectedItem as Product).Id;
                            saleReceipt.ProdQuantity = Convert.ToInt32(tbProdQuant.Text);
                            var product = db.Products.Where(a => a.Id == saleReceipt.IdProduct).FirstOrDefault();
                            saleReceipt.Total = product.Price * saleReceipt.ProdQuantity;
                            saleReceipt.DateReceipt = tbDateReceipt.Text;
                            db.SalesReceipts.Add(saleReceipt);
                            db.SaveChanges();
                            cmbProducts.SelectedItem = null;
                            tbProdQuant.Clear();
                            tbDateReceipt.Clear();
                            db.Albums.Load();
                            db.Products.Load();
                            db.SalesReceipts.Load();
                            dgSaleReceipts.ItemsSource = db.SalesReceipts.Local.ToBindingList();
                            cmbSRs.ItemsSource = db.SalesReceipts.Local.ToBindingList();
                            db.Dispose();
                        }
                        else
                        {
                            Forms.MessageBox.Show("Заполните поле Дата продажи для внесения записи в БД", "Добавление товарного чека", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        Forms.MessageBox.Show("Заполните поле Количество для внесения записи в БД", "Добавление товарного чека", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Forms.MessageBox.Show("Заполните поле Товар для внесения записи в БД", "Добавление товарного чека", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }
        }

        private void btUpdSR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgSaleReceipts.SelectedItem != null)
                {
                    db = new MyContext();
                    var UpdId = (dgSaleReceipts.SelectedItem as SaleReceipt).Id;
                    var saleReceipt = db.SalesReceipts.Where(a => a.Id == UpdId).FirstOrDefault();
                    if (cmbProducts.SelectedItem != null)
                        saleReceipt.IdProduct = (cmbProducts.SelectedItem as Product).Id;
                    if (!string.IsNullOrWhiteSpace(tbProdQuant.Text))
                        saleReceipt.ProdQuantity = Convert.ToInt32(tbProdQuant.Text);
                    var product = db.Products.Where(a => a.Id == saleReceipt.IdProduct).FirstOrDefault();
                    saleReceipt.Total = product.Price * saleReceipt.ProdQuantity;
                    if (!string.IsNullOrWhiteSpace(tbDateReceipt.Text))
                        saleReceipt.DateReceipt = tbDateReceipt.Text;
                    db.SaveChanges();
                    cmbProducts.SelectedItem = null;
                    tbProdQuant.Clear();
                    tbDateReceipt.Clear();
                    db.Albums.Load();
                    db.Products.Load();
                    db.SalesReceipts.Load();
                    dgSaleReceipts.ItemsSource = db.SalesReceipts.Local.ToBindingList();
                    cmbSRs.ItemsSource = db.SalesReceipts.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите товарный чек и заполните поле Товар, Количетсво, Итого, Дата продажи для изменения записи в БД", "Изменение товарного чека", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }
        }

        private void btDelSR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgSaleReceipts.SelectedItem != null)
                {
                    db = new MyContext();
                    var DelId = (dgSaleReceipts.SelectedItem as SaleReceipt).Id;
                    var saleReceipt = db.SalesReceipts.Where(a => a.Id == DelId).FirstOrDefault();
                    var answer = Forms.MessageBox.Show($"Вы уверены, что хотите удалить данную запись?\n{"Товар: " + saleReceipt.IdProduct + " Количество: " + saleReceipt.ProdQuantity + " Итого: " + saleReceipt.Total + " Дата продажи: " + saleReceipt.DateReceipt}",
                        "Удаление товарного чека", Forms.MessageBoxButtons.YesNo, Forms.MessageBoxIcon.Question);
                    switch (answer)
                    {
                        case Forms.DialogResult.Yes:
                            db.SalesReceipts.Remove(saleReceipt);
                            db.SaveChanges();
                            break;
                        case Forms.DialogResult.No:
                            break;
                    }
                    db.Albums.Load();
                    db.Products.Load();
                    db.SalesReceipts.Load();
                    dgSaleReceipts.ItemsSource = db.SalesReceipts.Local.ToBindingList();
                    cmbSRs.ItemsSource = db.SalesReceipts.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите товарный чек для удаления из ДБ", "Удаление товарного чека", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void dgSales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelId = (dgSales.SelectedItem as Sale).Id.ToString();
                tbSaleId.Text = SelId;
            }
            catch
            {
            }
        }

        private void btAddSales_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(cmbEmps.SelectedItem != null)
                {
                    if(cmbSRs.SelectedItem != null)
                    {
                        db = new MyContext();
                        Sale sale = new Sale
                        {
                            IdEmployee = (cmbEmps.SelectedItem as Employee).Id,
                            IdSaleReceipt = (cmbSRs.SelectedItem as SaleReceipt).Id
                        };
                        db.Sales.Add(sale);
                        db.SaveChanges();
                        cmbSRs.SelectedItem = null;
                        cmbEmps.SelectedItem = null;
                        db.Sales.Load();
                        db.Products.Load();
                        db.SalesReceipts.Load();
                        db.Albums.Load();
                        db.Employees.Load();
                        dgSales.ItemsSource = db.Sales.Local.ToBindingList();
                        db.Dispose();
                    }
                    else
                    {
                        Forms.MessageBox.Show("Заполните поле Товарный чек для добавления записи в БД", "Добавление продажи", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Forms.MessageBox.Show("Заполните поле Сотрудник для добавления записи в БД", "Добавление продажи", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }
        }

        private void btUpdSales_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgSales.SelectedItem != null)
                {
                    db = new MyContext();
                    var UpdId = (dgSales.SelectedItem as Sale).Id;
                    var sale = db.Sales.Where(a => a.Id == UpdId).FirstOrDefault();
                    if (cmbEmps.SelectedItem != null)
                        sale.IdEmployee = (cmbEmps.SelectedItem as Employee).Id;
                    if (cmbSRs.SelectedItem != null)
                        sale.IdSaleReceipt = (cmbSRs.SelectedItem as SaleReceipt).Id;
                    db.SaveChanges();
                    cmbSRs.SelectedItem = null;
                    cmbEmps.SelectedItem = null;
                    db.Sales.Load();
                    db.Products.Load();
                    db.SalesReceipts.Load();
                    db.Albums.Load();
                    db.Employees.Load();
                    dgSales.ItemsSource = db.Sales.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите продажу и заполните поле Сотрудник, Товарный чек для изменения записи в БД", "Изменение продажи", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btDelSales_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dgSales.SelectedItem != null)
                {
                    db = new MyContext();
                    var DelId = (dgSales.SelectedItem as Sale).Id;
                    var sale = db.Sales.Where(a => a.Id == DelId).FirstOrDefault();
                    var answer = Forms.MessageBox.Show($"Вы уверены, что хотите удалить данную запись?\n{"Сотрудник: " + sale.IdEmployee + " Товарный чек: " + sale.IdSaleReceipt}", "Удаление продажи", Forms.MessageBoxButtons.YesNo,
                        Forms.MessageBoxIcon.Question);
                    switch (answer)
                    {
                        case Forms.DialogResult.Yes:
                            db.Sales.Remove(sale);
                            db.SaveChanges();
                            break;
                        case Forms.DialogResult.No:
                            break;
                    }
                    db.Products.Load();
                    db.SalesReceipts.Load();
                    db.Albums.Load();
                    db.Employees.Load();
                    db.Sales.Load();
                    dgSales.ItemsSource = db.Sales.Local.ToBindingList();
                    db.Dispose();
                }
                else
                {
                    Forms.MessageBox.Show("Выберите продажу для удаления записи из БД", "Удаление продажи", Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(ex.Message);
            }

        }

        private void cmbEmpsFunc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlConnection cnn = new SqlConnection("data source=(local); initial catalog=MusicShop; integrated security=true;");
            SqlCommand cmd = new SqlCommand("select * from SalesEmployeeFunc(@LastName)", cnn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.SelectCommand.Parameters.AddWithValue("@LastName", (cmbEmpsFunc.SelectedItem as Employee).LastName.ToString());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgFunc.ItemsSource = dt.DefaultView.Table.DefaultView;
            cnn.Close();
        }

        private void btUpdateView_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection cnn = new SqlConnection("data source=(local); initial catalog=MusicShop; integrated security=true;");
            SqlCommand cmd = new SqlCommand("select * from SalesEmployees", cnn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgView.ItemsSource = dt.DefaultView.Table.DefaultView;
            cnn.Close();
        }
    }
}
