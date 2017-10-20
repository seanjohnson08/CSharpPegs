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
using System.Drawing;

namespace Pegs
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Pegs"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Pegs;assembly=Pegs"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:Peg/>
    ///
    /// </summary>
    public class Peg : Control
    {
        private IntPoint _coordinates;

        static Peg()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Peg), new FrameworkPropertyMetadata(typeof(Peg)));
        }

        public Peg(IntPoint coordinates)
        {
            _coordinates = coordinates;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            DataObject data = new DataObject();
            data.SetData(_coordinates);

            DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
        }
    }
}
