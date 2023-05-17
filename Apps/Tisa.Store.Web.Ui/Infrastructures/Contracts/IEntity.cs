using System;

namespace Tisa.Store.Web.Ui.Infrastructures.Contracts;

public interface IEntity<T> where T : struct
{
    T Id { get; set; }
}