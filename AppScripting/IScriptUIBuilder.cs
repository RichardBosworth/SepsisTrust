using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppScripting
{
    /// <summary>
    /// Provides functionality to allow scripts to build UI elements.
    /// </summary>
    interface IScriptUIBuilder
    {
        Label BuildLabel(string labelContent);
        Entry BuildEntry();
    }
}
