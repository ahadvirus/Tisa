using System.Collections;
using System.Collections.Generic;

namespace Tisa.Store.Web.Ui.Models.ViewModels.ViewComponents;

public record TypeDropdownVm : IEnumerable<KeyValuePair<int, string>>
{
    public TypeDropdownVm(IDictionary<int, string> options)
    {
        Options = options;
    }

    public int Selected { get; init; }

    public string Input { get; init; }
    
    protected IDictionary<int, string> Options { get; init; }
    
    public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
    {
        return Options.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}