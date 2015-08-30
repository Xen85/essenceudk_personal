using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes;
using System.Collections.ObjectModel;

namespace EssenceUDK.MapMaker.Elements.Interfaces
{
    public interface ITransition
    {
        ObservableCollection<CollectionLine> Lines { get; set; }
    }
}