using Tisa.Store.Models.Contracts;

namespace Tisa.Store.Models.Entities;

public class Attribute : BaseViewModel
{
    public Attribute()
    {
        _name = string.Empty;
        _title = string.Empty;
        _type = string.Empty;
    }
    
    public int Id { get; set; }
    
    private string _name;

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    private string _title;

    public string Title
    {
        get
        {
            return _title;
        }
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    

    private string _type;

    public string Type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
            OnPropertyChanged(nameof(Type));
        }
    }

}