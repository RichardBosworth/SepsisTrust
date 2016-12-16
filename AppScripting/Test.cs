using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jint;
using Jint.Parser.Ast;

namespace AppScripting
{
    class Test
    {
        public Test()
        {
            Engine engine = new Engine(options => options.AllowClr());
        }
    }
}
