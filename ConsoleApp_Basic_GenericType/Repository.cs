using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Basic_GenericType
{
    public class Repository<T> where T : IEntity, new()
    {
        private List<T> data = new List<T>();

        public void Add(T item)
        {
            var newItem = new T();
            newItem.Id = 34;

            if(item != null)
            {
                data.Add(item);
            }           
        }

        public void Remove(T item)
        {
            data.Remove(item);
        }

        public List<T> GetAll()
        {
            return data;
        }

        public T GetElementById(int id)
        {
            var element = data.FirstOrDefault(g => g.Id == id);
            return element;
        }

        public T GetElement(int index)
        {
            if(index < data.Count)
            {
                return data[index];
            }
            else
            {
                return default(T);
                //throw new IndexOutOfRangeException();
            }            
        }
         
    }

    
    public class Repository<TKey,TValue> where TKey : class where TValue : new()
    {
        private Dictionary<TKey,TValue> data = new Dictionary<TKey,TValue>();

        public void Add(TKey key, TValue item)
        {
            if (item != null)
            {
                data.Add(key,item);
            }
        }

        public void Remove(TKey key, TValue item)
        {
            data.Remove(key);
        }

   

        public TValue GetElement(TKey key)
        {
            if (data.TryGetValue(key, out TValue item))
            {
                return item;
            }
            else
            {
                return default;
                //throw new IndexOutOfRangeException();
            }
        }

    }
}
