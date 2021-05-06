using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace StorageAndAcounting.BL.Controller
{
    abstract public class ControllerBase
    {
        public void Save(string fileName, object obj)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, obj);
            }
        }
        public T Load<T>(string fileName)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    if (formatter.Deserialize(fs) is T items)
                    {
                        return items;
                    }
                }
                return default(T);
            }
        }
    }
}
