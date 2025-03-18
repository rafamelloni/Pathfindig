using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<(T item, float priority)> _queue;

    public int Count => _queue.Count;

    public PriorityQueue()
    {
        _queue = new List<(T item, float priority)>();
    }

    // Método para agregar un elemento con una prioridad
    public void Put(T item, float priority)
    {
        _queue.Add((item, priority));
        _queue.Sort((x, y) => x.priority.CompareTo(y.priority)); // Ordena la lista por prioridad (menor a mayor)
    }

    // Método para obtener el elemento con la mayor prioridad (el primero en la lista)
    public T Get()
    {
        if (_queue.Count == 0)
            return default;

        T item = _queue[0].item;
        _queue.RemoveAt(0); // Elimina el primer elemento después de obtenerlo
        return item;
    }
}
