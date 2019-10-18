using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsNewInCSharp8
{
    public interface IDoMany
    {
        string What { get { return ""; } }
        string Azz { get { return ""; } }
        void Something();
        //void Else()
        //{ Console.WriteLine(nameof(Else));  }
        //;

        void Deconstruct(out string what, out string azz)
        {
            what = What;
            azz = Azz;
        }
    }
}
