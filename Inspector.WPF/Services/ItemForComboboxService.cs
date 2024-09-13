
using Inspector.Models;

namespace Inspector.Services
{
    public static class ItemForComboboxService
    {
        public static List<ItemForCombobox> FillInvNumberWithNameCollection<T>(List<T> dataSource, Func<T, int> idSelector, Func<T, string> nameSelector)
        {
            var comboboxItems = new List<ItemForCombobox>
        {
            new ItemForCombobox
            {
                InvNumberWithName = "Пусто"
            }
        };

            comboboxItems.AddRange(dataSource.Select(item => new ItemForCombobox
            {
                Id = idSelector(item),
                InvNumberWithName = nameSelector(item)
            }));

            return comboboxItems
                .Take(1)
                .Concat(comboboxItems.Skip(1).OrderBy(item => item.InvNumberWithName, StringComparer.CurrentCulture))
                .ToList();
        }

        public static List<ItemForCombobox> FillNameCollection<T>(List<T> dataSource, Func<T, int> idSelector, Func<T, string> nameSelector)
        {
            var comboboxItems = new List<ItemForCombobox>
        {
            new ItemForCombobox
            {
                Name = "Пусто"
            }
        };

            comboboxItems.AddRange(dataSource.Select(item => new ItemForCombobox
            {
                Id = idSelector(item),
                Name = nameSelector(item)
            }));

            return comboboxItems
                .Take(1)
                .Concat(comboboxItems.Skip(1).OrderBy(item => item.Name))
                .ToList();
        }

        public static List<ItemForCombobox> FillNumberCollection<T>(List<T> dataSource, Func<T, int> idSelector, Func<T, string> nameSelector)
        {
            var comboboxItems = new List<ItemForCombobox>
        {
            new ItemForCombobox
            {
                Number = "Пусто"
            }
        };

            comboboxItems.AddRange(dataSource.Select(item => new ItemForCombobox
            {
                Id = idSelector(item),
                Number = nameSelector(item)
            }));

            return comboboxItems
                .Take(1)
                .Concat(comboboxItems.Skip(1).OrderBy(item => item.Name))
                .ToList();
        }

        public static List<ItemForCombobox> FillNumberWithNoteCollection<T>(List<T> dataSource, Func<T, int> idSelector, Func<T, string> nameSelector)
        {
            var comboboxItems = new List<ItemForCombobox>
        {
            new ItemForCombobox
            {
                NumberWithNote = "Пусто"
            }
        };

            comboboxItems.AddRange(dataSource.Select(item => new ItemForCombobox
            {
                Id = idSelector(item),
                NumberWithNote = nameSelector(item)
            }));

            return comboboxItems
                .Take(1)
                .Concat(comboboxItems.Skip(1).OrderBy(item => item.Name))
                .ToList();
        }

    }
}
