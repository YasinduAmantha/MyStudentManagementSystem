using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStudentManagementSystem
{
    public class MainMenuItem
    {
        public string Title { get; set; }
        public bool IsSelected { get; set; }

        public MainMenuItem(string title, bool isSelected)
        {
            Title = title;
            IsSelected = isSelected;
        }
    }
}
