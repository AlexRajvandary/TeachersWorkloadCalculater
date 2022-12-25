using StudingWorkloadCalculator.Models;
using StudingWorkloadCalculator.UserControls;
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
using System.Windows.Shapes;

namespace StudingWorkloadCalculator.Windows
{
    /// <summary>
    /// Interaction logic for TeacherWorkLoad.xaml
    /// </summary>
    public partial class TeacherWorkLoad : Window
    {
        public static DependencyProperty TeachersWorkloadProperty = DependencyProperty.Register("TeacherWorkload", typeof(TeachersWorkload), typeof(TeacherWorkLoad));

        public TeacherWorkLoad(TeachersWorkload teacherWorkLoad)
        {
            InitializeComponent();
            SetValue(TeachersWorkloadProperty, teacherWorkLoad);
        }

        public TeachersWorkload TeachersWorkload
        {
            get
            {
                return (TeachersWorkload)GetValue(TeachersWorkloadProperty);
            }
            set
            {
                SetValue(TeachersWorkloadProperty, value);
            }
        }
    }
}
