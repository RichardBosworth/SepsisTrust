using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Jint;
using Jint.Native;
using Jint.Parser.Ast;
using Xamarin.Forms;

namespace AppScripting
{
    public class Test
    {
        public Test()
        {
        }

        public string RunTest()
        {
            Engine engine = new Engine(options => options.AllowClr(typeof(View).GetTypeInfo().Assembly)));
            Label jsValue = (Label) engine.Execute("var Xamarin = importNamespace(\"Xamarin.Forms\");\r\n\r\nfunction BuildLabel(text) {\r\n    var label = new Xamarin.Label();\r\n    label.Text = text;\r\n    return label;\r\n}\r\n\r\nfunction BuildImage(imageSource) {\r\n    var image = new Xamarin.Image();\r\n}\r\n\r\nvar label = BuildLabel(\"Test Label\");")
                .GetValue("label").ToObject();
            return jsValue.Text;
        }
    }
}
