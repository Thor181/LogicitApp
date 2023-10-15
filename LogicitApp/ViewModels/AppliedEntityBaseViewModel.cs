using Castle.Components.DictionaryAdapter.Xml;
using LogicitApp.Data.DataLogic;
using LogicitApp.Data.DataLogic.Interfaces;
using LogicitApp.Data.Models.Interfaces;
using LogicitApp.Shared.Commands;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace LogicitApp.ViewModels
{
    public class AppliedEntityBaseViewModel<T, TDataLogic> : INotifyPropertyChanged, INotifyCollectionChanged 
        where T : class, IDbEntity, new() 
        where TDataLogic : class, IAppliedEntityLogic, new()
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public virtual ObservableCollection<T> Entities { get; set; } = new();

        public SimpleCommand AddCommand { get; set; }
        public SimpleCommand RemoveCommand { get; set; }

        public AppliedEntityBaseViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            AddCommand = new SimpleCommand(AddHandler);
            RemoveCommand = new SimpleCommand(RemoveHandler);

            Entities.CollectionChanged += CollectionChanged;

            LoadEntities();
        }

        private void LoadEntities()
        {
            using var baseLogic = new BaseLogic();

            var entities = baseLogic.GetAll<T>().ToList();

            Entities.Clear();
            foreach (var item in entities)
                Entities.Add(item);
        }

        public void AddHandler(object? parameter)
        {
            Entities.Add(new T());
        }

        public void RemoveHandler(object? parameter)
        {
            var idParsed = long.TryParse(parameter?.ToString(), out long id);

            if (!idParsed)
            {
                MessageBox.Show("ID сущности не распознан");
                return;
            }

            using var appliedEntityLogic = new TDataLogic();
            if (appliedEntityLogic.IsUsed(id))
            {
                MessageBox.Show("Сущность не может быть удалена, так как используется в заказах", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var confirmation = MessageBox.Show("Действительно удалить данную сущность?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmation == MessageBoxResult.No)
                return;

            var result = appliedEntityLogic.Remove<T>(id);
            if (!result.Success)
            {
                MessageBox.Show(result.Message);
                return;
            }

            LoadEntities();
        }

        public void CellEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            var updatedEntity = e.Row.Item as T;
            
            if (updatedEntity == null)
                return;

            var prop = updatedEntity.GetType().GetProperty(e.Column.SortMemberPath, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            var propType = prop.PropertyType.NonNullable();
            var converted = Convert.ChangeType(((TextBox)e.EditingElement).Text, propType);

            prop.SetValue(updatedEntity, converted);

            var props = updatedEntity?.GetType()
                .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(x => x.GetGetMethod()?.IsVirtual == false);

            if (props?.Any(x => x.GetValue(updatedEntity) == null) == true)
                return;

            using var baseLogic = new BaseLogic();

            var result = baseLogic.Update(updatedEntity!);

            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
