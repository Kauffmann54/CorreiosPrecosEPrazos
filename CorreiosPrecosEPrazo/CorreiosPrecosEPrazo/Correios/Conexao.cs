using System;
using System.IO;
using System.Net;

namespace CorreiosPrecosEPrazo.Correios
{
    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    class Conexao
    {
        public static httpVerb httpMethod { get; set; }

        public Conexao()
        {
            httpMethod = httpVerb.GET;
        }

        /// <summary>
        ///     Acessa a API dos Correios e retorna um XML com o conteúdo requisitado.
        /// </summary>
        /// <param name="url"></param>
        /// 
        public static string consultarCorreios(string url)
        {
            string strResponseValue = string.Empty;
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = httpMethod.ToString();
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        //throw new ApplicationException("error code: " + response.StatusCode);
                        return "error code: " + response.StatusCode;
                    }
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                strResponseValue = reader.ReadToEnd();
                            }

                        }

                    }
                }
            }
            catch (System.Net.WebException e)
            {
                return e.Message;
            }

            return strResponseValue;
        }
    }
}
