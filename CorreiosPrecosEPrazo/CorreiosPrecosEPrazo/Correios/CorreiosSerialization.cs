using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CorreiosPrecosEPrazo.Correios
{
    class CorreiosSerialization
    {
        /// <summary>
        ///     Recebe um arquivo e converte para o objeto passado como parâmetro T
        /// </summary>
        /// <param name="arquivo"></param>
        /// <param name="T"></param>
        ///
        public static T GetObjectFromFile<T>(string arquivo) where T : class
        {
            var serialize = new XmlSerializer(typeof(T));

            try
            {
                var xmlArquivo = System.Xml.XmlReader.Create(arquivo);
                return (T)serialize.Deserialize(xmlArquivo);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public static T GetObject<T>(string arquivo) where T : class
        {
            try
            {
                // Recupera as informações do Xml
                var ser = new XmlSerializer(typeof(T));
                using (TextReader reader = new StringReader(arquivo))
                {
                    return (T)ser.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
    }
}
