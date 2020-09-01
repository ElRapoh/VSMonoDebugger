using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdb2mdb
{
    internal interface IUsedNamespace
    {
        IName Alias { get; }
        IName NamespaceName { get; }
    }


    internal interface IName
    {
        int UniqueKey { get; }
        int UniqueKeyIgnoringCase { get; }
        string Value { get; }
    }
    internal interface INamespaceScope
    {
        IEnumerable<IUsedNamespace> UsedNamespaces { get; }
    }

    internal interface ILocalScope
    {
        uint Offset { get; }
        uint Length { get; }
    }

}
